namespace Edsa.AutoCadProxy
{
    public class ViewportTableRecord : AbstractViewTableRecord
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbViewportTableRecord; } }

        public PointD GetCenterPoint(int objectId)
        {
            var result = this.Get<double[]>(objectId, "中心点取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get center point of view.");

            PointD position = new PointD();
            position.X = result.Value[0];
            position.Y = result.Value[1];

            return position;
        }

        public PointD GetCenterPointOfModelLayout()
        {
            //どうやっても図面ビューの中心点を取得できないので、一時的にビューポートを作る。

            AutoCad.Command.SendLine("-vports s temp");

            var viewportId = AutoCad.Db.ViewportTable.GetId("temp");

            var centerPoint = AutoCad.Db.ViewportTableRecord.GetCenterPoint(viewportId);

            AutoCad.Command.SendLine("-vports d temp");

            return centerPoint;
        }
    }
}