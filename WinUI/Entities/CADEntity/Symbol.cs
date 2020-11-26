using System;
using System.Collections.Generic;
using System.Text;
using SocketPlan.WinUI.SocketPlanServiceReference;
using Edsa.AutoCadProxy;
using System.Reflection;

namespace SocketPlan.WinUI
{
    public partial class Symbol
    {
        #region コンストラクタ

        public Symbol() 
        {
        }

        /// <summary>Equipment付きだぉ☆</summary>
        public Symbol(int floor, int blockRefId)
            : this(blockRefId)
        {
            this.Floor = floor;
            this.Equipment = Equipment.Find(this);
        }

        /// <summary>ここではEquipmentは埋めませんよ</summary>
        public Symbol(int blockReferenceId)
        {
            var name = AutoCad.Db.BlockReference.GetBlockName(blockReferenceId);

            this.ObjectId = blockReferenceId;
            this.ClippedObjectId = this.ObjectId;
            this.BlockName = name;
            this.ClippedBlockName = this.BlockName;
            this.Position = AutoCad.Db.BlockReference.GetPosition(blockReferenceId);
            this.ActualPosition = this.Position;
            this.ClippedPosition = this.Position;
            this.Rotation = AutoCad.Db.BlockReference.GetRotation(blockReferenceId);
            this.ClippedRotation = this.Rotation;
            this.Layer = AutoCad.Db.BlockReference.GetLayerName(blockReferenceId);
            this.Attributes = Attribute.GetAll(blockReferenceId);

            var seqNo = this.Attributes.Find(p => p.Tag == Const.AttributeTag.SEQ_NO);
            if (seqNo == null)
                this.SequenceNo = -1;
            else
                this.SequenceNo = Convert.ToInt32(seqNo.Value);

            var seqNoLightEle = this.Attributes.Find(p => p.Tag == Const.AttributeTag.SEQ_NO_LE);
            if (seqNoLightEle == null)
                this.SequenceNoForLightElectrical = "0";
            else
                this.SequenceNoForLightElectrical = seqNoLightEle.Value;

            //キャッシュがあればそこからサイズを取得する。サイズの取得がクソ重い
            this.FillPoints();
            this.FillPositions();

            this.ClippedPointBottomLeft = this.PointBottomLeft;
            this.ClippedPointTopRight = this.PointTopRight;

            this.RelationSymbols = new List<Symbol>();
        }

        public void FillPositions()
        {
            var bound = AutoCad.Db.BlockReference.GetBlockBound(this.ObjectId);

            this.PositionBottomLeft = new PointD(bound[0].X, bound[0].Y);
            this.PositionTopRight = new PointD(bound[1].X, bound[1].Y);
        }

        public void FillPoints()
        {
            //速度改善としてキャッシュを導入
            var cache = Static.SymbolCache.Find(p => p.BlockName == this.BlockName);
            if (cache != null)
            {
                this.PointBottomLeft = cache.PointBottomLeft;
                this.PointTopRight = cache.PointTopRight;
                this.RealPointBottomLeft = this.PointBottomLeft.Clone();
                this.RealPointTopRight = this.PointTopRight.Clone();

                return;
            }

            var blockId = AutoCad.Db.BlockReference.GetBlockId(this.ObjectId);

            var refId = AutoCad.Db.BlockReference.Make(blockId, new PointD(0, 0));
            var bounds = AutoCad.Db.BlockReference.GetBlockBound(refId);
            AutoCad.Db.BlockReference.Erase(refId);

            this.PointBottomLeft = bounds[0];
            this.PointTopRight = bounds[1];
            this.RealPointBottomLeft = this.PointBottomLeft.Clone();
            this.RealPointTopRight = this.PointTopRight.Clone();

            if (cache == null)
                Static.SymbolCache.Add(this);
        }

        #endregion

        #region プロパティ

        #region 図面情報

        public string BlockName { get; set; }
        public int ObjectId { get; set; }
        public string Layer { get; set; }
        public double Rotation { get; set; }
        public int Floor { get; set; }

        #endregion

        #region シンボル付加情報

        public int RimokonNichePlateId { get; set; }
        public int GroupNo { get; set; } //電気配線で繋がれているシンボル同士に同じ番号を振る
        public int SequenceNo { get; set; }
        public int SubGroupNo { get; set; }
        public int SubGroupSeq { get; set; }
        public string SequenceNoForLightElectrical { get; set; }
        public double Angle { get; set; }
        //PI用
        public string CeilingPanelName { get; set; }

        public List<Symbol> RelationSymbols { get; set; }

        public Equipment Equipment { get; set; }
        public List<Attribute> Attributes { get; set; }
        public List<Attribute> OtherAttributes
        {
            get
            {
                //SwitchSerialもOtherAttributesに含めるように扱う。2015/06/29 @sato
                return this.Attributes.FindAll(p => (p.Tag == Const.AttributeTag.OTHER || p.Tag == Const.AttributeTag.SWITCH_SERIAL) && !string.IsNullOrEmpty(p.Value));
            }
        }

        public decimal Height
        {
            get
            {
                var heightAttribute = this.Attributes.Find(p => p.Tag == Const.AttributeTag.HEIGHT);
                if (heightAttribute == null)
                    throw new ApplicationException(Messages.HeightNotSet(this));

                decimal height;
                if (!decimal.TryParse(heightAttribute.Value.Replace("H=", string.Empty), out height))
                    throw new ApplicationException(Messages.HeightNotSet(this));

                return height;
            }
        }

        public bool HasEquipmentId
        {
            get
            {
                var attributeId = this.Attributes.Find(p => p.Tag == Const.AttributeTag.EQUIPMENT_ID);
                return attributeId != null;
            }
        }

        public int AttributeEquipmentId
        {
            get
            {
                var attribute = this.Attributes.Find(p => p.Tag == Const.AttributeTag.EQUIPMENT_ID);
                if (attribute == null)
                    return 0;

                var text = attribute.Value.Replace("EquipmentId=", string.Empty);

                int id;
                if (!int.TryParse(text, out id))
                    throw new ApplicationException("Invalid equipment ID is found. [ObjectId=" + this.ObjectId + "]");

                return id;
            }
        }

        public RoomObject Room { get; set; } //2013/3/6に追加 いまいち活かしきれていない
        public string RoomName { get; set; }
        /// <summary>どの天井パネル上に配置されるか（現実で）</summary>
        public CeilingPanel CeilingPanel { get; set; }

        /// <summary>回路に含めるべきシンボルかどうか</summary>
        public bool IsIncludedInKairo
        {
            get
            {
                if (this.Equipment == null)
                    return false;

                //VAが設定されている設備だけ回路に含める。
                return this.Equipment.ApparentPower != 0;
            }
        }
        #endregion

        #region 接続情報

        private List<Symbol> children = new List<Symbol>();
        public List<Symbol> Children
        {
            get { return this.children; }
            set { this.children = value; }
        }

        public Symbol Parent { get; set; } //上流の配線の先のシンボル

        public Wire Wire { get; set; } //上流の配線
        public PointD WiredPoint { get; set; } //上流側配線とこのシンボルの接続座標
        public List<Wire> DenkiWires { get; set; }

        /// <summary>
        /// <para>同じスイッチに繋がっているシンボルたち</para>
        /// <para>回路関係の処理で使う目的で、最低限のグループ情報だけ保持しています。</para>
        /// <para>ユニット配線関係の処理では、GetSameGroupSymbolsを使うこと</para>
        /// </summary>
        public List<Symbol> GroupedSymbolsForKairo { get; set; }

        public bool withOrangeMarking
        {
            get
            {
                if (this.Wire == null)
                    return false;

                return this.Wire.withOrangeMarking;
            }
        }

        #endregion

        #region 位置情報

        /// <summary>外周。家外周の外。枠上を含む</summary>
        public bool OnHouseOutLine { get; set; }
        public bool IsOutside { get; set; }

        /// <summary>屋外。家外周の外。枠上を含まない</summary>
        public bool IsOutdoor { get; set; }

        /// <summary>部屋の壁に設置かどうか</summary>
        public bool IsOnWall { get { return UnitWiring.Masters.Equipments.Exists(p => p.Id == this.Equipment.Id && p.MountingType == Const.MountingType.OnWall); } }

        public PointD Position { get; set; } //基点
        public PointD PositionBottomLeft { get; set; }
        public PointD PositionTopRight { get; set; }
        public PointD PositionCenter { get { return new PointD((this.PositionBottomLeft.X + this.PositionTopRight.X) / 2, (this.PositionBottomLeft.Y + this.PositionTopRight.Y) / 2); } }
        public PointD PositionTop { get { return new PointD(this.PositionCenter.X, this.PositionTopRight.Y); } }
        public PointD PositionBottom { get { return new PointD(this.PositionCenter.X, this.PositionBottomLeft.Y); } }
        public PointD PositionRight { get { return new PointD(this.PositionTopRight.X, this.PositionCenter.Y); } }
        public PointD PositionLeft { get { return new PointD(this.PositionBottomLeft.X, this.PositionCenter.Y); } }

        /// <summary>抜き出されていた時はその基点の座標を返す。通常は単純にシンボルが置かれている場所を返す</summary>
        public PointD ActualPosition { get; set; }

        ///<summary>SocketBoxの座標</summary>
        public PointD BoxPosition { get; set; }

        /// <summary>クリップされた図面上の座標を返す</summary>
        public int ClippedObjectId { get; set; }
        public PointD ClippedPosition { get; set; }
        public PointD ClippedPointBottomLeft { get; set; }
        public PointD ClippedPointTopRight { get; set; }
        public double ClippedRotation { get; set; }
        public string ClippedBlockName { get; set; }

        #endregion

        #region サイズ情報

        public PointD PointTopRight { get; set; } //シンボルの右上の座標（シンボルの基点から見た座標）
        public PointD PointBottomLeft { get; set; } //シンボルの左下の座標（シンボルの基点から見た座標）
        public PointD Size { get { return new PointD(this.PointTopRight.X - this.PointBottomLeft.X, this.PointTopRight.Y - this.PointBottomLeft.Y); } }
        public PointD RealPointTopRight { get; set; } //シンボルを書き出す時はこちらを使う
        public PointD RealPointBottomLeft { get; set; } //シンボルを書き出す時はこちらを使う
        public PointD RealSize { get { return new PointD(this.RealPointTopRight.X - this.RealPointBottomLeft.X, this.RealPointTopRight.Y - this.RealPointBottomLeft.Y); } } //シンボルを書き出す時はこちらを使う

        #endregion

        #region 器具種別

        // なんかIsBreaker信用できんぞ・・・
        public bool IsBreaker { get { return this.Equipment.EquipmentKindId == Const.EquipmentKind.BREAKER; } }
        // なので、IsBundenbanつくっちゃう
        public bool IsBundenban { get { return this.Equipment.Name.Contains("分電盤"); } }
        //弱電で基点取りたい人用
        public bool IsTop
        {
            get
            {
                if (this.HasComment(Const.Text.増設) && !this.IsBreaker)
                    return false;

                if (this.IsLightElectrical)
                    return UnitWiring.Masters.LightElectricals.Exists(p => p.EquipmentId == this.Equipment.Id && p.IsTop);
                else
                    return this.IsBreaker;
            }
        }

        public bool IsZenryouBundenban
        {
            get
            {
                return this.Equipment.Name == Const.EquipmentName.分電盤_3 ||
                    this.Equipment.Name == Const.EquipmentName.分電盤_4;
            }
        }

        public bool IsJointBox { get { return this.Equipment.EquipmentKindId == Const.EquipmentKind.JOINT_BOX; } }
        public bool IsSubJointBox
        {
            get
            {
                if (!this.IsJointBox)
                    return false;

                if (this.Parent == null)
                    return false;

                if (!this.Parent.IsJointBox)
                    return false;

                return true;
            }
        }

        public bool IsLight { get { return this.Equipment.EquipmentKindId == Const.EquipmentKind.LIGHT; } }
        public bool IsCeilingLight
        {
            get
            {
                return Array.Exists(this.Equipment.Specifications, p => p.Id == Const.Specification.シーリングライト) ||
                    this.Equipment.Block.Name == Const.BlockName.照明_01 ||
                    this.Equipment.Name == Const.BlockName.照明_05 ||
                    this.Equipment.Name == Const.BlockName.照明_06;
            }
        }
        public bool IsDownLight
        {
            get
            {
                return Array.Exists(this.Equipment.Specifications, p => p.Id == Const.Specification.ダウンライト) ||
                    this.Equipment.Block.Name == Const.BlockName.照明_02 || this.Equipment.Name == Const.BlockName.照明_41;
            }
        }
        public bool IsWallLight
        {
            get
            {
                return this.Equipment.Block.Name == Const.BlockName.照明_03;
            }
        }
        public bool IsSolarWireTop
        {
            get
            {
                return this.Equipment.Name == Const.EquipmentName.太陽光_1口 ||
                      this.Equipment.Name == Const.EquipmentName.太陽光_2口 ||
                      this.Equipment.Name == Const.EquipmentName.太陽光_3口;
            }
        }
        public bool IsPCBox { get { return (this.BlockName == Const.BlockName.PC || this.IsPowerBox); } }
        public bool IsPowerBox { get { return this.Equipment.Name.Contains(Const.EquipmentName.PowerBox); } }
        public bool IsOutlet { get { return this.Equipment.EquipmentKindId == Const.EquipmentKind.OUTLET; } }
        public bool IsSwitch { get { return this.Equipment.EquipmentKindId == Const.EquipmentKind.SWITCH; } }
        public bool IsArrow { get { return this.Equipment.EquipmentKindId == Const.EquipmentKind.FLOOR_ARROW; } }
        public bool IsCtBox { get { return this.Equipment.EquipmentKindId == Const.EquipmentKind.CT_BOX; } }
        public bool IsHomeGateway { get { return this.Equipment.EquipmentKindId == Const.EquipmentKind.HOME_GATEWAY; } }
        public bool IsHaikan
        {
            get
            {
                if (UnitWiring.Masters.SelectionCategoryDetails.Exists(p => p.EquipmentId == this.Equipment.Id &&
                                                                               p.SelectionCategoryId == Const.SelectionCategoryId.PIPE))
                    return true;

                if (this.IsToridashiForLightElectrical)
                    return true;

                return false;
            }
        }

        public bool IsFireAlarm
        {
            get
            {
                return UnitWiring.Masters.SelectionCategoryDetails.Exists(p => p.EquipmentId == this.Equipment.Id &&
                                                                               p.SelectionCategoryId == Const.SelectionCategoryId.FIRE_ALARM);
            }
        }

        //スイッチの場合の詳細情報
        public bool IsSensorSwitch { get { return this.IsSwitch && this.BlockName == Const.BlockName.センサスイッチ; } }
        public bool Is2WaySwitch { get { return this.IsSwitch && this.BlockName == Const.BlockName.スイッチ; } }
        public bool Is3WaySwitch
        {
            get
            {
                if (!this.IsSwitch)
                    return false;

                if (this.Is4WaySwitch)
                    return false;

                if (this.IsNormal3WaySwitch)
                    return true;

                if (this.IsKatteniSwitchControlUnit)
                    return true;

                if (this.WithAketaraSubSwitch && this.IsNormalSwitch && !this.IsAketaraSubSwitch)
                    return false;

                if (!this.IsKatteniSwitch && this.GetSameGroupSymbols().FindAll(p => p.IsNormalSwitch).Count == 2)
                    return true;

                //かってにスイッチ(3芯)+ノーマルスイッチの組み合わせは、3路スイッチ×2とみなす
                if (this.IsSwitch && this.WithKatteniSwitch3Way && this.GetSameGroupSymbols().FindAll(p => p.IsSwitch).Count == 2)
                    return true;

                return false;
            }
        }

        public bool IsNormal3WaySwitch
        {
            get
            {
                return
                    this.IsSwitch && (
                    (this.BlockName == Const.BlockName.スイッチ3路 || this.BlockName == Const.BlockName.防水スイッチ3路) ||
                    (this.ClippedBlockName == Const.BlockName.スイッチ3路 || this.ClippedBlockName == Const.BlockName.防水スイッチ3路));
            }
        }

        public bool Is4WaySwitch
        {
            get
            {
                return
                    this.IsSwitch &&
                    (this.BlockName == Const.BlockName.スイッチ4路 ||
                    this.ClippedBlockName == Const.BlockName.スイッチ4路);
            }
        }

        public bool Is3WayOr4WaySwitchConnection { get { return this.IsNormal3WaySwitch || this.Is4WaySwitch; } }

        //配線の芯数
        public bool Is2Core { get { return this.GetCoreCount() == 2; } }
        public bool Is3Core { get { return this.GetCoreCount() == 3; } }
        public bool Is4Core { get { return this.GetCoreCount() == 4; } }
        public bool Is5Core { get { return this.GetCoreCount() == 5; } }
        public bool Is6Core { get { return this.GetCoreCount() == 6; } }
        public bool Is7Core { get { return this.GetCoreCount() == 7; } }

        private int GetCoreCount()
        {
            if (this.IsAketaraSwitch)
            {
                if (this.IsAketaraSubSwitch)
                {
                    if (this.IsLastSwitchInGroup())
                        return 2;

                    return 4;
                }
                else if (this.IsAketaraSwitchCase1)
                {
                    if (this.GetSameGroupSymbols().FindAll(p => p.IsSwitch).Count == 1)
                        return 3;

                    return 6;
                }
                else if (this.IsAketaraSwitchCase2)
                {
                    if (this.GetSameGroupSymbols().Exists(p => p.Is3WayOr4WaySwitchConnection))
                        return 4;

                    return 3;
                }
                else
                {
                    if (this.GetSameGroupSymbols().Exists(p => p.Is3WayOr4WaySwitchConnection))
                        return 3;

                    return 2;
                }
            }
            else if (this.IsTottaraRimokon)
            {
                if (this.IsTottaraRimokonCase1)
                    return 4;

                return 2;
            }
            else if (this.IsLEDLightCon)
            {
                if (this.IsLEDLightConNormal)
                {
                    if (this.GetSameGroupSymbols().Exists(p => p.Is3WayOr4WaySwitchConnection))
                        return 3;

                    return 2;
                }
                else if (this.IsLEDLightConCase1)
                {
                    if (this.GetSameGroupSymbols().Exists(p => p.Is3WayOr4WaySwitchConnection))
                        return 4;

                    return 3;
                }
                else if (this.IsLEDLightConCase2)
                {
                    if (this.GetSameGroupSymbols().Exists(p => p.Is3WayOr4WaySwitchConnection))
                        return 5;

                    return 4;
                }
                else if (this.IsLEDLightConCase3)
                {
                    switch (this.GetSameChildGroup().Count)
                    {
                        case 1:
                            return 3;
                        case 2:
                            return 4;
                        case 3:
                            return 5;
                        case 4:
                            return 6;
                        case 5:
                            return 7;
                        default:
                            return 7;
                    }
                }
            }
            //OtherSwitch
            else if (this.WithOtherSwitch)
            {
                if (this.IsSwitch)
                {
                    if (this.IsOtherSwitchCase1)
                        return 4;
                    else if (this.IsOtherSwitchCase2)
                        return 3;
                    else if (this.IsOtherSwitchCase3)
                        return 4;
                    else
                        return 3;
                }
                else
                {
                    if (this.WithOtherSwitchCase2)
                        return 3;
                    if (this.WithOtherSwitchCase3 || this.WithOtherSwitchCase4)
                        return 2;
                }
            }

            //ライコン
            if (this.IsLightControlSwitch && !this.IsKatteniSwitch)
            {
                switch (this.GetSameChildGroup().Count)
                {
                    case 1:
                        return 3;
                    case 2:
                        return 4;
                    case 3:
                        return 5;
                    case 4:
                        return 6;
                    case 5:
                        return 7;
                    default:
                        return 7;
                }
            }

            if (this.Is3Wire)
                return 6;
            else if (this.Is2Wire)
            {
                if (this.IsKatteniSwitchControlUnit && this.WithKatteniSwitchToilet)
                    return 5;

                if (this.IsLightControlRotarySwitch && this.Is3WaySwitch)
                    return 5;

                if (this.Wire.Base.WithEarth)
                    return 5;

                return 4;
            }
            else if (this.Is1Wire)
            {
                if (this.IsKatteniSwitch3Way)
                    return 3;

                if (this.IsKatteniSwitchControlUnit && this.WithKatteniSwitchSpecialSerial)
                    return 3;

                if (this.Wire.Base.CoreNumber == 3)
                    return 3;

                if (this.Wire.Base.CoreNumber == 2)
                    return 2;
            }
            return 0;
        }

        //Is○Wireが増えすぎたのでこっちに集約させていく予定
        private int GetWireCount()
        {
            if (this.IsAketaraSwitch)
            {
                if (this.IsAketaraSubSwitch)
                {
                    if (this.IsLastSwitchInGroup())
                        return 1;

                    return 2;
                }
                else if (this.IsAketaraSwitchCase1)
                {
                    if (this.GetSameGroupSymbols().FindAll(p => p.IsSwitch).Count == 1)
                        return 1;

                    return 3;
                }
                else if (this.IsAketaraSwitchCase2)
                {
                    if (this.GetSameGroupSymbols().Exists(p => p.Is3WayOr4WaySwitchConnection))
                        return 2;

                    return 1;
                }
                else
                {
                    return 1;
                }
            }
            else if (this.IsTottaraRimokon)
            {
                if (this.IsTottaraRimokonCase1)
                    return 2;

                return 1;
            }
            else if (this.IsLEDLightCon)
            {
                if (this.IsLEDLightConNormal)
                {
                    return 1;
                }
                else if (this.IsLEDLightConCase1)
                {
                    if (this.GetSameGroupSymbols().Exists(p => p.Is3WayOr4WaySwitchConnection))
                        return 2;

                    return 1;
                }
                else if (this.IsLEDLightConCase2)
                {
                    return 2;
                }
                else if (this.IsLEDLightConCase3)
                {
                    switch (this.GetSameChildGroup().Count)
                    {
                        case 1:
                            return 1;
                        case 2:
                            return 2;
                        case 3:
                            return 2;
                        case 4:
                            return 3;
                        case 5:
                            return 3;
                        default:
                            return 3;
                    }
                }
            }
            //OtherSwitch
            else if (this.WithOtherSwitch)
            {
                if (this.IsSwitch)
                {
                    if (this.IsOtherSwitchCase1)
                        return 2;
                    else if (this.IsOtherSwitchCase2)
                        return 1;
                    else if (this.IsOtherSwitchCase3)
                        return 2;
                    else
                        return 1;
                }
                else
                {
                    if (this.WithOtherSwitchCase1 || this.WithOtherSwitchCase2)
                        return 1;
                }
            }

            if (this.IsLightControlRotarySwitch)
                return 2;
            else if (this.IsLightControlSwitch)
            {
                switch (this.GetSameChildGroup().Count)
                {
                    case 1:
                        return 1;
                    case 2:
                        return 2;
                    case 3:
                        return 2;
                    case 4:
                        return 3;
                    case 5:
                        return 3;
                    default:
                        return 3;
                }
            }

            //ライコン照明
            if (this.WithLEDLightCon || this.WithLightControlSwitch || this.WithLightControlRotarySwitch)
            {
                if (!this.IsFeedSymbol)
                    return 1;
                else if (this.children.Count > 0)
                    return 2;
            }
            return 0;
        }

        //器具に接続する配線の本数
        public bool Is1Wire
        {
            get
            {
                if (this.GetWireCount() == 1)
                    return true;

                if (this.IsKatteniSwitch3Way && !Is2Wire)
                    return true;

                //消去法。
                if (this.Is2Wire)
                    return false;

                if (this.Is3Wire)
                    return false;

                return true;
            }
        }

        public bool Is2Wire
        {
            get
            {
                if (this.Is3Wire) //3本6芯はここで対象から外す
                    return false;

                if (this.GetWireCount() == 2)
                    return true;

                if (this.IsSwitch)
                {
                    if (this.WithKatteniSwitchSpecialSerial)
                        return false;

                    if (this.IsKatteniSwitch3Way)
                    {
                        //3路のかってにスイッチ+ノーマルスイッチのとき2本4芯として扱う
                        if (this.GetSameGroupSymbols().FindAll(p => p.IsNormalSwitch).Count > 0)
                            return true;
                        else
                            return false;
                    }

                    if (this.Is4WaySwitch) //4路スイッチは2本4芯
                        return true;

                    if (this.IsKatteniSwitchParent && !this.WithKatteniSwitchChild) //かってにスイッチ親機（子機なし）は2本4芯
                        return true;

                    if (this.WithKatteniSwitchControlUnit)
                    {
                        if (this.WithKatteniSwitchToilet)
                            return true;
                        else if (this.IsKatteniSwitchChild && !this.IsFirstSwitchInGroup())
                            return true;
                        else
                            return false;
                    }

                    if (this.IsKatteniSwitchChild && !this.IsLastSwitchInGroup())
                        return true; //かってにスイッチ子機（一番後ろ以外）は2本4芯

                    if (this.With電動昇降機用)
                        return true;

                    return false;
                }
                else
                {
                    if (this.WithKatteniSwitch)
                    {
                        if (this.WithKatteniSwitchToilet)
                            return false;

                        if (this.IsFeedSymbol && !this.IsLastInGroup())
                            return true;

                        return false;
                    }

                    if (this.WithOnOffDisplay && !this.IsLastInGroup())
                        return true;

                    if (this.IsLight)
                    {
                        if (this.WithLightControlSwitch || this.WithLEDLightConCase3)
                        {
                            if (this.children.Count > 0)
                                return true;
                            else
                                return false;
                        }
                        else if (this.WithLightControlRotarySwitch)
                        {
                            if (!this.IsFeedSymbol)
                                return false;
                        }
                    }

                    if (this.IsFeedSymbol && !this.IsLastInGroup())
                        return true;

                    if (this.With電動昇降機用)
                        return true;

                    if (this.WithKatteniSwitch3Way)
                        return false;

                    return false;
                }
            }
        }

        public bool Is3Wire
        {
            get
            {
                if (this.GetWireCount() == 3)
                    return true;

                if (this.IsKatteniSwitch3Way)
                    return false;

                //かってにスイッチの親機（子機あり）なら3芯。
                if (this.IsKatteniSwitchParent && this.WithKatteniSwitchChild)
                    return true;

                //トイレ用の、照明と換気扇を繋げるかってにスイッチは3芯
                if (this.IsKatteniSwitchToilet)
                    return true;

                return false;
            }
        }

        //専用回路となる条件で、シンボルのAttributeに専用E付、専用E無のどちらかが入っていたらを追加した。
        public bool IsExclusive
        {
            get
            {
                return Array.Exists(this.Equipment.Specifications, p => p.Id == Const.Specification.専用)
                    || this.HasComment(Const.Text._200V)
                     || this.HasComment(Const.Text.専用E付)
                     || this.HasComment(Const.Text.専用Ｅ付)
                      || this.HasComment(Const.Text.専用E無)
                      || this.HasComment(Const.Text.専用Ｅ無)
                      || this.HasComment(Const.Text.専用);

            }
        }
        public bool IsSeparatedUnit { get { return Array.Exists(this.Equipment.Specifications, p => p.Id == Const.Specification.一次送り); } }
        public bool IsCombinationSymbolParent { get { return Array.Exists(this.Equipment.Specifications, p => p.Id == Const.Specification.組み合わせシンボル_親); } }
        public bool IsCombinationSymbolChild { get { return Array.Exists(this.Equipment.Specifications, p => p.Id == Const.Specification.組み合わせシンボル_子); } }
        public bool IsInterphone { get { return Array.Exists(this.Equipment.Specifications, p => p.Id == Const.Specification.インターホン); } }
        public bool IsRimokon { get { return Array.Exists(this.Equipment.Specifications, p => p.Id == Const.Specification.リモコン); } }
        public bool IsMR { get { return this.Equipment.Name == "ﾘﾓｺﾝ12" || this.Equipment.NameAtSelection.Contains("MR"); } }
        public bool IsSR { get { return this.Equipment.Name == "ﾘﾓｺﾝ13" || this.Equipment.NameAtSelection.Contains("SR"); } }
        public bool IsCV { get { return this.Equipment.NameAtSelection.StartsWith("CV") && !this.Equipment.NameAtSelection.StartsWith("CVD"); } }
        public bool IsInterphoneRimokon { get { return this.Equipment.Name == Const.EquipmentName.int_01 || this.Equipment.Name == Const.EquipmentName.int_02 || this.Equipment.Name == Const.EquipmentName.int_07; } }
        public bool IsPVR
        {
            get
            {
                return this.IsRimokon && Array.Exists(this.Equipment.Specifications, p => p.Id == Const.Specification.PVリモコン);
            }
        }

        public bool WithEarth
        {
            get
            {
                if (this.IsLight)
                    if (this.IsOutdoor)
                        return true;

                if (this.IsOutlet)
                {
                    if (Array.Exists(this.Equipment.Specifications, p => p.Id == Const.Specification.E付 || p.Id == Const.Specification.防水))
                        return true;

                    if (this.Is電動昇降機用)
                        return true;
                }

                if (this.IsJointBox)
                {
                    foreach (var child in this.children)
                    {
                        if (child.WithEarth)
                            return true;
                    }
                }

                return false;
            }
        }

        public bool IsWaterProof
        {
            get
            {
                return Array.Exists(this.Equipment.Specifications, p => p.Id == Const.Specification.防水);
            }
        }

        public bool Is200V { get { return this.ContainsComment(Const.Text._200V); } }
        public bool ForIH { get { return this.ContainsComment(Const.Text.IH); } }
        public bool ForAC { get { return this.ContainsComment(Const.Text.AC); } }
        public bool ForFan { get { return this.ContainsComment(Const.Text.換気扇); } }

        public bool IsEVSwitch
        {
            get
            {
                return this.ContainsComment(Const.Text.EV_Switch) ||
                    this.ContainsComment(Const.Text.EV_Switch充電用);
            }
        }
        public bool IsEVOutlet
        {
            get
            {
                if (!this.IsOutlet)
                    return false;

                if (this.ContainsComment(Const.Text.EV_Outlet) ||
                    this.ContainsComment(Const.Text.EV_OutletWithout充電用))
                    return true;

                //コンセントにEVコメントが無くても、スイッチにEVコメントがあれば、EV用コンセントとみなす。

                if (this.Parent == null)
                    return false;

                return this.Parent.IsEVSwitch;
            }
        }

        public bool WithEV { get { return this.IsEVOutlet || this.IsEVSwitch; } }

        public bool IsDirectSwitch { get { return this.IsSwitch && this.Parent.IsBreaker; } }
        public bool IsDirectSwitchOutlet { get { return this.IsOutlet && this.Parent.IsDirectSwitch; } }

        /// <summary>ドレインヒーター用スイッチ</summary>
        public bool IsDrainHeaterSwitch { get { return this.IsSwitch && this.HasComment(Const.Text.ﾄﾞﾚｲﾝ用); } }

        /// <summary>ドレインヒーター用スイッチに繋がっているシンボル</summary>
        public bool WithDrainHeaterSwitch { get { return this.GetSameGroupSymbols().Exists(p => p.IsDrainHeaterSwitch); } }

        /// <summary>融雪用スイッチ</summary>
        public bool IsRoofHeaterSwitch { get { return this.IsSwitch && this.HasComment(Const.Text.融雪用); } }

        /// <summary>融雪用スイッチに繋がっているシンボル</summary>
        public bool WithRoofHeaterSwitch { get { return this.GetSameGroupSymbols().Exists(p => p.IsRoofHeaterSwitch); } }

        /// <summary>オーニング用スイッチ</summary>
        public bool IsAwningSwitch { get { return this.IsSwitch && this.HasComment(Const.Text.オーニング用); } }

        /// <summary>壁内配線</summary>
        public bool HasWireInWall
        {
            get
            {
                //パワコンの場合親に属性がついている
                if (this.Parent != null)
                {
                    if (this.Parent.IsSolarWireTop && Comment.HasWireInWall(this.Parent))
                        return true;
                }
                return Comment.HasWireInWall(this);
            }
        }

        public bool ForKitchen
        {
            get
            {
                if (this.RoomName.Contains(Const.Room.キッチン))
                    return true;

                if (this.RoomName.Contains(Const.Room.DK))
                    return true;

                return false;
            }
        }

        #region スイッチ種別
        public bool IsKatteniSwitch
        {
            get
            {
                return
                    this.IsKatteniSwitchParent ||
                    this.IsKatteniSwitchChild ||
                    this.IsKatteniSwitchToilet ||
                    this.IsKatteniSwitchControlUnit ||
                    this.IsKatteniSwitch3Way;
            }
        }

        public bool IsAketaraSwitch { get { return Comment.IsAketaraSwitch(this); } }
        public bool IsAketaraSwitch1st3Way { get { return Comment.IsAketaraSwitch1st3Way(this); } }
        public bool IsAketaraSwitch2nd3Way { get { return Comment.IsAketaraSwitch2nd3Way(this); } }
        public bool IsAketaraSwitchCase1 { get { return Comment.IsAketaraSwitchCase1(this); } }
        public bool IsAketaraSwitchCase2 { get { return Comment.IsAketaraSwitchCase2(this); } }
        public bool IsAketaraSwitchNormal { get { return Comment.IsAketaraSwitchNormal(this); } }
        public bool IsAketaraSubSwitch { get { return Comment.IsAketaraSubSwitch(this); } }
        public bool IsTottaraRimokon { get { return Comment.IsTottaraRimokon(this); } }
        public bool IsTottaraRimokon1st3Way { get { return Comment.IsTottaraRimokon1st3Way(this); } }
        public bool IsTottaraRimokon2nd3Way { get { return Comment.IsTottaraRimokon2nd3Way(this); } }
        public bool IsTottaraRimokonCase1 { get { return Comment.IsTottaraRimokonCase1(this); } }
        public bool IsTottaraRimokonNormal { get { return Comment.IsTottaraRimokonNormal(this); } }
        public bool IsLEDLightCon { get { return Comment.IsLEDLightCon(this); } }
        public bool IsLEDLightCon1st3way { get { return Comment.IsLEDLightCon1st3way(this); } }
        public bool IsLEDLightCon2nd3Way { get { return Comment.IsLEDLightCon2nd3Way(this); } }
        public bool IsLEDLightConCase1 { get { return Comment.IsLEDLightConCase1(this); } }
        public bool IsLEDLightConCase2 { get { return Comment.IsLEDLightConCase2(this); } }
        public bool IsLEDLightConCase3 { get { return Comment.IsLEDLightConCase3(this); } }
        public bool IsLEDLightConNormal { get { return Comment.IsLEDLightConNormal(this); } }
        public bool IsOtherSwitch { get { return Comment.IsOtherSwitch(this); } }
        public bool IsOtherSwitchCase1 { get { return Comment.IsOtherSwitchCase1(this); } }
        public bool IsOtherSwitchCase2 { get { return Comment.IsOtherSwitchCase2(this); } }
        public bool IsOtherSwitchCase3 { get { return Comment.IsOtherSwitchCase3(this); } }
        public bool IsOtherSwitchCase4 { get { return Comment.IsOtherSwitchCase4(this); } }

        public bool WithAketaraSwitch { get { return this.GetSameGroupSymbols().Exists(child => child.IsAketaraSwitch); } }
        public bool WithAketaraSwitch1st3Way { get { return this.GetSameGroupSymbols().Exists(child => child.IsAketaraSwitch1st3Way); } }
        public bool WithAketaraSwitch2nd3Way { get { return this.GetSameGroupSymbols().Exists(child => child.IsAketaraSwitch2nd3Way); } }
        public bool WithAketaraSwitchCase1 { get { return this.GetSameGroupSymbols().Exists(child => child.IsAketaraSwitchCase1); } }
        public bool WithAketaraSwitchCase2 { get { return this.GetSameGroupSymbols().Exists(child => child.IsAketaraSwitchCase2); } }
        public bool WithAketaraSwitchNormal { get { return this.GetSameGroupSymbols().Exists(child => child.IsAketaraSwitchNormal); } }
        public bool WithAketaraSubSwitch { get { return this.GetSameGroupSymbols().Exists(child => child.IsAketaraSubSwitch); } }

        public bool WithTottaraRimokon { get { return this.GetSameGroupSymbols().Exists(child => child.IsTottaraRimokon); } }
        public bool WithTottaraRimokon1st3Way { get { return this.GetSameGroupSymbols().Exists(child => child.IsTottaraRimokon1st3Way); } }
        public bool WithTottaraRimokon2nd3Way { get { return this.GetSameGroupSymbols().Exists(child => child.IsTottaraRimokon2nd3Way); } }
        public bool WithTottaraRimokonCase1 { get { return this.GetSameGroupSymbols().Exists(child => child.IsTottaraRimokonCase1); } }
        public bool WithTottaraRimokonNormal { get { return this.GetSameGroupSymbols().Exists(child => child.IsTottaraRimokonNormal); } }

        public bool WithLEDLightCon { get { return this.GetSameGroupSymbols().Exists(child => child.IsLEDLightCon); } }
        public bool WithLEDLightCon1st3way { get { return this.GetSameGroupSymbols().Exists(child => child.IsLEDLightCon1st3way); } }
        public bool WithLEDLightCon2nd3Way { get { return this.GetSameGroupSymbols().Exists(child => child.IsLEDLightCon2nd3Way); } }
        public bool WithLEDLightConCase1 { get { return this.GetSameGroupSymbols().Exists(child => child.IsLEDLightConCase1); } }
        public bool WithLEDLightConCase2 { get { return this.GetSameGroupSymbols().Exists(child => child.IsLEDLightConCase2); } }
        public bool WithLEDLightConCase3 { get { return this.GetSameGroupSymbols().Exists(child => child.IsLEDLightConCase3); } }
        public bool WithLEDLightConNormal { get { return this.GetSameGroupSymbols().Exists(child => child.IsLEDLightConNormal); } }

        public bool WithOtherSwitch { get { return this.GetSameGroupSymbols().Exists(child => child.IsOtherSwitch); } }
        public bool WithOtherSwitchCase1 { get { return this.GetSameGroupSymbols().Exists(child => child.IsOtherSwitchCase1); } }
        public bool WithOtherSwitchCase2 { get { return this.GetSameGroupSymbols().Exists(child => child.IsOtherSwitchCase2); } }
        public bool WithOtherSwitchCase3 { get { return this.GetSameGroupSymbols().Exists(child => child.IsOtherSwitchCase3); } }
        public bool WithOtherSwitchCase4 { get { return this.GetSameGroupSymbols().Exists(child => child.IsOtherSwitchCase4); } }
        #endregion

        public bool IsSignalWireConnectable
        {
            get
            {
                if (Comment.IsSignalWireConnectable(this))
                    return true;
                if (this.IsLightControlSwitch || this.IsLightControlRotarySwitch || this.IsSubLightControlSwitch)
                    return true;

                return false;
            }
        }

        public bool IsNormalSwitch
        {
            get
            {
                return this.IsSwitch &&
                      !this.IsKatteniSwitch &&
                      !this.IsAketaraSwitch &&
                      !this.IsTottaraRimokon &&
                      !this.IsLEDLightCon;
            }
        }

        public bool IsKatteniSwitchParent { get { return Comment.IsKatteniSwitchParent(this); } }
        public bool IsKatteniSwitchChild { get { return Comment.IsKatteniSwitchChild(this); } }
        public bool IsKatteniSwitchToilet { get { return Comment.IsKatteniSwitchToilet(this); } }
        public bool IsKatteniSwitchControlUnit { get { return Comment.IsKatteniSwitchControlUnit(this); } }
        public bool IsKatteniSwitch3Way
        {
            get
            {
                //ライコンに変更されたけど結線は従来のパターンを使うやつ
                if (this.Attributes.Exists(p => p.Value == Const.LivingLightControlSerial.NQ20355 ||
                                                p.Value == Const.LivingLightControlSerial.NQ20356))
                    return true;

                return Comment.IsKatteniSwitch3Way(this);
            }
        }

        public bool WithKatteniSwitch { get { return this.GetSameGroupSymbols().Exists(child => child.IsKatteniSwitch); } }
        public bool WithKatteniSwitchParent { get { return this.GetSameGroupSymbols().Exists(child => child.IsKatteniSwitchParent); } }
        public bool WithKatteniSwitchChild { get { return this.GetSameGroupSymbols().Exists(child => child.IsKatteniSwitchChild); } }
        public bool WithKatteniSwitchToilet { get { return this.GetSameGroupSymbols().Exists(child => child.IsKatteniSwitchToilet); } }
        public bool WithKatteniSwitchControlUnit { get { return this.GetSameGroupSymbols().Exists(child => child.IsKatteniSwitchControlUnit); } }
        public bool WithKatteniSwitch3Way { get { return this.GetSameGroupSymbols().Exists(child => child.IsKatteniSwitch3Way); } }

        //シリアルでしか判別できないかってにスイッチのパターン
        public bool WithKatteniSwitchSpecialSerial
        {
            get
            {
                if (this.GetSameGroupSymbols().Exists(child => child.HasComment(Const.KatteniSwitchSerial.WTC5820)) &&
                    this.GetSameGroupSymbols().Exists(child => child.HasComment(Const.KatteniSwitchSerial.WTK37314)) &&
                    this.GetSameGroupSymbols().Exists(child => child.HasComment(Const.KatteniSwitchSerial.WTK39114)))
                    return true;

                return false;
            }
        }

        public bool WithKatteniAndNormalSwitch
        {
            get
            {
                var group = this.GetSameGroupSymbols();
                if (group.Exists(p => p.IsKatteniSwitch) &&
                    group.Exists(p => p.IsNormalSwitch))
                    return true;
                else
                    return false;
            }
        }

        public bool IsOnOffDisplay { get { return this.HasComment(Const.Text.入切表示トイレ); } }
        public bool WithOnOffDisplay { get { return this.GetSameGroupSymbols().Exists(p => p.IsOnOffDisplay); } }

        public bool IsFeedSymbol
        {
            get
            {
                if (this.IsBundenban || this.IsJointBox || this.Parent == null)
                    return false;

                if (!this.IsLight)
                {
                    //ライト以外は直結のソケットがFEEDしている場合のみ許可する
                    if (this.OtherAttributes.Exists(p => p.Value == Const.Text.直結) && this.Parent.IsLight)
                        return true;
                    else
                        return false;
                }

                if (this.Parent.IsLight)
                    return true;

                if (this.Children.Exists(p => !p.IsJointBox && !p.IsSwitch))
                    return true;

                return false;
            }
        }

        public bool IsSmartSeries { get { return this.HasComment(Const.Text.S); } }

        public bool HasSmartSeriesSerial
        {
            get
            {
                var smartSerials = UnitWiring.Masters.Comments.FindAll(p =>
                    p.CategoryId == Const.CommentCategoryId.スマートシリーズシリアル);

                foreach (Comment serialComment in smartSerials)
                {
                    if (this.HasComment(serialComment.Text))
                        return true;
                }

                return false;
            }
        }

        public bool Is電動昇降機用 { get { return this.HasComment(Const.Text.電動昇降機用); } }
        public bool With電動昇降機用 { get { return this.GetSameGroupSymbols().Exists(p => p.Is電動昇降機用); } }

        public bool IsBatteryTypeFireAlarm { get { return this.HasComment(Const.Text.電池L); } }

        public bool IsEcocute { get { return this.ContainsComment(Const.Text.ｴｺｷｭｰﾄ) || this.ContainsComment(Const.Text.ｴｺｷｭ_ﾄ); } }
        public bool IsEcowill { get { return this.ContainsComment(Const.Text.エコウィル); } }
        public bool IsHCU { get { return this.Equipment.Name == Const.EquipmentName.HCU; } }

        public bool Is換気制御
        {
            get
            {
                return this.Equipment.Name == Const.EquipmentName.ｽｲｯﾁ_04 ||
                    this.Attributes.Exists(p => p.Value == "換気制御");
            }
        }

        public bool Is電気錠スイッチ
        {
            get { return this.Equipment.Name == Const.EquipmentName.電気錠スイッチ; }
        }

        public bool IsEV_PHEV
        {
            get { return this.Equipment.Name == Const.EquipmentName.ｽｲｯﾁ_09; }
        }

        public bool IsYR
        {
            get { return this.IsRimokon && this.Equipment.NameAtSelection.StartsWith("YR"); }
        }

        public bool IsTwinLight { get { return Array.Exists(this.Equipment.Specifications, p => p.Id == Const.Specification.ツインライト); } }

        private bool isLightElectrical = false;
        public bool IsLightElectrical
        {
            get
            {
                if (UnitWiring.Masters.LightElectricals.Exists(p => p.EquipmentId == this.Equipment.Id))
                    return true;

                if (this.IsToridashiForLightElectrical)
                    return true;

                return false;
            }
            set { this.isLightElectrical = value; }
        }

        public bool IsGreenMarking
        {
            get
            {
                bool isGreen = this.Equipment.Name == Const.EquipmentName.MarkingGreen;
                return isGreen;
            }
        }

        public bool IsYellowMarking
        {
            get
            {
                bool isYellow = this.Equipment.Name == Const.EquipmentName.MarkingYellow;
                return isYellow;
            }
        }

        public bool IsBuleMarking
        {
            get
            {
                bool isBule = this.Equipment.Name == Const.EquipmentName.MarkingBule;
                return isBule;
            }
        }

        public bool HasToridashiComment
        {
            get
            {
                return this.Attributes.Exists(p => p.Value == Const.Text.光ﾌｧｲﾊﾞｰ ||
                                                   p.Value == Const.Text.NET取出 ||
                                                   p.Value == Const.Text.LAN取出 ||
                                                   p.Value == Const.Text.TEL取出 ||
                                                   p.Value == Const.Text.TV取出 ||
                                                   p.Value == Const.Text.BS用取出 ||
                                                   p.Value == Const.Text.CS用取出 ||
                                                   p.Value == Const.Text.CATV取出 ||
                                                   p.Value == Const.Text.アンテナ取出 ||
                                                   p.Value == Const.Text.BSアンテナ取出 ||
                                                   p.Value == Const.Text.TVアンテナ取出 ||
                                                   p.Value == Const.Text.CSアンテナ取出 ||
                                                   p.Value == Const.Text.TV配線取出 ||
                                                   p.Value == Const.Text.光取出);
            }
        }

        public bool IsSubLightControlSwitch
        {
            get
            {
                if (!this.IsSwitch)
                    return false;

                if (Comment.IsSubSwitch(this))
                    return true;

                return this.IsSwitch && this.Attributes.Exists(p => p.Value == Const.LivingLightControlSerial.NQ28706W ||
                                                                    p.Value == Const.LivingLightControlSerial.NQ28706S ||
                                                                    p.Value == Const.LivingLightControlSerial.NK28706W);
            }
        }

        public bool WithLightControlSwitch { get { return this.GetSameGroupSymbols().Exists(child => child.IsLightControlSwitch); } }

        public bool IsLightControlSwitch
        {
            get
            {
                if (!this.IsSwitch)
                    return false;

                return this.Attributes.Exists(p => p.Value == Const.LivingLightControlSerial.NQ28732SK ||
                                                   p.Value == Const.LivingLightControlSerial.NQ28732WK ||
                                                   p.Value == Const.LivingLightControlSerial.NQ28751SK ||
                                                   p.Value == Const.LivingLightControlSerial.NQ28751WK ||
                                                   p.Value == Const.LivingLightControlSerial.NQ28752WK ||
                                                   p.Value == Const.LivingLightControlSerial.NQ28752SK ||
                                                   p.Value == Const.LivingLightControlSerial.NQ20355 ||
                                                   p.Value == Const.LivingLightControlSerial.NQ20356);
            }
        }

        public string GetSwitchSerial()
        {
            if (!this.IsSwitch)
                return string.Empty;

            var attr = this.Attributes.Find(p => p.Tag == Const.AttributeTag.SWITCH_SERIAL);
            //スイッチシリアルが見つからない時は、何かそれっぽいのを探してくる
            if (attr == null)
            {
                attr = this.Attributes.Find(p => p.Value.StartsWith("NQ") ||
                                                 p.Value.StartsWith("WT") ||
                                                 p.Value.StartsWith("NK") ||
                                                 p.Value.StartsWith("SAE") ||
                                                 p.Value.StartsWith("AE") ||
                                                 p.Value.StartsWith("LC") ||
                                                 p.Value.StartsWith("※"));
            }
            if (attr == null)
                return string.Empty;
            return attr.Value;
        }

        public bool WithLightControlRotarySwitch { get { return this.GetSameGroupSymbols().Exists(child => child.IsLightControlRotarySwitch); } }
        public bool IsLightControlRotarySwitch
        {
            get
            {
                if (!this.IsSwitch)
                    return false;

                return this.Attributes.Exists(p => p.Value == Const.LivingLightControlSerial.NQ21585Z ||
                                                   p.Value == Const.LivingLightControlSerial.NQ21582Z ||
                                                   p.Value == Const.LivingLightControlSerial.NQ21595Z ||
                                                   p.Value == Const.LivingLightControlSerial.NQ21592Z);
            }
        }

        public bool IsToridashiForLightElectrical
        {
            get
            {
                if (this.Equipment.Name == Const.EquipmentName.Blackmark && this.HasToridashiComment)
                    return true;
                else
                    return false;
            }
        }

        public bool IsConnectableForLightElectrical
        {
            get
            {
                if (this.Equipment.IsConnectable)
                    return true;

                if (this.IsToridashiForLightElectrical)
                    return true;

                return false;
            }
        }

        public bool IsLightToLight
        {
            get
            {
                if (!this.IsLight)
                    return false;

                if (this.Parent == null)
                    return false;

                if (!this.Parent.IsLight)
                    return false;

                return true;
            }
        }

        /// <summary>
        /// ParentがJointBoxで自身がLightだった場合trueを返却する
        /// </summary>
        public bool IsJointBoxToLight
        {
            get
            {
                if (!this.IsLight)
                    return false;
                if (this.Parent == null)
                    return false;
                if (!this.Parent.IsJointBox)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// 取り出し01~05だったらtrue
        /// </summary>
        public bool IsToridashi
        {
            get
            {
                if (this.Equipment.Name == Const.EquipmentName.取出し_01 ||
                this.Equipment.Name == Const.EquipmentName.取出し_02 ||
                this.Equipment.Name == Const.EquipmentName.取出し_03 ||
                this.Equipment.Name == Const.EquipmentName.取出し_04 ||
                this.Equipment.Name == Const.EquipmentName.取出し_05)
                    return true;
                else
                    return false;
            }
        }

        public bool IsJBox
        {
            get
            {
                return this.Equipment.IsJBox;
            }
        }

        public bool IsSingleSocketBox
        {
            get
            {
                return UnitWiring.Masters.SingleSocketBoxEquipments.Exists(p => p.EquipmentId == this.Equipment.Id);
            }
        }

        #endregion

        #region 配線情報

        public bool Has100VMainWire
        {
            get
            {
                if (this.IsJointBox)
                    return true;

                if (this.WithLEDLightConCase2)
                {
                    if (!this.IsSwitch)
                        return false;
                    if (this.IsLEDLightCon && this.GetSameGroupSymbols().Find(p => p.IsSwitch && !p.IsLEDLightCon) == null)
                        return true;
                    if (this.IsLEDLightCon && !this.IsLEDLightCon1st3way)
                        return true;
                    if (this.Is3WayOr4WaySwitchConnection && !this.WithLEDLightCon2nd3Way)
                        return true;

                    return false;
                }

                if (this.WithKatteniSwitchControlUnit)
                {
                    if (this.IsKatteniSwitchParent)
                        return true;

                    if (this.IsKatteniSwitchToilet)
                        return true;

                    if (this.IsKatteniSwitchControlUnit)
                        return true;

                    return false;
                }

                return this.IsFirstInGroup();
            }
        }

        public bool Has0VMainWire
        {
            get
            {
                if (this.IsJointBox)
                    return true;
                if (this.IsAketaraSubSwitch)
                {
                    return false;
                }
                else if (this.WithAketaraSwitchCase1)
                {
                    var switches = this.GetSameGroupSymbols().FindAll(p => p.IsSwitch);
                    if (!this.IsSwitch)
                    {
                        if (switches.Count == 1 && this.IsFirstLightInGroup())
                            return true;

                        return false;
                    }
                    else
                    {
                        if (switches.Count > 1 && !this.IsAketaraSwitch)
                            return false;

                        return true;
                    }
                }
                else if (this.WithAketaraSwitchCase2)
                {
                    if (this.IsAketaraSwitch)
                        return true;
                    if (this.IsSwitch)
                        return false;

                    return true;
                }

                if (this.WithTottaraRimokonCase1)
                {
                    if (this.IsTottaraRimokonCase1)
                        return true;
                    else
                        return false;
                }
                if (this.IsLEDLightConCase1 && this.IsLEDLightCon2nd3Way)
                    return true;
                if (this.WithLEDLightConCase1)
                {
                    if (!this.IsSwitch && !this.Parent.IsLight)
                        return true;
                }
                if (this.WithLEDLightConCase2)
                {
                    if (!this.IsSwitch)
                        return false;
                    if (this.IsLEDLightCon && this.GetSameGroupSymbols().Find(p => p.IsSwitch && !p.IsLEDLightCon) == null)
                        return true;
                    if (this.IsLEDLightCon && !this.IsLEDLightCon2nd3Way)
                        return true;
                    if (this.Is3WayOr4WaySwitchConnection && this.WithLEDLightCon2nd3Way)
                        return true;
                    return false;
                }
                if (this.IsLEDLightConCase3)
                    return true;

                //OtherSwitch
                if (this.WithOtherSwitch)
                {
                    if (this.IsSwitch)
                    {
                        if (this.IsOtherSwitchCase1 || this.IsOtherSwitchCase3)
                            return true;
                        else
                            return false;
                    }
                    else
                    {
                        if (this.WithOtherSwitchCase1)
                            return false;
                        else
                            return true;
                    }
                }

                //特殊ケースは個別に対応する
                if (this.WithKatteniSwitch3Way)
                {
                    if (!this.IsSwitch)
                    {
                        if (this.WithKatteniSwitchToilet)
                            return true;

                        //DLtoDL
                        if (this.GetSameGroupSymbols().FindAll(p => !p.IsSwitch).Count > 1 && !this.IsSwitch)
                        {
                            if (this.IsFirstInGroupWithoutSwitch())
                                return true;
                            else
                                return false;
                        }
                        return true;
                    }

                    if (this.IsKatteniSwitch3Way)
                    {
                        if (this.IsKatteniSwitchToilet)
                            return false;
                        else
                            return true;
                    }
                    return false;
                }

                if (this.IsDrainHeaterSwitch || this.IsRoofHeaterSwitch)
                    return true;

                if (this.IsKatteniSwitchParent)
                    return true;

                if (this.IsKatteniSwitchToilet)
                    return true;

                if (this.IsKatteniSwitchControlUnit)
                    return false;

                if (this.WithKatteniSwitchParent)
                    return false;

                if (this.WithKatteniSwitchToilet)
                    return false;

                if (this.WithOnOffDisplay)
                    return this.IsLight;

                if (this.IsLightControlSwitch || this.IsLightControlRotarySwitch)
                    return true;

                if (this.IsLight)
                {
                    if (this.WithLEDLightConCase3 || this.WithLightControlSwitch)
                    {
                        if (!this.Parent.IsLight)
                            return true;
                    }
                    else if (this.WithLightControlRotarySwitch)
                        return false;
                }

                if (this.IsFeedSymbol)
                {
                    if (this.Parent.IsJointBox)
                        return true;
                    if (this.IsFirstLightInGroup())
                        return true;

                    return false;
                }

                return !this.IsSwitch;
            }
        }

        public bool HasEarthMainWire
        {
            get
            {
                //天井ヒータースイッチは直結とそうでない場合で結線を変える
                if (this.IsRoofHeaterSwitch)
                {
                    if (this.Parent.IsJointBox)
                        return false;
                }

                if (this.IsLight)
                {
                    if (this.WithLightControlRotarySwitch)
                    {
                        if (this.Parent.IsLight)
                            return false;
                        else
                            return this.Wire.Base.WithEarth;
                    }
                    else if (this.WithLightControlSwitch)
                    {
                        if (this.IsWallLight)
                            return this.Wire.Base.WithEarth;
                        else if (!this.Parent.IsLight)
                            return this.Wire.Base.WithEarth;
                        else
                            return false;
                    }
                }

                if (this.IsFeedSymbol)
                {
                    //2個目以降のダウンライト送り配線は、アースも送る為、幹線には繋がない。
                    if (!this.IsFirstLightInGroup())
                        return false;
                }

                return this.Wire.Base.WithEarth;
            }
        }

        #endregion

        #endregion

        #region メソッド

        #region 接続判定

        public bool IsConnected(Symbol symbol)
        {
            if (this.Floor != symbol.Floor)
                return false;

            if (!symbol.Contains(this.PositionBottom) &&
                !symbol.Contains(this.PositionTop) &&
                !symbol.Contains(this.PositionRight) &&
                !symbol.Contains(this.PositionLeft))
                return false;

            return true;
        }

        /// <summary>
        /// 基点が含まれていないと接触と見なさない
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public bool IsConnectedByActualPosition(Symbol symbol)
        {
            if (this.Floor != symbol.Floor)
                return false;

            if (symbol.Contains(this.ClippedPosition))
                return true;

            if (this.Contains(symbol.ClippedPosition))
                return true;

            return false;
        }

        public bool IsOn(CeilingPanel panel)
        {
            if (this.CeilingPanel == null)
                return false;

            if (this.CeilingPanel.Name != panel.Name)
                return false;

            return true;
        }

        public bool Contains(PointD point)
        {
            var localPoint = point.ConvertToLocalPoint(this.Position, this.Rotation);

            return localPoint.IsOn(this.PointBottomLeft, this.PointTopRight, true);
        }

        public bool ContainsClippedPosition(PointD point)
        {
            var localPoint = point.ConvertToLocalPoint(this.ClippedPosition, this.ClippedRotation);

            return localPoint.IsOn(this.ClippedPointBottomLeft, this.ClippedPointTopRight, true);
        }


        public bool Clipped
        {
            get
            {
                return this.Position != this.ClippedPosition;
            }
        }
        #endregion

        #region 配線状況チェック

        public List<Symbol> GetChildrenWithoutJB()
        {
            List<Symbol> list = new List<Symbol>();
            foreach (var child in this.Children)
            {
                if (child.IsJointBox)
                    continue;

                list.Add(child);
                list.AddRange(child.GetChildrenWithoutJB());
            }

            //強電を含んでいたら、弱電の子は対象にしない
            if (list.Exists(p => !p.IsLightElectrical))
                list.RemoveAll(p => p.IsLightElectrical && !p.IsTop);

            return list;
        }

        public List<Symbol> GetChildrenWithJB()
        {
            List<Symbol> list = new List<Symbol>();
            foreach (var child in this.Children)
            {
                list.Add(child);

                if (!child.IsJointBox)
                    list.AddRange(child.GetChildrenWithoutJB());
            }

            //強電を含んでいたら、弱電の子は対象にしない
            if (list.Exists(p => !p.IsLightElectrical))
                list.RemoveAll(p => p.IsLightElectrical && !p.IsTop);

            return list;
        }

        public bool IsFirstInGroup()
        {
            return this.GetSameGroupSymbols().Find(p => true) == this;
        }

        public bool IsLastInGroup()
        {
            return this.GetSameGroupSymbols().FindLast(p => true) == this;
        }

        public bool IsFirstInGroupWithoutSwitch()
        {
            if (this.IsSwitch)
                return false;

            return this.GetSameGroupSymbols().Find(p => !p.IsSwitch) == this;
        }

        public bool IsFirstSwitchInGroup()
        {
            if (!this.IsSwitch)
                return false;

            return this.GetSameGroupSymbols().Find(p => p.IsSwitch) == this;
        }

        public bool IsLastSwitchInGroup()
        {
            if (!this.IsSwitch)
                return false;

            return this.GetSameGroupSymbols().FindLast(p => p.IsSwitch) == this;
        }

        public bool IsFirstLightInGroup()
        {
            if (!this.IsLight)
                return false;

            return this.GetSameGroupSymbols().Find(p => p.IsLight) == this;
        }

        public List<Symbol> GetSameGroupSymbols()
        {
            if (this.IsJointBox)
                return new List<Symbol>(new Symbol[] { this });

            var jointBox = this.GetParentJointBox();
            if (jointBox == null)
                return new List<Symbol>(new Symbol[] { this });

            var children = jointBox.GetChildrenWithoutJB();
            return children.FindAll(p => p.GroupNo == this.GroupNo);
        }

        #endregion

        #region 集計

        //数量

        public int CountAllChildren()
        {
            int count = 0;
            count += this.children.Count;
            this.Children.ForEach(p => count += p.CountAllChildren());
            return count;
        }

        public int CountLight()
        {
            int count = 0;
            if (this.IsLight)
                count++;

            this.Children.ForEach(p => count += p.CountLight());

            return count;
        }

        public int CountOutlet()
        {
            int count = 0;
            if (this.IsOutlet)
                count++;

            this.Children.ForEach(p => count += p.CountOutlet());

            return count;
        }

        //VA

        public int GetVATotal()
        {
            int va = this.Equipment.ApparentPower;

            this.Children.ForEach(p => va += p.GetVATotal());

            return va;
        }

        public int GetVATotalWithout200V()
        {
            int va = 0;
            if (!this.Is200V)
                va += this.Equipment.ApparentPower;

            this.Children.ForEach(p => va += p.GetVATotalWithout200V());

            return va;
        }

        public int GetVATotalOnly200V()
        {
            int va = 0;
            if (this.Is200V)
                va += this.Equipment.ApparentPower;

            this.Children.ForEach(p => va += p.GetVATotalOnly200V());

            return va;
        }

        #endregion

        #region 取得

        public Symbol GetRootJointBox()
        {
            if (this.IsJointBox && !this.IsSubJointBox)
                return this;

            if (this.Parent == null)
                throw new ApplicationException(Messages.UnexpectedProcessCalled("GetRootJointBox"));

            return this.Parent.GetRootJointBox();
        }

        public Symbol GetParentJointBox()
        {
            if (this.Parent == null)
                return null;

            if (this.Parent.IsJointBox)
                return this.Parent;

            if (this.Parent.IsBreaker)
                return this.Parent;

            return this.Parent.GetParentJointBox();
        }

        public string GetWireNo()
        {
            if (this.IsLightElectrical)
            {
                if (this.Parent == null)
                    return this.GetWireNoForLightElectrical();
                if (this.Parent.IsLightElectrical)
                    return this.GetWireNoForLightElectrical();
            }

            //分電盤
            if (this.IsBreaker)
                return "0";

            //サブスイッチ
            if (this.IsSubLightControlSwitch)
            {
                var wireNoAttr = this.Attributes.Find(p => p.Tag == Const.AttributeTag.WIRE_NO);
                if (wireNoAttr != null)
                    return wireNoAttr.Value.ToString();
            }

            //分電盤直下の部材
            if (this.Parent.IsBreaker)
            {
                if (this.IsJointBox)
                    return "J" + this.SequenceNo;
                else if (this.WithRoofHeaterSwitch && !this.IsRoofHeaterSwitch)
                    return this.GetSameGroupSymbols().Find(p => p.IsRoofHeaterSwitch).SequenceNo + Utilities.ToAlphabet(this.SequenceNo);
                else
                    return this.SequenceNo.ToString();
            }

            if (this.IsDirectSwitchOutlet)
            {
                return this.Parent.GetWireNo() + Utilities.ToAlphabet(this.SequenceNo);
            }

            //パワコン
            if (this.Parent.IsSolarWireTop)
                return "PC-" + Utilities.ToMaruNumber(this.SequenceNo);

            //分電盤の孫以下の部材
            if (this.IsSubJointBox)
                return this.GetRootJointBox().SequenceNo + "-" + Utilities.ToMaruNumber(this.SequenceNo);

            if (this.GetRootJointBox() == this.GetParentJointBox())
                return this.GetParentJointBox().SequenceNo + "-" + Utilities.ToMaruNumber(this.SequenceNo);
            else
                return this.GetParentJointBox().GetWireNo() + Utilities.ToAlphabet(this.SequenceNo);
        }

        private string GetWireNoForLightElectrical()
        {
            if (!this.IsLightElectrical)
                return string.Empty;

            if (this.IsToridashiForLightElectrical)
                return string.Format("H-{0}", Utilities.ToMaruNumber(this.SequenceNo));

            var lightElectricals = UnitWiring.Masters.LightElectricals;
            if (lightElectricals.Count == 0)
                throw new ApplicationException(Messages.UnexpectedProcessCalled("NotFoundLightElectricals"));

            var lightEle = lightElectricals.Find(p => p.EquipmentId == this.Equipment.Id);
            if (lightEle == null)
                throw new ApplicationException(Messages.UnexpectedProcessCalled("NotFoundLightElectricals"));

            //SymbolのAttributeには入れないためプロパティが空ならそのまま返す
            if (string.IsNullOrEmpty(this.SequenceNoForLightElectrical))
            {
                var wireNo = this.Attributes.Find(p => p.Tag == Const.AttributeTag.WIRE_NO);
                if (wireNo == null)
                    return string.Empty;
                if (!string.IsNullOrEmpty(wireNo.Value))
                    return wireNo.Value;
            }


            if (lightEle.NumberingFormat == null)
                throw new ApplicationException(Messages.UnexpectedProcessCalled("NotFoundLightElectricals"));

            return string.Format(lightEle.NumberingFormat, this.SequenceNoForLightElectrical.Split(','));
        }

        /// <summary>子シンボルの中で最大のGroupNoを返す</summary>
        public int GetMaxGroupNo()
        {
            int maxGroupNo = 0;
            foreach (var child in this.Children)
            {
                if (maxGroupNo < child.GroupNo)
                    maxGroupNo = child.GroupNo;
            }

            return maxGroupNo;
        }

        /// <summary>配下のシンボルが属する部屋名を連結して返す。(オプションで改行有無指定)</summary>
        public string GetRoomTitle(bool withLineBreak)
        {
            var roomList = this.GetChildRoomList(this);

            var roomTitle = string.Empty;
            foreach (var room in roomList)
            {
                if (withLineBreak && roomList.IndexOf(room) % 3 == 0 && roomList.IndexOf(room) != 0)
                    roomTitle += @"\P"; //AutoCADのマルチテキストの改行コード

                roomTitle += room + "・";
            }

            return roomTitle.TrimEnd('・');
        }

        /// <summary>JointBox配下のシンボルが属する部屋をリストで返す</summary>
        private List<string> GetChildRoomList(Symbol jointBox)
        {
            var roomList = new List<string>();
            foreach (var child in jointBox.Children)
            {
                if (child.IsJointBox)
                {
                    var childRoomList = GetChildRoomList(child);
                    foreach (var childRoom in childRoomList)
                    {
                        if (!roomList.Contains(childRoom))
                            roomList.Add(childRoom);
                    }

                    continue; //ジョイントボックスの部屋名は登録しない
                }

                if (!roomList.Contains(child.RoomName))
                    roomList.Add(child.RoomName);
            }

            return roomList;
        }

        #endregion

        public void FillRoom(List<RoomObject> rooms)
        {
            this.Room = null;
            this.RoomName = Const.Room.外部;

            foreach (var room in rooms)
            {
                if (this.Floor != room.Floor)
                    continue;

                if (!this.ActualPosition.IsIn(room.ObjectId))
                    continue;

                this.Room = room;
                this.RoomName = room.Name;
                return;
            }
        }

        /// <summary>シンボルの指定した側面の中心座標を返す</summary>
        public PointD GetSurfacePoint(Direction direction)
        {
            double MARGIN = 10;

            if (direction == Direction.Center)
                return this.PositionCenter;

            if (direction == Direction.Up)
                return new PointD(this.PositionTop.X, this.PositionTop.Y - MARGIN);

            if (direction == Direction.Down)
                return new PointD(this.PositionBottom.X, this.PositionBottom.Y + MARGIN);

            if (direction == Direction.Right)
                return new PointD(this.PositionRight.X - MARGIN, this.PositionRight.Y);

            if (direction == Direction.Left)
                return new PointD(this.PositionLeft.X + MARGIN, this.PositionLeft.Y); ;

            return null;
        }

        /// <summary>指定したtextと一致するコメントを保持していたらtrue</summary>
        public bool HasComment(string text)
        {
            return this.OtherAttributes.Exists(p => p.Value == text);
        }

        /// <summary>指定したtextを含むコメントを保持していたらtrue</summary>
        public bool ContainsComment(string text)
        {
            return this.OtherAttributes.Exists(p => p.Value.Contains(text));
        }

        /// <summary>指定したtextをから始まるコメントを保持していたらtrue</summary>
        public bool StartWithComment(string text)
        {
            return this.OtherAttributes.Exists(p => p.Value.StartsWith(text));
        }

        #endregion

        public static List<Symbol> GetAll(int floor)
        {
            return GetSymbols(floor);
        }

        public static List<Symbol> GetAllForPI(int floor)
        {
            var symbols = GetSymbolsForPI(floor);
            return symbols;
        }

        private static List<Symbol> GetSymbols(int floor)
        {
            var list = new List<Symbol>();

            var allBlocks = AutoCad.Db.BlockTable.GetIds();
            foreach (var blockId in allBlocks)
            {
                var blockName = AutoCad.Db.BlockTableRecord.GetBlockName(blockId);
                if (!UnitWiring.Masters.Blocks.Exists(p => p.Name == blockName))
                    continue;

                foreach (var objectId in AutoCad.Db.BlockTableRecord.GetBlockReferenceIds(blockId))
                {
                    var layerName = AutoCad.Db.Entity.GetLayerName(objectId);
                    if (layerName == Const.Layer.電気_SocketPlan || layerName == Const.Layer.電気_SocketPlan_Specific)
                        continue;

                    //ﾓﾃﾞﾙレイアウト以外に置いたブロックは無視する
                    var ownerId = AutoCad.Db.Object.GetOwnerId(objectId);
                    var ownerBlockName = AutoCad.Db.BlockTableRecord.GetBlockName(ownerId);
                    if (!ownerBlockName.Contains("Model"))
                        continue;

                    var symbol = new Symbol(floor, objectId);
                    if (symbol.Equipment == null)
                        continue;

                    list.Add(symbol);
                }
            }

            return list;
        }

        private static List<Symbol> GetSymbolsForPI(int floor)
        {
            var list = new List<Symbol>();

            var allBlocks = AutoCad.Db.BlockTable.GetIds();
            foreach (var blockId in allBlocks)
            {
                var blockName = AutoCad.Db.BlockTableRecord.GetBlockName(blockId);
                if (!UnitWiring.Masters.Blocks.Exists(p => p.Name == blockName))
                    continue;

                foreach (var objectId in AutoCad.Db.BlockTableRecord.GetBlockReferenceIds(blockId))
                {
                    //こっちはﾓﾃﾞﾙに置いたブロックを無視する
                    var ownerId = AutoCad.Db.Object.GetOwnerId(objectId);
                    var ownerBlockName = AutoCad.Db.BlockTableRecord.GetBlockName(ownerId);
                    if (ownerBlockName.Contains("Model"))
                        continue;

                    var symbol = new Symbol(floor, objectId);
                    if (symbol == null)
                        continue;

                    list.Add(symbol);
                }
            }
            list.Sort((a, b) => { return a.SequenceNo.CompareTo(b.SequenceNo); });

            return list;
        }

        /// <summary>
        /// 分電盤を最上位とするツリー構造のままだと再帰処理が必要になり、コードが汚くなる。
        /// ツリー構造よりリストの方が都合が良い時は、リストに変換する
        /// </summary>
        public static List<Symbol> ConvertToSymbolList(List<Symbol> breakers)
        {
            List<Symbol> list = new List<Symbol>();

            foreach (var breaker in breakers)
            {
                Symbol.AddChildrenToList(breaker, ref list);
            }

            return list;
        }

        private static void AddChildrenToList(Symbol symbol, ref List<Symbol> list)
        {
            foreach (var child in symbol.Children)
            {
                list.Add(child);

                Symbol.AddChildrenToList(child, ref list);
            }
        }

        /// <summary>デバッグしやすいようにOverrideしとく。実際の処理では使わないよ。</summary>
        public override string ToString()
        {
            var str = "[" + this.Floor + "F " + this.RoomName + "] " + this.Equipment.Name + " (";

            foreach (var att in this.OtherAttributes)
            {
                str += att.Value + "/";
            }
            str = str.TrimEnd('/');

            str += ")";

            return str;
        }

        /// <summary>ToString()をDataGridViewに表示したかったから作ったプロパティだよ。</summary>
        public string DetailName
        {
            get
            {
                return this.ToString();
            }
        }

        /// <summary>GroupedSymbolsプロパティを設定する</summary>
        public void SetGroupedSymbol(List<List<Symbol>> groupedSymbols)
        {
            foreach (var group in groupedSymbols)
            {
                if (!group.Contains(this))
                    continue;

                this.GroupedSymbolsForKairo = group;
            }
        }

        public Symbol CreateClone()
        {
            var clone = new Symbol(this.Floor, this.ObjectId);

            Type type = this.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (!property.CanWrite)
                    continue;

                property.SetValue(clone, property.GetValue(this, null), null);
            }

            return clone;
        }

        public void FillOutdoor(int floor)
        {
            var isOn = false;
            var isIn = false;
            var houseLines = HouseObject.GetAll(floor);
            this.Floor = floor;

            this.OnHouseOutLine = false;
            foreach (var houseOutline in houseLines)
            {
                if (this.Floor != houseOutline.Floor)
                    continue;

                if (this.ActualPosition.IsIn(houseOutline.ObjectId))//IsInは「以内」「≦」って感じ
                    isIn = true;

                if (AutoCad.Db.Curve.IsOn(this.ActualPosition, houseOutline.ObjectId)) //IsOnは「=」って感じ
                {
                    isOn = true;
                    this.OnHouseOutLine = true;
                }
            }

            if (!this.IsOutside)
                this.IsOutside = isOn || !isIn; //外周線上or外周線外だったらtrue

            this.IsOutdoor = !isIn;

            var roomLines = RoomObject.GetAll(floor);
            var syakoRoom = roomLines.FindAll(p => p.Name.Contains(Const.Room.車庫) && p.Floor == floor);
            if (syakoRoom.Count == 0)
                return;

            if (syakoRoom.Exists(p => this.ActualPosition.IsIn(p.ObjectId)))
            {
                this.IsOutdoor = true;
                this.IsOutside = true;
                this.OnHouseOutLine = true;

                return;
            }

            if (syakoRoom.Exists(p => AutoCad.Db.Curve.IsOn(this.ActualPosition, p.ObjectId)))
                this.IsOutside = true;
        }

        public decimal GetExtraWireLength()
        {
            //配線先器具によって、一律で加算する
            if (this.IsBreaker && !this.IsLightElectrical)
                return Static.HouseSpecs.BreakerExtraLength;
            else if (this.IsJointBox)
                return Static.HouseSpecs.JBExtraLength;
            else if (this.IsDownLight)
                return Static.HouseSpecs.DownLightExtraLength;
            else if (this.IsSolarWireTop)
                return Static.HouseSpecs.SolarSocketExtraLength;
            else if (this.IsPCBox)
                return Static.HouseSpecs.PowerConExtraLength;
            else if (this.IsToridashiForLightElectrical)
                return Static.HouseSpecs.HaikanTakeOutExtraLength;
            else
                return Static.HouseSpecs.TerminalExtraLength;
        }

        /// <summary>
        /// ライコンの子グループの先頭を取得（スイッチから探す時用）
        /// </summary>
        /// <returns></returns>
        public List<Symbol> GetSameChildGroup()
        {
            var symbols = new List<Symbol>();
            if (this.DenkiWires == null)
                return symbols;
            foreach (var wire in this.DenkiWires)
            {
                //結線が途切れてたら処理中断（ContainerにValidationで拾わせる）
                if (wire.ParentSymbol == null || wire.ChildSymbol == null)
                    return symbols;

                symbols.Add(wire.ParentSymbol);
                symbols.Add(wire.ChildSymbol);
                symbols.Remove(this);
            }
            symbols.RemoveAll(p => p.IsJointBox || p.IsBreaker);
            return symbols;
        }

        public string GetRotate()
        {
            //端数を丸める
            var rotate = "";
            var angle = Math.Round(AutoCad.Db.BlockReference.GetRotation(this.ObjectId) / Math.PI * 180);
            if (angle <= 45 || angle > 315)
                rotate = "Down";
            else if (angle > 45 && angle <= 135)
                rotate = "Right";
            else if (angle > 135 && angle <= 225)
                rotate = "Up";
            else
                rotate = "Left";

            return rotate;
        }
    }
}
