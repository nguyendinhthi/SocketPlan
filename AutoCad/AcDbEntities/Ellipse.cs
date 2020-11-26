using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public class Ellipse : Curve
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbEllipse; } }

        public int Make(PointD point, double radiusV, double radiusH, double startAngle, double endAngle)
        {
            var args = new List<object>();
            args.Add(new double[] { point.X, point.Y, point.Z });
            args.Add(new double[] { 0, 0, 1 });
            if (radiusV > radiusH)
            {
                args.Add(new double[] { 0, radiusV, 0 });
                args.Add(radiusH / radiusV);
            }
            else
            {
                args.Add(new double[] { radiusH, 0, 0 });
                args.Add(radiusV / radiusH);
            }

            args.Add(startAngle);
            args.Add(endAngle);

            var result = this.Make<object>(args.ToArray());
            if (!result.Success)
                throw new AutoCadException("Failed to make ellipse.");

            return result.Value;
        }
    }
}