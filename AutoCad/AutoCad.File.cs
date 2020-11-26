using System.IO;

namespace Edsa.AutoCadProxy
{
    //図面ファイルの管理をするメソッド
    public partial class AutoCad
    {
        public class File
        {
            public static void Create(string templateName)
            {
                var autocadHandle = WindowController2.GetAutoCadHandle();

                //SendCommandだとﾃﾝﾌﾟﾚｰﾄﾌｧｲﾙ名を入力するまで処理が帰ってこない
                WindowController2.PostCommand(autocadHandle, 57600); //Ctrl+NキーをAutoCADに送信し、ﾌｧｲﾙ>新規作成メニューを呼び出す

                //2013だと、新規図面を開くと別のプロンプトが立ち上がるが、
                //開いた瞬間は以前のプロンプトからコマンドを読み取るため、
                //うまく動かない。とりあえずループしてごまかす

                while (true)
                {
                    var result = AutoCad.Status.WaitPrompt("テンプレート ファイル名を入力", "Enter template file name");
                    if (result == Status.EventResult.Prompted)
                        break;
                }

                AutoCad.Command.SendLine(templateName);
                AutoCad.Status.WaitFinish();
            }

            /// <summary>指定した図面を開く</summary>
            public static void Open(string path)
            {
                AutoCad.Command.SendLineEsc("open");
                
                //2013だと、新規図面を開くと別のプロンプトが立ち上がるが、
                //開いた瞬間は以前のプロンプトからコマンドを読み取るため、
                //うまく動かない。とりあえずループしてごまかす
                while (true)
                {
                    var result = AutoCad.Status.WaitPrompt("開く図面ファイル名を入力", "Enter name of drawing to open");
                    if (result == Status.EventResult.Prompted)
                        break;
                }

                AutoCad.Command.SendLine(path);

                //「extslim2.shx はビッグフォント ファイルで、ビッグフォント ファイルではありません。」対策
                //extslim2をextfont2に置き換える
                while (true)
                {
                    if (AutoCad.Status.IsCommandFinish())
                        break;

                    if (AutoCad.Status.ContainTextInCommandPrompt("別の 文字スタイルのフォント ファイル名", "Enter another font file name for style"))
                    {
                        AutoCad.Command.SendLine("extfont2");
                    }
                }
            }

            /// <summary>AutoCAD2000形式でファイルを保存する</summary>
            public static void Save(string path, bool overwrite)
            {
                AutoCad.Command.SendLineEsc("saveas 2000");
                AutoCad.Command.SendLine(path);
                if (overwrite)
                    AutoCad.Command.SendLine("y");
                AutoCad.Status.WaitFinish();
            }

            public static string GetDrawingName()
            {
                var drawingHandles = WindowController2.GetDrawingHandles();

                if (drawingHandles.Count != 0)
                    WindowController2.Maximize(drawingHandles[0]);

                foreach (var handle in drawingHandles)
                {
                    var title = WindowController2.GetWindowTitle(handle);
                    return Path.GetFileNameWithoutExtension(title);
                }

                return string.Empty;
            }
        }
    }
}
