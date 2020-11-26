using System;
using System.Collections.Generic;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public class CeilingPanel
    {
        #region コンストラクタ

        private CeilingPanel() 
        {
        }

        private CeilingPanel(int floor, string layerName, int objectId)
        {
            this.ObjectId = objectId;
            this.Floor = floor;
            this.LayerName = layerName;
            this.CeilingReceivers = this.GetCeilingReceivers(layerName, floor);

            var bound = AutoCad.Db.Entity.GetEntityBound(objectId);
            this.PositionBottomLeft = bound[0];
            this.PositionTopRight = bound[1];
        }

        #endregion

        #region プロパティ

        public int ObjectId { get; set; }
        public int Floor { get; set; }
        public string LayerName { get; set; }
        public List<CeilingReciever> CeilingReceivers { get; set; }

        public PointD PositionBottomLeft { get; set; }
        public PointD PositionTopRight { get; set; }
        public PointD PositionCenter { get { return new PointD((PositionBottomLeft.X + PositionTopRight.X) / 2, (PositionBottomLeft.Y + PositionTopRight.Y) / 2); } }

        public double Width { get { return Math.Abs(this.PositionBottomLeft.X - this.PositionTopRight.X); } }
        public double Height { get { return Math.Abs(this.PositionBottomLeft.Y - this.PositionTopRight.Y); } }
        public string Name { get { return this.LayerName.Replace(Const.Layer.電気_天井パネル + "_", string.Empty); } }

        public int Number
        {
            get
            {
                if (this.Name.Split('-').Length < 2)
                    return 0;

                return Convert.ToInt32(this.Name.Split('-')[1]);
            }
        }

        private List<CeilingReciever> GetCeilingReceivers(string layerName, int floor) 
        {
            List<CeilingReciever> result = new List<CeilingReciever>();
            var ids = Filters.GetCeilingReceiverIds(layerName);

            foreach (var id in ids) 
            {
                result.Add(new CeilingReciever(this, id, floor));
            }
            return result;        
        }

        #endregion

        #region publicメソッド

        public bool Contains(PointD point)
        {
            return point.IsIn(this.ObjectId);
        }

        public bool IsOutsideForPolyline(int lineId)
        {
            if (AutoCad.Db.Polyline.IsIntersect(this.ObjectId, lineId))
                return false;
            
            var vertexes = AutoCad.Db.Polyline.GetVertex(lineId);

            if (!(vertexes.Exists(p => this.Contains(p))))
                return true;

            return false;
        }

        public Direction GetAspect(PointD point)
        {
            //パラメータを取得する
            var parameter = AutoCad.Db.Curve.GetParameter(this.ObjectId, point);
            //パネルの頂点リストを取得する
            var vertexes = AutoCad.Db.Polyline.GetVertex(this.ObjectId);

            //パラメータの前後の頂点がどっち向きに進んでいるかを取得する
            var preVertexParam = (int)Math.Floor(parameter);
            var nextVertexParam = (int)Math.Ceiling(parameter);
            var preVertex = vertexes[preVertexParam];
            var nextVertex = vertexes[nextVertexParam % vertexes.Count];
            var direction = preVertex.GetDirection(nextVertex);

            //時計回りかどうかを判定する
            var isClockwise = Calc.IsClockwise(vertexes);

            //時計回り情報と線の向き情報からどっち面かを判定する
            if (direction == Direction.Down)
                return isClockwise ? Direction.Right : Direction.Left;
            if (direction == Direction.Left)
                return isClockwise ? Direction.Down : Direction.Up;
            if (direction == Direction.Up)
                return isClockwise ? Direction.Left : Direction.Right;
            if (direction == Direction.Right)
                return isClockwise ? Direction.Up : Direction.Down;

            return Direction.Unknown;
        }

        #endregion

        #region staticメソッド

        public static List<CeilingPanel> GetAll(int floor)
        {
            var list = new List<CeilingPanel>();

            var layerIds = CeilingPanel.GetLayers();
            foreach (var layerId in layerIds)
            {
                var layerName = AutoCad.Db.LayerTableRecord.GetLayerName(layerId);
                if (layerName.Contains(Const.Layer.電気_天井パネル補足))
                    continue;

                var lines = Filters.GetCeilingPanelLineIds(layerName);
                if (lines.Count == 0)
                    continue;

                if (2 <= lines.Count)
                    throw new ApplicationException("There are 2 or more ceiling panel lines for " + layerName);

                var panel = new CeilingPanel(floor, layerName, lines[0]);

                list.Add(panel);
            }

            list.Sort((a, b) => a.Name.CompareTo(b.Name));

            return list;
        }

        public static List<int> GetLayers()
        {
            List<int> layers = new List<int>();
            foreach (var layerId in AutoCad.Db.LayerTable.GetLayerIds())
            {
                var name = AutoCad.Db.LayerTableRecord.GetLayerName(layerId);

                if (name.Contains(Const.Layer.電気_天井パネル))
                    layers.Add(layerId);
            }

            return layers;
        }

        public static void DrawCeilingPanel(string panelNo, bool isPolyline)
        {
            WindowController2.BringAutoCadToTop();
            AutoCad.Command.Prepare();
            AutoCad.Command.SetCurrentLayoutToModel();

            //レイヤーを追加する
            var layerName = Const.Layer.電気_天井パネル + "_" + panelNo;
            AutoCad.Db.LayerTableRecord.Make(layerName, CadColor.Red, Const.LineWeight.Default);
            AutoCad.Command.SetCurrentLayer(layerName);

            DrawResult result;

            //天井パネル枠を書いてもらう
            if (isPolyline)
                result = AutoCad.Drawer.DrawPolyline(true);
            else
                result = AutoCad.Drawer.DrawRectangle("");

            if (result.Status == DrawStatus.Canceled)
                return;

            if (result.Status == DrawStatus.Failed)
                throw new ApplicationException(Messages.CeilingPanelNotSet());

            var lineId = result.ObjectId;

            //線の始点を終点をくっつける
            if (!AutoCad.Db.Polyline.IsClosed(lineId))
                AutoCad.Db.Polyline.SetClose2(lineId);

            //線種を変える
            if (!AutoCad.Db.LinetypeTable.Exist(Const.Linetype.CeilingPanel))
                AutoCad.Db.LinetypeTable.Add(Const.Linetype.CeilingPanel);

            AutoCad.Db.Entity.SetLineType(lineId, Const.Linetype.CeilingPanel);

            //天井パネル番号を書く
            var center = AutoCad.Db.Entity.GetCenter(lineId);
            AutoCad.Db.TextStyleTableRecord.Make(Const.Font.MSGothic, Const.Font.MSGothic);
            AutoCad.Db.Text.Make(panelNo, 250, center, Align.中央);
        }

        public static void DrawCeilingReceiver(string panelNo)
        {
            WindowController2.BringAutoCadToTop();
            AutoCad.Command.Prepare();
            AutoCad.Command.SetCurrentLayoutToModel();

            //レイヤーを追加する
            var layerName = Const.Layer.電気_天井パネル + "_" + panelNo;
            AutoCad.Db.LayerTableRecord.Make(layerName, CadColor.Red, Const.LineWeight.Default);
            AutoCad.Command.SetCurrentLayer(layerName);

            //繰り返し書く
            while (true)
            {
                //天井受けを書いてもらう
                var result = AutoCad.Drawer.DrawRectangle("");
                if (result.Status == DrawStatus.Canceled)
                    return;

                if (result.Status == DrawStatus.Failed)
                    throw new ApplicationException(Messages.CeilingReceiverNotSet());

                var rectangleId = result.ObjectId;

                //線種を変える
                if (!AutoCad.Db.LinetypeTable.Exist(Const.Linetype.CeilingReceiver))
                    AutoCad.Db.LinetypeTable.Add(Const.Linetype.CeilingReceiver);

                AutoCad.Db.Entity.SetLineType(rectangleId, Const.Linetype.CeilingReceiver);
            }
        }

        public static void CleanUpCeilingPanelDrawing()
        {
            WindowController2.BringAutoCadToTop();
            AutoCad.Command.Prepare();
            AutoCad.Command.SetCurrentLayoutToModel();

            var layerIds = AutoCad.Db.LayerTable.GetLayerIds();

            foreach (var layerId in layerIds)
            {
                var layerName = AutoCad.Db.LayerTableRecord.GetLayerName(layerId);
                AutoCad.Db.LayerTableRecord.SetColor(layerId, CadColor.Gray);

                if (layerName == Const.Layer.Zero)
                    continue;

                if (layerName.Contains(Const.Layer.電気))
                    continue;

                AutoCad.Db.LayerTableRecord.SetLayerName(layerId, Const.Layer.電気_天井パネル補足 + "_" + layerName);
            }

            AutoCad.Command.SetColorAll(CadColor.ByLayer);
        }

        public static void CopyCeilingPanel()
        {
            WindowController2.BringAutoCadToTop();

            AutoCad.Command.SendLineEsc("copybase");

            //コピー基点入力待ち
            AutoCad.Status.WaitFinish();
        }

        /// <summary>デバッグしやすいようにOverrideしとく。実際の処理では使わないよ。</summary>
        public override string ToString()
        {
            string str = "【Floor = " + Floor + "】,【Name = " + Name + "】, 【PositionBottomLeft = " + PositionBottomLeft.ToString() + "】,【PositionTopRight = " + PositionTopRight.ToString() + "】";
            return str;
        }

        #endregion
    }
}
