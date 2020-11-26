using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public class Viewport : Entity
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbViewport; } }

        public double GetCustomScale(int viewportId)
        {
            var result = Get<double>(viewportId, "カスタム尺度取得");
            if (result.Value <= 0)
                throw new AutoCadException("Failed to get viewport custom scale.");

            return result.Value;
        }

        public void SetCustomScale(int viewportId, double scale)
        {
            if (!Set<double>(viewportId, "カスタム尺度設定", scale))
                throw new AutoCadException("Failed to set viewport custom scale.");
        }

        public void SetWidth(int viewportId, double width)
        {
            if (!Set<double>(viewportId, "幅設定", width))
                throw new AutoCadException("Failed to set viewport width.");
        }

        public void SetHeight(int viewportId, double height)
        {
            if (!Set<double>(viewportId, "高さ設定", height))
                throw new AutoCadException("Failed to set viewport height.");
        }

        public void SetFrozen(int viewportId, List<string> layers)
        {
            if (!Set<string[]>(viewportId, "ビューポート内フリーズ画層設定", layers.ToArray()))
                throw new AutoCadException("Failed to set viewport freeze.");
        }

        public void SetCenterPointOfModelViewport(int viewportId, PointD point)
        {
            if (!Set<double[]>(viewportId, "ビュー中心設定", new double[] { point.X, point.Y, point.Z }))
                throw new AutoCadException("Failed to set viewport center.");
        }

        public PointD GetCenterPointOfModelViewport(int viewportId)
        {
            Result<double[]> result = this.Get<double[]>(viewportId, "ビュー中心取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get center point of viewport.");

            return new PointD(result.Value[0], result.Value[1]);
        }

        public void SetCenterPointOfPaperViewport(int viewportId, PointD point)
        {
            if (!Set<double[]>(viewportId, "中心点設定", new double[] { point.X, point.Y, point.Z }))
                throw new AutoCadException("Failed to set viewport center.");
        }

        public PointD GetCenterPointOfPaperViewport(int viewportId)
        {
            Result<double[]> result = this.Get<double[]>(viewportId, "中心点取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get center point of viewport.");

            return new PointD(result.Value[0], result.Value[1]);
        }

        public void SetClipId(int viewportId, int clipId)
        {
            if(!Set<int>(viewportId, "矩形でないクリップ図形ID設定", clipId))
                throw new AutoCadException("Failed to set clip id of viewport.");
        }

        public void SetClipOn(int viewportId)
        {
            if (!Set(viewportId, "矩形でないクリップオン"))
                throw new AutoCadException("Failed to enable clip of viewport.");
        }

        public int GetId()
        {
            var paperSpaceId = AutoCad.Db.Database.GetPaperSpaceViewportId();
            var viewportIds = AutoCad.Db.Viewport.GetAll();

            //viewport(ペーパー空間viewportを除く)が見つからなかったらだめー
            if (!viewportIds.Exists(p => p != paperSpaceId))
                throw new AutoCadException("Failed to get viewport id.");

            return viewportIds.Find(p => p != paperSpaceId);
        }

        public List<int> GetAll()
        {
            Filter filter = new Filter();
            filter.Add(FilterOption.ObjectType.Viewport);
            return filter.Execute();
        }
    }
}