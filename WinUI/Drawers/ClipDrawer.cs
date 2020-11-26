using System;
using System.Collections.Generic;
using System.Text;
using Edsa.AutoCadProxy;
using System.IO;

namespace SocketPlan.WinUI
{
    public class ClipDrawer
    {
        public static void DrawClip()
        {
            WindowController2.BringAutoCadToTop();
            AutoCad.Command.Prepare();
            AutoCad.Command.SetCurrentLayoutToModel();

            AutoCad.Db.LayerTableRecord.Make(Const.Layer.電気_抜き出し, CadColor.BlackWhite, Const.LineWeight.Default);
            AutoCad.Command.SetCurrentLayer(Const.Layer.電気_抜き出し);

            var clip = UnitWiring.Masters.Equipments.Find(p => p.EquipmentKindId == Const.EquipmentKind.CLIP);

            //直交モードを一時的に有効にする
            var orhtoMode = AutoCad.Db.Database.IsOrthogonalMode();
            if (!orhtoMode)
                AutoCad.Db.Database.SetOrthogonalMode(true);

            //白黒○を置く
            AutoCad.Command.InsertBlockWithRotation(Paths.GetSystemBlockPath(clip.Block.FileName));

            AutoCad.Status.WaitFinish();
            if (AutoCad.Status.IsCanceled())
                return;

            var blockId = AutoCad.Selection.GetLastObjectId();
            if (!blockId.HasValue)
                throw new ApplicationException(Messages.ClipNotSet());

            //直交モードを復元する
            if (!orhtoMode)
                AutoCad.Db.Database.SetOrthogonalMode(orhtoMode);

            //枠を置く
            var result = AutoCad.Drawer.DrawRectangle(" w 50");

            if (result.Status == DrawStatus.Canceled)
                return;

            if (result.Status == DrawStatus.Failed)
                throw new ApplicationException(Messages.ClipNotSet());

            var rectangleId = result.ObjectId;

            AutoCad.Db.Entity.SetColor(rectangleId, CadColor.Orange);

            //矢印をひく
            var blockPoint = AutoCad.Db.BlockReference.GetPosition(blockId.Value);
            var rectBound = AutoCad.Db.Entity.GetEntityBound(rectangleId);

            var rectPoint = new PointD(rectBound[0].X, rectBound[0].Y); //枠の左下

            //ブロックの下側に枠がある時
            if (rectBound[1].Y < blockPoint.Y)
                rectPoint.Y = rectBound[1].Y;

            //ブロックの左側に枠がある時
            if (rectBound[1].X < blockPoint.X)
                rectPoint.X = rectBound[1].X;

            var points = new List<PointD>();
            points.Add(rectPoint);
            points.Add(blockPoint);

            var arrowPath = Paths.GetSystemBlockPath(@"Original\LeaderArrow.dwg");
            var blockName = Path.GetFileNameWithoutExtension(arrowPath);
            if (!AutoCad.Db.BlockTable.Exist(blockName))
            {
                //画面の中心の座標を求める(他に方法ないのかなぁ)
                var center = AutoCad.Db.ViewportTableRecord.GetCenterPointOfModelLayout();

                AutoCad.Command.SendLineEsc("-insert\n" + arrowPath + "\n" + center.X + "," + center.Y + "\n1.0\n\n0.0");
                AutoCad.Command.SendLine("erase\nlast\n");
            }

            var arrowId = AutoCad.Db.BlockTable.GetId(blockName);
            AutoCad.Db.Leader.Make(points, arrowId);

            AutoCad.Command.Refresh();
        }

        public static int GetClipRectangle(Symbol baseSymbol, List<int> leaders, List<int> rectangles)
        {
            //TODO 矢印が繋がっているかを判定するのはシビアすぎるから拡張データに抜き出し枠のハンドルIDを持たせよう
            int leader = 0;
            foreach (var l in leaders)
            {
                var vertexCount = AutoCad.Db.Leader.GetVertexNumber(l);
                if (vertexCount <= 1)
                    continue;

                var start = AutoCad.Db.Leader.GetFirstVertex(l);
                var end = AutoCad.Db.Leader.GetLastVertex(l);

                if (baseSymbol.Contains(start))
                    leader = l;

                if (baseSymbol.Contains(end))
                    leader = l;
            }

            if (leader == 0)
                Validation.NotFoundArrowOfClip(baseSymbol);

            PointD startPoint = AutoCad.Db.Leader.GetStartPoint(leader);
            PointD endPoint = AutoCad.Db.Leader.GetEndPoint(leader);

            //引き出し線に繋がっている抜き出し枠を探す
            int rectangle = 0;
            foreach (var r in rectangles)
            {
                if (startPoint.IsIn(r))
                    rectangle = r;

                if (endPoint.IsIn(r))
                    rectangle = r;
            }

            //矢印を抜き出し枠の微妙に外に出すのが流行っているようだ。だから、矢印をちょっと延ばして判定してみよう。
            if (rectangle == 0)
            {
                //startPoint側が矢印の先端です
                var vector = startPoint.Minus(endPoint);
                var longer = Math.Abs(vector.X < vector.Y ? vector.Y : vector.X);
                var unitVec = new PointD(vector.X / longer, vector.Y / longer);
                var extend = new PointD(unitVec.X * 50, unitVec.Y * 50);
                var exStartPoint = startPoint.Plus(extend);
                foreach (var r in rectangles)
                {
                    if (exStartPoint.IsIn(r))
                        rectangle = r;
                }
            }

            if (rectangle == 0)
                Validation.NotFoundFrameOfClip(baseSymbol);

            return rectangle;
        }
    }
}
