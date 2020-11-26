using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public class Arc : Curve
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbArc; } }

        public int Make(PointD point, double radius, double startAngle, double endAngle)
        {
            var args = new List<object>();
            args.Add(new double[] { point.X, point.Y, point.Z });
            args.Add(radius);
            args.Add(startAngle);
            args.Add(endAngle);

            var result = this.Make<object>(args.ToArray());
            if (!result.Success)
                throw new AutoCadException("Failed to make arc.");

            return result.Value;
        }
    }
}