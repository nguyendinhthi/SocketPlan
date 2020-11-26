using System;
using System.Collections.Generic;
using Edsa.AutoCadProxy;
using SocketPlan.WinUI.SocketPlanServiceReference;
using SocketPlan.WinUI.Entities.CADEntity;

namespace SocketPlan.WinUI
{
    //ゆくゆくは図形情報の取得はここに１本化したい。

    [Flags]
    public enum CadObjectTypes
    {
        Symbol = 1,
        Wire = 2,
        DenkiWire = 4,
        CeilingPanel = 8,
        RoomOutline = 16,
        HouseOutline = 32,
        Text = 64,
        Kairo = 128,
        NisetaiArea = 256,
        Plate = 512,
        Clip = 1024,
        KansenWire = 2048,
        SignalWire = 4096,
        JboxWire = 8192
    }

    public partial class CadObjectContainer
    {
        #region フィールド変数(プロパティ用を除く)

        private CadObjectTypes objectTypes;

        #endregion

        #region コンストラクタ

        private CadObjectContainer()
        {
        }

        /// <summary>
        /// <para>図面からいろんなオブジェクトをまとめて取得します。</para>
        /// <para>取得するオブジェクトは「|」で連結して指定します。</para>
        /// <para>指定しなかったオブジェクトはnullになります。</para>
        /// </summary>
        public CadObjectContainer(List<Drawing> drawings, CadObjectTypes objectTypes)
            : this()
        {
            this.objectTypes = objectTypes;
            this.Drawings = drawings;
            this.InitializeContainer();
            this.FillProperty();

            this.ValidateCadObject();
        }

        private void InitializeContainer()
        {
            this.IgnoreSymbols = new List<Symbol>();

            if (this.HasFlag(CadObjectTypes.Symbol))
                this.Symbols = new List<Symbol>();

            if (this.HasFlag(CadObjectTypes.Wire))
                this.Wires = new List<Wire>();

            if (this.HasFlag(CadObjectTypes.DenkiWire))
            {
                this.DenkiWires = new List<Wire>();
                this.RedLines = new List<Wire>();
            }

            if (this.HasFlag(CadObjectTypes.KansenWire))
                this.KansenWires = new List<Wire>();

            if (this.HasFlag(CadObjectTypes.SignalWire))
                this.SignalWires = new List<Wire>();

            if (this.HasFlag(CadObjectTypes.JboxWire))
                this.JboxWires = new List<Wire>();

            if (this.HasFlag(CadObjectTypes.CeilingPanel))
                this.CeilingPanels = new List<CeilingPanel>();

            if (this.HasFlag(CadObjectTypes.RoomOutline))
                this.RoomOutlines = new List<RoomObject>();

            if (this.HasFlag(CadObjectTypes.HouseOutline))
                this.HouseOutlines = new List<HouseObject>();

            if (this.HasFlag(CadObjectTypes.Text))
                this.Texts = new List<TextObject>();

            if (this.HasFlag(CadObjectTypes.Plate))
                this.Plates = new List<Plate>();

            if (this.HasFlag(CadObjectTypes.Clip))
                this.Clips = new List<Symbol>();
        }

        private void FillProperty()
        {
            foreach (var drawing in this.Drawings)
            {
                WindowController2.BringDrawingToTop(drawing.WindowHandle);
                AutoCad.Command.Prepare();
                AutoCad.Command.PurgeBlocks();
                AutoCad.Command.SetCurrentLayoutToModel();
                this.UnlockLayers();

                var allSymbols = Symbol.GetAll(drawing.Floor);
                this.IgnoreSymbols.AddRange(allSymbols.FindAll(p => p.Equipment.EquipmentKindId == Const.EquipmentKind.DISTANCE_PATTERN));

                if (this.HasFlag(CadObjectTypes.Symbol))
                {
                    var symbols = allSymbols.FindAll(p => p.Equipment.EquipmentKindId != Const.EquipmentKind.DISTANCE_PATTERN);

                    if (this.HasFlag(CadObjectTypes.Clip))
                    {
                        var clips = symbols.FindAll(p => p.Equipment.Id == -2);
                        foreach (var clip in clips)
                        {
                            this.Clips.Add(clip.CreateClone());
                        }
                    }

                    this.ReplaceClipSymbols(symbols);
                    this.ReplaceTakeOutSymbols(symbols);
                    this.Symbols.AddRange(symbols);
                }

                if (this.HasFlag(CadObjectTypes.Wire))
                    this.Wires.AddRange(Wire.GetAll(drawing.Floor));

                if (this.HasFlag(CadObjectTypes.DenkiWire))
                {
                    this.DenkiWires.AddRange(Wire.GetSwitchWireAll(drawing.Floor));
                    var redLines = Filters.GetRedLineIds();
                    foreach (var redLine in redLines)
                    {
                        var w = new Wire(redLine);
                        w.Floor = drawing.Floor;
                        this.RedLines.Add(w);
                    }
                }

                if (this.HasFlag(CadObjectTypes.KansenWire))
                    this.KansenWires.AddRange(Wire.GetKansenWireAll(drawing.Floor));

                if (this.HasFlag(CadObjectTypes.SignalWire))
                    this.SignalWires.AddRange(Wire.GetSignalWireAll(drawing.Floor));

                if (this.HasFlag(CadObjectTypes.JboxWire))
                    this.JboxWires.AddRange(Wire.GetJboxWireAll(drawing.Floor));

                if (this.HasFlag(CadObjectTypes.CeilingPanel))
                    this.CeilingPanels.AddRange(CeilingPanel.GetAll(drawing.Floor));

                if (this.HasFlag(CadObjectTypes.RoomOutline))
                    this.RoomOutlines.AddRange(RoomObject.GetAll(drawing.Floor));

                if (this.HasFlag(CadObjectTypes.HouseOutline))
                    this.HouseOutlines.AddRange(HouseObject.GetAll(drawing.Floor));

                if (this.HasFlag(CadObjectTypes.Text))
                    this.Texts.AddRange(TextObject.GetAll(drawing.Floor));

                if (this.HasFlag(CadObjectTypes.Plate))
                    this.Plates.AddRange(Plate.GetAll(drawing.Floor));
            }

            if (this.IgnoreSymbols.Count > 0)
                this.IgnoreSymbols.ForEach(p => p.FillRoom(this.RoomOutlines));

            if (this.HasFlag(CadObjectTypes.Symbol))
            {
                Validation.ValidateConnectorNoAlphabet(this.Symbols);

                //複数階にまたがる配線は、インスタンスを1つにまとめる
                if (this.HasFlag(CadObjectTypes.Wire))
                {
                    this.SetOrangeMarkingWire(this.Symbols, this.Wires);
                    this.ReplaceMarkingWire(this.Symbols, this.Wires);

                    //StartsWithは、画面非表示用に語尾に_非表示用とついているレイヤーがあるための処置
                    var arrows = this.Symbols.FindAll(p => p.IsArrow && p.Layer.StartsWith(Const.Layer.電気_配線));
                    this.ReplaceToRisingWire(this.Wires, arrows);
                }

                if (this.HasFlag(CadObjectTypes.DenkiWire))
                {
                    var arrows = this.Symbols.FindAll(p => p.IsArrow && p.Layer == Const.Layer.電気_電気図面配線);
                    this.ReplaceToRisingWire(this.DenkiWires, arrows);
                }

                if (this.HasFlag(CadObjectTypes.KansenWire))
                {
                    var arrows = this.Symbols.FindAll(p => p.IsArrow && p.Layer == Const.Layer.電気_幹線);
                    this.ReplaceToRisingWire(this.KansenWires, arrows);
                    this.SetWireEndSymbol(this.KansenWires, this.Symbols);
                }

                if (this.HasFlag(CadObjectTypes.SignalWire))
                {
                    var arrows = this.Symbols.FindAll(p => p.IsArrow && p.Layer == Const.Layer.Signal_Wire);
                    this.ReplaceToRisingWire(this.SignalWires, arrows);
                }

                if (this.HasFlag(CadObjectTypes.JboxWire))
                {
                    var arrows = this.Symbols.FindAll(p => p.IsArrow && p.Layer == Const.Layer.電気_JboxWire);
                    this.ReplaceToRisingWire(this.JboxWires, arrows);
                }

                //システム用のシンボルを取り除く
                this.Symbols.RemoveAll(p => p.Equipment.Id < 0);

                if (this.HasFlag(CadObjectTypes.Plate))
                {
                    this.Plates.ForEach(p => p.SetSymbols(this.Symbols));
                }
            }
        }
        #endregion

        #region プロパティ

        public List<Drawing> Drawings { get; set; }

        public List<Symbol> Symbols { get; set; }
        public List<Symbol> Clips { get; set; }
        public List<Wire> Wires { get; set; }
        public List<Wire> DenkiWires { get; set; }
        public List<Wire> KansenWires { get; set; }
        public List<Wire> JboxWires { get; set; }
        public List<Wire> SignalWires { get; set; }
        public List<CeilingPanel> CeilingPanels { get; set; }
        public List<RoomObject> RoomOutlines { get; set; }
        public List<HouseObject> HouseOutlines { get; set; }
        public List<TextObject> Texts { get; set; }
        public List<Plate> Plates { get; set; }
        public List<Symbol> IgnoreSymbols { get; set; }

        /// <summary>赤色の線なのにレイヤーが電気配線関係じゃなかったらエラー</summary>
        public List<Wire> RedLines { get; set; }

        #endregion

        #region メソッド

        private void ReplaceClipSymbols(List<Symbol> symbols)
        {
            var clipBases = symbols.FindAll(p => p.Equipment.Block.Name == Const.BlockName.Clip);
            var leaders = Filters.GetClipLeaderIds();
            var rectangles = Filters.GetClipRectangleIds();
            foreach (var clipBase in clipBases)
            {
                var clipedSymbol = this.GetClipedSymbol(clipBase, symbols, leaders, rectangles);
                if (clipedSymbol != null)
                {
                    clipBase.Equipment = clipedSymbol.Equipment;
                    clipBase.Attributes = clipedSymbol.Attributes;
                    clipBase.RealPointTopRight = clipedSymbol.PointTopRight.Clone();
                    clipBase.RealPointBottomLeft = clipedSymbol.PointBottomLeft.Clone();
                    // クリップされているもともとのSymbolの座標を保持する
                    clipBase.ClippedObjectId = clipedSymbol.ObjectId;
                    clipBase.ClippedPosition = clipedSymbol.ClippedPosition;
                    clipBase.ClippedPointBottomLeft = clipedSymbol.PointBottomLeft;
                    clipBase.ClippedPointTopRight = clipedSymbol.PointTopRight;
                    clipBase.ClippedRotation = clipedSymbol.Rotation;
                    clipBase.ClippedBlockName = clipedSymbol.BlockName;
                    clipBase.RelationSymbols = clipedSymbol.RelationSymbols;

                    symbols.Remove(clipedSymbol);
                }
            }
        }

        /// <summary>
        /// ワイヤー両端のシンボルをつけて戻す（線の向きは考慮しません）
        /// ※電気配線はGetWiredSymbolsで普通に両端が入るはずなのでこの関数は使用しないでください。
        /// </summary>
        /// <param name="wires"></param>
        /// <param name="symbols"></param>
        public void SetWireEndSymbol(List<Wire> wires, List<Symbol> symbols)
        {
            foreach (var wire in wires)
            {
                //RisingWireの場合は子のワイヤーから探す
                if (wire is RisingWire)
                {
                    List<Symbol> risingWireEnd = new List<Symbol>();
                    foreach (var childWire in ((RisingWire)wire).Wires)
                    {
                        var childSymbols = symbols.FindAll(p => p.Floor == childWire.Floor);

                        this.SetWireEndSymbol(new List<Wire> { childWire }, childSymbols);

                        if (childWire.ParentSymbol != null)
                            risingWireEnd.Add(childWire.ParentSymbol);
                        if (childWire.ChildSymbol != null)
                            risingWireEnd.Add(childWire.ChildSymbol);
                    }

                    //FillChildSymbolsで細かく見てるのでチェックはゆるめ
                    if (risingWireEnd.Count >= 2)
                    {
                        wire.ParentSymbol = risingWireEnd[0];
                        wire.ChildSymbol = risingWireEnd[1];
                    }
                    continue;
                }
                var connectedSymbols = symbols.FindAll(symbol => wire.ParentSymbol != symbol &&
                                                                      wire.IsConnected(symbol) &&
                                                                      symbol.Floor == wire.Floor);

                //対象シンボルが特定できるケースでは絞り込みを試みる
                if (wire.Layer == Const.Layer.電気_幹線)
                {
                    //複数分電盤が取れてしまう場合があるので1つだけにする（長さは同じなのでどれでもいい）
                    var bundenban = connectedSymbols.Find(symbol => symbol.IsBundenban);
                    connectedSymbols = connectedSymbols.FindAll(symbol =>
                                                                symbol.Equipment.Name.Contains(Const.EquipmentName.幹線引込) ||
                                                                symbol.Equipment.Name.Contains(Const.EquipmentName.PowerBox) ||
                                                                symbol.Equipment.IsKansenHikiomiMeter);
                    if (bundenban != null)
                        connectedSymbols.Add(bundenban);
                }
                else if (wire.Layer == Const.Layer.電気_JboxWire)
                {
                    connectedSymbols = connectedSymbols.FindAll(symbol =>
                                                                symbol.IsJBox ||
                                                                symbol.Equipment.Name == Const.EquipmentName.JC ||
                                                                symbol.Equipment.Name == Const.EquipmentName.JCL ||
                                                                symbol.Equipment.Name == Const.EquipmentName.JCT);
                }

                for (int i = 0; i < connectedSymbols.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            wire.ParentSymbol = connectedSymbols[i];
                            break;
                        case 1:
                            wire.ChildSymbol = connectedSymbols[i];
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public Symbol GetClipedSymbol(Symbol baseSymbol, List<Symbol> symbols, List<int> leaders, List<int> rectangles)
        {
            //△シンボルに繋がっている抜き出し枠を探す
            int rectangle = ClipDrawer.GetClipRectangle(baseSymbol, leaders, rectangles);

            //抜き出し枠内にあるSymbolを探す
            var clipedSymbols = new List<Symbol>();
            foreach (var symbol in symbols)
            {
                if (symbol.Position.IsIn(rectangle))
                {
                    symbol.ActualPosition = baseSymbol.Position;
                    clipedSymbols.Add(symbol);
                }
            }

            if (clipedSymbols.Count == 0)
                return null;

            //抜き出し枠内にシンボルが複数あったら、優先度の高いシンボルを返す。とりあえずKind順で。
            //照明→コンセント→スイッチ→ジョイントボックス→ブレーカー→・・・てきとーに決めた順番です。
            clipedSymbols.Sort((p, q) => p.Equipment.EquipmentKindId.CompareTo(q.Equipment.EquipmentKindId));

            clipedSymbols.ForEach(p => clipedSymbols[0].RelationSymbols.Add(p));

            return clipedSymbols[0];
        }

        private void ReplaceToRisingWire(List<Wire> wires, List<Symbol> allowSymbols)
        {
            var arrows = allowSymbols.FindAll(p => p.IsArrow);

            //グルーピングするための入れ物を作る。
            var wireGroups = new Dictionary<string, List<Wire>>();
            var arrowGroups = new Dictionary<string, List<Symbol>>();

            foreach (var arrow in arrows)
            {
                var att = arrow.Attributes.Find(p => p.Tag == Const.AttributeTag.LINK_CODE);
                if (att == null)
                    throw new ApplicationException(Messages.InvalidFloorArrow2(arrow));

                var linkCode = att.Value;
                if (!wireGroups.ContainsKey(linkCode))
                {
                    wireGroups.Add(linkCode, new List<Wire>());
                    arrowGroups.Add(linkCode, new List<Symbol>());
                }

                arrowGroups[linkCode].Add(arrow);
            }

            //同じコードの矢印に繋がっている配線をグルーピングする
            foreach (var arrow in arrows)
            {
                var connectedWires = wires.FindAll(p => p.IsConnectedToArrowHead(arrow));
                if (connectedWires.Count != 1)
                    throw new ApplicationException(Messages.InvalidFloorArrow(arrow));

                foreach (var wire in connectedWires)
                {
                    var linkCode = arrow.Attributes.Find(p => p.Tag == Const.AttributeTag.LINK_CODE).Value;

                    if (!wireGroups[linkCode].Contains(wire))
                        wireGroups[linkCode].Add(wire);
                }
            }

            //複数の配線インスタンスを1つにまとめる
            foreach (var linkCode in wireGroups.Keys)
            {
                //無印コネクタは対になるコネクタが無いやつらなので、単独でRisingWireにする
                if (linkCode == Const.LINK_CODE_BLANK)
                {
                    foreach (var wire in wireGroups[linkCode])
                    {
                        var mujirusiWire = new RisingWire(new List<Wire> { wire });
                        mujirusiWire.FloorArrows.AddRange(arrowGroups[linkCode]); //雑に突っ込んどく。問題は無いはず。
                        wires.Add(mujirusiWire);

                        wires.Remove(wire);
                    }

                    continue;
                }

                if (wireGroups[linkCode].Count > 2)
                    throw new ApplicationException(Messages.LinkCodeDupulicate(linkCode, arrowGroups[linkCode]));
                else if (wireGroups[linkCode].Count < 2)
                    throw new ApplicationException(Messages.NotPairFloorArrow(linkCode, arrowGroups[linkCode]));

                var risingWire = new RisingWire(wireGroups[linkCode]);
                risingWire.FloorArrows.AddRange(arrowGroups[linkCode]);
                wires.Add(risingWire);

                wireGroups[linkCode].ForEach(p => wires.Remove(p));
            }
        }

        private void ReplaceTakeOutSymbols(List<Symbol> symbols)
        {
            var arrows = symbols.FindAll(p => p.Equipment.Kind.Id == Const.EquipmentKind.TAKE_OUT);
            foreach (var arrow in arrows)
            {
                var attribute = arrow.Attributes.Find(p => p.Tag == Const.AttributeTag.TAKE_OUT);
                if (attribute == null)
                    throw new ApplicationException(Messages.LostRelationOfTakeOutArrow(arrow));

                var handleId = attribute.Value;
                var blockId = AutoCad.Db.Utility.GetObjectId(handleId);

                Symbol symbol = null;
                if (blockId != 0)
                    symbol = symbols.Find(p => p.ObjectId == blockId);

                if (symbol == null)
                {
                    //コピペなどでシンボルのハンドルIDが変わってしまっている時は、
                    //シンボル側に同ハンドルIDを登録しているので、それを探す
                    foreach (var unko in symbols)
                    {
                        if (unko.Equipment.Kind.Id == Const.EquipmentKind.TAKE_OUT)
                            continue;

                        var handleAtt = unko.Attributes.Find(p => p.Tag == Const.AttributeTag.TAKE_OUT);
                        if (handleAtt == null)
                            continue;

                        if (handleAtt.Value != handleId)
                            continue;

                        symbol = unko;
                        break;
                    }
                }

                if (symbol == null)
                    throw new ApplicationException(Messages.LostRelationOfTakeOutArrow(arrow));

                arrow.Equipment = symbol.Equipment;
                //弱電のときWireNoを退避して残す
                if (arrow.IsLightElectrical)
                {
                    var wireNo = arrow.Attributes.Find(p => p.Tag == Const.AttributeTag.WIRE_NO);
                    arrow.Attributes = symbol.Attributes;
                    if (wireNo != null)
                        arrow.Attributes.Add(wireNo);
                }
                else
                {
                    arrow.Attributes = symbol.Attributes;
                }
                arrow.RealPointTopRight = symbol.PointTopRight.Clone();
                arrow.RealPointBottomLeft = symbol.PointBottomLeft.Clone();

                arrow.RelationSymbols.Add(symbol);
                symbols.Remove(symbol);
            }
        }

        /// <summary>SymbolsとHouseOutlinesを取得した上で呼び出してください</summary>
        /// <summary>RoomOutlinesも取得した上で呼び出してください、車庫の場合も外部とみなすので</summary>
        public void FillIsOutsideAndIsOutdoor()
        {
            foreach (var symbol in this.Symbols)
            {
                var isOn = false;
                var isIn = false;
                foreach (var houseOutline in this.HouseOutlines)
                {
                    if (symbol.Floor != houseOutline.Floor)
                        continue;

                    if (symbol.ActualPosition.IsIn(houseOutline.ObjectId))//IsInは「以内」「≦」って感じ
                        isIn = true;

                    if (AutoCad.Db.Curve.IsOn(symbol.ActualPosition, houseOutline.ObjectId)) //IsOnは「=」って感じ
                        isOn = true;
                }

                symbol.OnHouseOutLine = isOn;

                if (!symbol.IsOutside)
                    symbol.IsOutside = isOn || !isIn; //外周線上or外周線外だったらtrue

                symbol.IsOutdoor = !isIn;

                var syakoRoom = this.RoomOutlines.FindAll(p => p.Name.Contains(Const.Room.車庫) && p.Floor == symbol.Floor);
                if (syakoRoom.Exists(p => symbol.ActualPosition.IsIn(p.ObjectId)))
                {
                    symbol.IsOutdoor = true;
                    symbol.IsOutside = true;
                }

                if (syakoRoom.Exists(p => AutoCad.Db.Curve.IsOn(symbol.ActualPosition, p.ObjectId)))
                {
                    symbol.IsOutside = true;
                    symbol.OnHouseOutLine = true;
                }

                if (!symbol.IsOutside)
                    continue;

                //壁|
                //壁|▼▼
                //壁|
                //（▼がシンボル）
                //同じ位置に設備が2個以上ある場合このように作図するが、
                //この場合、右側のシンボルが壁に接しているかを単純に判定できない。
                //左側のシンボルが壁に接していた場合、そのシンボルに接するシンボルも壁に接すると判定する必要がある。
                this.FillIsOutsideOfChainedSymbol(symbol);
            }
        }

        /// <summary>
        /// 自分と同じ子グループシンボルの取得（スイッチを渡すと空白を返します）
        /// </summary>
        /// <returns></returns>
        public void GetSameSubGroupSymbols(Symbol symbol, ref List<Symbol> result)
        {
            foreach (var wire in this.DenkiWires)
            {
                if (!wire.IsConnected(symbol))
                    continue;
                if (!result.Exists(p => p == wire.ParentSymbol) && !wire.ParentSymbol.IsSwitch && !wire.ParentSymbol.IsJointBox)
                {
                    result.Add(wire.ParentSymbol);
                    this.GetSameSubGroupSymbols(wire.ParentSymbol, ref result);
                }
                if (!result.Exists(p => p == wire.ChildSymbol) && !wire.ChildSymbol.IsSwitch && !wire.ChildSymbol.IsJointBox)
                {
                    result.Add(wire.ChildSymbol);
                    this.GetSameSubGroupSymbols(wire.ChildSymbol, ref result);
                }
            }
        }

        private void FillDenkiWires(Symbol symbol)
        {
            symbol.DenkiWires = new List<Wire>();
            foreach (var wire in this.DenkiWires)
            {
                if (wire.IsConnected(symbol))
                    symbol.DenkiWires.Add(wire);
            }

            foreach (var child in symbol.Children)
            {
                this.FillDenkiWires(child);
            }
        }

        private void FillIsOutsideOfChainedSymbol(Symbol symbol)
        {
            foreach (var candidateSymbol in this.Symbols)
            {
                if (candidateSymbol == symbol)
                    continue;

                if (candidateSymbol.Floor != symbol.Floor)
                    continue;

                if (candidateSymbol.IsOutside)
                    continue;

                if (!candidateSymbol.IsConnected(symbol))
                    continue;

                candidateSymbol.IsOutside = true;

                //再帰呼び出しをして、漏れを無くす。
                this.FillIsOutsideOfChainedSymbol(candidateSymbol);
            }
        }

        /// <summary>SymbolsとRoomOutlinesを取得した上で呼び出してください</summary>
        public void FillSymbolRooms()
        {
            foreach (var symbol in this.Symbols)
            {
                symbol.FillRoom(this.RoomOutlines);

                //置く部屋によってVAが変わる部材がある。ついでにここで変更しておく。
                this.UpdateApparentPower(symbol);
            }

            if (this.Clips != null)
            {
                foreach (var clip in this.Clips)
                {
                    clip.FillRoom(this.RoomOutlines);
                }
            }

            Validation.ValidateAllGaibu(this.Symbols);
        }

        public void FillTextRooms()
        {
            foreach (var text in this.Texts)
            {
                text.FillRoom(this.RoomOutlines);
            }
        }

        private void UpdateApparentPower(Symbol symbol)
        {
            if (symbol.BlockName == Const.BlockName.照明_01)
            {
                if (symbol.RoomName.Contains(Const.Room.トイレ) ||
                    symbol.RoomName.Contains(Const.Room.ホール))
                    symbol.Equipment.ApparentPower = 60;
            }

            if (symbol.IsDrainHeaterSwitch)
                symbol.Equipment.ApparentPower = 150;

            if (symbol.IsRoofHeaterSwitch)
                symbol.Equipment.ApparentPower = 700;
        }

        /// <summary>いろいろ不正データをチェックできるんだよ～☆</summary>
        private void ValidateCadObject()
        {
            if (this.Drawings.Count == 0)
                throw new ApplicationException(Messages.FailedToGetDrawings());

            if (this.HasFlag(CadObjectTypes.CeilingPanel) && this.CeilingPanels.Count == 0)
                throw new ApplicationException(Messages.NotFoundCeilingPanel());

            if (this.HasFlag(CadObjectTypes.RoomOutline) && this.RoomOutlines.Count == 0)
                throw new ApplicationException(Messages.NotFoundRoomOutline());

            if (this.HasFlag(CadObjectTypes.HouseOutline))
            {
                foreach (var drawing in this.Drawings)
                {
                    if (!this.HouseOutlines.Exists(p => p.Floor == drawing.Floor))
                        throw new ApplicationException(Messages.NotFoundHouseOutline());
                }
            }

            if (this.HasFlag(CadObjectTypes.Symbol))
            {
                Validation.ValidateDuplicatedSymbol(this.Symbols);
                Validation.ValidateDuplicatedAttribute(this.Symbols);
                Validation.ValidateSymbolLayer(this.Symbols);
            }
        }

        #region GetWiredSymbols

        /// <summary>FillIsOutsideAndIsOutdoorとFillSymbolRoomsをした上で呼び出す</summary>
        public List<Symbol> GetWiredSymbols(bool withLength)
        {
            var entrancePoint = this.GetEntrancePoint();

            var breakers = this.Symbols.FindAll(p => p.IsBreaker && !p.IsLightElectrical);
            if (breakers.Count == 0)
                throw new ApplicationException(Messages.NotFoundBreaker());

            foreach (var breaker in breakers)
            {
                this.FillChildSymbols(breaker, false);
                this.SortForGroupNo(breaker, entrancePoint);
                this.FillGroupNo(breaker);
                this.FillWireItem(breaker);
                this.FillDenkiWires(breaker);
                this.FillSubGroupNo(breaker);
                this.FillCeilingPanel(breaker);
                this.SetAlignmentMultiWires(breaker);
                this.SortSymbols(breaker, entrancePoint);
                if (withLength) //グロい処理なので、配線の長さが必要ない時は回避できるようにした。
                    this.FillLength3D(breaker);
            }
            this.FillCeilingPanelForSubLightCon();

            this.ValidateWiredSymbols();

            return breakers;
        }

        public List<Symbol> GetWiredLightElectricalSymbols(bool withLength)
        {
            var entrancePoint = this.GetEntrancePoint();
            var symbols = this.Symbols.FindAll(p => p.IsTop && p.IsLightElectrical);

            if (symbols.Count == 0)
                return new List<Symbol>();

            foreach (var symbol in symbols)
            {
                this.FillChildSymbols(symbol, true);
                this.SortForGroupNo(symbol, entrancePoint);
                this.FillGroupNo(symbol);
                this.FillWireItem(symbol);
                this.FillDenkiWires(symbol);
                this.FillSubGroupNo(symbol);
                this.FillCeilingPanel(symbol);
                this.SetAlignmentMultiWires(symbol);
                this.SortSymbols(symbol, entrancePoint);
                if (withLength) //グロい処理なので、配線の長さが必要ない時は回避できるようにした。
                    this.FillLength3D(symbol);
            }

            return symbols;
        }

        public List<Symbol> GetWiredSolarSymbols(bool withLength)
        {
            var entrancePoint = this.GetEntrancePoint();
            var symbols = this.Symbols.FindAll(p => p.IsSolarWireTop);

            if (symbols.Count == 0)
                return new List<Symbol>();

            foreach (var symbol in symbols)
            {
                this.FillChildSymbols(symbol, false);
                this.SortForGroupNo(symbol, entrancePoint);
                this.FillGroupNo(symbol);
                this.FillWireItem(symbol);
                this.FillDenkiWires(symbol);
                this.FillSubGroupNo(symbol);
                this.FillCeilingPanel(symbol);
                this.SetAlignmentMultiWires(symbol);
                this.SortSymbols(symbol, entrancePoint);
                if (withLength)
                    this.FillLength3D(symbol);
            }

            return symbols;
        }

        public List<Symbol> GetWiredJboxSymbols()
        {
            var result = new List<Symbol>();
            this.SetWireEndSymbol(this.JboxWires, this.Symbols);

            foreach (var wire in this.JboxWires)
            {
                var parent = this.Symbols.Find(p => p == wire.ParentSymbol);
                var child = this.Symbols.Find(p => p == wire.ChildSymbol);

                var jboxParent = new Symbol(parent.Floor, parent.ObjectId);
                var jboxChild = new Symbol(child.Floor, child.ObjectId);
                jboxParent.RoomName = parent.RoomName;
                jboxChild.RoomName = child.RoomName;

                if (jboxParent != null && jboxChild != null)
                {
                    //JB-Dは常に親
                    if (jboxChild.IsJBox)
                    {
                        var work = jboxParent;
                        jboxParent = jboxChild;
                        jboxChild = work;
                        wire.ParentSymbol = jboxParent;
                        wire.ChildSymbol = jboxChild;
                    }

                    jboxChild.Parent = jboxParent;
                    jboxChild.Wire = wire;
                    this.FillLength3DForJBOX(jboxChild);
                    jboxParent.Children.Add(jboxChild);
                    result.Add(jboxParent);
                }
            }
            return result;
        }

        public PointD GetEntrancePoint()
        {
            foreach (var roomOutline in this.RoomOutlines)
            {
                if (roomOutline.Floor != 1)
                    continue;

                if (roomOutline.Name != Const.Room.玄関)
                    continue;

                return roomOutline.CenterPoint;
            }

            //そんな重要な値ではないので、玄関が無かったら0,0を玄関として処理する
            return new PointD(0, 0);
        }
        #region FillChildSymbols

        public void FillChildSymbols(Symbol parentSymbol, bool isLightElectric)
        {
            //親シンボルに繋がる子配線を探す
            var childWires = new List<Wire>();
            foreach (var wire in this.Wires)
            {
                if (wire.ParentSymbol != null) //接続済みの配線は無視
                    continue;

                if (!wire.IsConnected(parentSymbol)) //繋がってなかったら無視
                    continue;

                if (!isLightElectric && wire.IsLightElectric) // 強電のとき弱電ワイヤーを無視する
                    continue;

                wire.ParentSymbol = parentSymbol;
                childWires.Add(wire);
            }

            //子配線に繋がる子シンボルを探す
            List<Wire> removeWires = new List<Wire>();
            foreach (var childWire in childWires)
            {
                var child = this.FindConnectedSymbol(childWire);
                if (child == null)
                    continue;


                if (isLightElectric && child.IsLightElectrical != parentSymbol.IsLightElectrical)   //弱電のとき強電-弱電のワイヤーは処理しない
                {
                    removeWires.Add(childWire);
                    continue;
                }

                childWire.ChildSymbol = child;
                child.Wire = childWire;
                parentSymbol.Children.Add(child);
                child.Parent = parentSymbol;
            }
            foreach (var remove in removeWires)
            {
                var wire = this.Wires.Find(p => p == remove);
                if (wire != null)
                    wire.ParentSymbol = null;
            }
            removeWires.ForEach(p => childWires.Remove(p));

            //[再帰呼び出し] 子シンボルに繋がる孫配線を探しに行く
            foreach (var child in parentSymbol.Children)
            {
                if (child.Equipment.CanHaveChild)
                    this.FillChildSymbols(child, isLightElectric);
            }
        }

        /// <summary>配線に繋がる子シンボルを探す</summary>
        private Symbol FindConnectedSymbol(Wire wire)
        {
            //繋がっているシンボルの一覧を単純に作る
            var connectedSymbols = this.Symbols.FindAll(symbol => wire.ParentSymbol != symbol && wire.IsConnected(symbol));

            //関連持たせたシンボルが最優先
            var relatedSymbol = connectedSymbols.Find(symbol => symbol.ObjectId == wire.ConnectedSymbolObjectId);
            if (relatedSymbol != null)
            {
                relatedSymbol.WiredPoint = this.GetWiredPoint(relatedSymbol, wire);
                return relatedSymbol;
            }

            //未配線で接続可能なシンボルがその次
            var connectedSymbol = connectedSymbols.Find(symbol => symbol.IsConnectableForLightElectrical && this.CanConnect(wire, symbol) && symbol.Wire == null);
            if (connectedSymbol != null)
            {
                connectedSymbol.WiredPoint = this.GetWiredPoint(connectedSymbol, wire);
                return connectedSymbol;
            }

            //繋がってるけど接続不可能なシンボルは、連鎖シンボルの可能性があるので、連鎖先を探す
            foreach (var symbol in connectedSymbols)
            {
                var chainedSymbol = this.FindChainedSymbol(wire, symbol, new List<Symbol>());
                if (chainedSymbol == null)
                    continue;

                chainedSymbol.WiredPoint = this.GetWiredPoint(symbol, wire);
                return symbol;
            }

            return null;
        }

        private Symbol FindChainedSymbol(Wire wire, Symbol currentSymbol, List<Symbol> scannedSymbols)
        {
            scannedSymbols.Add(currentSymbol);

            foreach (var candidateSymbol in this.Symbols)
            {
                if (candidateSymbol.Wire != null)
                    continue; //未接続のシンボルに絞る

                if (scannedSymbols.Contains(candidateSymbol))
                    continue; //すでに走査済みのシンボルは無視

                if (!candidateSymbol.IsConnected(currentSymbol))
                    continue; //現在のシンボルと接してなかったら無視

                if (!this.CanConnect(wire, candidateSymbol))
                    continue;

                Symbol resultSymbol;
                if (candidateSymbol.IsConnectableForLightElectrical)
                    resultSymbol = candidateSymbol;
                else
                    resultSymbol = this.FindChainedSymbol(wire, candidateSymbol, scannedSymbols);

                if (resultSymbol == null)
                    continue;

                return resultSymbol;
            }

            foreach (var nextRemovedSymbol in this.Symbols)
            {
                if (nextRemovedSymbol.Wire == null)
                    continue; //接続済みのシンボルに絞る

                if (scannedSymbols.Contains(nextRemovedSymbol))
                    continue;

                if (!nextRemovedSymbol.IsConnected(currentSymbol))
                    continue;

                var resultSymbol = this.FindChainedSymbol(wire, nextRemovedSymbol, scannedSymbols);
                if (resultSymbol == null)
                    continue;

                return resultSymbol;
            }

            return null;
        }

        /// <summary>配線規則上、接続可能かどうか</summary>
        private bool CanConnect(Wire wire, Symbol symbol)
        {
            if (wire.ParentSymbol.IsLightElectrical) // 弱電は子はTOPじゃなければOK
            {
                if (symbol.IsLightElectrical)
                {
                    var le = UnitWiring.Masters.LightElectricals.Find(p => p.EquipmentId == symbol.Equipment.Id);
                    if (le != null && !le.IsTop)
                        return true;
                }
            }
            else
            {
                if (wire.ParentSymbol.IsBreaker) //今のところ、分電盤直下はJBか専用コンセントかEVスイッチのみ
                    if (!(symbol.IsJointBox || symbol.IsExclusive || symbol.Equipment.CanHaveChild))
                        return false;

                if (wire.ParentSymbol.IsJointBox) //JB直下に専用コンセントは不可
                {
                    //天井ヒーター用のソケットはJB接続を許可する
                    if (symbol.OtherAttributes.Exists(p => p.Value == Const.Text.天井裏) &&
                        symbol.OtherAttributes.Exists(p => p.Value == Const.Text.専用E付 || p.Value == Const.Text.専用Ｅ付) &&
                        symbol.OtherAttributes.Exists(p => p.Value == Const.Text.直結))
                        return true;

                    if (symbol.IsExclusive)
                        return false;
                }
            }

            return true;
        }

        private PointD GetWiredPoint(Symbol symbol, Wire wire)
        {
            var targetWire = wire;
            if (wire is RisingWire)
            {
                targetWire = (wire as RisingWire).Wires.Find(p => p.Floor == symbol.Floor);
                if (targetWire is MarkingWire)
                {
                    var markingWire = (MarkingWire)targetWire;
                    targetWire = markingWire.Wires.Find(p => p.IsConnected(symbol));
                }
            }
            else if (wire is MarkingWire)
                targetWire = (wire as MarkingWire).Wires.Find(p => p.IsConnected(symbol));
            if (symbol.Contains(targetWire.StartPoint))
                return targetWire.StartPoint;

            if (symbol.Contains(targetWire.EndPoint))
                return targetWire.EndPoint;

            return null;
        }

        #endregion

        #region SortForGroupNo

        private void SortForGroupNo(Symbol symbol, PointD entrancePoint)
        {
            foreach (var child in symbol.Children)
            {
                this.SortForGroupNo(child, entrancePoint);
            }

            symbol.Children.Sort((p, q) =>
            {
                if (symbol.IsBreaker)
                {
                    //ジョイントボックスは最初
                    if (p.IsJointBox != q.IsJointBox)
                        return q.IsJointBox.CompareTo(p.IsJointBox);

                    if (p.IsExclusive != q.IsExclusive)
                        return p.IsExclusive.CompareTo(q.IsExclusive);

                    //専用配線は、キッチン→その他→エアコン→IH→エコキュートの順
                    if (p.IsExclusive && q.IsExclusive)
                    {
                        if (p.IsSeparatedUnit != q.IsSeparatedUnit)
                            return p.IsSeparatedUnit.CompareTo(q.IsSeparatedUnit);

                        if (p.ForIH != q.ForIH)
                            return p.ForIH.CompareTo(q.ForIH);

                        if (p.ForAC != q.ForAC)
                            return p.ForAC.CompareTo(q.ForAC);

                        if (p.ForKitchen != q.ForKitchen)
                            return q.ForKitchen.CompareTo(p.ForKitchen);
                    }
                }
                else
                {
                    //ジョイントボックスは最後
                    if (p.IsJointBox != q.IsJointBox)
                        return p.IsJointBox.CompareTo(q.IsJointBox);
                }

                if (p.Floor != q.Floor)
                    return p.Floor.CompareTo(q.Floor); //下階が先

                var pDistance = Calc.GetDistance(p.ActualPosition, entrancePoint);
                var qDistance = Calc.GetDistance(q.ActualPosition, entrancePoint);

                return pDistance.CompareTo(qDistance); //玄関に近い方が先
            });
        }

        #endregion

        #region FillGroupNo

        private void FillGroupNo(Symbol parent)
        {
            int groupNo = 1;

            var children = parent.GetChildrenWithJB();
            foreach (var child in children)
            {
                if (child.GroupNo != 0)
                    continue;

                child.GroupNo = groupNo;

                this.FillSameGroupNo(child, children);

                groupNo++;
            }

            foreach (var child in children)
            {
                if (child.IsJointBox)
                    this.FillGroupNo(child);
            }
        }

        private void FillSubGroupNo(Symbol parent)
        {
            var children = parent.GetChildrenWithJB();
            foreach (var child in children)
            {
                if (!child.IsSwitch)
                    continue;

                var subGroupNo = 1;
                var subChildren = child.GetSameChildGroup();
                foreach (var subChild in subChildren)
                {
                    if (subChild.SubGroupNo != 0)
                        continue;

                    subChild.SubGroupNo = subGroupNo;
                    subChild.SubGroupSeq = 1;

                    this.FillSameSubGroupNo(subChild);

                    subGroupNo++;
                }
            }

            foreach (var child in children)
            {
                if (child.IsJointBox)
                    this.FillSubGroupNo(child);
            }
        }

        private void FillSameGroupNo(Symbol currentSymbol, List<Symbol> children)
        {
            var connectedWires = this.DenkiWires.FindAll(p => p.ChildSymbol == null && p.IsConnected(currentSymbol));

            foreach (var connectedWire in connectedWires)
            {
                var connectedSymbol = children.Find(p => p.GroupNo == 0 && connectedWire.IsConnected(p));
                if (connectedSymbol == null)
                    continue;

                connectedSymbol.GroupNo = currentSymbol.GroupNo;
                connectedWire.ParentSymbol = currentSymbol;
                connectedWire.ChildSymbol = connectedSymbol;

                this.FillSameGroupNo(connectedSymbol, children);
            }
        }

        private void FillSameSubGroupNo(Symbol currentSymbol)
        {
            //スイッチにぶつかるまで同じサブグループにする
            var groups = currentSymbol.GetSameGroupSymbols();
            foreach (var wire in currentSymbol.DenkiWires)
            {
                var connected = groups.FindAll(p => wire.IsConnected(p));
                connected.Remove(currentSymbol);
                connected.RemoveAll(p => p.IsSwitch || p.SubGroupNo != 0);

                foreach (var subChild in connected)
                {
                    subChild.SubGroupNo = currentSymbol.SubGroupNo;
                    subChild.SubGroupSeq = currentSymbol.SubGroupSeq + 1;
                    this.FillSameSubGroupNo(subChild);
                }
            }
        }

        #endregion

        #region FillWireItem

        private void FillWireItem(Symbol parent)
        {
            foreach (var child in parent.Children)
            {
                child.Wire.Base = this.FindWireItem(child);

                this.FillWireItem(child);
            }
        }

        private WireItem FindWireItem(Symbol symbol)
        {
            var items = UnitWiring.Masters.WireItems;

            //弱電の親が強電のケースは今のところ火災報知機だけ
            if (symbol.IsLightElectrical)
            {
                if (symbol.Parent != null)
                {
                    if (!symbol.Parent.IsLightElectrical)
                    {
                        if (symbol.OnHouseOutLine)
                            return items.Find(p => p.Id == Const.WireItemId.火災報知機_壁付);
                        else
                            return items.Find(p => p.Id == Const.WireItemId.火災報知機);
                    }
                }
                return items.Find(p => p.Id == Const.WireItemId.弱電);
            }

            if (symbol.IsSwitch)
            {
                if (symbol.IsEVSwitch)
                    return items.Find(p => p.Id == Const.WireItemId.IHorEV);
                else if (symbol.IsRoofHeaterSwitch)
                    return items.Find(p => p.Id == Const.WireItemId.専用orコンセント_外周_E付);
                else if (symbol.Is3WaySwitch || symbol.IsDrainHeaterSwitch)
                    return items.Find(p => p.Id == Const.WireItemId.スイッチ_3路);
                else if (symbol.IsDirectSwitch)
                {
                    if (symbol.Children.Count == 0)
                        return items.Find(p => p.Id == Const.WireItemId.専用orパワコンorコンセント_外周);

                    //専用E付の子があったら線種を変える
                    var child = symbol.Children[0];
                    if (child.HasComment(Const.Text.専用E付) || child.HasComment(Const.Text.専用Ｅ付) ||
                        (Array.Exists(child.Equipment.Specifications, p => p.Id == Const.Specification.専用) &&
                         Array.Exists(child.Equipment.Specifications, p => p.Id == Const.Specification.E付)))
                        return items.Find(p => p.Id == Const.WireItemId.専用orコンセント_外周_E付);
                    else
                        return items.Find(p => p.Id == Const.WireItemId.専用orパワコンorコンセント_外周);
                }
                else
                    return items.Find(p => p.Id == Const.WireItemId.スイッチ);
            }

            if (symbol.IsLight)
            {
                if (symbol.IsOutdoor)
                    return items.Find(p => p.Id == Const.WireItemId.照明_屋外);
                else
                    return items.Find(p => p.Id == Const.WireItemId.照明);

            }

            if (symbol.IsJointBox)
            {
                //子のシンボルにE付があれば、ジョイントボックスもE付
                if (symbol.WithEarth)
                    return items.Find(p => p.Id == Const.WireItemId.ジョイントボックス_E付);
                else
                    return items.Find(p => p.Id == Const.WireItemId.ジョイントボックス);
            }

            if (symbol.IsPCBox)
                return items.Find(p => p.Id == Const.WireItemId.専用orパワコンorコンセント_外周);

            if (symbol.IsOutlet)
            {
                //IH、EVのコンセントは太いやつ
                if (symbol.ForIH || symbol.IsEVOutlet)
                    return items.Find(p => p.Id == Const.WireItemId.IHorEV);

                if (symbol.IsDirectSwitchOutlet)
                {
                    if (symbol.HasComment(Const.Text.専用E付) || symbol.HasComment(Const.Text.専用Ｅ付) ||
                        (Array.Exists(symbol.Equipment.Specifications, p => p.Id == Const.Specification.専用) &&
                         Array.Exists(symbol.Equipment.Specifications, p => p.Id == Const.Specification.E付)))
                        return items.Find(p => p.Id == Const.WireItemId.専用orコンセント_外周_E付);
                    else
                        return items.Find(p => p.Id == Const.WireItemId.専用orパワコンorコンセント_外周);
                }

                //外周部でもトイレの換気扇は 2芯1.6mm
                if (symbol.IsOutside && symbol.RoomName.Contains(Const.Room.トイレ) && symbol.ForFan)
                    return items.Find(p => p.Id == Const.WireItemId.コンセント);

                if (symbol.WithEarth)
                {
                    if (symbol.IsExclusive || symbol.IsOutside)
                        return items.Find(p => p.Id == Const.WireItemId.専用orコンセント_外周_E付);
                    else
                        return items.Find(p => p.Id == Const.WireItemId.コンセント_水周り);
                }
                else
                {
                    if (symbol.IsExclusive || symbol.IsOutside)
                        return items.Find(p => p.Id == Const.WireItemId.専用orパワコンorコンセント_外周);
                    else
                        return items.Find(p => p.Id == Const.WireItemId.コンセント);
                }
            }

            throw new ApplicationException(Messages.UnexpectedSymbol(symbol));
        }

        #endregion

        #region FillCeilingPanel

        /// <summary>SignalWireしか持たないシンボルのために無理やりCeilingPanelを設定する </summary>
        private void FillCeilingPanelForSubLightCon()
        {
            var subSwitches = this.Symbols.FindAll(p => p.IsSubLightControlSwitch);

            foreach (var sub in subSwitches)
            {
                var signal = this.SignalWires.Find(p => p.IsConnected(sub));
                sub.Wire = signal;
                this.FillCeilingPanel(sub);
            }
        }

        private void FillCeilingPanel(Symbol symbol)
        {
            foreach (var child in symbol.Children)
            {
                this.FillCeilingPanel(child);
            }

            symbol.CeilingPanel = null;
            foreach (var ceilingPanel in this.CeilingPanels)
            {
                if (symbol.Floor != ceilingPanel.Floor)
                    continue;

                if (!ceilingPanel.Contains(symbol.ActualPosition))
                    continue;

                symbol.CeilingPanel = ceilingPanel;
                break;
            }

            //ブレーカーは基本動かさないが、天井パネルが取れなかった場合に直近のパネルを探してセットしている
            if (symbol.IsTop || symbol.IsSolarWireTop)
            {
                if (symbol.CeilingPanel == null)
                {
                    symbol.CeilingPanel = this.GetNearestCeilingPanelForBreaker(symbol, this.CeilingPanels);
                }

                return;
            }

            if (symbol.CeilingPanel != null)
                return;

            //シンボルが天井パネル上に無い場合は、シンボルに一番近い、配線の経路上にある天井パネルを設定する
            symbol.CeilingPanel = this.GetNearestCeilingPanel(symbol, this.CeilingPanels);
        }
 
        private CeilingPanel GetNearestCeilingPanelForBreaker(Symbol breaker, List<CeilingPanel> ceilingPanels)
        {
            foreach (var child in breaker.Children)
            {
                var targetWires = new List<Wire>();
                if (child.Wire is RisingWire)
                {
                    var risingWire = (RisingWire)child.Wire;
                    foreach (var wire in risingWire.Wires)
                    {
                        if (wire.Floor != child.Floor)
                            continue;

                        if (wire is MarkingWire)
                            targetWires.AddRange(((MarkingWire)wire).Wires);
                        else
                            targetWires.Add(wire);
                    }
                }
                else if (child.Wire is MarkingWire)
                {
                    targetWires.AddRange(((MarkingWire)child.Wire).Wires);
                }
                else
                {
                    targetWires.Add(child.Wire);
                }

                //マーキング配線の場合パネルにぶつかる前に切れることがあるので全部見る
                foreach (var targetWire in targetWires)
                {
                    var panel = GetNearestCeilingPanel(child, targetWire, ceilingPanels);
                    if (panel != null)
                        return panel; //複数配線のときに別々のパネルがみつかることがあるが、区別できないので最初に見つけた方を採用する
                }
            }
            return null;
        }

        private CeilingPanel GetNearestCeilingPanel(Symbol symbol, List<CeilingPanel> ceilingPanels)
        {
            var targetWires = new List<Wire>();
            if (symbol.Wire is RisingWire)
            {
                var risingWire = (RisingWire)symbol.Wire;
                foreach (var wire in risingWire.Wires)
                {
                    if (wire.Floor != symbol.Floor)
                        continue;

                    if (wire is MarkingWire)
                        targetWires.AddRange(((MarkingWire)wire).Wires);
                    else
                        targetWires.Add(wire);
                }
            }
            else if (symbol.Wire is MarkingWire)
            {
                targetWires.AddRange(((MarkingWire)symbol.Wire).Wires);
            }
            else
            {
                targetWires.Add(symbol.Wire);
            }

            //マーキング配線の場合パネルにぶつかる前に切れることがあるので全部見る
            foreach (var targetWire in targetWires)
            {
                var panel = GetNearestCeilingPanel(symbol, targetWire, ceilingPanels);
                if (panel != null)
                    return panel;
            }

            return null;
        }

        private CeilingPanel GetNearestCeilingPanel(Symbol symbol, Wire targetWire, List<CeilingPanel> ceilingPanels)
        {
            var candidatePanels = ceilingPanels.FindAll(p => p.Floor == symbol.Floor && targetWire.IsOn(p));
            if (candidatePanels.Count == 0)
                return null;

            var startParam = AutoCad.Db.Curve.GetParameter(targetWire.ObjectId, targetWire.StartPoint);
            var endParam = AutoCad.Db.Curve.GetParameter(targetWire.ObjectId, targetWire.EndPoint);

            //配線を子供側から辿って、最初に接触した天井パネルを返す。

            //配線のStartPointでシンボルに繋がる時。
            if (targetWire.StartPoint == symbol.WiredPoint)
            {
                for (double param = startParam; param <= endParam; param += 0.1d)
                {
                    var point = AutoCad.Db.Curve.GetPoint(targetWire.ObjectId, param);

                    foreach (var candidatePanel in candidatePanels)
                    {
                        if (candidatePanel.Contains(point))
                            return candidatePanel;
                    }
                }
            }

            //配線のEndPointでシンボルに繋がる時
            if (targetWire.EndPoint == symbol.WiredPoint)
            {
                for (double param = endParam; startParam <= param; param -= 0.1d)
                {
                    var point = AutoCad.Db.Curve.GetPoint(targetWire.ObjectId, param);

                    foreach (var candidatePanel in candidatePanels)
                    {
                        if (candidatePanel.Contains(point))
                            return candidatePanel;
                    }
                }
            }

            return null;
        }

        #endregion

        #region FillLength3D

        public decimal GetLength3DForSignalWire(Symbol symbol)
        {
            var lightEle = symbol.CreateClone();

            lightEle.IsLightElectrical = true;
            lightEle.Parent.IsLightElectrical = true;
            this.FillLength3D(lightEle);

            return lightEle.Wire.Length3D;
        }

        /// <summary>実際の配線長を求める。詳しくは配線長の求め方.xlsを参照</summary>
        /// WirePickingReportでSignalWireを計算するのでpublicに変更する
        private void FillLength3D(Symbol symbol)
        {
            foreach (var child in symbol.Children)
            {
                this.FillLength3D(child);
            }

            if (symbol.IsBreaker || symbol.IsTop || symbol.IsSolarWireTop)
            {
                if (!symbol.IsLightElectrical)
                    return;

                if (symbol.Parent == null)
                    return;
            }

            decimal length = 0;

            //AutoCAD上のStart&Endを上流がStartになるように並べる
            symbol.Wire = this.SetReplaseWireFlag(symbol, symbol.Wire);

            //壁内配線の優先度は一番上
            if (symbol.HasWireInWall || symbol.withOrangeMarking)
            {
                this.FillLength3DInWall(symbol);
                symbol.Wire.CalculatedWires = symbol.Wire.GetAllChildrenWire();
                return;
            }

            //分割～サマリ～天井受け計算
            this.SetCalculatedWires(symbol);

            //天井から分電盤までの長さを加算する
            if (!symbol.Parent.IsJointBox || symbol.Parent.IsLightElectrical)
            {
                if (symbol.Wire is MarkingWire)
                    length += this.GetHeightFromDropPointToItemForMarkingWire(symbol.Wire as MarkingWire, true, false);
                else
                    length += this.GetLengthFromDropPointToItemForWire(symbol.Parent, true);
            }

            //図面上で描かれた長さを加算する
            length += symbol.Wire.Length2D;

            if (symbol.Wire is MarkingWire)
                length += this.GetHeightFromDropPointToItemForMarkingWire(symbol.Wire as MarkingWire, false, false);
            else
                //天井から器具までの長さを加算する（床下配線の水平配線分は除く）
                length += this.GetLengthFromDropPointToItemForWire(symbol, false);

            //天井パネルをまたぐ箇所にはコネクタを取り付ける。その際の余長。
            //弱電以外のシンボルと配管のシンボルだった場合にのみ余長を加算する。
            if (!symbol.IsLightElectrical || symbol.IsHaikan)
            {
                length += this.GetConnectorExtraLength(symbol);
            }

            //弱電かつ配管つきの場合天井受けを無視
            if (!(symbol.IsLightElectrical && symbol.IsHaikan))
            {
                //天井受け余長の追加
                length += this.GetCeilingPanelRecieverAllowLengthAll(symbol);
            }

            //終端接続部余長
            length += symbol.Parent.GetExtraWireLength();
            length += symbol.GetExtraWireLength();

            //階またぎ
            {
                //2F分電盤に1Fから接続している線は天井を通さない（=分電盤高さ＋子の天井懐1/2）
                if (symbol.Parent.IsBundenban && symbol.Parent.Floor > symbol.Floor)
                {
                    if (symbol.Floor == 1)
                        length += Static.HouseSpecs.CeilingDepth1F / 2;
                    else
                        length += Static.HouseSpecs.CeilingDepth2F / 2;
                    length += symbol.Parent.Height;
                }
                else
                {
                    //別階に配線する時は、別階に立ち上げる分の長さを足す
                    length += Math.Abs(this.GetCeilingDistanceFrom1FCeiling(symbol.Parent) - this.GetCeilingDistanceFrom1FCeiling(symbol));
                }
            }

            //床下余長
            if (symbol.Wire is MarkingWire)
            {
                MarkingWire markingWire = symbol.Wire as MarkingWire;
                if (markingWire.IsUnderfloor) 
                {
                    var uWires = markingWire.CalculatedWires.FindAll(p => p.IsUnderfloor);
                    foreach (var wire in uWires) 
                    {
                        length += Static.HouseSpecs.GetUnderFloorExtraLength(wire.Floor);
                    }
                }
            }
            else if (symbol.Wire != null && symbol.Wire.IsUnderfloor) //risingしたら二回床下には行かないと思うのでこっち
                length += Static.HouseSpecs.GetUnderFloorExtraLength(symbol.Floor);

            symbol.Wire.Length3D = length;
        }

        /// <summary>
        /// 壁内配線長計算
        /// </summary>
        /// <param name="symbol"></param>
        private void FillLength3DInWall(Symbol symbol)
        {
            foreach (var child in symbol.Children)
            {
                this.FillLength3D(child);
            }

            if (symbol.Parent == null)
                return;

            decimal length = 0;

            //床下配線の場合
            //＃天井設置部分が区別できるようになったら追加する
            if (symbol.Wire.IsUnderfloor)
            {
                length += symbol.Height;
                length += symbol.Parent.Height;
                length += Static.HouseSpecs.FloorThickness * 2;

                //床下のときだけ器具余長を足す
                length += symbol.Parent.GetExtraWireLength();
                length += symbol.GetExtraWireLength();

                //床下かつ階またぎ
                if (symbol.Floor != symbol.Parent.Floor)
                {
                    length += Static.HouseSpecs.GetCeilingHeight(1);
                    length += Static.HouseSpecs.CeilingDepth1F;
                    length += Static.HouseSpecs.CeilingThickness1F;
                }
            }
            //階またぎの場合
            else if (symbol.Floor != symbol.Parent.Floor)
            {
                Symbol underSymbol, upperSymbol;
                if (symbol.Floor > symbol.Parent.Floor)
                {
                    upperSymbol = symbol;
                    underSymbol = symbol.Parent;
                }
                else
                {
                    upperSymbol = symbol.Parent;
                    underSymbol = symbol;
                }

                length += upperSymbol.Height;
                length += Static.HouseSpecs.GetCeilingHeight(symbol.Floor) - symbol.Height;
                if (underSymbol.Floor == 1 || upperSymbol.Parent.Floor == 1)
                {
                    length += Static.HouseSpecs.CeilingDepth1F;
                    length += Static.HouseSpecs.CeilingThickness1F;
                }
                else
                {
                    length += Static.HouseSpecs.CeilingDepth2F;
                    length += Static.HouseSpecs.CeilingThickness2F;
                }
            }
            else
            {
                length += Math.Abs(symbol.Height - symbol.Parent.Height);
            }

            //図面上の長さ
            length += symbol.Wire.Length2D;
            //固定余長
            length += this.GetAllowanceInWall(symbol);

            symbol.Wire.Length3D = length;
        }

        /// <summary>
        /// 壁内配線固定余長取得
        /// </summary>
        /// <param name="symbol"></param>
        public decimal GetAllowanceInWall(Symbol symbol)
        {
            if (symbol.Parent.IsSolarWireTop || symbol.withOrangeMarking)
            {
                return decimal.Zero;
            }
            else if (symbol.IsHaikan)
            {
                return 100 * 2;
            }
            else
            {
                return 200 * 2;
            }
        }

        /// <summary>
        /// JBOX配線用配線長計算
        /// </summary>
        /// <param name="symbol"></param>
        private void FillLength3DForJBOX(Symbol symbol)
        {
            decimal length = 0;

            //矩形->シンボルまでの長さ
            length += Static.HouseSpecs.GetCeilingHeight(symbol.Parent.Floor) - symbol.Parent.Height;
            length += Static.HouseSpecs.GetCeilingHeight(symbol.Floor) - symbol.Height;

            //床下余長を矩形で判定する
            if (symbol.Wire != null && symbol.Wire.IsUnderfloor)
            {
                length += Static.HouseSpecs.GetUnderFloorExtraLengthJbox(symbol.Floor);
            }

            //別階へのRisingWireのときコネクタ分を足す
            if (symbol.Wire is RisingWire)
            {
                RisingWire parentWire = (RisingWire)symbol.Wire;
                if (parentWire.Wires.Count > 1)
                {
                    var prevWire = parentWire.Wires[0];
                    for (var i = 1; i < parentWire.Wires.Count; i++)
                    {
                        if (prevWire.Floor != parentWire.Wires[i].Floor)
                        {
                            if (Static.HouseSpecs.Kanabakari == Const.Kanabakari._265)
                                length += Static.HouseSpecs.CeilingHeight_2F + Const.CeilingDepth_OverStepFloor._511;
                            else if (Static.HouseSpecs.Kanabakari == Const.Kanabakari._260 ||
                                Static.HouseSpecs.Kanabakari == Const.Kanabakari._240)
                                length += Static.HouseSpecs.CeilingHeight_2F + Const.CeilingDepth_OverStepFloor._337;
                            else
                                throw new ApplicationException(Messages.InvalidKanabakari(Static.HouseSpecs.Kanabakari));
                        }
                        prevWire = parentWire.Wires[i];
                    }
                }
            }

            //図面上の長さ
            length += symbol.Wire.Length2D;
            //シンボル接続部
            length += 300 * 2;
            //固定余長
            length += 100 * 2;

            symbol.Wire.Length3D = length;
        }

        /// <summary>天井から器具までの配線長を取得する。床下配線の床下分は含まない。</summary>
        public decimal GetLengthFromDropPointToItemForWire(Symbol symbol, bool isParent)
        {

            if (symbol.IsJointBox) //ジョイントボックスは天井パネル上に取り付けるので0
                return decimal.Zero;

            if (symbol.HasWireInWall && !symbol.withOrangeMarking) //壁内配線は値が取れないので0
                return decimal.Zero;

            decimal length = decimal.Zero;

            if (symbol.Wire != null && symbol.Wire.IsUnderfloor)
            {
                //床下配線だった場合は、設備の高さのみを加算する。
                length += symbol.Height;
            }
            else
            {
                //それ以外の場合は、天井から器具までの長さを加算する。
                length += Static.HouseSpecs.GetCeilingHeight(symbol.Floor) - symbol.Height;
            }

            //DLtoDLの場合は、天井懐と天井板の厚みは計算しない
            if (isParent)
            {
                foreach (var child in symbol.Children)
                {
                    if (child.IsLightToLight)
                        return length;
                }
            }
            else
            {
                if (symbol.IsLightToLight)
                    return length;
            }

            //JBtoDLだった場合は天井懐と天井厚みは計算しない
            if (symbol.IsJointBoxToLight)
                return length;

            //天井懐の半分を加算する
            //天井板の厚みを加算する
            if (symbol.Floor == 1)
            {
                length += Static.HouseSpecs.CeilingDepth1F / 2;
                length += Static.HouseSpecs.CeilingThickness1F;
            }
            else
            {
                length += Static.HouseSpecs.CeilingDepth2F / 2;
                length += Static.HouseSpecs.CeilingThickness2F;
            }
            return length;
        }

        private decimal GetConnectorExtraLength(Symbol symbol)
        {
            Wire wire = symbol.Wire;

            if (wire is MarkingWire)
                return GetConnectorExtraLengthForCombineWire(symbol);

            int connectorCount = -1; //通るパネル数-1が必要なコネクタの数
            foreach (var panel in this.CeilingPanels)
            {
                if (wire.IsOn(panel))
                    connectorCount++;
            }

            if (!symbol.IsHaikan && connectorCount >= 1)
                return Static.HouseSpecs.ConnectorExtraLength * connectorCount;

            return 0;
        }

        /// <summary>1Fの天井懐の中心の高さを0とし、シンボルのある階の天井懐の中心の高さを返す</summary>
        public decimal GetCeilingDistanceFrom1FCeiling(Symbol symbol)
        {
            decimal f1 = 0;
            decimal f2 = 0;
            if (Static.HouseSpecs.Kanabakari == Const.Kanabakari._240 ||
                Static.HouseSpecs.Kanabakari == Const.Kanabakari._260)
                f2 = Static.HouseSpecs.CeilingHeight_2F + Const.CeilingDepth_OverStepFloor._337;
            else if (Static.HouseSpecs.Kanabakari == Const.Kanabakari._265)
                f2 = Static.HouseSpecs.CeilingHeight_2F + Const.CeilingDepth_OverStepFloor._511;
            else
                f2 = f1 + (Static.HouseSpecs.CeilingDepth1F / 2) + Static.HouseSpecs.CeilingHeight_2F + Static.HouseSpecs.CeilingThickness2F + (Static.HouseSpecs.CeilingDepth2F / 2);

            decimal f3 = f2 + (Static.HouseSpecs.CeilingDepth2F / 2) + Static.HouseSpecs.CeilingHeight_2F + Static.HouseSpecs.CeilingThickness2F + (Static.HouseSpecs.CeilingDepth2F / 2);

            if (symbol.Floor == 1)
                return f1;
            else if (symbol.Floor == 2)
                return f2;
            else
                return f3;
        }

        #endregion

        #region SortSymbols

        /// <summary>
        /// かってにスイッチ親に接続している子の順番の番号を返す
        /// </summary>
        private int GetKatteniSwitchChildIndex(Symbol target, List<Symbol> children)
        {
            if (this.DenkiWires.Count == 0)
                return 0;

            if (!target.IsKatteniSwitchChild)
                return 0;

            var parent = children.Find(p => p.IsKatteniSwitchParent);
            if (parent == null)
                return 0;

            List<Symbol> others = new List<Symbol>();
            others.AddRange(children);
            others.Remove(parent);

            var ret = GetChildSymbolIndex(target, parent, others, 0);
            return ret;
        }

        /// <summary>
        /// DenkiWireで繋がれたParentとTarget間のシンボル数を返す
        /// </summary>
        private int GetChildSymbolIndex(Symbol target, Symbol parent, List<Symbol> children, int count)
        {
            count++;
            if (this.DenkiWires.Count == 0)
                return 0;

            var parentWires = this.DenkiWires.FindAll(p => p.IsConnected(parent));
            foreach (var wire in parentWires)
            {
                if (wire.IsConnected(target))
                    return count;
                else
                {
                    var newParent = children.Find(p => wire.IsConnected(p));
                    if (newParent != null)
                    {
                        children.Remove(newParent);
                        var ret = GetChildSymbolIndex(newParent, target, children, count);
                        if (ret > 0)
                            return ret;
                    }
                }
            }
            return 0;
        }

        private void SortSymbols(Symbol parent, PointD entrancePoint)
        {
            foreach (var child in parent.Children)
            {
                this.SortSymbols(child, entrancePoint);
            }

            parent.Children.Sort((p, q) =>
            {
                if (p.IsJointBox && !p.IsSubJointBox && q.IsJointBox && !q.IsSubJointBox)
                {
                    return p.SequenceNo.CompareTo(q.SequenceNo);
                }

                //弱電同士のとき、取出しを後ろにまとめる
                if (p.IsLightElectrical && q.IsLightElectrical) 
                {
                    var wireNoP = p.Attributes.Find(a => a.Tag == Const.AttributeTag.WIRE_NO);
                    var wireNoQ = q.Attributes.Find(b => b.Tag == Const.AttributeTag.WIRE_NO);
                    if (wireNoP != null && wireNoQ != null) 
                    {
                        return wireNoP.Value.CompareTo(wireNoQ.Value);
                    }
                }

                if (p.GroupNo != q.GroupNo)
                    return p.GroupNo.CompareTo(q.GroupNo);

                if (p.IsSwitch != q.IsSwitch)
                    return q.IsSwitch.CompareTo(p.IsSwitch);

                //スイッチ同士の接続ではサブグループを無視する
                if (!p.IsSwitch || !q.IsSwitch)
                {
                    if (p.SubGroupNo != q.SubGroupNo)
                        return p.SubGroupNo.CompareTo(q.SubGroupNo);

                    if (p.SubGroupSeq != q.SubGroupSeq)
                        return (p.SubGroupSeq).CompareTo(q.SubGroupSeq);
                }

                if (p.IsAketaraSwitch != q.IsAketaraSwitch)
                {
                    if (p.Is3WayOr4WaySwitchConnection != q.Is3WayOr4WaySwitchConnection && (p.IsAketaraSwitch2nd3Way || q.IsAketaraSwitch2nd3Way))
                        return q.Is3WayOr4WaySwitchConnection.CompareTo(p.Is3WayOr4WaySwitchConnection);

                    return q.IsAketaraSwitch.CompareTo(p.IsAketaraSwitch);
                }
                else if (p.IsTottaraRimokon != q.IsTottaraRimokon)
                {
                    if (p.Is3WayOr4WaySwitchConnection != q.Is3WayOr4WaySwitchConnection && (p.IsTottaraRimokon2nd3Way || q.IsTottaraRimokon2nd3Way))
                        return q.Is3WayOr4WaySwitchConnection.CompareTo(p.Is3WayOr4WaySwitchConnection);

                    return q.IsTottaraRimokon.CompareTo(p.IsTottaraRimokon);
                }
                else if (p.IsLEDLightCon != q.IsLEDLightCon)
                {
                    //ライコンのCASE2で複数スイッチがあったら常にライコンが後ろ
                    if (p.IsSwitch && q.IsSwitch && (p.IsLEDLightConCase2 || q.IsLEDLightConCase2))
                        return (!q.IsLEDLightConCase2).CompareTo(!p.IsLEDLightConCase2);

                    if (p.Is3WayOr4WaySwitchConnection != q.Is3WayOr4WaySwitchConnection && (p.IsLEDLightCon2nd3Way || q.IsLEDLightCon2nd3Way))
                        return q.Is3WayOr4WaySwitchConnection.CompareTo(p.Is3WayOr4WaySwitchConnection);

                    return q.IsLEDLightCon.CompareTo(p.IsLEDLightCon);
                }
                else if (p.IsKatteniSwitch && q.IsKatteniSwitch) //かってにスイッチは順序がややこしい
                {
                    //子が複数ある場合は親に近い順に並べる
                    if (p.IsKatteniSwitchChild && q.IsKatteniSwitchChild)
                        return GetKatteniSwitchChildIndex(p, parent.Children).CompareTo(GetKatteniSwitchChildIndex(q, parent.Children));

                    //シリアル判別の特殊パターン
                    if (p.WithKatteniSwitchSpecialSerial && q.WithKatteniSwitchSpecialSerial)
                    {
                        //親機→子機→操作ユニットの順にする
                        if (p.IsKatteniSwitchParent != q.IsKatteniSwitchParent)
                            return q.IsKatteniSwitchParent.CompareTo(p.IsKatteniSwitchParent);

                        if (p.IsKatteniSwitchChild != q.IsKatteniSwitchChild)
                            return q.IsKatteniSwitchChild.CompareTo(p.IsKatteniSwitchChild);
                    }

                    if (p.WithKatteniSwitchControlUnit && q.WithKatteniSwitchControlUnit)
                    { //操作ユニットがある時は、子機→親機→操作ユニットの順にする

                        //操作ユニットかつトイレ用がある場合は先頭に置く
                        if (p.IsKatteniSwitchToilet != q.IsKatteniSwitchToilet)
                            return q.IsKatteniSwitchToilet.CompareTo(p.IsKatteniSwitchToilet);

                        if (p.IsKatteniSwitchChild != q.IsKatteniSwitchChild)
                            return q.IsKatteniSwitchChild.CompareTo(p.IsKatteniSwitchChild);

                        if (p.IsKatteniSwitchParent != q.IsKatteniSwitchParent)
                            return q.IsKatteniSwitchParent.CompareTo(p.IsKatteniSwitchParent);
                    }
                    else
                    { //通常は、親機→子機
                        if (p.IsKatteniSwitchParent != q.IsKatteniSwitchParent)
                            return q.IsKatteniSwitchParent.CompareTo(p.IsKatteniSwitchParent);
                    }
                }
                else if (p.IsLightControlRotarySwitch || q.IsLightControlRotarySwitch)
                {
                    if (p.Is3WaySwitch && q.Is3WaySwitch)
                        return p.IsLightControlRotarySwitch.CompareTo(q.IsLightControlRotarySwitch);
                }

                if (p.IsSwitch && q.IsSwitch && p.IsKatteniSwitch != q.IsKatteniSwitch)
                {
                    return p.IsKatteniSwitch.CompareTo(q.IsKatteniSwitch);
                }

                if (p.WithAketaraSubSwitch || q.WithAketaraSubSwitch)
                {
                    if (p.IsAketaraSubSwitch != q.IsAketaraSubSwitch)
                        return (p.IsAketaraSubSwitch).CompareTo(q.IsAketaraSubSwitch);
                    //あけたらタイマーのサブスイッチは品番で並べ替える
                    if (p.IsAketaraSubSwitch && q.IsAketaraSubSwitch)
                        return (!p.Attributes.Exists(a => a.Value.StartsWith("WTC"))).CompareTo(!q.Attributes.Exists(b => b.Value.StartsWith("WTC")));
                }

                if (p.Floor != q.Floor)
                    return p.Floor.CompareTo(q.Floor); //下階が先

                if (p.WithOnOffDisplay)
                { //入切表示スイッチは照明が先
                    if (p.IsLight != q.IsLight)
                        return q.IsLight.CompareTo(p.IsLight);
                }

                var pDistance = Calc.GetDistance(p.ActualPosition, entrancePoint);
                var qDistance = Calc.GetDistance(q.ActualPosition, entrancePoint);

                return pDistance.CompareTo(qDistance); //玄関に近い方が先
            });

            if (!parent.Children.Exists(p => p.Is4WaySwitch))
                return;

            //4路スイッチが使われている場合は、3-4-3、3-4-4-3のようにスイッチを並び替える。
            for (int i = 1; i <= parent.GetMaxGroupNo(); i++)
            {
                var switch4Ways = parent.Children.FindAll(p => p.GroupNo == i && p.Is4WaySwitch);
                if (switch4Ways.Count == 0)
                    continue;

                //4路スイッチと結線される場合、あけたらタイマーを3路と見なす
                var switch3Ways = parent.Children.FindAll(p => p.GroupNo == i && p.IsAketaraSwitch);
                switch3Ways.AddRange(parent.Children.FindAll(p => p.GroupNo == i && p.Is3WaySwitch && !p.IsAketaraSwitch));
                if (switch3Ways.Count <= 1)
                    throw new ApplicationException(Messages.Invalid4WaySwitch());

                if (switch3Ways.Exists(p => p.IsAketaraSwitch2nd3Way))
                    switch3Ways.Sort((p, q) => (p.IsAketaraSwitch2nd3Way).CompareTo(q.IsAketaraSwitch2nd3Way));

                var newOrderSymbols = new List<Symbol>();
                newOrderSymbols.Add(switch3Ways[0]);
                newOrderSymbols.AddRange(switch4Ways);
                newOrderSymbols.Add(switch3Ways[1]);

                int index = parent.Children.FindIndex(p => p.GroupNo == i);

                parent.Children.RemoveRange(index, newOrderSymbols.Count);
                parent.Children.InsertRange(index, newOrderSymbols);
            }
        }

        #endregion

        private void ValidateWiredSymbols()
        {
            Validation.ValidateNoWireJointBox(this.Symbols);
            Validation.ValidateUnconnectedWire(this.Wires, this.Symbols);
            Validation.ValidateUnconnectedWire(this.DenkiWires, this.Symbols);
            Validation.ValidateUnconnectedSymbol(this.Symbols);
            Validation.ValidateWireNotOnCeilingPanel(this.Symbols);
            Validation.ValidateEvBadConnection(this.Symbols);
            Validation.ValidateRoofHeaterBadConnection(this.Symbols);
            Validation.ValidateSingleSwitchOrLight(this.Symbols);
        }

        #endregion

        /// <summary>SymbolsとdenkiWiresを取得した上で呼び出してください</summary>
        public List<List<Symbol>> GetGroupedSymbols(out List<Wire> unconnectedWires)
        {
            var symbols = new List<Symbol>(this.Symbols);
            var groupedSymbols = new List<List<Symbol>>();
            var wires = new List<Wire>(this.DenkiWires);

            symbols.RemoveAll(p => !p.IsConnectableForLightElectrical);

            while (0 < symbols.Count)
            {
                var currentSymbol = symbols[0];

                var sameGroupSymbols = new List<Symbol>();
                sameGroupSymbols.Add(currentSymbol);
                symbols.Remove(currentSymbol);

                sameGroupSymbols.AddRange(this.FindChainedGroupedSymbols(currentSymbol, ref symbols, ref wires));

                groupedSymbols.Add(sameGroupSymbols);
            }

            unconnectedWires = wires;

            return groupedSymbols;
        }

        /// <summary>配線の繋がっているシンボルを芋づる式に探して全てゲットする</summary>
        public virtual List<Symbol> FindChainedGroupedSymbols(Symbol currentSymbol, ref List<Symbol> symbols, ref List<Wire> wires)
        {
            var groupSymbols = new List<Symbol>();
            var connectedWires = new List<Wire>();
            foreach (var wire in wires)
            {
                if (!wire.IsConnected(currentSymbol))
                    continue;

                var connectedSymbol = symbols.Find(p => wire.IsConnected(p));
                if (connectedSymbol == null)
                    continue;

                groupSymbols.Add(connectedSymbol);
                connectedWires.Add(wire);
            }

            //判定が終わったシンボルと配線を排除する
            foreach (var groupSymbol in groupSymbols)
            {
                symbols.Remove(groupSymbol);
            }

            foreach (var connectedWire in connectedWires)
            {
                wires.Remove(connectedWire);
            }

            var chainedSymbols = new List<Symbol>();
            foreach (var groupSymbol in groupSymbols)
            {
                //再帰呼び出し
                chainedSymbols.AddRange(this.FindChainedGroupedSymbols(groupSymbol, ref symbols, ref wires));
            }

            groupSymbols.AddRange(chainedSymbols);

            return groupSymbols;
        }

        public List<Light> GetLights()
        {
            //対象を絞る
            var symbols = this.Symbols.FindAll(p => p.IsLight &&
                                                    p.Equipment.CustomAction照明品番入力);

            //扱いやすいクラスに入れ替える
            List<Light> lights = new List<Light>();
            symbols.ForEach(p => lights.Add(new Light(p)));

            //出力順に並び変える（照明番号の昇順）LightNoでソートするとA1→A10→A2→A3になるからダメ
            lights.Sort((p, q) =>
            {
                if (p.RoomCode != q.RoomCode)
                    return p.RoomCode.CompareTo(q.RoomCode);

                if (p.SeqNo != q.SeqNo)
                    return p.SeqNo.CompareTo(q.SeqNo);

                return p.Hinban.CompareTo(q.Hinban);
            });

            return lights;
        }

        /// <summary>図面上にあるコメントのうち、マスタに登録されているコメントを一覧にして返す</summary>
        public List<string> FindAllText()
        {
            var texts = new List<string>();
            texts.AddRange(this.GetSimpleTexts());
            texts.AddRange(this.GetSymbolTexts());

            var splitTexts = this.GetSplitTexts(texts);

            var appearTexts = this.FindTextsForCsv(splitTexts);

            return appearTexts;
        }

        /// <summary>「ほげx3」のように数量が含まれるコメントを「ほげ」3つに分解する</summary>
        private List<string> GetSplitTexts(List<string> texts)
        {
            var splitTexts = new List<string>();
            texts.ForEach(text =>
            {
                splitTexts.AddRange(TextObject.GetCountedText(text));
            });
            return splitTexts;
        }

        /// <summary>単独のコメントを一覧にして返す</summary>
        protected virtual List<string> GetSimpleTexts()
        {
            var texts = new List<string>();
            this.Texts.ForEach(text => texts.Add(text.Text));
            return texts;
        }

        /// <summary>シンボルに付属するコメントを一覧にして返す</summary>
        protected virtual List<string> GetSymbolTexts()
        {
            var texts = new List<string>();
            this.Symbols.ForEach(symbol =>
            {
                symbol.OtherAttributes.ForEach(att =>
                {
                    if (AutoCad.Db.Attribute.GetVisible(att.ObjectId))
                        texts.Add(att.Value); //シンボルコメント一覧
                });
            });
            return texts;
        }

        /// <summary>マスタでAppearInCsvにチェックが入っているコメントのみに絞る</summary>
        private List<string> FindTextsForCsv(List<string> texts)
        {
            var appearTexts = new List<string>();
            texts.ForEach(text =>
            {
                if (UnitWiring.Masters.Comments.Exists(com => com.Text == text && com.AppearInCsv))
                    appearTexts.Add(text);
            });
            return appearTexts;
        }

        private bool HasFlag(CadObjectTypes flag)
        {
            return (this.objectTypes & flag) == flag;
        }

        private void UnlockLayers()
        {
            //レイヤがロックされていると取得時にエラーになる
            var layerIds = AutoCad.Db.LayerTable.GetLayerIds();
            foreach (var layerId in layerIds)
            {
                if (AutoCad.Db.LayerTableRecord.IsLocked(layerId))
                    AutoCad.Db.LayerTableRecord.SetLock(layerId, false);
            }
        }

        public List<Plate> GetLightControlPlates()
        {
            var lightControlPlates = new List<Plate>();
            foreach (var plate in this.Plates)
            {
                var switches = plate.Symbols.FindAll(p => p.IsSwitch);
                if (switches.Count == 0)
                    continue;

                //全てのスイッチがNQで始まる品番を持っていたら、このプレートはライトコントロールとみなす。
                if (!switches.TrueForAll(p => p.StartWithComment(Const.Text.LIGHT_CONTROL_SERIAL_PREFIX)))
                    continue;

                lightControlPlates.Add(plate);
            }

            return lightControlPlates;
        }

        public void PurgeLightElectric()
        {
            var breakers = this.Symbols.FindAll(p => p.IsBreaker && !p.IsLightElectrical);
            if (breakers.Count == 0)
                return;

            foreach (var breaker in breakers)
            {
                this.RemoveLightElectricChild(breaker);
            }
        }

        private void RemoveLightElectricChild(Symbol parent)
        {
            parent.Children.RemoveAll(p => p.IsLightElectrical && p.Parent.IsLightElectrical);
            foreach (var child in parent.Children)
            {
                this.RemoveLightElectricChild(child);
            }
        }

        /// <summary>
        /// WireをMarkingWireに置き換える
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="wires"></param>
        public void ReplaceMarkingWire(List<Symbol> symbols, List<Wire> wires)
        {
            List<Symbol> markings = symbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.MarkingBule ||
                                                         p.Equipment.Name == Const.EquipmentName.MarkingGreen ||
                                                         p.Equipment.Name == Const.EquipmentName.MarkingYellow);
            
            //マーキング作成前のエラー処理
            this.ValidatePreMarkingSymbol(markings, wires);

            List<MarkingWire> markingWires = MarkingWire.CreateMarkingWire(wires, markings);

            Validation.ValidateOrangeMarkingOthers(markingWires);//MarkingWireとオレンジの混在は認めない

            foreach (MarkingWire markingWire in markingWires)
            {
                markingWire.Wires.ForEach(a => wires.Remove(a));
                wires.Add(markingWire);
            }
        }

        /// <summary>
        /// オレンジマーキングをつける
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="wires"></param>
        public void SetOrangeMarkingWire(List<Symbol> symbols, List<Wire> wires)
        {
            List<Symbol> orangeMarkings = symbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.MarkingOrange);

            Validation.ValidateOrangeMarkingWire(wires, orangeMarkings);

            foreach (Symbol orangeMarking in orangeMarkings)
            {
                //マーキングをまたぐワイヤー持ってくる
                List<Wire> penetrateWires = wires.FindAll(p => p.IsCrossover(orangeMarking));

                foreach (Wire penetrateWire in penetrateWires)
                    penetrateWire.withOrangeMarking = true; //オレンジはフラグを立てるだけ
            }
        }

        /// <summary>
        /// マーキング作成前のチェック処理
        /// </summary>
        private void ValidatePreMarkingSymbol(List<Symbol> markingSymbol, List<Wire> wires)
        {
            Validation.ValidateUnconnectedMarkingSymbol(markingSymbol, wires);
            Validation.ValidateConnectedOnlyOneMarkingWire(markingSymbol, wires);
            Validation.ValidateConnectedThreeOrMoreMarkingWire(markingSymbol, wires);
            Validation.ValidateConnectedMarkingWireLayer(markingSymbol, wires);

            Validation.ValidateUnderFloorWireCombine(markingSymbol, wires);
            Validation.ValidateSameWireCombine(markingSymbol, wires);
        }

        /// <summary>
        /// CombineWire用。コネクタの余長を取得する。
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private decimal GetConnectorExtraLengthForCombineWire(Symbol symbol)
        {
            MarkingWire markingWire = symbol.Wire as MarkingWire;
            int connectorCount = -1; //通るパネル数-1が必要なコネクタの数

            foreach (Wire wire in markingWire.Wires)
            {
                //床下分のコネクタの余長は考慮しない。
                if (wire.IsUnderfloor)
                    continue;

                //親シンボル⇔黄色または緑色マーキングのWireは天井設置部分がないので、コネクタの余長はカウントしない
                if (wire == markingWire.ParentSymbolToMarkingWire && markingWire.ParentMarking != null && !markingWire.ParentMarking.IsBuleMarking)
                    continue;

                //黄色または緑色マーキング⇔子シンボルのWireは天井設置部分がないので、コネクタの余長はカウントしない
                if (wire == markingWire.ChildSymbolToMarkingWire && !markingWire.ChildMarking.IsBuleMarking)
                    continue;

                foreach (CeilingPanel panel in this.CeilingPanels)
                {
                    if (wire.IsOn(panel))
                        connectorCount++;
                }
            }

            if (!symbol.IsHaikan && connectorCount >= 1)
                return Static.HouseSpecs.ConnectorExtraLength * connectorCount;

            return 0;
        }

        /// <summary>
        /// CadObjectContainerのIsShortOnceilingPanleをCombineWire用にメソッドを分けた
        ///   見つけた天井パネル上に配線が500mm以下しか描かれていなかったら、
        ///   実際に器具を取り付ける天井パネルは、一つ手前の天井パネルとする、というルールがある。
        ///   500mm以下だったらtrue。
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private bool IsShortOnCeilingPanelForCombineWire(Symbol symbol)
        {
            MarkingWire combine = symbol.Wire as MarkingWire;
            Dictionary<Wire, double> lengthOnPanels = new Dictionary<Wire, double>();
            double totalLengthOnPanel = 0;
            foreach (Wire w in combine.Wires)
            {
                if (w.IsOn(symbol.CeilingPanel))
                {
                    double lengthOnPanel = Calc.GetLengthInsideOutline(w.ObjectId, symbol.CeilingPanel.ObjectId);
                    lengthOnPanels.Add(w, lengthOnPanel);
                    totalLengthOnPanel += lengthOnPanel;
                }
            }

            if (totalLengthOnPanel <= Const.INGORE_LENGTH)
                return true;

            return false;
        }

        //複数ワイヤーの整列とマーキング情報のセットをする
        private void SetAlignmentMultiWires(Symbol parentSymbol)
        {
            foreach (Symbol childSymbol in parentSymbol.Children)
            {
                if (childSymbol.Equipment.CanHaveChild)
                    this.SetAlignmentMultiWires(childSymbol);//孫接続の可能性があれば再帰処理

                if (childSymbol.Wire is RisingWire)
                {
                    var rWire = (RisingWire)childSymbol.Wire;

                    if (!rWire.Wires[0].IsConnected(childSymbol.Parent))
                        rWire.Wires.Reverse();

                    foreach (var risingChild in rWire.Wires)
                    {
                        if (risingChild is MarkingWire)
                        {
                            MarkingWire mWire = (MarkingWire)risingChild;

                            //RisingのMarkingだと片側がnullになるので子にGetWiredで両端が入らない、のでここでやる
                            this.FillRisingMarkingChild(childSymbol.Wire, ref mWire);
                            mWire.FillMarkingProperty(this.CeilingPanels);
                        }
                    }
                }
                else if (childSymbol.Wire is MarkingWire)
                {
                    MarkingWire mWire = (MarkingWire)childSymbol.Wire;
                    mWire.FillMarkingProperty(this.CeilingPanels);
                }
            }
        }

        private void FillRisingMarkingChild(Wire parent, ref MarkingWire markingWire) 
        {
            var parentSymbol = parent.ParentSymbol;
            var childSymbol = parent.ChildSymbol;
            foreach (var wire in markingWire.Wires) 
            {
                if (wire.IsConnected(parentSymbol) && wire.Floor == parentSymbol.Floor) 
                {
                    wire.ParentSymbol = parentSymbol;
                    markingWire.ParentSymbol = parentSymbol;
                }

                if (wire.IsConnected(childSymbol) && wire.Floor == childSymbol.Floor) 
                {
                    wire.ChildSymbol = childSymbol;
                    markingWire.ChildSymbol = childSymbol;
                }
            }
        }

        /// <summary>
        /// MarkingWire用
        /// 天井懐・天井厚みを含むドロップポイントからシンボルまでの高さを返却する。
        /// </summary>
        /// <param name="markingWire">マーキングによって結合されたWire</param>
        /// <param name="isParent">親側の計算か否か</param>
        /// <param name="withKoyaura">小屋裏配線か否か</param>
        /// <returns></returns>
        private decimal GetHeightFromDropPointToItemForMarkingWire(MarkingWire markingWire, bool isParent, bool withKoyaura)
        {
            Symbol targetSymbol = null;
            Symbol targetMarking = null;
            Wire targetWire = null;

            if (isParent)
            {
                targetSymbol = markingWire.ParentSymbol;
                targetMarking = markingWire.ParentMarking;
                targetWire = markingWire.ParentSymbolToMarkingWire;
            }
            else
            {
                targetSymbol = markingWire.ChildSymbol;
                targetMarking = markingWire.ChildMarking;
                targetWire = markingWire.ChildSymbolToMarkingWire;
            }

            if (targetSymbol.IsJointBox)
                return decimal.Zero;

            if (targetSymbol.HasWireInWall)
                return decimal.Zero;

            decimal length = decimal.Zero;

            if (targetWire.IsUnderfloor)
                length += targetSymbol.Height;//床下の場合は設備の高さのみ
            else
            {

                if (withKoyaura && !isParent)
                {
                    length += Const.Koyaura.CeilingHeight - targetSymbol.Height;
                    //JBtoDLだった場合は天井懐は計算しない
                    if (!targetSymbol.IsJointBoxToLight)
                        length += Const.Koyaura.CeilingDepth;
                    return length;
                }

                length += Static.HouseSpecs.GetCeilingHeight(targetSymbol.Floor) - targetSymbol.Height;

                if (isParent)
                {
                    foreach (Symbol child in targetSymbol.Children)
                    {
                        if (child.IsLightToLight)
                            return length;
                    }
                }
                else
                {
                    if (targetSymbol.IsLightToLight)
                        return length;
                }

                if (targetSymbol.IsJointBoxToLight)
                    return length;


                //天井懐の半分を加算する
                //天井板の厚みを加算する
                if (targetSymbol.Floor == 1)
                {
                    length += Static.HouseSpecs.CeilingDepth1F / 2;
                    length += Static.HouseSpecs.CeilingThickness1F;
                }
                else
                {
                    length += Static.HouseSpecs.CeilingDepth2F / 2;
                    length += Static.HouseSpecs.CeilingThickness2F;
                }
            }

            return length;
        }


        #endregion
    }
}
