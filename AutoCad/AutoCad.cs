using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace Edsa.AutoCadProxy
{
    public partial class AutoCad
    {
        public static LtVbCom vbcom = new LtVbCom();

        /// <summary>一連の処理の一番最初に一回呼び出す</summary>
        public static void Prepare()
        {
            AutoCad.vbcom.LoadComObject();

            AutoCad.Command.Prepare();
            AutoCad.Command.SetCurrentLayoutToModel();
            AutoCad.Db.Database.SetLineWeightDisplay(true);
        }

        /// <summary>一連の処理の一番最初に一回呼び出す。レイアウトを切り替えないバージョン</summary>
        public static void Prepare2()
        {
            AutoCad.vbcom.LoadComObject();

            AutoCad.Command.Prepare();
            AutoCad.Db.Database.SetLineWeightDisplay(true);
        }

        public static void PrepareAutoProcess()
        {
            AutoCad.vbcom.LoadComObject();

            WindowController2.BringAutoCadToTop();
            AutoCad.Command.Prepare();
            AutoCad.Db.Database.SetFileDialogMode(false);
            AutoCad.Command.CloseLayerManager();
        }

        public static void FinalizeAutoProcess()
        {
            AutoCad.Db.Database.SetFileDialogMode(true);
        }

        public static void Prompt(string text)
        {
            AutoCad.Db.Utility.DoUtil<string>("acutPrintf", "\n" + text);
        }
    }
}
