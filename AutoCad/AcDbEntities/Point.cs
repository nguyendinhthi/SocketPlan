namespace Edsa.AutoCadProxy
{
    public class Point : Entity
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbPoint; } }

        public PointD GetPosition(int objectId)
        {
            Result<double[]> result = this.Get<double[]>(objectId, "座標値取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get point.");

            return new PointD(result.Value[0], result.Value[1]);
        }

        public int Make(PointD point)
        {
            var result = AutoCad.Db.Point.Make<double[]>(point.ToArray());
            if(!result.Success)
                throw new AutoCadException("Failed to make point.");

            return result.Value;
        }
    }
}