using System;
using System.Collections.Generic;
using System.Text;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI.Entities.CADEntity
{

    /// <summary>
    /// Markingシンボルを経由してシンボルからシンボルまで接続されているWireの情報を管理する
    /// </summary>
    public class MarkingWire : Wire
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private MarkingWire()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="wires"></param>
        /// <param name="markingSymbols"></param>
        public MarkingWire(List<Wire> wires, List<Symbol> markingSymbols)
        {
            this.wires = wires;
            this.markingSymbols = markingSymbols;
        }

        /// <summary>
        /// 結合されているWire情報
        /// </summary>
        private List<Wire> wires = new List<Wire>();
        public List<Wire> Wires
        {
            get { return this.wires; }
            set { this.wires = value; }
        }

        /// <summary>
        /// マーキング情報
        /// </summary>
        private List<Symbol> markingSymbols = new List<Symbol>();
        public List<Symbol> MarkingSymbols
        {
            get { return this.markingSymbols; }
            set { this.markingSymbols = value; }
        }

        /// <summary>
        /// 子シンボルと接続されているマーキングシンボル
        /// </summary>
        public Symbol ChildMarking { get; set; }
        /// <summary>
        /// 親シンボルと接続されているマーキングシンボル
        /// 親シンボル⇔マーキング⇔子シンボルのような配線をした場合、ParentMarkingはnullになる。
        /// </summary>
        public Symbol ParentMarking { get; set; }
        /// <summary>
        /// 子シンボルとマーキングを接続しているWire情報
        /// </summary>
        public Wire ChildSymbolToMarkingWire { get; set; }
        /// <summary>
        /// 親シンボルとマーキングを接続しているWire情報
        /// </summary>
        public Wire ParentSymbolToMarkingWire { get; set; }

        /// <summary>
        /// 配線に床下配線が含まれていたらtrue
        /// </summary>
        public override bool IsUnderfloor
        {
            get
            {
                foreach (var wire in this.wires)
                {
                    if (wire.IsUnderfloor)
                        return true;
                }
                return false;
            }
        }

        public override bool IsConnected(Symbol symbol)
        {
            foreach (var wire in this.Wires)
            {
                if (wire.IsConnected(symbol))
                    return true;
            }

            return false;
        }

        public bool IsConnectedConsideringClipped(Symbol symbol)
        {
            foreach (var wire in this.Wires)
            {
                if (wire.IsConnected(symbol))
                    return true;

                if (wire.IsClippedPositionConnected(symbol))
                    return true;
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
                if (wire.IsOn(panel))
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
                if (wire.IsConnected(panel))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// MarkingWire同士が接続されているかどうか。
        /// 引数に指定したMarkingWireが持ってるマーキングシンボルと繋がっている
        /// Wireを持っていたらTRUE
        /// </summary>
        /// <param name="combineWire"></param>
        /// <returns></returns>
        public bool IsConnected(MarkingWire combineWire)
        {
            foreach (Wire wire in this.Wires)
            {
                foreach (Symbol marking in combineWire.markingSymbols)
                {
                    if (wire.IsConnected(marking))
                        return true;

                }
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
                if (wire.IsCrossover(panel))
                    return true;
            }

            return false;
        }

        public List<Wire> GetTargetWires(CeilingPanel panel)
        {
            List<Wire> targegtWires = new List<Wire>();
            foreach (var wire in this.Wires)
            {
                if (wire.IsOn(panel))
                    targegtWires.Add(wire);
            }

            return targegtWires;
        }

        /// <summary>
        /// 水色マーキングの数を取得
        /// </summary>
        /// <returns></returns>
        public int GetBuleMarkingCount()
        {
            List<Symbol> buleMarkings = this.markingSymbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.MarkingBule);
            return buleMarkings.Count;
        }

        /// <summary>
        /// プロパティに値を設定する
        /// </summary>
        /// <param name="ceilingPanels"></param>
        public void FillMarkingProperty(List<CeilingPanel> ceilingPanels)
        {
            this.FillMarkingCelingPanel(ceilingPanels);
            if (this.ChildSymbol != null)
            {
                this.FillChildWireProperty();
                this.FillOtherWirePropertyAndSort();
            }
            //子が無いときは親を使う
            else if (this.ParentSymbol != null)
            {
                this.FillParentWireProperty();
                this.FillOtherWirePropertyFromParent();
            }
            this.SetAvoidCalculationFlag();
        }

        //パネル下配線のうち、緑マーキング配線について計算対象外にする
        private void SetAvoidCalculationFlag()
        {
            foreach (var wire in this.wires)
            {
                if (!wire.IsNotInstalledCeiling)
                    continue;

                if (wire.ParentSymbol != null)
                {
                    if (wire.ParentSymbol.IsGreenMarking)
                        wire.IsAvoidCalculation = true;
                }

                if (wire.ChildSymbol != null)
                {
                    if (wire.ChildSymbol.IsGreenMarking)
                        wire.IsAvoidCalculation = true;
                }
            }
        }

        /// <summary>
        /// マーキングシンボルにシーリングパネル位置を設定する
        /// </summary>
        /// <param name="ceilingPanels"></param>
        private void FillMarkingCelingPanel(List<CeilingPanel> ceilingPanels)
        {
            foreach (Symbol marking in markingSymbols)
            {
                marking.CeilingPanel = null;
                foreach (var ceilingPanel in ceilingPanels)
                {
                    if (marking.Floor != ceilingPanel.Floor)
                        continue;

                    if (!ceilingPanel.Contains(marking.ActualPosition))
                        continue;

                    marking.CeilingPanel = ceilingPanel;
                    break;
                }

            }
        }

        /// <summary>
        /// 子側のワイヤー情報を設定する
        /// </summary>
        private void FillChildWireProperty()
        {
            if (this.ChildSymbol == null)
                return;

            Wire childWire = this.Wires.Find(p => p.IsConnected(this.ChildSymbol));

            foreach (Symbol marking in this.markingSymbols)
            {
                if (!childWire.IsConnected(marking))
                    continue;

                childWire.ParentSymbol = marking;
                childWire.ChildSymbol = this.ChildSymbol;
                childWire.IsNotInstalledCeiling = true;

                this.ChildSymbolToMarkingWire = childWire;
                this.ChildMarking = marking;

                break;
            }
        }

        /// <summary>
        /// 親側のワイヤー情報を設定する
        /// </summary>
        private void FillParentWireProperty()
        {
            if (this.ParentSymbol == null)
                return;

            Wire parentWire = this.Wires.Find(p => p.IsConnected(this.ParentSymbol));

            foreach (Symbol marking in this.markingSymbols)
            {
                if (!parentWire.IsConnected(marking))
                    continue;

                parentWire.ParentSymbol = this.ParentSymbol;
                parentWire.ChildSymbol = marking;

                //子が階またぎ位置であるか、マーキングが2個なら親との接続はパネル下
                if (this.ChildSymbol == null || this.markingSymbols.Count > 1)
                    parentWire.IsNotInstalledCeiling = true;

                this.ParentSymbolToMarkingWire = parentWire;
                this.ParentMarking = marking;

                break;
            }
        }

        /// <summary>
        /// 子供側から順にワイヤーの両端のシンボル情報を付与とリストのWireの順番を親側から順番に並び替える。
        /// </summary>
        private void FillOtherWirePropertyAndSort()
        {
            if (this.ChildSymbolToMarkingWire == null)
                return;//子側のワイヤー情報が無ければ設定できない。

            //ソート後のワイヤー
            List<Wire> sortWires = new List<Wire>();

            List<Wire> tempWires = new List<Wire>(this.wires);
            List<Symbol> tempMarkings = new List<Symbol>(this.markingSymbols);

            Wire lastWire = this.ChildSymbolToMarkingWire;
            tempWires.Remove(lastWire);
            tempMarkings.Remove(lastWire.ParentSymbol);

            sortWires.Add(lastWire);

            Wire baseWire = lastWire;
            while (0 < tempWires.Count)
            {
                Wire wire = tempWires.Find(p => p.IsConnected(baseWire.ParentSymbol));
                if (wire == null)
                    break;

                wire.ChildSymbol = baseWire.ParentSymbol;

                Symbol marking = tempMarkings.Find(p => wire.IsConnected(p));
                if (marking != null)
                {
                    wire.ParentSymbol = marking;
                    tempMarkings.Remove(marking);
                }
                else
                {
                    wire.ParentSymbol = this.ParentSymbol;//マーキングが取得できなかった時、親側のシンボルに到達
                    //マーキングが2個ならパネル下で接続
                    if (this.markingSymbols.Count > 1)
                        wire.IsNotInstalledCeiling = true;

                    this.ParentSymbolToMarkingWire = wire;

                    if (this.ChildMarking != wire.ChildSymbol)
                        this.ParentMarking = wire.ChildSymbol;
                }
                sortWires.Insert(0, wire);

                baseWire = wire;
                tempWires.Remove(wire);
            }

            this.wires = sortWires;
        }

        /// <summary>
        /// 親側から順にワイヤーの両端のシンボル情報を付与（ソートは不要）
        /// </summary>
        private void FillOtherWirePropertyFromParent()
        {
            if (this.ParentSymbolToMarkingWire == null)
                return;

            //ソート後のワイヤー
            List<Wire> resultWires = new List<Wire>();

            List<Wire> tempWires = new List<Wire>(this.wires);
            List<Symbol> tempMarkings = new List<Symbol>(this.markingSymbols);

            Wire firstWire = this.ParentSymbolToMarkingWire;
            tempWires.Remove(firstWire);
            tempMarkings.Remove(firstWire.ChildSymbol);

            resultWires.Add(firstWire);

            Wire baseWire = firstWire;
            while (0 < tempWires.Count)
            {
                Wire wire = tempWires.Find(p => p.IsConnected(baseWire.ChildSymbol));
                if (wire == null)
                    break;

                wire.ParentSymbol = baseWire.ChildSymbol;

                Symbol marking = tempMarkings.Find(p => wire.IsConnected(p));
                if (marking != null)
                {
                    wire.ChildSymbol = marking;
                    tempMarkings.Remove(marking);
                }
                else
                {
                    wire.ChildSymbol = this.ChildSymbol;
                    if (this.markingSymbols.Count > 1)
                    {
                        wire.IsNotInstalledCeiling = true;
                        this.ChildSymbolToMarkingWire = wire;
                        if (this.ParentMarking != wire.ParentSymbol)
                            this.ChildMarking = wire.ParentSymbol;
                    }
                }
                resultWires.Add(wire);

                baseWire = wire;
                tempWires.Remove(wire);
            }

            this.wires = resultWires;
        }

        /// <summary>シンボルの基点に接続していたらtrue</summary>
        public override bool IsConnectedToArrowHead(Symbol symbol)
        {
            foreach (var wire in this.Wires)
            {

                if (wire.Floor != symbol.Floor)
                    return false;

                if (wire.StartPoint.EqualsRound(symbol.Position))
                    return true;

                if (wire.EndPoint.EqualsRound(symbol.Position))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// ワイヤーとマーキングシンボルからMarkingWireを生成する。
        /// </summary>
        /// <param name="wires"></param>
        /// <param name="markings"></param>
        /// <returns></returns>
        public static List<MarkingWire> CreateMarkingWire(List<Wire> wires, List<Symbol> markings)
        {
            List<MarkingWire> markingWires = new List<MarkingWire>();
            foreach (Symbol marking in markings)
            {
                //マーキングと繋がっているワイヤー持ってくる
                List<Wire> connectedWires = wires.FindAll(p => p.IsConnected(marking));

                if (connectedWires.Count != 2)
                    continue;

                MarkingWire markingWire = new MarkingWire();
                markingWire.Wires.AddRange(connectedWires);
                markingWire.MarkingSymbols.Add(marking);
                markingWires.Add(markingWire);
                markingWire.Floor = marking.Floor;
            }

            //MarkingWire同士で接続されていた場合、さらにまとめる。
            for (int i = 0; i < markingWires.Count; i++)
            {
                MarkingWire markingWire = markingWires[i];
                List<MarkingWire> connectedMarkingWires = markingWires.FindAll(p => p != markingWire && markingWire.IsConnected(p));
                while (1 <= connectedMarkingWires.Count)//接触しているMarkingWireが無くなるまでループ
                {
                    foreach (MarkingWire connectedMarkingWire in connectedMarkingWires)
                    {
                        foreach (Wire wire in connectedMarkingWire.Wires)
                        {
                            if (markingWire.Wires.Contains(wire))
                                continue;//重複したWireが入らないように
                            markingWire.Wires.Add(wire);
                        }
                        markingWire.MarkingSymbols.AddRange(connectedMarkingWire.MarkingSymbols);
                        markingWires.Remove(connectedMarkingWire);
                    }

                    connectedMarkingWires = markingWires.FindAll(p => p != markingWire && markingWire.IsConnected(p));
                }
            }

            //余りの座標をワイヤーの両端にする
            foreach (var markingWire in markingWires)
            {
                foreach (var childWire in markingWire.wires)
                {
                    var targets = markings.FindAll(p => p.Floor == childWire.Floor);

                    if (!targets.Exists(p => p.Contains(childWire.StartPoint)))
                        markingWire.StartPoint = childWire.StartPoint;

                    if (!targets.Exists(p => p.Contains(childWire.EndPoint)))
                        markingWire.EndPoint = childWire.EndPoint;
                }
            }

            //マーキングの子配列にConnectedSymbolObjectIdが入っている場合削除する
            markingWires.ForEach(p => p.Wires.ForEach(q => q.ConnectedSymbolObjectId = -1));
            return markingWires;
        }

        public override string ToString()
        {
            string strTitle = "[MarkingWire] ";
            string symbolStr = "markingSymbol = ";
            foreach (Symbol s in this.markingSymbols)
            {
                symbolStr += "【" + s.ToString() + "】,";
            }


            string wireStr = "Wire = ";
            foreach (Wire w in this.Wires)
            {
                wireStr += w.ToString() + ",";
            }

            string hashCode = "hashCode = 【" + this.GetHashCode() + "】";

            return strTitle + symbolStr + "," + wireStr + "," + hashCode;
        }

    }
}

