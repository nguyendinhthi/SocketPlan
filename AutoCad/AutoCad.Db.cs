namespace Edsa.AutoCadProxy
{
    public partial class AutoCad
    {
        public static class Db
        {
            public static readonly Database Database = new Database();

            public static readonly Utility Utility = new Utility();

            public static readonly AbstractViewportTable AbstractViewportTable = new AbstractViewportTable();
            public static readonly AbstractViewTableRecord AbstractViewTableRecord = new AbstractViewTableRecord();
            public static readonly Arc Arc = new Arc();
            public static readonly Attribute Attribute = new Attribute();
            public static readonly BlockReference BlockReference = new BlockReference();
            public static readonly BlockTable BlockTable = new BlockTable();
            public static readonly BlockTableRecord BlockTableRecord = new BlockTableRecord();
            public static readonly Circle Circle = new Circle();
            public static readonly Curve Curve = new Curve();
            public static readonly Dictionary Dictionary = new Dictionary();
            public static readonly Dimension Dimension = new Dimension();
            public static readonly Ellipse Ellipse = new Ellipse();
            public static readonly Entity Entity = new Entity();
            public static readonly Group Group = new Group();
            public static readonly Hatch Hatch = new Hatch();
            public static readonly LayerTable LayerTable = new LayerTable();
            public static readonly LayerTableRecord LayerTableRecord = new LayerTableRecord();
            public static readonly LayoutAcDb Layout = new LayoutAcDb();
            public static readonly Leader Leader = new Leader();
            public static readonly Line Line = new Line();
            public static readonly LinetypeTable LinetypeTable = new LinetypeTable();
            public static readonly LinetypeTableRecord LinetypeTableRecord = new LinetypeTableRecord();
            public static readonly MText MText = new MText();
            public static readonly Object Object = new Object();
            public static readonly PlotSettings PlotSettings = new PlotSettings();
            public static readonly Point Point = new Point();
            public static readonly Polyline Polyline = new Polyline();
            public static readonly Ray Ray = new Ray();
            public static readonly RegAppTable RegAppTable = new RegAppTable();
            public static readonly Region Region = new Region();
            public static readonly SymbolTable SymbolTable = new SymbolTable();
            public static readonly SymbolTableRecord SymbolTableRecord = new SymbolTableRecord();
            public static readonly Text Text = new Text();
            public static readonly TextStyleTable TextStyleTable = new TextStyleTable();
            public static readonly TextStyleTableRecord TextStyleTableRecord = new TextStyleTableRecord();
            public static readonly Viewport Viewport = new Viewport();
            public static readonly ViewportTable ViewportTable = new ViewportTable();
            public static readonly ViewportTableRecord ViewportTableRecord = new ViewportTableRecord();
        }
    }
}
