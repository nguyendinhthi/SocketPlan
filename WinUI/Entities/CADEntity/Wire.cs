using System;
using System.Collections.Generic;
using System.Text;
using SocketPlan.WinUI.SocketPlanServiceReference;
using Edsa.AutoCadProxy;
using SocketPlan.WinUI.Entities.CADEntity;

namespace SocketPlan.WinUI
{
    public partial class Wire
    {
        //配線の定義
        public WireItem Base { get; set; }

        //配線の情報
        public int ObjectId { get; set; }
        public PointD StartPoint { get; set; } //AutoCAD上での開始位置
        public PointD EndPoint { get; set; } //AutoCAD上での終了位置
        public string Layer { get; set; }
        public virtual decimal Length2D { get; set; }

        //DropPointToLength(仮運用)
        public Decimal DropPointLengthParent { get; set; }
        public Decimal DropPointLengthChild { get; set; }

        //天井受け＆天井受け余長
        public decimal CeilingReceiverAllowLength { get; set; }
        public List<CeilingReciever> CeilingRecievers { get; set; }

        public decimal Length3D { get; set; }
        public Symbol ParentSymbol { get; set; }
        public Symbol ChildSymbol { get; set; }
        public int Floor { get; set; }

        public virtual bool IsUnderfloor { get; set; }
        public int ConnectedSymbolObjectId { get; set; }
        
        #region WirePickingReportで使う系

        //マーキング位置からパネル下で伸ばす配線のとき（青マーキング）
        //天井懐+天井厚をDropPointToLengthとして出さない（パネル上と見做す）ので別に保存しとく
        public decimal MarkingCeilingAllowLength { get; set; }

        //ロール長を含む全長
        public decimal TotalLength2D
        {
            get
            {
                return this.Length2D + this.RolledLengthParent + this.RolledLengthChild + this.MarkingCeilingAllowLength;
            }
        }

        public bool IsFirst { get; set; }
        public bool IsLast { get; set; }
        public decimal RolledLengthParent { get; set; }
        public decimal RolledLengthChild { get; set; }
        public List<Wire> RolledWireParents { get; set; }
        public List<Wire> RolledWireChildren { get; set; }
        public List<Wire> CalculatedWires { get; set; } //分割計算後のワイヤーを格納

        public bool IsNotInstalledCeiling { get; set; } //天井パネルを通らない
        public CeilingPanel CeilingPanel { get; set; } //※分割後用
        public bool withKoyaura { get; set; } //小屋裏に繋がる配線
        public virtual bool withOrangeMarking { get; set; } //オレンジマーキング配線かどうか。1階にDropするが2階部分は天井を通さない
        public bool IsReversedWirePoint { get; set; } //ワイヤーが子側から始まっている。分割時に始点と終点を入れ替える必要がある
        public bool IsAvoidCalculation { get; set; } //計算対象外
        public bool IsInhibitRoll { get; set; } //巻き取り禁止フラグ
        #endregion

        public List<Wire> GetRolledWiresParentsAll()
        {
            var result = this.RolledWireParents;
            this.RolledWireParents.ForEach(p => result.AddRange(p.GetRolledWiresParentsAll()));
            return result;
        }

        public List<Wire> GetRolledWiresChildrenAll()
        {
            var result = this.RolledWireChildren;
            this.RolledWireChildren.ForEach(p => result.AddRange(p.GetRolledWiresChildrenAll()));
            return result;
        }

        /// <summary>配管用シンボルにつながっていたら配管あり </summary>
        public bool WithHaikan
        {
            get
            {
                List<Symbol> symbols = new List<Symbol>();
                if (this.ChildSymbol != null)
                    symbols.Add(this.ChildSymbol);
                if (this.ParentSymbol != null)
                    symbols.Add(this.ParentSymbol);

                foreach (var symbol in symbols)
                {
                    if (symbol.IsHaikan)
                        return true;
                }
                return false;
            }
        }

        public bool IsLightElectric
        {
            get
            {
                if (this.Base == null)
                    return false;

                return this.Base.Id == Const.WireItemId.弱電;
            }
        }

        public Wire()
        {
            this.CeilingReceiverAllowLength = 0;
            this.CeilingRecievers = new List<CeilingReciever>();
            this.RolledWireParents = new List<Wire>();
            this.RolledWireChildren = new List<Wire>();
            this.CalculatedWires = new List<Wire> { this };
        }

        public Wire(int polylineId)
            : this()
        {
            this.ObjectId = polylineId;
            this.StartPoint = AutoCad.Db.Polyline.GetStartPoint(polylineId);
            this.EndPoint = AutoCad.Db.Polyline.GetEndPoint(polylineId);
            this.Layer = AutoCad.Db.Entity.GetLayerName(polylineId);
            this.Length2D = Convert.ToDecimal(AutoCad.Db.Polyline.GetLength(polylineId));

            var extendedText = XData.Wire.GetUnderfloor(polylineId);
            this.IsUnderfloor = (extendedText == Const.UNDERFLOOR_WIRING);

            this.ConnectedSymbolObjectId = -1;
            var symbolHandleId = XData.Wire.GetConnectedSymbol(polylineId);
            if (!string.IsNullOrEmpty(symbolHandleId))
            {
                var symbolObjectId = AutoCad.Db.Utility.GetObjectId(symbolHandleId);
                if (symbolObjectId != 0)
                    this.ConnectedSymbolObjectId = symbolObjectId;
            }
        }

        public Wire(int polylineId, Wire wire)
            : this()
        {
            this.ObjectId = polylineId;
            this.StartPoint = AutoCad.Db.Polyline.GetStartPoint(polylineId);
            this.EndPoint = AutoCad.Db.Polyline.GetEndPoint(polylineId);
            this.Layer = AutoCad.Db.Entity.GetLayerName(polylineId);
            this.Length2D = Convert.ToDecimal(AutoCad.Db.Polyline.GetLength(polylineId));
            this.Floor = wire.Floor;
            this.IsNotInstalledCeiling = wire.IsNotInstalledCeiling;

            var extendedText = XData.Wire.GetUnderfloor(wire.ObjectId);
            this.IsUnderfloor = (extendedText == Const.UNDERFLOOR_WIRING);

            this.ConnectedSymbolObjectId = wire.ConnectedSymbolObjectId;
        }

        /// <summary>シンボルの基点に接続していたらtrue</summary>
        public virtual bool IsConnectedToArrowHead(Symbol symbol)
        {
            if (this.Floor != symbol.Floor)
                return false;

            if (this.StartPoint.EqualsRound(symbol.Position))
                return true;

            if (this.EndPoint.EqualsRound(symbol.Position))
                return true;

            return false;
        }

        /// <summary>図面上で配線の端がシンボル上にある時true</summary>
        public virtual bool IsConnected(Symbol symbol)
        {
            //接続するシンボルを明示的に指定した時はそれに従う
            if (this.ConnectedSymbolObjectId == symbol.ObjectId)
                return true;

            //それ以外の時は、図面上での接続を見て判定する

            if (this.Floor != symbol.Floor)
                return false;

            if (!symbol.Contains(this.StartPoint) &&
                !symbol.Contains(this.EndPoint))
            {
                //シンボルがクリップされていて実位置に接続されていなければ、クリップ位置を確認する
                if (symbol.Clipped)
                    return IsClippedPositionConnected(symbol);

                return false;
            }

            return true;
        }

        public virtual bool IsClippedPositionConnected(Symbol symbol)
        {
            //接続するシンボルを明示的に指定した時はそれに従う
            if (this.ConnectedSymbolObjectId == symbol.ObjectId)
                return true;

            //それ以外の時は、図面上での接続を見て判定する

            if (this.Floor != symbol.Floor)
                return false;

            if (!symbol.ContainsClippedPosition(this.StartPoint) &&
                !symbol.ContainsClippedPosition(this.EndPoint))
                return false;

            return true;
        }


        //線の点が全てPanelの上にあったら中にある判定
        public virtual bool IsIn(CeilingPanel panel)
        {
            if (this.Floor != panel.Floor)
                return false;

            var points = AutoCad.Db.Polyline.GetVertex(this.ObjectId);
            if (points.Count == 0)
                return false;

            foreach (var point in points)
            {
                if (!point.IsIn(panel.ObjectId))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 配線が天井パネル上を通る時true
        /// </summary>
        public virtual bool IsOn(CeilingPanel panel)
        {
            if (this.Floor != panel.Floor)
                return false;

            if (this.IsConnected(panel))
                return true;

            if (this.IsCrossover(panel))
                return true;

            return false;
        }

        /// <summary>
        /// 配線の端が天井パネル上にある時true
        /// </summary>
        public virtual bool IsConnected(CeilingPanel panel)
        {
            if (panel.Floor != this.Floor)
                return false;

            if (panel.Contains(this.StartPoint))
                return true;

            if (panel.Contains(this.EndPoint))
                return true;

            return false;
        }

        /// <summary>
        /// 配線が天井パネルの端をまたぐ時true
        /// </summary>
        public virtual bool IsCrossover(CeilingPanel panel)
        {
            if (this.Floor != panel.Floor)
                return false;

            if (!AutoCad.Db.Entity.IsIntersect(this.ObjectId, panel.ObjectId))
                return false;

            return true;
        }

        /// <summary>
        /// 配線がシンボルをまたぐ時true
        /// </summary>
        public virtual bool IsCrossover(Symbol symbol)
        {
            if (this.Floor != symbol.Floor)
                return false;

            if (!AutoCad.Db.Entity.IsIntersect(this.ObjectId, symbol.ObjectId))
                return false;

            return true;
        }

        public static List<Wire> GetAll(int floor)
        {
            var list = new List<Wire>();

            var wireIds = Filters.GetWireIds();
            foreach (var wireId in wireIds)
            {
                var wire = new Wire(wireId);
                if (wire.Length2D == 0)
                    continue;

                wire.Floor = floor;
                list.Add(wire);
            }

            return list;
        }

        public static List<Wire> GetSwitchWireAll(int floor)
        {
            var list = new List<Wire>();

            var wireIds = Filters.GetDenkiWireIds();
            foreach (var wireId in wireIds)
            {
                var wire = new Wire(wireId);
                if (wire.Length2D == 0)
                    continue;

                wire.Floor = floor;
                list.Add(wire);
            }

            return list;
        }

        public static List<Wire> GetKansenWireAll(int floor)
        {
            var list = new List<Wire>();

            var wireIds = Filters.GetKansenWireIds();
            foreach (var wireId in wireIds)
            {
                var wire = new Wire(wireId);
                if (wire.Length2D == 0)
                    continue;

                wire.Floor = floor;
                list.Add(wire);
            }

            return list;
        }

        public static List<Wire> GetSignalWireAll(int floor)
        {
            var list = new List<Wire>();

            var wireIds = Filters.GetSignalWireIds();
            foreach (var wireId in wireIds)
            {
                var wire = new Wire(wireId);
                if (wire.Length2D == 0)
                    continue;

                wire.Floor = floor;
                list.Add(wire);
            }

            return list;
        }

        public static List<Wire> GetJboxWireAll(int floor)
        {
            var list = new List<Wire>();

            var wireIds = Filters.GetJboxWireIds();
            foreach (var wireId in wireIds)
            {
                var wire = new Wire(wireId);
                if (wire.Length2D == 0)
                    continue;

                wire.Floor = floor;
                list.Add(wire);
            }

            return list;
        }

        private List<CeilingReciever> GetCrossingCeilingReciever(CeilingPanel panel)
        {
            var result = new List<CeilingReciever>();
            foreach (var ceilingReceiver in panel.CeilingReceivers)
            {
                var points = AutoCad.Db.Entity.GetIntersect2D(this.ObjectId, ceilingReceiver.ObjectId);

                //交点2つで1回とする
                int receiverCount = (int)Math.Ceiling((double)points.Count / 2);

                for (var i = 1; i <= receiverCount; i++)
                    result.Add(ceilingReceiver);
            }
            return result;
        }

        public void SetIntersectCeilingRecievers(CeilingPanel ceilingPanel)
        {
            if (ceilingPanel == null)
                return;

            this.CeilingRecievers.Clear();
            this.CeilingRecievers.AddRange(this.GetCrossingCeilingReciever(ceilingPanel));
        }

        public static void SetWireEdgeFlg(ref List<Wire> wires)
        {
            if (wires.Count == 0)
                throw new ApplicationException("処理する配線が見つかりません。");

            for (int i = 0; i < wires.Count; i++)
            {
                wires[i].IsFirst = false;
                wires[i].IsLast = false;

                if (i == 0)
                    wires[i].IsFirst = true;

                if (i == wires.Count - 1)
                    wires[i].IsLast = true;
            }
        }

        public List<Wire> GetAllChildrenWire()
        {
            var result = new List<Wire>();
            if (this is RisingWire)
            {
                foreach (var wire in ((RisingWire)this).Wires)
                {
                    if (wire is MarkingWire)
                        result.AddRange(((MarkingWire)wire).Wires);
                    else
                        result.Add(wire);
                }
            }
            else if (this is MarkingWire)
            {
                result.AddRange(((MarkingWire)this).Wires);
            }
            else
            {
                result.Add(this);
            }
            return result;
        }
    }
}
