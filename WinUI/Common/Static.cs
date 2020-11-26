using System;
using System.Collections.Generic;
using System.Text;
using SocketPlan.WinUI.SocketPlanServiceReference;

namespace SocketPlan.WinUI
{
    public static class Static
    {
        //OpenDrawingFormで図面を開いた時に設定する。FileCommandFormのクローズでnullにする
        private static string constructionCode;
        public static string ConstructionCode
        {
            get
            {
                return Static.constructionCode;
            }
            set
            {
                Static.constructionCode = value;
                if (ConstructionChanged != null)
                    ConstructionChanged(null, EventArgs.Empty);
            }
        }

        public static string ConstructionTypeCode { get; set; }
        public static bool IsBeforeKakouIrai { get; set; } //よく使うのでStaticに持ってきました
        public static bool IsZeh { get; set; }
        public static bool IsTenjijyo { get; set; }
        public static ConstructionSchedule Schedule { get; set; }
        
        public static event EventHandler ConstructionChanged;

        //OpenDrawingFormで図面を開いた時に設定する。FileCommandFormのクローズでnullにする
        /// <summary>現在開いている図面情報を保持しておく</summary>
        public static Drawing Drawing { get; set; }
        public static HouseSpecs HouseSpecs { get; set; }

        //SymbolのNewが重いので、キャッシュで対策
        private static List<Symbol> symbolCache = new List<Symbol>();
        public static List<Symbol> SymbolCache
        {
            get { return Static.symbolCache; }
            set { Static.symbolCache = value; }
        }
    }
}
