using System;
using System.Collections.Generic;
using System.Text;
using SocketPlan.WinUI.Entities.CADEntity;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public class RisingWire : Wire
    {
        public RisingWire(List<Wire> wires)
        {
            this.wires.AddRange(wires);
        }

        private List<Wire> wires = new List<Wire>();
        public List<Wire> Wires
        {
            get { return this.wires; }
            set { this.wires = value; }
        }

        private List<Symbol> floorArrows = new List<Symbol>();
        public List<Symbol> FloorArrows
        {
            get { return this.floorArrows; }
            set { this.floorArrows = value; }
        }

        public string FloorLinkCode
        {
            get
            {
                if (this.FloorArrows.Count == 0)
                    return string.Empty;

                var linkCode = this.FloorArrows[0].Attributes.Find(p => p.Tag == Const.AttributeTag.LINK_CODE);
                if (linkCode == null)
                    return string.Empty;

                return linkCode.Value;
            }
        }

        public override decimal Length2D
        {
            get
            {
                decimal length = 0;
                foreach (var wire in this.Wires)
                {
                    length += wire.Length2D;
                }

                return length;
            }
        }

        public override bool withOrangeMarking
        {
            get
            {
                if (this.wires.Exists(p => p.withOrangeMarking))
                    return true;

                return false;
            }
        }

        /// <summary>
        /// 配線が床下配線だった場合true
        /// </summary>
        public override bool IsUnderfloor
        {
            get
            {
                foreach (var wire in this.wires)
                {
                    if (wire is MarkingWire)
                    {
                        if (((MarkingWire)wire).IsUnderfloor)
                            return true;
                    }
                    else if (wire.IsUnderfloor)
                        return true;
                }
                return false;
            }
        }

        public override bool IsConnected(Symbol symbol)
        {
            foreach (var wire in this.Wires)
            {
                if (wire is MarkingWire)
                {
                    if (((MarkingWire)wire).IsConnected(symbol))
                        return true;
                }
                else if (wire.IsConnected(symbol))
                    return true;
            }

            return false;
        }

        public bool IsConnectedConsideringClipped(Symbol symbol)
        {
            foreach (var wire in this.Wires)
            {
                if (wire is MarkingWire)
                {
                    if (((MarkingWire)wire).IsConnectedConsideringClipped(symbol))
                        return true;
                }
                else
                {
                    if (wire.IsConnected(symbol))
                        return true;

                    if (wire.IsClippedPositionConnected(symbol))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 配線が天井パネル上を通る時true
        /// </summary>
        public override bool IsOn(CeilingPanel panel)
        {
            foreach (var wire in this.Wires)
            {
                if (wire is MarkingWire)
                {
                    if (((MarkingWire)wire).IsOn(panel))
                        return true;
                }
                else if (wire.IsOn(panel))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 配線の端が天井パネル上にある時true
        /// </summary>
        public override bool IsConnected(CeilingPanel panel)
        {
            foreach (var wire in this.Wires)
            {
                if (wire is MarkingWire)
                {
                    if (((MarkingWire)wire).IsConnected(panel))
                        return true;
                }
                else if (wire.IsConnected(panel))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 配線が天井パネルの端をまたぐ時true
        /// </summary>
        public override bool IsCrossover(CeilingPanel panel)
        {
            foreach (var wire in this.Wires)
            {
                if (wire is MarkingWire)
                {
                    if (((MarkingWire)wire).IsCrossover(panel))
                        return true;
                }
                else if (wire.IsCrossover(panel))
                    return true;
            }

            return false;
        }

        public List<Wire> GetTargetWires(CeilingPanel panel)
        {
            List<Wire> result = new List<Wire>();
            foreach (var wire in this.Wires)
            {
                if (wire is MarkingWire)
                    result.AddRange(((MarkingWire)wire).GetTargetWires(panel));
                else if (wire.IsOn(panel))
                    result.Add(wire);
            }
            return result;
        }

        //RisingWireを上階と下階に分ける
        public static void SplitRisingWire(Symbol symbol, out Symbol upperSymbol, out Symbol underSymbol, out Wire upperWire, out Wire underWire)
        {
            var wires = ((RisingWire)symbol.Wire).Wires;
            upperWire = null;
            underWire = null;

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

            foreach (var wire in wires)
            {
                if (wire.IsConnected(upperSymbol))
                    upperWire = wire;
                else if (wire.IsConnected(underSymbol))
                    underWire = wire;
            }
            if (upperWire == null || underWire == null)
                throw new ApplicationException("ワイヤーの両端が見つけられませんでした。");
        }
    }
}
