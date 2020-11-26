namespace Edsa.AutoCadProxy
{
    public class Dimension : Entity
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbDimension; } }

        public void SetLineColor(int dimensionId, CadColor color)
        {
            if (!Set<int>(dimensionId, "寸法線の色設定", color.Code))
                throw new AutoCadException("Failed to set dimension line color.");
        }

        public void SetExtraLineColor(int dimensionId, CadColor color)
        {
            if (!Set<int>(dimensionId, "補助線の色設定", color.Code))
                throw new AutoCadException("Failed to set dimension extra line color.");
        }

        public void SetTextColor(int dimensionId, CadColor color)
        {
            if (!Set<int>(dimensionId, "寸法値の色設定", color.Code))
                throw new AutoCadException("Failed to set dimension text color.");
        }

        public void SetTextHeight(int dimensionId, double height)
        {
            if (!Set<double>(dimensionId, "寸法値高さ設定", height))
                throw new AutoCadException("Failed to set dimension text height.");
        }
    }
}