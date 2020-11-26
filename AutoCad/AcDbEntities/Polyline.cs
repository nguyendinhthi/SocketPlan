using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public class Polyline : Curve
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbPolyline; } }

        public List<int> GetAll()
        {
            Filter filter = new Filter();
            filter.Add(FilterOption.ObjectType.Polyline);
            return filter.Execute();
        }

        public List<PointD> GetVertex(int objectId)
        {
            var result = this.Get<object[]>(objectId, "全頂点取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get vertex.");

            var list = new List<PointD>();
            foreach (var vertex in result.Value)
            {
                var point = (double[])vertex;

                list.Add(new PointD(point[0], point[1]));
            }

            return list;
        }

        public override double GetLength(int objectId)
        {
            var vertexes = this.GetVertex(objectId);
            //全部の頂点の座標が同じだったら距離取得に失敗するので、回避
            var v1 = vertexes[0];
            if (vertexes.TrueForAll(p => v1.EqualsRound(p)))
                return 0;

            return AutoCad.Db.Curve.GetLength(objectId, vertexes[vertexes.Count - 1]);
        }

        /// <summary>ポリラインを膨らませます</summary>
        public void SetBurge(int objectId, double r, int vertexNo)
        {
            List<object> args = new List<object>();
            args.Add(vertexNo);
            args.Add(r);

            if (!this.Set<object[]>(objectId, "ふくらみ順番指定設定", args.ToArray()))
                throw new AutoCadException("Failed to set fillet to polyline.");
        }

        public void SetClose(int objectId)
        {
            if (!this.Set<short>(objectId, "閉じる", (short)1))
                throw new AutoCadException("Failed to close polyline.");
        }

        /// <summary>終点の座標を始点にくっつける待遇の良さが魅力</summary>
        public void SetClose2(int polylineId)
        {
            var startPoint = AutoCad.Db.Polyline.GetStartPoint(polylineId);
            var vertexCount = AutoCad.Db.Polyline.GetVertexCount(polylineId);
            AutoCad.Db.Polyline.SetVertex(polylineId, vertexCount - 1, startPoint);
            AutoCad.Db.Polyline.SetClose(polylineId);
        }

        public int Make(List<PointD> points)
        {
            var vertexes = new List<object>();
            foreach (var point in points)
            {
                var linePoint = new List<object>();
                linePoint.Add(point.X);
                linePoint.Add(point.Y);
                linePoint.Add(point.Z);
                vertexes.Add(linePoint.ToArray());
            }

            var result = this.Make<object>(vertexes.ToArray());
            if (!result.Success)
                throw new AutoCadException("Failed to make polyline.");

            return result.Value;
        }

        /// <summary>法線ベクトルを取得する</summary>
        public PointD GetNormal(int polylineId)
        {
            var result = this.Get<double[]>(polylineId, "法線取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get normal of polyline.");

            return new PointD(result.Value[0], result.Value[1], result.Value[2]);
        }

        public int MakeRectangle(PointD leftDown, PointD rightUp)
        {
            var rightDown = new PointD(rightUp.X, leftDown.Y);
            var leftUp = new PointD(leftDown.X, rightUp.Y);

            var vertices = new List<PointD>();
            vertices.Add(leftDown);
            vertices.Add(rightDown);
            vertices.Add(rightUp);
            vertices.Add(leftUp);
            vertices.Add(leftDown);

            var rectId = this.Make(vertices);
            AutoCad.Db.Polyline.SetClose(rectId);

            return rectId;
        }

        public int MakeRectangle2(PointD leftUp, PointD rightDown)
        {
            var rightUp = new PointD(rightDown.X, leftUp.Y);
            var leftDown = new PointD(leftUp.X, rightDown.Y);

            var vertices = new List<PointD>();
            vertices.Add(leftDown);
            vertices.Add(rightDown);
            vertices.Add(rightUp);
            vertices.Add(leftUp);
            vertices.Add(leftDown);

            var rectId = this.Make(vertices);
            AutoCad.Db.Polyline.SetClose(rectId);

            return rectId;
        }

        public void AddVertex(int polylineId, int vertexIndex, PointD vertexPoint)
        {
            List<object> args = new List<object>();
            args.Add(vertexIndex);
            args.Add(vertexPoint.ToArray());

            if (!this.Set<object[]>(polylineId, "頂点順番指定追加", args.ToArray()))
                throw new AutoCadException("Failed to add vertex to polyline.");
        }

        public void SetVertex(int polylineId, int vertexIndex, PointD vertexPoint)
        {
            List<object> args = new List<object>();
            args.Add(vertexIndex);
            args.Add(vertexPoint.ToArray());

            if (!this.Set<object[]>(polylineId, "頂点順番指定設定", args.ToArray()))
                throw new AutoCadException("Failed to set vertex of polyline.");
        }

        public int GetVertexCount(int polylineId)
        {
            var result = this.Get<int>(polylineId, "頂点数取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get vertex count of polyline.");

            return result.Value;
        }

        public void SetElevation(int objectId, double height)
        {
            if (!this.Set<double>(objectId, "高度設定", height))
                throw new AutoCadException("Failed to set elevation to polyline.");
        }
    }
}