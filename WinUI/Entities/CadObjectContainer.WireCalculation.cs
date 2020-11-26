using System;
using System.Collections.Generic;
using System.Text;
using Edsa.AutoCadProxy;
using SocketPlan.WinUI.Entities.CADEntity;

namespace SocketPlan.WinUI
{
    public partial class CadObjectContainer
    {
        public void SetCalculatedWires(Symbol symbol)
        {
            //分割して親側からの配列に並べる
            List<Wire> targetWires = this.DivideSymbolWire(symbol);

            if (!symbol.IsLightElectrical && !symbol.ForIH && !symbol.WithEV)
                //天井受けをセット
                targetWires = this.SetCeilingRecieverForDividedWires(symbol, targetWires);

            //条件に従ってサマリする
            targetWires = this.CreateSummaryWires(symbol, targetWires);

            if (!symbol.IsLightElectrical && !symbol.ForIH && !symbol.WithEV)
                //天井受け余長の計算
                this.CalculateCeilingPanelRecieverAllowLength(ref targetWires);

            symbol.Wire.CalculatedWires = targetWires;
        }

        private List<Wire> SummarizeMarkingWires(List<Wire> wires)
        {
            List<Wire> resultWires = new List<Wire>();

            resultWires.AddRange(this.SummarizeUnderCeilingWireSingle(wires));

            return resultWires;
        }

        private List<Wire> SummarizeUnderCeilingWireSingle(List<Wire> wires)
        {
            if (!wires.Exists(p => !p.IsNotInstalledCeiling && !p.IsUnderfloor))
                return wires;

            //先頭
            if (wires.Count <= 1)
                return wires;

            var firstWire = wires[0];
            var nextWire = wires[1];
            var rollWire = new Wire();
            if (firstWire.IsAvoidCalculation || firstWire.IsNotInstalledCeiling)
            {
                //緑以外はパネル位置にロールする
                if (firstWire.IsAvoidCalculation)
                    rollWire = null;
                else
                    rollWire = firstWire;

                //マーキング位置のワイヤーが100mm以下のとき、次の線と結合（ロールしない）
                //次の線が無いか、パネル上に無い時は仕方無いので残す
                if (nextWire.Length2D <= Const.MARKING_SHOULD_ROLL_LENGTH)
                {
                    if (wires.Count >= 3)
                    {
                        if (!wires[2].IsAvoidCalculation && !wires[2].IsNotInstalledCeiling)
                        {
                            wires[2].Length2D += nextWire.TotalLength2D;
                            wires.Remove(nextWire);
                        }
                    }
                }

                //ロールしてもしなくても1本目は消す
                wires.Remove(firstWire);
                if (rollWire != null)
                {
                    wires[0].MarkingCeilingAllowLength += this.GetDropPointCeilingDepthLength(rollWire);
                    wires[0].RolledWireParents.Add(rollWire);
                    wires[0].RolledLengthParent += rollWire.TotalLength2D;
                }
            }

            //末尾
            if (wires.Count <= 1)
                return wires;

            firstWire = wires[wires.Count - 1];
            nextWire = wires[wires.Count - 2];
            rollWire = new Wire();
            if (firstWire.IsAvoidCalculation || firstWire.IsNotInstalledCeiling)
            {
                //緑以外はパネル位置にロールする
                if (firstWire.IsAvoidCalculation)
                    rollWire = null;
                else
                    rollWire = firstWire;

                //マーキング位置のワイヤーが100mm以下のとき、次の線と結合（ロールしない）
                //次の線が無いか、パネル上に無い時は仕方無いので残す
                if (nextWire.Length2D <= Const.MARKING_SHOULD_ROLL_LENGTH)
                {
                    if (wires.Count >= 3)
                    {
                        if (!wires[wires.Count - 3].IsAvoidCalculation && !wires[wires.Count - 3].IsNotInstalledCeiling)
                        {
                            wires[wires.Count - 3].Length2D += nextWire.TotalLength2D;
                            wires.Remove(nextWire);
                        }
                    }
                }

                //ロールしてもしなくても1本目は消す
                wires.Remove(firstWire);
                if (rollWire != null)
                {
                    wires[wires.Count - 1].MarkingCeilingAllowLength += this.GetDropPointCeilingDepthLength(rollWire);
                    wires[wires.Count - 1].RolledWireChildren.Add(rollWire);
                    wires[wires.Count - 1].RolledLengthChild += rollWire.TotalLength2D;
                }
            }
            return wires;
        }

        public List<Wire> CreateSummaryWires(Symbol symbol, List<Wire> wires)
        {
            List<Wire> targetWires = new List<Wire>();

            if (wires.Count == 1)
                targetWires = wires;
            else
            {
                //マーキング用ロール処理
                wires = this.SummarizeMarkingWires(wires);

                //DLtoDL
                if (symbol.IsLightToLight)
                {
                    targetWires = this.SummarizedWireDLtoDL(symbol, wires);
                }
                else
                {
                    //ノーマルパターン
                    targetWires = this.SummarizedWire(symbol, wires);
                }
            }

            if (targetWires.Count == 0)
                throw new ApplicationException("配線の計算に失敗しました。");

            //先頭＆末尾のフラグセット
            Wire.SetWireEdgeFlg(ref targetWires);

            return targetWires;
        }

        #region 天井受け計算

        public decimal GetCeilingPanelRecieverAllowLengthAll(Symbol symbol)
        {
            decimal result = 0;

            List<Wire> targetWires = new List<Wire>();
            if (symbol.Wire.CalculatedWires.Count == 0)
                targetWires.Add(symbol.Wire);
            else
                targetWires = symbol.Wire.CalculatedWires;

            foreach (var wire in targetWires)
            {
                if (!wire.IsNotInstalledCeiling)
                    result += wire.CeilingReceiverAllowLength;
                wire.RolledWireChildren.ForEach(p => result += this.GetRolledCeilingRecieverAllowLength(p));
                wire.RolledWireParents.ForEach(p => result += this.GetRolledCeilingRecieverAllowLength(p));
            }
            return result;
        }

        public decimal GetDropPointCeilingDepthLength(Wire wire)
        {
            decimal result = 0;
            if (wire.Floor == 1)
            {
                result += Static.HouseSpecs.CeilingDepth1F / 2;
                result += Static.HouseSpecs.CeilingThickness1F;
            }
            else
            {
                result += Static.HouseSpecs.CeilingDepth2F / 2;
                result += Static.HouseSpecs.CeilingThickness2F;
            }
            return result;
        }

        //線上の2点を指定して、その間に頂点が存在していたらtrue
        private bool ExistsVertexByPoints(Wire baseWire, PointD pointA, PointD pointB)
        {
            List<int> tempWireIds = new List<int>();
            try
            {
                var parameters = new List<double>();
                //端の座標はパラメータにしない
                if (baseWire.StartPoint != pointA && baseWire.EndPoint != pointA)
                    parameters.Add(AutoCad.Db.Curve.GetParameter(baseWire.ObjectId, pointA));
                if (baseWire.StartPoint != pointB && baseWire.EndPoint != pointB)
                    parameters.Add(AutoCad.Db.Curve.GetParameter(baseWire.ObjectId, pointB));

                parameters.Sort((p, q) => p.CompareTo(q));
                if (parameters.Count == 0)
                    tempWireIds = new List<int> { baseWire.ObjectId };
                else
                    tempWireIds = AutoCad.Db.Polyline.GetSplitCurves(baseWire.ObjectId, parameters);

                if (tempWireIds.Count == 0)
                    throw new ApplicationException("線がちぎれました");

                foreach (var wireId in tempWireIds)
                {
                    var vertexes = AutoCad.Db.Polyline.GetVertex(wireId);

                    //始点＆終点を含む線の頂点数を数える
                    if (vertexes.Exists(p => p.EqualsRound(pointA)) && vertexes.Exists(q => q.EqualsRound(pointB)))
                    {
                        if (vertexes.Count > 2)
                            return true;
                    }
                }
            }
            finally
            {
                if (tempWireIds.Count != 0)
                    tempWireIds.ForEach(p => AutoCad.Db.Polyline.Erase(p));
            }
            return false;
        }

        //天井受け余長を再計算する
        public void CalculateCeilingPanelRecieverAllowLength(ref List<Wire> wires)
        {
            foreach (var wire in wires)
            {
                decimal result = 0;

                if (wire.IsNotInstalledCeiling)
                    continue;

                //器具位置の天井受けは500mm（ロールしてたら器具位置ではない）
                if (wire.IsLast && wire.RolledWireChildren.Count == 0)
                {
                    result = wire.CeilingRecievers.Count * 500;
                }
                else
                {
                    if (wire.CeilingRecievers.Count > 0)
                    {
                        //終点に一番近い天井受けとの交点を探す
                        int targetIdx = -1;
                        double minDistance = double.MaxValue;
                        foreach (var reciever in wire.CeilingRecievers)
                        {
                            var points = AutoCad.Db.Entity.GetIntersect2D(reciever.ObjectId, wire.ObjectId);
                            if (points.Count == 0)
                                continue;

                            var distance = Utilities.GetDistance(wire.EndPoint, points[0]);
                            if (minDistance > distance)
                            {
                                minDistance = distance;
                                targetIdx = wire.CeilingRecievers.IndexOf(reciever);
                            }
                        }

                        bool is200mm = false;
                        //天井受けがパネル端だったら、ワイヤーの出入り口をチェックする
                        if (wire.CeilingRecievers[targetIdx].IsOnCeilingPanelEdge)
                        {
                            var p = AutoCad.Db.Entity.GetIntersect2D(wire.CeilingRecievers[targetIdx].ObjectId, wire.ObjectId);

                            //一番出口寄りの天井受けを取得してあるのでチェックいらないと思う
                            ////出口側に近ければ200mm余長
                            //if (Utilities.GetDistance(wire.StartPoint, p[0]) > Utilities.GetDistance(wire.EndPoint, p[0]))
                            //{
                            //さらに、天井受け～パネル端の間で線が折れていたら500mmになる
                            if (this.ExistsVertexByPoints(wire, p[0], wire.EndPoint))
                                is200mm = false;
                            else
                                is200mm = true;
                            //}
                        }

                        if (is200mm)
                            result += 200;
                        else
                            result += 500;

                        result += (wire.CeilingRecievers.Count - 1) * 500;
                    }
                }
                wire.CeilingReceiverAllowLength = result;

                //ロールされたワイヤーの天井受けをロール長に加算
                if (wire.RolledWireChildren.Count > 0)
                {
                    wire.RolledWireChildren.ForEach(p => wire.RolledLengthChild += this.GetRolledCeilingRecieverAllowLength(p));
                }
                if (wire.RolledWireParents.Count > 0)
                {
                    wire.RolledWireParents.ForEach(p => wire.RolledLengthParent += this.GetRolledCeilingRecieverAllowLength(p));
                }
            }
        }

        //ロールされたワイヤーの天井受け余長をセットする
        private decimal GetRolledCeilingRecieverAllowLength(Wire wire)
        {
            decimal result = 0;
            if (wire.IsNotInstalledCeiling)
                return result;

            wire.CeilingReceiverAllowLength = wire.CeilingRecievers.Count * 200;
            result += wire.CeilingReceiverAllowLength;

            //孫が両側にロールしていることはあり得ないけどMethod分けるほどでもないので・・・
            wire.RolledWireChildren.ForEach(p => result += this.GetRolledCeilingRecieverAllowLength(p));
            wire.RolledWireParents.ForEach(p => result += this.GetRolledCeilingRecieverAllowLength(p));

            return result;
        }
        #endregion

        #region 分割処理

        /// <summary>
        /// 配線の始点と終点が親側から並ぶように並び替える（子配線も含む）
        /// </summary>
        public Wire SetReplaseWireFlag(Symbol symbol, Wire targetWire)
        {
            var childWires = new List<Wire>();
            if (targetWire is RisingWire)
            {
                foreach (var wire in ((RisingWire)targetWire).Wires)
                    childWires.Add(this.SetReplaseWireFlag(symbol, wire));

                ((RisingWire)targetWire).Wires = childWires;
            }
            else if (targetWire is MarkingWire)
            {
                var markingWire = (MarkingWire)targetWire;
                var wires = ((MarkingWire)targetWire).Wires; //インスタンスが分かれないように
                foreach (var wire in wires)
                {
                    if (markingWire.ChildMarking != null)
                    {
                        if (wire.IsConnected(markingWire.ChildMarking))
                        {
                            //子側
                            if (wire.IsConnected(symbol))
                            {
                                if (symbol.Contains(wire.StartPoint))
                                    wire.IsReversedWirePoint = true;
                            }
                            //中間or親側
                            else
                            {
                                if (markingWire.ChildMarking.Contains(wire.StartPoint))
                                    wire.IsReversedWirePoint = true;
                            }
                        }
                        else
                        {
                            //子マーキングと接触なしなら親マーキングがあるはず
                            if (markingWire.ParentMarking == null)
                                throw new ApplicationException("マーキング配線が異常です。");

                            if (markingWire.ParentMarking.Contains(wire.StartPoint))
                                wire.IsReversedWirePoint = true;
                        }
                    }
                    //親⇒arrowのときは子マーキングが無いので
                    else if (markingWire.ParentMarking != null)
                    {
                        if (wire.IsConnected(markingWire.ParentMarking))
                        {
                            //親側
                            if (wire.IsConnected(symbol.Parent))
                            {
                                if (symbol.Contains(wire.EndPoint))
                                    wire.IsReversedWirePoint = true;
                            }
                            //中間or子側
                            else
                            {
                                if (markingWire.ParentMarking.Contains(wire.EndPoint))
                                    wire.IsReversedWirePoint = true;
                            }
                        }
                        //else
                        //{
                        //    //親マーキングと接触なしなら子マーキングがあるはず
                        //    if (markingWire.ChildMarking == null)
                        //        throw new ApplicationException("マーキング配線が異常です。");

                        //    if (markingWire.ChildMarking.Contains(wire.EndPoint))
                        //        wire.IsReversedWirePoint = true;
                        //}
                    }
                    childWires.Add(wire);
                }
                ((MarkingWire)targetWire).Wires = childWires;
            }
            else
            {
                if ((symbol.Contains(targetWire.StartPoint) && symbol.Floor == targetWire.Floor) ||
                    (symbol.Parent.Contains(targetWire.EndPoint) && symbol.Parent.Floor == targetWire.Floor))
                    targetWire.IsReversedWirePoint = true;
            }
            return targetWire;
        }

        public List<Wire> DivideSymbolWire(Symbol symbol)
        {
            var connectedWires = new List<Wire>();
            if (symbol.Wire is RisingWire)
            {
                var risingWires = (symbol.Wire as RisingWire).Wires;

                foreach (var wire in risingWires)
                {
                    var addWires = new List<Wire>();
                    if (wire is MarkingWire)
                        addWires = ((MarkingWire)wire).GetAllChildrenWire();
                    else
                        addWires.Add(wire);

                    if (addWires.Exists(p => p.ParentSymbol == symbol.Parent) ||
                        addWires.Exists(p => p.IsConnected(symbol.Parent)))
                        connectedWires.InsertRange(0, addWires);
                    else
                        connectedWires.AddRange(addWires);
                }
            }
            else if (symbol.Wire is MarkingWire)
            {
                var addWires = ((MarkingWire)symbol.Wire).GetAllChildrenWire();

                if (addWires.Exists(p => p.ParentSymbol == symbol.Parent) ||
                    addWires.Exists(p => p.IsConnected(symbol.Parent)))
                    connectedWires.InsertRange(0, addWires);
                else
                    connectedWires.AddRange(addWires);
            }
            else
            {
                connectedWires.Add(symbol.Wire);
            }

            var wires = new List<Wire>();
            foreach (var wire in connectedWires)
            {
                List<Wire> divideWires = this.DivideWirePerCeilingPanel(wire);
                divideWires = this.ReplaseWirePoints(wire, divideWires);
                wires.AddRange(divideWires);
            }
            return wires;
        }

        /// <summary>
        /// 分割したワイヤー別に天井受けをセットする
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="wires"></param>
        public List<Wire> SetCeilingRecieverForDividedWires(Symbol symbol, List<Wire> wires)
        {
            foreach (var wire in wires)
            {
                if (!wire.IsNotInstalledCeiling)
                    wire.SetIntersectCeilingRecievers(wire.CeilingPanel);
            }
            return wires;
        }

        private List<Wire> DivideWirePerCeilingPanel(Wire wire)
        {
            var wires = new List<Wire>();

            //ロール長を計算し直すので初期化
            wire.RolledLengthParent = 0;
            wire.RolledLengthChild = 0;
            wire.RolledWireParents = new List<Wire>();
            wire.RolledWireChildren = new List<Wire>();

            //床下配線or天井設置なし配線は分割しない
            if (wire.IsNotInstalledCeiling || wire.IsUnderfloor)
                return new List<Wire> { wire };

            var candidatePanels = this.CeilingPanels.FindAll(p => wire.IsOn(p));
            if (candidatePanels.Count == 0)
                return new List<Wire> { wire };

            var startParam = AutoCad.Db.Curve.GetParameter(wire.ObjectId, wire.StartPoint);

            //ワイヤーとパネルの交点をまとめる。
            var crossPoints = new List<PointD>();
            //パネル同士がぴったり接している場合は同じ座標が交点に入るので削除する。
            foreach (var panel in candidatePanels)
            {
                var points = AutoCad.Db.Entity.GetIntersect2D(panel.ObjectId, wire.ObjectId);

                foreach (var point in points)
                {
                    if (crossPoints.Exists(p => p.X == point.X && p.Y == point.Y))
                        continue;

                    crossPoints.Add(point);
                }
            }

            //一枚のパネルで完結しているワイヤーはそのまま返す。
            if (crossPoints.Count == 0)
            {
                wire.CeilingPanel = candidatePanels[0];
                wires.Add(wire);
                return wires;
            }

            var parameters = new List<double>();
            foreach (var point in crossPoints)
            {
                var parameter = AutoCad.Db.Curve.GetParameter(wire.ObjectId, point);

                parameters.Add(parameter);
            }

            //交点を配線の始点から近い順に並び変える。
            parameters.Sort((p, q) => p.CompareTo(q));

            var tempWireIds = new List<int>();
            //ここで、ワイヤーの長さを算出している。
            //作成した余計な線は必ず削除するよ！
            try
            {
                tempWireIds = AutoCad.Db.Polyline.GetSplitCurves(wire.ObjectId, parameters);
                var divideWires = new List<Wire>();

                foreach (var wireId in tempWireIds)
                {
                    var divideWire = new Wire(wireId, wire);
                    var panel = candidatePanels.Find(p => divideWire.IsIn(p));
                    if (panel == null)
                        divideWire.CeilingPanel = null;
                    else
                        divideWire.CeilingPanel = panel;

                    divideWires.Add(divideWire);
                }
                wires.AddRange(divideWires);
            }
            finally
            {
                if (tempWireIds.Count != 0)
                    tempWireIds.ForEach(p => AutoCad.Db.Polyline.Erase(p));
            }

            return wires;
        }

        //分割前のワイヤーの始点終点が逆だったら、分割後のワイヤーを入れ替えて配列順も逆にする
        public List<Wire> ReplaseWirePoints(Wire baseWire, List<Wire> wires)
        {
            if (!baseWire.IsReversedWirePoint)
                return wires;

            foreach (var wire in wires)
            {
                var wkS = wire.StartPoint;
                var wkE = wire.EndPoint;
                wire.StartPoint = wkE;
                wire.EndPoint = wkS;
            }
            wires.Reverse();
            return wires;
        }

        #endregion

        #region サマリ処理
        public List<Wire> SummarizedWire(Symbol symbol, List<Wire> wires)
        {
            //階マタギの場合は別処理をする。
            wires = this.SummarizedWireAtDrop(symbol, wires);

            if (wires.Count <= 1)
                return wires;

            //分電盤接続で、分電盤位置が1.5m以内で、巻き取られていない場合は子側に巻き取りをする
            if (symbol.Parent.IsBundenban && wires[0].RolledWireParents.Count == 0 && wires[0].TotalLength2D <= 1500)
            {
                //分電盤が上階の場合は×
                if (symbol.Parent.Floor <= symbol.Floor)
                    wires = this.SummarizedWireForChildOnce(symbol, wires, 0);
            }

            if (wires.Count <= 1)
                return wires;

            //子側から処理する
            wires.Reverse();

            var prevWire = new Wire();
            var resultWires = new List<Wire>();
            var summaryWires = new List<Wire>();
            decimal summaryLength = 0;
            foreach (var wire in wires)
            {
                var wireIdx = wires.IndexOf(wire);

                //先頭かつJBなら対象外
                if (wireIdx == 0 && symbol.IsJointBox)
                {
                    resultWires.Add(wire);
                    continue;
                }

                //巻き取り済みがあるとき、継続可能かチェックする
                if (summaryWires.Count > 0)
                {
                    //子方向にロール済みの線に、継ぎ足すような足し方はだめ
                    if (wire.RolledWireChildren.Count > 0)
                    {
                        resultWires.Add(this.CombineMargedWires(summaryWires, true));
                        summaryWires = new List<Wire>();
                        summaryLength = 0;
                    }
                    else
                    {
                        //ロール1回目は制限なし
                        if (summaryWires.Count == 1)
                        {
                            summaryWires.Add(wire);
                            summaryLength += wire.TotalLength2D;
                        }
                        //ロール二回目以降は、
                        //①ここまでの合計が1.5m以下 or
                        //②対象が1.5m以下かつ足したとき2m以下なら巻き取り
                        else
                        {
                            if (summaryLength <= 1500 || (wire.TotalLength2D <= 1500 && summaryLength + wire.TotalLength2D <= 2000))
                            {
                                summaryWires.Add(wire);
                                summaryLength += wire.TotalLength2D;
                            }
                            else
                            {
                                resultWires.Add(this.CombineMargedWires(summaryWires, true));
                                summaryWires = new List<Wire>();
                                summaryLength = 0;
                            }
                        }
                    }
                }

                //巻き取りがないorクリアされていたら、1.5mチェック
                if (summaryWires.Count == 0)
                {
                    if (wire.IsInhibitRoll)
                    {
                        resultWires.Add(wire);
                    }
                    else if (wire.TotalLength2D <= 1500)
                    {
                        summaryWires.Add(wire);
                        summaryLength += wire.TotalLength2D;
                    }
                    else
                    {
                        resultWires.Add(wire);
                    }
                }

                //末尾で巻き取りが残っていたらまとめる
                if (wireIdx == wires.Count - 1 && summaryWires.Count > 0)
                {
                    resultWires.Add(this.CombineMargedWires(summaryWires, true));
                }
            }

            //配列順を戻す
            resultWires.Reverse();

            return resultWires;
        }

        //最後にまとめられたワイヤーに、それ以外のワイヤーを巻き取り長さとして加算してから戻す
        private Wire CombineMargedWires(List<Wire> wires, bool addChildSide)
        {
            if (wires.Count == 1)
                return wires[0];

            var baseWire = wires[wires.Count - 1];
            for (int i = wires.Count - 2; i >= 0; i--)
            {
                if (addChildSide)
                {
                    baseWire.RolledWireChildren.Add(wires[i]);
                    baseWire.RolledLengthChild += wires[i].TotalLength2D;
                }
                else
                {
                    baseWire.RolledWireParents.Add(wires[i]);
                    baseWire.RolledLengthParent += wires[i].TotalLength2D;
                }
            }
            return baseWire;
        }

        /// <summary>
        /// 階またぎ用の処理（現時点で階またぎは1配線で1回のみ）
        /// </summary>
        /// <param name="wires"></param>
        /// <returns></returns>
        private List<Wire> SummarizedWireAtDrop(Symbol symbol, List<Wire> wires)
        {
            if (wires.Count == 1)
                return wires;

            var prevWire = new Wire();
            var removeWires = new List<Wire>();

            //小屋裏の手前にあるパネルに小屋裏を全部まとめる
            //小屋裏は末尾にしかないはず
            foreach (var wire in wires)
            {
                if (wire.CeilingPanel == null)
                    continue;
                
                if (wire.CeilingPanel.Name.StartsWith("RC"))
                {
                    if (prevWire.CeilingPanel != null)
                    {
                        prevWire.RolledLengthChild += wire.TotalLength2D;
                        prevWire.RolledWireChildren.Add(wire);
                        prevWire.withKoyaura = true;
                        removeWires.Add(wire);
                    }
                    continue;
                }
                else 
                {
                    prevWire = wire;
                }
            }

            if (removeWires.Count > 0)
                removeWires.ForEach(p => wires.Remove(p));

            prevWire = new Wire();
            removeWires = new List<Wire>();

            //階またぎ位置の二本を探す
            //立ち上げ位置が1.5m以下なら上階に巻き取る
            foreach (var wire in wires)
            {
                if (wires.IndexOf(wire) == 0)
                {
                    prevWire = wire;
                    continue;
                }

                if (wire.Floor != prevWire.Floor)
                {
                    var underWire = new Wire();
                    var upperWire = new Wire();

                    if (wire.Floor < prevWire.Floor)
                    {
                        underWire = wire;
                        upperWire = prevWire;
                    }
                    else
                    {
                        underWire = prevWire;
                        upperWire = wire;
                    }
                    var underWireIdx = wires.IndexOf(underWire);
                    var upperWireIdx = wires.IndexOf(upperWire);

                    //立ち上げ部分が1.5m以下なら上にもっていく
                    if (underWire.Length2D <= 1500)
                    {
                        //JB接続位置ならロールしない
                        if (underWireIdx == 0 && symbol.Parent.IsJointBox)
                            break;

                        if (underWireIdx == wires.Count - 1 && symbol.IsJointBox)
                            break;

                        if (underWireIdx < upperWireIdx)
                        {
                            //二階が子側のとき、子側に一回だけまとめる
                            wires = this.SummarizedWireForChildOnce(symbol, wires, underWireIdx);
                        }
                        else
                        {
                            wires[upperWireIdx].RolledLengthChild += underWire.TotalLength2D;
                            wires[upperWireIdx].RolledWireChildren.Add(underWire);
                            wires.Remove(underWire);
                        }
                    }
                    //二階が子側で、階またぎの巻き取りがなく、二階部分のみ1.5m以下の場合、
                    //子側に巻き取りが可能であれば巻き取る
                    //※そのままだと1階側に行ってしまうので
                    //※親側のときは通常処理にいくのでOK
                    else if (upperWire.Length2D <= 1500) 
                    {
                        if (underWireIdx < upperWireIdx && upperWireIdx < wires.Count - 1)
                            wires = this.SummarizedWireForChildOnce(symbol, wires, upperWireIdx);
                    }
                    break;
                }
                prevWire = wire;
            }
            return wires;
        }

        //指定位置から子側に向かって一回だけ巻き取りをやる
        private List<Wire> SummarizedWireForChildOnce(Symbol symbol, List<Wire> wires, int startIdx)
        {
            List<Wire> resultWires = new List<Wire>();

            var prevWire = new Wire();
            var summaryWires = new List<Wire>();
            decimal summaryLength = 0;
            bool finished = false; //1回しかやらないので
            for (var i = 0; i < wires.Count; i++)
            {
                var wire = wires[i];
                if (i < startIdx) 
                {
                    resultWires.Add(wire);
                    continue;
                }

                if (finished)
                {
                    resultWires.Add(wire);
                    continue;
                }

                //巻き取り済みがあるとき、継続可能かチェックする
                if (summaryWires.Count > 0)
                {
                    //ロール1回目は制限なし
                    if (summaryWires.Count == 1)
                    {
                        summaryWires.Add(wire);
                        summaryLength += wire.TotalLength2D;
                    }
                    //ロール二回目以降は、
                    //①ここまでの合計が1.5m以下 or
                    //②対象が1.5m以下かつ足したとき2m以下なら巻き取り
                    else
                    {
                        if (summaryLength <= 1500 || (wire.TotalLength2D <= 1500 && summaryLength + wire.TotalLength2D <= 2000))
                        {
                            summaryWires.Add(wire);
                            summaryLength += wire.TotalLength2D;
                        }
                        else
                        {
                            resultWires.Add(this.CombineMargedWires(summaryWires, false));
                            resultWires[resultWires.Count - 1].IsInhibitRoll = true; //あとで動いてしまわないように
                            summaryWires = new List<Wire>();
                            summaryLength = 0;
                            finished = true;
                        }
                    }
                }

                //巻き取りがないorクリアされていたら、1.5mチェック
                if (summaryWires.Count == 0)
                {
                    if (wire.IsInhibitRoll || finished)
                    {
                        resultWires.Add(wire);
                    }
                    else if (wire.TotalLength2D <= 1500)
                    {
                        summaryWires.Add(wire);
                        summaryLength += wire.TotalLength2D;
                    }
                    else
                    {
                        resultWires.Add(wire);
                    }
                }

                //末尾で巻き取りが残っていたらまとめる
                if (i == wires.Count - 1 && summaryWires.Count > 0)
                {
                    resultWires.Add(this.CombineMargedWires(summaryWires, false));
                    resultWires[resultWires.Count - 1].IsInhibitRoll = true;
                    finished = true;
                }
            }
            return resultWires;
        }

        //DLtoDL
        public List<Wire> SummarizedWireDLtoDL(Symbol symbol, List<Wire> dividedWires)
        {
            List<Wire> resultWires = new List<Wire>();

            if (dividedWires.Count <= 1)
                return dividedWires;

            //基点のワイヤーを探す
            var baseWires = dividedWires.FindAll(p => p.TotalLength2D > 1500 && p.CeilingPanel != null);
            if (baseWires.Count == 0)
            {
                Decimal maxLength = 0;
                Wire baseWire = new Wire();
                foreach (var wk in dividedWires)
                {
                    if (wk.CeilingPanel == null)
                        continue;

                    if (maxLength < wk.TotalLength2D)
                    {
                        maxLength = wk.TotalLength2D;
                        baseWire = wk;
                    }
                }
                baseWires.Add(baseWire);
            }

            List<int> removeIdxes = new List<int>();
            //基点の両端をチェックして、1.5m以下の線を巻き取る
            foreach (var baseWire in baseWires)
            {
                var parentIdx = dividedWires.IndexOf(baseWire);
                //順方向
                for (var i = parentIdx + 1; i < dividedWires.Count; i++)
                {
                    if (dividedWires[i].CeilingPanel == null)
                        continue;

                    if (dividedWires[i].TotalLength2D <= 1500)
                    {
                        dividedWires[parentIdx].RolledLengthChild += dividedWires[i].TotalLength2D;
                        dividedWires[parentIdx].RolledWireChildren.Add(dividedWires[i]);
                        removeIdxes.Add(i);

                        if (i + 1 == dividedWires.Count)
                            symbol.CeilingPanel = dividedWires[parentIdx].CeilingPanel;
                    }
                    else
                    {
                        break; //対象外にぶつかったら終了
                    }
                }

                //逆方向
                for (var i = parentIdx - 1; i >= 0; i--)
                {
                    if (dividedWires[i].CeilingPanel == null)
                        continue;

                    if (dividedWires[i].TotalLength2D <= 1500)
                    {
                        dividedWires[parentIdx].RolledLengthParent += dividedWires[i].TotalLength2D;
                        dividedWires[parentIdx].RolledWireParents.Add(dividedWires[i]);
                        removeIdxes.Add(i);

                        if (i == 0)
                            symbol.Parent.CeilingPanel = dividedWires[parentIdx].CeilingPanel;
                    }
                    else
                    {
                        break; //対象外にぶつかったら終了
                    }
                }
                //巻き取りが発生していたら配列を変更してやり直し
                if (removeIdxes.Count > 0)
                {
                    removeIdxes.Sort();
                    removeIdxes.Reverse();
                    foreach (var idx in removeIdxes)
                        dividedWires.RemoveAt(idx);

                    resultWires = this.SummarizedWireDLtoDL(symbol, dividedWires);
                    break;
                }
            }
            resultWires = dividedWires;
            return resultWires;
        }

        /// <summary>
        /// </summary>
        /// <param name="symbol">シンボル</param>
        /// <param name="divideWires">分割したワイヤー。親側のワイヤーから順に並んでいる</param>
        /// <param name="extraLength">加算する変数</param>
        public void IncludeLengthOfCeilingPanelForward(Symbol symbol, ref List<Wire> divideWires, ref decimal extraLength)
        {
            if (divideWires.Count == 0)
                return;

            Wire lastWire = divideWires[divideWires.Count - 1];
            //シンボルと繋がっているワイヤーがなかったら既にextraLengthに巻き取られている。
            if (!lastWire.IsConnected(symbol))
            {
                symbol.CeilingPanel = lastWire.CeilingPanel;
                divideWires[divideWires.Count - 1].RolledLengthChild = extraLength; //実体がないのでRolledWireには足さない
                extraLength = 0;
                return;
            }

            if (divideWires.Count == 1)
            {
                //床下で一本しかない場合は旧配線なので、天井設置部分が隠れている。
                divideWires[0].CeilingPanel = symbol.CeilingPanel;
                return;
            }

            //シンボルとワイヤーが繋がってたら、一つ前のパネルに巻き取る。
            int index = divideWires.Count - 2;
            Wire nextWire = null;
            for (int i = index; 0 <= i; i--)
            {
                Wire w = divideWires[i];
                if (w.CeilingPanel == null)
                    continue;

                nextWire = w;
                break;
            }

            if (nextWire == null)
            {
                List<Wire> notCeilingPanelWires = divideWires.FindAll(p => p != lastWire);
                string msg = Messages.WireNotOnCeilingPanel(notCeilingPanelWires);
                throw new ApplicationException(msg);
            }

            var rolledLength = lastWire.TotalLength2D;
            symbol.CeilingPanel = nextWire.CeilingPanel;
            divideWires.Remove(lastWire);
            lastWire.IsNotInstalledCeiling = true;
            divideWires[divideWires.Count - 1].RolledLengthChild = rolledLength;
            divideWires[divideWires.Count - 1].RolledWireChildren.Add(lastWire);
        }
        #endregion
    }
}
