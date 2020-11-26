using System;

namespace Edsa.AutoCadProxy
{
    public class PointD
    {
        #region コンストラクタ
        
        public PointD()
        {
        }

        public PointD(double x, double y, double z) : this()
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public PointD(double x, double y) : this(x, y, 0)
        {
        }
        
        #endregion

        #region プロパティ

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        private int? rayId;
        /// <summary>
        /// この点から右斜め上に延ばした線のId。主に点の枠内外判定に使う。
        /// 同じ点から2度Rayを伸ばさないように、RayIdは保持しておく。
        /// </summary>
        public int RayId
        {
            get
            {
                if (!this.rayId.HasValue)
                    this.rayId = this.CastRay();

                return this.rayId.Value;
            }
        }

        #endregion

        #region メソッド

        public bool IsOn(PointD bottomLeft, PointD topRight, bool withRound)
        {
            if (withRound)
            {
                if (Math.Round(this.X) < Math.Round(bottomLeft.X) || Math.Round(topRight.X) < Math.Round(this.X))
                    return false;

                if (Math.Round(this.Y) < Math.Round(bottomLeft.Y) || Math.Round(topRight.Y) < Math.Round(this.Y))
                    return false;
            }
            else
            {
                if (this.X < bottomLeft.X || topRight.X < this.X)
                    return false;

                if (this.Y < bottomLeft.Y || topRight.Y < this.Y)
                    return false;
            }

            return true;
        }

        /// <summary>X座標とY座標が一致していたらtrue。</summary>
        public bool Equals(PointD point)
        {
            if (this.X == point.X && this.Y == point.Y)
                return true;

            return false;
        }

        /// <summary>X座標とY座標が一致していたらtrue。小数点以下は見ません</summary>
        public bool EqualsRound(PointD point)
        {
            if (Math.Round(this.X) == Math.Round(point.X) && Math.Round(this.Y) == Math.Round(point.Y))
                return true;

            return false;
        }

        public bool IsIn(int outlineId)
        {
            //このメソッド、Regionに対してもなぜか使える

            if (AutoCad.Db.Curve.IsOn(this, outlineId))
                return true;

            var intersections = AutoCad.Db.Entity.GetIntersect2D(this.RayId, outlineId);
            if ((intersections.Count % 2) == 0) //交点の数が偶数だったら点が外部にある
                return false;
            else//交点の数が奇数だったら点が内部にある
                return true;
        }

        public PointD Clone()
        {
            var clone = new PointD();
            clone.X = this.X;
            clone.Y = this.Y;
            clone.Z = this.Z;
            return clone;
        }

        public double[] ToArray()
        {
            return new double[] { this.X, this.Y, this.Z };
        }

        /// <summary>「X.XX, Y.YY」の形式で返します</summary>
        public override string ToString()
        {
            return "(" + this.X.ToString("0.00") + ", " + this.Y.ToString("0.00") + ")";
        }

        /// <summary>「X,Y,Z」の形式で返します</summary>
        public string ToString2()
        {
            return this.X + "," + this.Y + "," + this.Z;
        }

        public PointD Shift(Direction direction, double distance)
        {
            var point = this.Clone();

            if (direction == Direction.Down)
                point.Y -= distance;
            else if (direction == Direction.Left)
                point.X -= distance;
            else if (direction == Direction.Up)
                point.Y += distance;
            else if (direction == Direction.Right)
                point.X += distance;

            return point;
        }

        /// <summary>引数の座標がこの点から見てどの方向にあるか求める。</summary>
        public Direction GetDirection(PointD point)
        {
            var thisX = Math.Round(this.X, 2);
            var thisY = Math.Round(this.Y, 2);
            var otherX = Math.Round(point.X, 2);
            var otherY = Math.Round(point.Y, 2);

            if (thisX == otherX && thisY == otherY)
                return Direction.Center;

            if (thisX == otherX)
            {
                if (thisY < otherY)
                    return Direction.Up;
                else
                    return Direction.Down;
            }

            if (thisY == otherY)
            {
                if (thisX < otherX)
                    return Direction.Right;
                else
                    return Direction.Left;
            }

            return Direction.Unknown;
        }

        /// <summary>図面上の絶対座標をブロックの基点からの相対座標に変換する。ブロックの傾きも考慮する。</summary>
        public PointD ConvertToLocalPoint(PointD basePoint, double rotation)
        {
            double cos = Math.Cos(rotation);
            double sin = Math.Sin(rotation);

            //ブロックの傾きを無視したブロック基点からの座標
            double x = this.X - basePoint.X;
            double y = this.Y - basePoint.Y;

            //ブロックの傾きを考慮した座標の成分を取得
            double xVectorX = cos * x;
            double xVectorY = sin * y;
            double yVectorX = -sin * x;
            double yVectorY = cos * y;

            double localX = xVectorX + xVectorY;
            double localY = yVectorX + yVectorY;

            return new PointD(localX, localY);
        }

        private int CastRay()
        {
            return this.CastRay(new PointD(1, 2.468));
        }

        public int CastRay(PointD vector)
        {
            var rayId = AutoCad.Db.Ray.Make(this, vector.X, vector.Y);
            AutoCad.Db.Ray.Erase(rayId);
            return rayId;
        }

        public void PlusEqual(PointD point)
        {
            this.X += point.X;
            this.Y += point.Y;
            this.Z += point.Z;
        }
        
        public void MinusEqual(PointD point)
        {
            this.X -= point.X;
            this.Y -= point.Y;
            this.Z -= point.Z;
        }

        public PointD Plus(PointD point)
        {
            var p = this.Clone();
            p.PlusEqual(point);
            return p;
        }

        public PointD Plus(double x, double y)
        {
            return this.Plus(new PointD(x, y));
        }

        public PointD Minus(PointD point)
        {
            var p = this.Clone();
            p.MinusEqual(point);
            return p;
        }

        public PointD Minus(double x, double y)
        {
            return this.Minus(new PointD(x, y));
        }

        public PointD PlusX(double x)
        {
            var p = this.Clone();
            p.X += x;
            return p;
        }

        public PointD PlusY(double y)
        {
            var p = this.Clone();
            p.Y += y;
            return p;
        }

        public PointD MinusX(double x)
        {
            return this.PlusX(-x);
        }

        public PointD MinusY(double y)
        {
            return this.PlusY(-y);
        }

        #endregion
    }
}
