namespace Edsa.AutoCadProxy
{
    public struct FilterOption
    {
        public struct LayerName
        {
            private string value;
            public string Value { get { return this.value; } }

            public LayerName(string value) { this.value = value; }

            public static LayerName All { get { return new LayerName(string.Empty); } }
        }

        public struct Color
        {
            private short value;
            public short Value { get { return this.value; } }

            public Color(short value) { this.value = value; }
            private Color(CadColor value) { this.value = (short)value.Code; }

            public static Color All { get { return new Color(-1); } }
            public static Color Orange { get { return new Color(CadColor.Orange); } }
            public static Color Red { get { return new Color(CadColor.Red); } }
        }

        public struct LineType
        {
            private string value;
            public string Value { get { return this.value; } }

            public LineType(string value) { this.value = value; }

            public static LineType All { get { return new LineType(string.Empty); } }
            public static LineType ByLayer { get { return new LineType("ByLayer"); } }
        }

        public struct ByLayer
        {
            private short value;
            public short Value { get { return this.value; } }

            private ByLayer(short value) { this.value = value; }

            /// <summary>
            /// 図形に直接設定されている「色」でフィルタ選択行います。
            /// つまり、指定のフィルタ対象の色で見えている図形でも、図形に色が設定されておらず（図形色=ByLayer）、
            /// 画層の色で指定の色に見えている図形は、フィルター選択の際に対象となりません。
            /// </summary>
            public static ByLayer Off { get { return new ByLayer(0); } }

            /// <summary>
            /// 図形の色だけではなく、所定色の設定された画層上の図形色=ByLayerの
            /// 図形もフィルターの対象ととして選択されます。
            /// 簡単に申しますと、画面上で指定した色で見えているすべての図形を対象とすることができます。
            /// </summary>
            public static ByLayer On { get { return new ByLayer(1); } }
        }

        public struct Hilight
        {
            private short value;
            public short Value { get { return this.value; } }

            private Hilight(short value) { this.value = value; }

            /// <summary>特に何も起こりません。</summary>
            public static Hilight Off { get { return new Hilight(0); } }

            /// <summary>フィルター選択の対象となった図形がハイライト表示されます。対象図形が選択されたような見た目になります。</summary>
            public static Hilight On { get { return new Hilight(1); } }
        }

        public struct AndOr
        {
            private short value;
            public short Value { get { return this.value; } }

            private AndOr(short value) { this.value = value; }

            public static AndOr Or { get { return new AndOr(0); } }
            public static AndOr And { get { return new AndOr(1); } }
        }

        public struct ObjectType
        {
            private string value;
            public string Value { get { return this.value; } }

            private ObjectType(string value) { this.value = value; }
            public bool IsMatch(ObjectType obj) { return this.value == obj.value; }

            public static ObjectType All { get { return new ObjectType(string.Empty); } }
            public static ObjectType Circle { get { return new ObjectType("AcDbCircle"); } }
            public static ObjectType Ellispe { get { return new ObjectType("AcDbEllipse"); } }
            public static ObjectType Polyline { get { return new ObjectType("AcDbPolyline"); } }
            public static ObjectType Polyline2D { get { return new ObjectType("AcDb2dPolyline"); } }
            public static ObjectType Ray { get { return new ObjectType("AcDbRay"); } }
            public static ObjectType Region { get { return new ObjectType("AcDbRegion"); } }
            public static ObjectType Text { get { return new ObjectType("AcDbText"); } }
            public static ObjectType MultiText { get { return new ObjectType("AcDbMText"); } }
            public static ObjectType Layout { get { return new ObjectType("AcDbLayout"); } }
            public static ObjectType Viewport { get { return new ObjectType("AcDbViewport"); } }
            public static ObjectType Leader { get { return new ObjectType("AcDbLeader"); } }
            public static ObjectType Line { get { return new ObjectType("AcDbLine"); } }
            public static ObjectType Dimension { get { return new ObjectType("AcDbDimension"); } }
            public static ObjectType BlockReference { get { return new ObjectType("AcDbBlockReference"); } }
        }
    }
}
