using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public class Line : Curve
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbLine; } }

        /// <summary>始点から指定した長さで切る。余りは削除する。</summary>
        public int Cut(int lineId, double length)
        {
            //切る
            var param = new List<double>();
            param.Add(length);
            var parts = this.GetSplitCurves(lineId, param);

            //古い線を削除する
            this.Erase(lineId);

            //切った余りの線を削除する
            foreach (var part in parts)
            {
                if (part != parts[0])
                    this.Erase(part);

            }

            return parts[0];
        }

        public int Make(PointD startPoint, PointD endPoint)
        {
            var args = new List<object>();
            args.Add(startPoint.ToArray());
            args.Add(endPoint.ToArray());

            var result = this.Make<object>(args.ToArray());
            if (!result.Success)
                throw new AutoCadException("Failed to make line.");

            return result.Value;
        }

        public PointD GetMiddlePoint(int lineId)
        {
            var startPoint = this.GetStartPoint(lineId);
            var endPoint = this.GetEndPoint(lineId);

            return new PointD((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
        }
        
        public void SetEndPoint(int objectId, PointD endPoint)
        {
            if (!this.Set<double[]>(objectId, "終点設定", endPoint.ToArray()))
                throw new AutoCadException("Failed to set end point of line.");
        }
    }
}