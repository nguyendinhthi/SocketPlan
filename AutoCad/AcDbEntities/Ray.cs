namespace Edsa.AutoCadProxy
{
    public class Ray : Curve
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbRay; } }

        public int Make(PointD point, double directX, double directY)
        { //pointから(x,y)=(directX,directY)の方向へ半直線を引く。　作った半直線のObjectIdを返す。
            Result<int> result = this.Make();
            if (!result.Success)
                throw new AutoCadException("Failed to make Ray.");

            if (!this.Set<double[]>(result.Value, "基点設定", new double[] { point.X, point.Y, 0 }))
                throw new AutoCadException("Failed to make Ray.");

            if (!this.Set<double[]>(result.Value, "方向設定", new double[] { directX, directY, 0 }))
                throw new AutoCadException("Failed to make Ray.");

            return result.Value;
        }
    }
}