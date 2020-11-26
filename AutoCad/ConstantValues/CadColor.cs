namespace Edsa.AutoCadProxy
{
    public struct CadColor
    {
        private int code;
        public int Code { get { return this.code; } }

        public CadColor(int code)
        {
            this.code = code;
        }

        public string CodeForCommand
        {
            get
            {
                if (this.code == 0)
                    return "ByBlock";

                if (this.code == 256)
                    return "ByLayer";

                return this.code.ToString();
            }
        }

        public static CadColor Red { get { return new CadColor(1); } }
        public static CadColor Yellow { get { return new CadColor(2); } }
        public static CadColor Green { get { return new CadColor(3); } }
        public static CadColor Cyan { get { return new CadColor(4); } }
        public static CadColor Blue { get { return new CadColor(5); } }
        public static CadColor Magenta { get { return new CadColor(6); } }
        public static CadColor BlackWhite { get { return new CadColor(7); } }
        public static CadColor Gray { get { return new CadColor(8); } }
        public static CadColor Silver { get { return new CadColor(9); } }
        public static CadColor Orange { get { return new CadColor(30); } }
        public static CadColor Brown { get { return new CadColor(32); } }
        public static CadColor DeepYellow { get { return new CadColor(40); } }
        public static CadColor Unko { get { return new CadColor(42); } }
        public static CadColor LimeGreen { get { return new CadColor(60); } }
        public static CadColor DeepGreen { get { return new CadColor(102); } }
        public static CadColor LightBlue { get { return new CadColor(141); } }
        public static CadColor Purple { get { return new CadColor(190); } }
        public static CadColor Pink { get { return new CadColor(220); } }
        public static CadColor LightRed { get { return new CadColor(240); } }
        public static CadColor Black { get { return new CadColor(250); } }
        public static CadColor DimGray2 { get { return new CadColor(251); } }
        public static CadColor DimGray { get { return new CadColor(252); } }
        public static CadColor DarkGray { get { return new CadColor(253); } }
        public static CadColor LightGray { get { return new CadColor(254); } }
        public static CadColor White { get { return new CadColor(255); } }

        public static CadColor ByBlock { get { return new CadColor(0); } }
        public static CadColor ByLayer { get { return new CadColor(256); } }

        public static CadColor 幹線 { get { return new CadColor(192); } }
        public static CadColor 弱電 { get { return new CadColor(212); } }
        public static CadColor シグナル { get { return new CadColor(220); } }
        public static CadColor JBOX { get { return new CadColor(102); } }
        public static CadColor JBOX_UnderFloor { get { return new CadColor(60); } }

        //灰色の濃さ順（7番は、背景が白いと黒色に、背景が黒いと白色になる）
        //色番号(明度) 色名
        //255, 7 (255) White
        //254    (204) LightGray
        //  9    (192) Silver
        //253    (153) DarkGray
        //  8    (128) Gray
        //252    (102) DimGray
        //251    (101) DimGray2
        //250, 7 (  0) Black
    }
}