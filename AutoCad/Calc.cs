using System;
using System.Collections.Generic;
using System.Text;

namespace Edsa.AutoCadProxy
{
    public static class Calc
    {
        /// <summary>座標A、Bそれぞれから垂直・水平に伸ばした線が交差する座標のうち、currentで指定した側の座標を返す</summary>
        public static PointD GetCrossPoint(PointD currentPt, PointD ptA, PointD ptB)
        {
            //外積とやらの計算です
            Side borderA = Calc.GetSide(currentPt, ptA, ptB);

            if (borderA == 0)
                return null;

            PointD orthogonalPoint = new PointD(ptA.X, ptB.Y);
            PointD oppositePoint = new PointD(ptB.X, ptA.Y);

            //外積とやらの計算です
            Side borderB = Calc.GetSide(orthogonalPoint, ptA, ptB);

            if (borderA == borderB)
                return orthogonalPoint;

            return oppositePoint;
        }

        /// <summary>指定点が線分ABの左右どちらにあるかを判定する。</summary>
        public static Side GetSide(PointD point, PointD ptA, PointD ptB)
        {
            //外積を求める
            double side = (point.X * (ptA.Y - ptB.Y)) + (ptA.X * (ptB.Y - point.Y)) + (ptB.X * (point.Y - ptA.Y));
            if (Math.Round(side, 5) == 0)
                side = 0;

            return (Side)Math.Sign(side);
        }

        /// <summary>モデル空間の座標をペーパー空間の座標に変換する</summary>
        public static PointD ConvertToPaperSpacePoint(PointD modelSpacePoint, int viewportId)
        {
            var customScale = AutoCad.Db.Viewport.GetCustomScale(viewportId);
            var modelCenter = AutoCad.Db.Viewport.GetCenterPointOfModelViewport(viewportId);
            var paperCenter = AutoCad.Db.Viewport.GetCenterPointOfPaperViewport(viewportId);

            var vector = new PointD(modelSpacePoint.X - modelCenter.X, modelSpacePoint.Y - modelCenter.Y);
            vector.X = vector.X * customScale;
            vector.Y = vector.Y * customScale;

            return new PointD(paperCenter.X + vector.X, paperCenter.Y + vector.Y);
        }

        public static Orientation GetOrientation(PointD pointA, PointD pointB)
        {
            //50mmの誤差を認める（50mmはてきとー。システム毎に調整してくだされ）
            const double MARGIN = 50;

            if (pointA.X - MARGIN <= pointB.X && pointB.X <= pointA.X + MARGIN)
                return Orientation.Vertical;

            if (pointA.Y - MARGIN <= pointB.Y && pointB.Y <= pointA.Y + MARGIN)
                return Orientation.Horizontal;

            return Orientation.Unknown;
        }

        public static Orientation GetOrientation(int lineId)
        {
            var start = AutoCad.Db.Line.GetStartPoint(lineId);
            var end = AutoCad.Db.Line.GetEndPoint(lineId);
            return Calc.GetOrientation(start, end);
        }

        /// <summary>basePointから見てpointがどっち方向にあるか</summary>
        public static Direction GetDirection(PointD basePoint, PointD point)
        {
            var orientation = Calc.GetOrientation(basePoint, point);

            if (orientation == Orientation.Vertical)
            {
                if (basePoint.Y < point.Y)
                    return Direction.Up;

                if (point.Y < basePoint.Y)
                    return Direction.Down;
            }

            if (orientation == Orientation.Horizontal)
            {
                if (basePoint.X < point.X)
                    return Direction.Right;

                if (point.X < basePoint.X)
                    return Direction.Left;
            }

            return Direction.Unknown;
        }

        /// <summary>basePointから見てpointがどっち方向にあるか、おおざっぱに上下左右で判定する</summary>
        public static Direction GetDirectionOozappa(PointD basePoint, PointD point)
        {
            var sa = point.Clone();
            sa.MinusEqual(basePoint);

            var absX = Math.Abs(sa.X);
            var absY = Math.Abs(sa.Y);
            if (absY < absX)
            {
                if (0 < sa.X)
                    return Direction.Right;
                else
                    return Direction.Left;
            }
            else
            {
                if (0 < sa.Y)
                    return Direction.Up;
                else
                    return Direction.Down;
            }
        }

        public static double GetRotation(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up   : return 0 / (180 / Math.PI);
                case Direction.Left : return 90 / (180 / Math.PI);
                case Direction.Down : return 180 / (180 / Math.PI);
                case Direction.Right: return 270 / (180 / Math.PI);
                default: return 0;
            }
        }

        public static Direction ShiftDirection(Direction direction, Side side)
        {
            Direction dir = Direction.Unknown;

            switch (side)
            {
                case Side.Left  : dir = direction + 3; break; //ほんとは-1が分かりやすいけど、-になると%が効かないので。。
                case Side.Right : dir = direction + 1; break;
                case Side.On    : dir = direction; break;
                default         : dir = direction; break;
            }

            return (Direction)((int)dir % 4);
        }

        /// <summary>多角形が時計回りならtrue</summary>
        public static bool IsClockwise(List<PointD> vertexes)
        {
            var area = Calc.GetArea(vertexes);
            return area < 0;
        }

        /// <summary>
        /// 多角形の座標のリストから多角形の面積を求める
        /// 反時計回りなら正、時計回りなら負で面積が求まる
        /// </summary>
        public static double GetArea(List<PointD> vertexes)
        {
            double area = 0;

            for (int i = 0; i < vertexes.Count; i++)
            {
                area += Calc.GetOuterProduct(vertexes[i], vertexes[(i + 1) % vertexes.Count]);
            }

            return area / 2d;
        }

        /// <summary>2次元ベクトルの外積を求める</summary>
        public static double GetOuterProduct(PointD a, PointD b)
        {
            return a.X * b.Y - a.Y * b.X;
        }
        
        /// <summary>2次元ベクトルの内積を求める</summary>
        public static double GetInnerProduct(PointD a, PointD b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        /// <summary>centerを中心に、aからbに何度回転しているか求める</summary>
        public static double GetAngle(PointD center, PointD a, PointD b)
        {
            var vectorA = a.Clone();
            vectorA.MinusEqual(center);
            var vectorB = b.Clone();
            vectorB.MinusEqual(center);

            var lengthA = Calc.GetDistance(a, center);
            var lengthB = Calc.GetDistance(b, center);
            var innerProduct = Calc.GetInnerProduct(vectorA, vectorB);
            var outerProduct = Calc.GetOuterProduct(vectorA, vectorB);

            double cosTheta = innerProduct / (lengthA * lengthB);
            if (1 < Math.Abs(cosTheta))
                cosTheta = Math.Sign(cosTheta);

            double theta = Math.Acos(cosTheta);

            var angle = theta * (180 / Math.PI);
            //このままだと回転方向が分からず0～180°で値が返ってくるので、
            //0～360°になるように調整する。
            if (Math.Sign(outerProduct) < 0)
                angle = 360 - angle;

            return angle;
        }

        public static double GetDistance(PointD a, PointD b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        public static double GetLengthInsideOutline(int polylineId, int outlineId)
        {
            var intersections = AutoCad.Db.Entity.GetIntersect2D(polylineId, outlineId);
            
            //たまに、Parameterが線の長さを超えた値になることがあるので、超えた時は補正する
            var vertices = AutoCad.Db.Polyline.GetVertex(polylineId);
            var maxPara = vertices.Count - 1;

            var parameters = new List<double>();
            foreach (var intersection in intersections)
            {
                var para = AutoCad.Db.Curve.GetParameter(polylineId, intersection);
                if (maxPara < para)
                    para = maxPara;

                parameters.Add(para);
            }

            //交点を配線の始点から近い順に並び変える。
            parameters.Sort((p, q) => p.CompareTo(q));

            double length = 0;
            bool isInside = Calc.IsStartPointInOutline(polylineId, outlineId);
            foreach (var parameter in parameters)
            {
                if (parameter == 0)
                    continue; //配管の始点が外周線上の場合はスルー

                if (isInside)
                    length += AutoCad.Db.Polyline.GetLength(polylineId, parameter);
                else
                    length -= AutoCad.Db.Polyline.GetLength(polylineId, parameter);

                isInside = !isInside;
            }

            if (isInside) //最後に引いて終わっていたら、足して終わる。
                length += AutoCad.Db.Polyline.GetLength(polylineId);

            return length;
        }

        private static bool IsStartPointInOutline(int polylineId, int outlineId)
        {
            var startPoint = AutoCad.Db.Polyline.GetStartPoint(polylineId);

            PointD point;
            if (AutoCad.Db.Curve.IsOn(startPoint, outlineId)) //もし、始点がOutline上にあったら、内外を判定できないので、
                point = AutoCad.Db.Curve.GetPoint(polylineId, 0.000001d); //始点ではない、始点に限りなく近い配線上の点を取る
            else
                point = startPoint;

            var intersections = AutoCad.Db.Entity.GetIntersect2D(point.RayId, outlineId);

            if ((intersections.Count % 2) == 0) //交点の数が偶数だったら始点が外部にある
                return false;
            else//交点の数が奇数だったら始点が内部にある
                return true;
        }

        public static double[,] GetUnitMatrix()
        {
            var matrix = new double[,]
            {
                {1, 0, 0, 0},
                {0, 1, 0, 0},
                {0, 0, 1, 0},
                {0, 0, 0, 1}
            };

            return matrix;
        }
    }
}