using System;
using System.Collections.Generic;
using System.Text;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public class CeilingReciever : CadObject
    {
        public bool IsOnCeilingPanelEdge { get; set; }
        public CeilingPanel ceilingPanel { get; set; }
        private PointD PositionBottomLeft { get; set; }
        private PointD PositionTopRight { get; set; }
        private PointD PositionCenter { get { return new PointD((PositionBottomLeft.X + PositionTopRight.X) / 2, (PositionBottomLeft.Y + PositionTopRight.Y) / 2); } }
        private Orientation RecieverOrientation
        {
            get
            {
                if (Math.Abs(this.PositionTopRight.X - this.PositionBottomLeft.X) > Math.Abs(this.PositionTopRight.Y - this.PositionBottomLeft.Y))
                    return Orientation.Horizontal;
                else
                    return Orientation.Vertical;
            }
        }

        public CeilingReciever() : base() 
        {
        }

        public CeilingReciever(CeilingPanel panel, int objectId, int floor)
            : base(objectId, floor)
        {
            this.ObjectId = objectId;
            this.Floor = floor;

            var bound = AutoCad.Db.Entity.GetEntityBound(objectId);
            this.PositionBottomLeft = bound[0];
            this.PositionTopRight = bound[1];
            this.IsOnCeilingPanelEdge = this.GetPanelEdgeFlg(panel);
            this.ceilingPanel = panel;
        }

        private bool GetPanelEdgeFlg(CeilingPanel panel)
        {
            bool result = false;
            var lineIds = new List<int>();
            var points = new List<PointD>();
            if (this.RecieverOrientation == Orientation.Horizontal)
            {
                points = new List<PointD>();
                points.Add(new PointD(this.PositionBottomLeft.X, this.PositionCenter.Y - Const.CEILING_RECEIVER_MARGIN));
                points.Add(new PointD(this.PositionTopRight.X, this.PositionCenter.Y - Const.CEILING_RECEIVER_MARGIN));
                lineIds.Add(AutoCad.Db.Polyline.Make(points));

                points = new List<PointD>();
                points.Add(new PointD(this.PositionBottomLeft.X, this.PositionCenter.Y + Const.CEILING_RECEIVER_MARGIN));
                points.Add(new PointD(this.PositionTopRight.X, this.PositionCenter.Y + Const.CEILING_RECEIVER_MARGIN));
                lineIds.Add(AutoCad.Db.Polyline.Make(points));
            }
            else if (this.RecieverOrientation == Orientation.Vertical)
            {
                points = new List<PointD>();
                points.Add(new PointD(this.PositionCenter.X - Const.CEILING_RECEIVER_MARGIN, this.PositionTopRight.Y));
                points.Add(new PointD(this.PositionCenter.X - Const.CEILING_RECEIVER_MARGIN, this.PositionBottomLeft.Y));
                lineIds.Add(AutoCad.Db.Polyline.Make(points));

                points = new List<PointD>();
                points.Add(new PointD(this.PositionCenter.X + Const.CEILING_RECEIVER_MARGIN, this.PositionTopRight.Y));
                points.Add(new PointD(this.PositionCenter.X + Const.CEILING_RECEIVER_MARGIN, this.PositionBottomLeft.Y));
                lineIds.Add(AutoCad.Db.Polyline.Make(points));
            }
            else 
            {
                throw new ApplicationException("天井受けの形状が異常です。");
            }

            //移動させた天井受けが完全にパネル外ならパネル端
            foreach (var lineId in lineIds)
            {
                if (panel.IsOutsideForPolyline(lineId))
                    result = true;
            }

            //CADオブジェクトを作ったら消すべし
            lineIds.ForEach(p => AutoCad.Db.Polyline.Erase(p));

            return result;
        }
    }
}
