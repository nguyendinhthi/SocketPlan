using System;
using System.Collections.Generic;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public class LeadLineDrawer
    {
        /// <summary>
        /// 色指定ができる引出線描画機能
        /// </summary>
        /// <param name="blockRefId"></param>
        /// <param name="textId"></param>
        /// <param name="determinePosition"></param>
        /// <param name="lineColor">引出線の色</param>
        public static void DrawLeadLine(int blockRefId, int textId, bool determinePosition, CadColor? lineColor)
        {
            var attBound = AutoCad.Db.Entity.GetEntityBound(textId);

            var symbol = new Symbol(blockRefId);

            var point1st = new PointD();
            var point2nd = new PointD(attBound[0].X, attBound[0].Y); //テキストの左下
            var point3rd = new PointD(attBound[1].X, attBound[0].Y); //テキストの右下

            if (symbol.PositionTop.Y < attBound[0].Y)
                point1st = symbol.PositionTop;
            else if (attBound[1].Y < symbol.PositionBottom.Y)
                point1st = symbol.PositionBottom;
            else if (symbol.PositionLeft.X < attBound[0].X)
                point1st = symbol.PositionRight;
            else
                point1st = symbol.PositionLeft;

            //ブロックの左側にテキストがある時
            if (attBound[1].X < symbol.PositionLeft.X)
            {
                point2nd.X = attBound[1].X;
                point3rd.X = attBound[0].X;
            }

            if (determinePosition)
            {
                var distance = Edsa.AutoCadProxy.Calc.GetDistance(point1st, point2nd);
                if (distance < 300)
                    return;
            }

            var points = new List<PointD>();
            points.Add(point1st);
            points.Add(point2nd);
            points.Add(point3rd);

            AutoCad.Command.SetCurrentLayer(Const.Layer.電気_コメント);
            int leaderId = AutoCad.Db.Leader.Make(points, null, Const.LineWeight._0_30);

            if (lineColor.HasValue)
                AutoCad.Db.Leader.SetColor(leaderId, lineColor.Value);

            AutoCad.Command.Refresh();
        }
    }
}
