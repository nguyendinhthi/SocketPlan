using System;
using System.Collections.Generic;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public class ViewController
    {
        /// <summary>指定したレイヤーの表示状態を取得する</summary>
        public static bool IsVisible(string layerName)
        {
            if (!AutoCad.Db.LayerTable.Exist(layerName))
                return false;

            var layerId = AutoCad.Db.LayerTable.GetLayerId(layerName);
            var isFrozen = AutoCad.Db.LayerTableRecord.IsFrozen(layerId);

            return !isFrozen;
        }

        /// <summary>指定したレイヤーを指定した表示状態にする</summary>
        public static void ChangeVisible(string layerName, bool visible)
        {
            if (!AutoCad.Db.LayerTable.Exist(layerName))
                return;

            var layerId = AutoCad.Db.LayerTable.GetLayerId(layerName);
            var isFrozen = AutoCad.Db.LayerTableRecord.IsFrozen(layerId);
            
            if (isFrozen == !visible) //すでに指定した表示状態になっていたら抜ける
                return;

            if(!visible) //現在層を非表示にすることができないので、念のため現在層を0にしておく
                AutoCad.Command.SetCurrentLayer(Const.Layer.Zero);

            AutoCad.Db.LayerTableRecord.SetFrozen(layerId, !visible);
        }

        /// <summary>指定したレイヤーの表示状態を切り替える</summary>
        public static void ChangeVisible(string layerName)
        {
            if (!AutoCad.Db.LayerTable.Exist(layerName))
                return;

            var layerId = AutoCad.Db.LayerTable.GetLayerId(layerName);
            var isFrozen = AutoCad.Db.LayerTableRecord.IsFrozen(layerId);

            if(!isFrozen)
                AutoCad.Command.SetCurrentLayer(Const.Layer.Zero);

            AutoCad.Db.LayerTableRecord.SetFrozen(layerId, !isFrozen);

            AutoCad.Command.RefreshEx();
        }

        /// <summary>天井パネルの表示状態を切り替える</summary>
        public static void ChangeVisibleOfCeilingPanel()
        {
            var layers = CeilingPanel.GetLayers();
            if (layers.Count == 0)
                return;

            var isFrozen = AutoCad.Db.LayerTableRecord.IsFrozen(layers[0]);

            ViewController.ChangeVisibleOfCeilingPanel(isFrozen);
        }

        /// <summary>天井パネルを指定した表示状態にする</summary>
        public static void ChangeVisibleOfCeilingPanel(bool isVisible)
        {
            AutoCad.Command.Prepare();
            AutoCad.Command.SetCurrentLayer("0"); //現在の画層をフリーズすることはできないので。

            var layers = CeilingPanel.GetLayers();
            foreach (var layerid in layers)
            {
                AutoCad.Db.LayerTableRecord.SetFrozen(layerid, !isVisible);
            }

            AutoCad.Command.RefreshEx();
        }

        /// <summary>指定したレイヤーの図形を全て削除する</summary>
        public static void EraseAll(string layerName)
        {
            foreach (var id in AutoCad.Db.Entity.GetAll(layerName))
            {
                AutoCad.Db.Object.Erase(id);
            }

            AutoCad.Command.RefreshExEx();
        }

        /// <summary>指定したレイヤーだけが表示状態になっていたらTrue</summary>
        public static bool IsDisplayLayerOnly(string layerName)
        {
            foreach (var layerId in AutoCad.Db.LayerTable.GetLayerIds())
            {
                var name = AutoCad.Db.LayerTableRecord.GetLayerName(layerId);

                if (name == layerName)
                {
                    if (AutoCad.Db.LayerTableRecord.IsFrozen(layerId))
                        return false;
                }
                else
                {
                    if (!AutoCad.Db.LayerTableRecord.IsFrozen(layerId))
                        return false;
                }
            }

            return true;
        }

        /// <summary>指定したレイヤーのみを表示させる</summary>
        public static void DisplayLayerOnly(string layerName)
        {
            if (!AutoCad.Db.LayerTable.Exist(layerName))
                return;

            //指定したレイヤーを表示させ、現在のレイヤーにする
            var layerId = AutoCad.Db.LayerTable.GetLayerId(layerName);
            if (AutoCad.Db.LayerTableRecord.IsFrozen(layerId))
                AutoCad.Db.LayerTableRecord.SetFrozen(layerId, false);

            AutoCad.Command.SetCurrentLayer(layerName);

            //全レイヤーを非表示にする
            foreach (var id in AutoCad.Db.LayerTable.GetLayerIds())
            {
                if (id == layerId)
                    continue;

                AutoCad.Db.LayerTableRecord.SetFrozen(id, true);
            }

            AutoCad.Command.RefreshExEx();
        }

        /// <summary>全レイヤーを表示させる</summary>
        public static void DisplayLayerAll()
        {
            foreach (var layerId in AutoCad.Db.LayerTable.GetLayerIds())
            {
                //現在層をFrozenするとエラる。現在層がFrozenになっていることはない。
                if (!AutoCad.Db.LayerTableRecord.IsFrozen(layerId))
                    continue;

                AutoCad.Db.LayerTableRecord.SetFrozen(layerId, false);
            }

            AutoCad.Command.RefreshExEx();
        }

        /// <summary>不要なテキストを削除する</summary>
        public static void EraseUnnecessaryTexts()
        {
            if (AutoCad.Db.LayerTable.Exist(Const.Layer._2_E_文字))
            {
                foreach (var id in AutoCad.Db.Text.GetAll())
                {
                    var text = AutoCad.Db.Text.GetText(id);
                    bool delete = false;
                    //床暖房
                    if (text.Contains("床暖房"))
                        delete = true;

                    //タオルリング
                    if (text.Contains("ﾀｵﾙﾘﾝｸﾞ"))
                        delete = true;

                    //施-
                    if (text.Contains("施-"))
                        delete = true;

                    if (delete)
                        AutoCad.Db.Object.Erase(id);
                }
            }

            AutoCad.Command.RefreshExEx();
        }

        /// <summary>グリッドの表示状態を切り替える</summary>
        public static void ChangeVisibleOfGrid()
        {
            //レイヤのIdを取得する
            var layerId = AutoCad.Db.LayerTable.GetLayerId(Const.Layer.電気_Grid);

            //作図の邪魔にならないようにレイヤをロックしてしまおう
            if (!AutoCad.Db.LayerTableRecord.IsLocked(layerId))
                AutoCad.Db.LayerTableRecord.SetLock(layerId, true);

            //レイヤの表示・非表示を切り替える
            var isFrozen = AutoCad.Db.LayerTableRecord.IsFrozen(layerId);
            if (!isFrozen)
                AutoCad.Command.SetCurrentLayer(Const.Layer.Zero);

            AutoCad.Db.LayerTableRecord.SetFrozen(layerId, !isFrozen);
            AutoCad.Command.RefreshEx();
        }

        #region MakeGrid

        public static void MakeGrid()
        {
            var lines = ViewController.Select2Line();
            if (lines == null)
                return;

            var gridBasePoint = GetGridBasePoint(lines);
            var gridArea = GetGridArea(lines);

            var startPoint = GetGridStartPoint(gridBasePoint, gridArea);
            var endPoint = GetGridEndPoint(gridBasePoint, gridArea);

            var layerId = AutoCad.Db.LayerTableRecord.Make(Const.Layer.電気_Grid, CadColor.BlackWhite, Const.LineWeight.Default);
            AutoCad.Command.SetCurrentLayer(Const.Layer.電気_Grid);

            DrawGridLines(startPoint, endPoint);
            DrawCircleNumber(startPoint, endPoint);

            AutoCad.Db.LayerTableRecord.SetLock(layerId, true);
        }

        /// <summary>ユーザに線を2本選択させ、正しく選択できていたら{縦線,横線}のリストを返し、間違って選択していたらnullを返す。</summary>
        private static List<int> Select2Line()
        {
            var objectIds = AutoCad.Selection.SelectObjects();
            if (AutoCad.Status.IsCanceled())
                return null;

            var lines = objectIds.FindAll(p => AutoCad.Db.Line.IsType(p));
            if (lines.Count != 2)
                throw new ApplicationException(Messages.PleaseSelect2Lines());

            var ori0 = Calc.GetOrientation(lines[0]);
            var ori1 = Calc.GetOrientation(lines[1]);

            if (ori0 == Edsa.AutoCadProxy.Orientation.Vertical &&
                ori1 == Edsa.AutoCadProxy.Orientation.Horizontal)
                return new List<int> { lines[0], lines[1] };
            else if (ori0 == Edsa.AutoCadProxy.Orientation.Horizontal &&
                     ori1 == Edsa.AutoCadProxy.Orientation.Vertical)
                return new List<int> { lines[1], lines[0] };
            else
                throw new ApplicationException(Messages.PleaseSelect2Lines());
        }

        /// <summary>グリッドの基点の座標を取得する</summary>
        private static PointD GetGridBasePoint(List<int> lineIds)
        {
            var verticalLineId = lineIds[0];
            var horizontalLineId = lineIds[1];

            var v = AutoCad.Db.Line.GetStartPoint(verticalLineId);
            var h = AutoCad.Db.Line.GetEndPoint(horizontalLineId);

            return new PointD(v.X, h.Y);
        }

        /// <summary>グリッドを描く範囲の左下と右上の座標を取得する</summary>
        private static List<PointD> GetGridArea(List<int> lineIds)
        {
            var layerName = AutoCad.Db.Line.GetLayerName(lineIds[0]);
            var objectIds = Filters.GetAll(layerName);

            PointD leftDown = AutoCad.Db.Line.GetStartPoint(lineIds[0]);
            PointD rightUp = leftDown.Clone();

            foreach (var objectId in objectIds)
            {
                var bound = AutoCad.Db.Line.GetEntityBound(objectId);
                if (bound[0].X < leftDown.X)
                    leftDown.X = bound[0].X;

                if (bound[0].Y < leftDown.Y)
                    leftDown.Y = bound[0].Y;

                if (rightUp.X < bound[1].X)
                    rightUp.X = bound[1].X;

                if (rightUp.Y < bound[1].Y)
                    rightUp.Y = bound[1].Y;
            }

            return new List<PointD> { leftDown, rightUp };
        }

        /// <summary>グリッドの一番左下の交点の座標を返す</summary>
        private static PointD GetGridStartPoint(PointD gridBasePoint, List<PointD> gridArea)
        {
            var areaStartPoint = gridBasePoint.Clone();
            while (gridArea[0].X <= areaStartPoint.X - Const.GRID_INTERVAL)
            {
                areaStartPoint.X -= Const.GRID_INTERVAL;
            }

            while (gridArea[0].Y <= areaStartPoint.Y - Const.GRID_INTERVAL)
            {
                areaStartPoint.Y -= Const.GRID_INTERVAL;
            }

            return areaStartPoint;
        }

        /// <summary>グリッドの一番右上の交点の座標を返す</summary>
        private static PointD GetGridEndPoint(PointD gridBasePoint, List<PointD> gridArea)
        {
            var areaEndPoint = gridBasePoint.Clone();
            while (areaEndPoint.X + Const.GRID_INTERVAL <= gridArea[1].X)
            {
                areaEndPoint.X += Const.GRID_INTERVAL;
            }

            while (areaEndPoint.Y + Const.GRID_INTERVAL <= gridArea[1].Y)
            {
                areaEndPoint.Y += Const.GRID_INTERVAL;
            }

            return areaEndPoint;
        }

        private static void DrawGridLines(PointD startPoint, PointD endPoint)
        {
            var currentPoint = startPoint.Clone();

            double GRID_DETAIL = Const.GRID_INTERVAL / 8;
            double GRID_MAWARI = Const.GRID_INTERVAL * 2;

            //縦の線を引く
            while (currentPoint.X <= endPoint.X)
            {
                var s = new PointD(currentPoint.X, startPoint.Y - GRID_MAWARI);
                var e = new PointD(currentPoint.X, endPoint.Y + GRID_MAWARI);
                var id = AutoCad.Db.Line.Make(s, e);

                var amari = (currentPoint.X - startPoint.X) % Const.GRID_INTERVAL;
                if (amari == GRID_DETAIL * 0)
                    AutoCad.Db.Line.SetColor(id, CadColor.Magenta);
                else if (amari == GRID_DETAIL * 4)
                    AutoCad.Db.Line.SetColor(id, CadColor.Green);
                else
                    AutoCad.Db.Line.SetColor(id, CadColor.Gray);

                currentPoint.X += GRID_DETAIL;
            }

            //横の線を引く
            while (currentPoint.Y <= endPoint.Y)
            {
                var s = new PointD(startPoint.X - GRID_MAWARI, currentPoint.Y);
                var e = new PointD(endPoint.X + GRID_MAWARI, currentPoint.Y);
                var id = AutoCad.Db.Line.Make(s, e);

                var amari = (currentPoint.Y - startPoint.Y) % Const.GRID_INTERVAL;
                if (amari == GRID_DETAIL * 0)
                    AutoCad.Db.Line.SetColor(id, CadColor.Magenta);
                else if (amari == GRID_DETAIL * 4)
                    AutoCad.Db.Line.SetColor(id, CadColor.Green);
                else
                    AutoCad.Db.Line.SetColor(id, CadColor.Gray);

                currentPoint.Y += GRID_DETAIL;
            }
        }

        private static void DrawCircleNumber(PointD startPoint, PointD endPoint)
        {
            var currentPoint = startPoint.Clone();

            double CIRCLE_RADIUS = (Const.GRID_INTERVAL - 150) / 2;
            double GRID_MAWARI = Const.GRID_INTERVAL * 2;

            while (currentPoint.X <= endPoint.X)
            {
                var number = (currentPoint.X - startPoint.X) / Const.GRID_INTERVAL + 1;

                var s = new PointD(currentPoint.X, startPoint.Y - (GRID_MAWARI + CIRCLE_RADIUS));
                var startCircleId = AutoCad.Db.Circle.Make(s, CIRCLE_RADIUS);
                AutoCad.Db.Circle.SetColor(startCircleId, CadColor.Cyan);
                var startTextId = AutoCad.Db.Text.Make(number.ToString(), CIRCLE_RADIUS, s, Align.中央);
                AutoCad.Db.Text.SetColor(startTextId, CadColor.Cyan);

                var e = new PointD(currentPoint.X, endPoint.Y + (GRID_MAWARI + CIRCLE_RADIUS));
                var endCircleId = AutoCad.Db.Circle.Make(e, CIRCLE_RADIUS);
                AutoCad.Db.Circle.SetColor(endCircleId, CadColor.Cyan);
                var endTextId = AutoCad.Db.Text.Make(number.ToString(), CIRCLE_RADIUS, e, Align.中央);
                AutoCad.Db.Text.SetColor(endTextId, CadColor.Cyan);

                currentPoint.X += Const.GRID_INTERVAL;
            }

            while (currentPoint.Y <= endPoint.Y)
            {
                var number = (currentPoint.Y - startPoint.Y) / Const.GRID_INTERVAL + 1;

                var s = new PointD(startPoint.X - (GRID_MAWARI + CIRCLE_RADIUS), currentPoint.Y);
                var startCircleId = AutoCad.Db.Circle.Make(s, CIRCLE_RADIUS);
                AutoCad.Db.Circle.SetColor(startCircleId, CadColor.Cyan);
                var startTextId = AutoCad.Db.Text.Make(number.ToString(), CIRCLE_RADIUS, s, Align.中央);
                AutoCad.Db.Text.SetColor(startTextId, CadColor.Cyan);

                var e = new PointD(endPoint.X + (GRID_MAWARI + CIRCLE_RADIUS), currentPoint.Y);
                var endCircleId = AutoCad.Db.Circle.Make(e, CIRCLE_RADIUS);
                AutoCad.Db.Circle.SetColor(endCircleId, CadColor.Cyan);
                var endTextId = AutoCad.Db.Text.Make(number.ToString(), CIRCLE_RADIUS, e, Align.中央);
                AutoCad.Db.Text.SetColor(endTextId, CadColor.Cyan);

                currentPoint.Y += Const.GRID_INTERVAL;
            }
        }

        #endregion
    }
}
