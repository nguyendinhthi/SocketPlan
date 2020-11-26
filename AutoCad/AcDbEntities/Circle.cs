using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public class Circle : Curve
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbCircle; } }

        public List<int> GetAll()
        {
            Filter filter = new Filter();
            filter.Add(FilterOption.ObjectType.Circle);
            return filter.Execute();
        }

        public int Make(PointD point, double radius)
        {
            var args = new List<object>();
            args.Add(point.ToArray());
            args.Add(radius);

            var result = this.Make<object>(args.ToArray());
            if (!result.Success)
                throw new AutoCadException("Failed to make circle.");

            return result.Value;
        }
    }
}