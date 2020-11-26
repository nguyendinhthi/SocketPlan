using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Edsa.AutoCadProxy
{
    //AutoCADのコマンドラインを監視して、処理を制御するメソッド
    public partial class AutoCad
    {
        public class Status
        {
            #region コマンド履歴チェック

            public static bool IsCanceled()
            {
                return AutoCad.Status.ContainTextInLastHistory("*キャンセル*", "*Cancel*");
            }

            public static bool ContainTextInLastHistory(string japaneseText, string englishText)
            {
                string history = AutoCad.Status.GetLastHistory();

                if (history.ToUpper().Contains(japaneseText.ToUpper()) ||
                    history.ToUpper().Contains(englishText.ToUpper()))
                    return true;

                return false;
            }

            public static bool MatchTextInLastHistory(string japaneseText, string englishText)
            {
                string history = AutoCad.Status.GetLastHistory();

                if (history == japaneseText || history == englishText)
                    return true;

                return false;
            }

            private static string GetLastHistory()
            {
                List<string> histories = AutoCad.Status.GetCommandHistories();
                return histories[histories.Count - 1];
            }

            private static List<string> GetCommandHistories()
            { //WM_GETTEXTを使った方法では、改行を取得できないし新しい行が数行漏れるので、クリップボード経由で取ることにした。

                //処理途中でクリップボードを上書きしちゃうので退避しておいて後で復元する。
                //↓でケアしていない形式をコピーしていた場合は復元されません。
                string format = null;
                string[] formatList = { "AutoCAD-LT15", DataFormats.Text, DataFormats.Bitmap };
                foreach (string f in formatList)
                {
                    if (Clipboard.ContainsData(f))
                    {
                        format = f;
                        break;
                    }
                }

                object backup = Clipboard.GetData(format);

                //コマンドウィンドウのハンドルを取得する
                var commandWindowHandle = WindowController2.GetCommandWindowHandle();

                //コマンドウィンドウに対してコピー命令を発行し、コマンド履歴をクリップボードに入れる。
                //32781（0x800D)は編集(E)→ヒストリーをコピー(H) 時に送られるメッセージ。Spy++で見れる
                WindowController2.SendCommand(commandWindowHandle, 32781);

                string clip;
                //たまにクリップボードのコピーがスカるので、取れるまでループさせる。
                while (true)
                {
                    clip = Clipboard.GetText();

                    if (!string.IsNullOrEmpty(clip))
                        break;
                }

                string[] histories = clip.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                if (!string.IsNullOrEmpty(format))
                    Clipboard.SetData(format, backup);

                return new List<string>(histories);
            }

            #endregion

            #region コマンドプロンプトチェック

            public static bool IsCommandFinish()
            {
                return AutoCad.Status.MatchTextInCommandPrompt("コマンド: ", "Command: ");
            }

            public static bool ContainTextInCommandPrompt(string japaneseText, string englishText)
            {
                string text = AutoCad.Status.GetPromptText();

                if (text.Contains(japaneseText) || text.Contains(englishText))
                    return true;
                
                return false;
            }

            public static bool MatchTextInCommandPrompt(string japaneseText, string englishText)
            {
                string text = AutoCad.Status.GetPromptText();

                if (text == japaneseText || text == englishText)
                    return true;

                return false;
            }

            private static string GetPromptText()
            {
                var promptHandle = WindowController2.GetPromptHandle();

                return WindowController2.GetText(promptHandle);
            }

            #endregion

            #region Wait系

            public enum EventResult
            {
                Canceled,
                Prompted,
                Finished,
                Historied,
                RightClicked
            }


            public static void WaitHistory(string text)
            {
                AutoCad.Status.WaitEvent(true, false, false, "", "", true, text, text);
            }

            public static void WaitHistory(string jpText, string engText)
            {
                AutoCad.Status.WaitEvent(true, false, false, "", "", true, jpText, engText);
            }

            public static EventResult WaitPrompt(string jpText, string engText)
            {
                while (true)
                {
                        if (AutoCad.Status.IsCommandFinish())
                            return EventResult.Finished;

                        if (AutoCad.Status.ContainTextInCommandPrompt(jpText, engText))
                            return EventResult.Prompted;
                }
            }

            public static void WaitFinish()
            {
                AutoCad.Status.WaitEvent(false, true, false, "", "", false, "", "");
            }

            public static void WaitCancelOrFinish()
            {
                AutoCad.Status.WaitEvent(true, true, false, "", "", false, "", "");
            }

            public static EventResult WaitEvent(bool catchCancel, bool catchFinish, bool catchPrompt, string promptJp, string promptEng, bool catchHistory, string historyJp, string historyEng)
            {
                while (true)
                {
                    if (catchCancel)
                        if (AutoCad.Status.IsCanceled())
                            return EventResult.Canceled;

                    if (catchFinish)
                        if (AutoCad.Status.IsCommandFinish())
                            return EventResult.Finished;

                    if (catchPrompt)
                        if (AutoCad.Status.MatchTextInCommandPrompt(promptJp, promptEng))
                            return EventResult.Prompted;

                    if (catchHistory)
                        if (AutoCad.Status.ContainTextInLastHistory(historyJp, historyEng))
                            return EventResult.Historied;
                }
            }

            #region 右クリックフック（安定しないので、やめる）

            //private static bool isRightClicked;

            //private static void HookRightClick()
            //{
            //    AutoCad.Status.isRightClicked = false;

            //    GlobalMouseCapture.MouseDown += new EventHandler<GlobalMouseCapture.MouseCaptureEventArgs>(GlobalMouseCapture_MouseDown);
            //}

            //private static void UnhookRightClick()
            //{
            //    GlobalMouseCapture.MouseDown -= new EventHandler<GlobalMouseCapture.MouseCaptureEventArgs>(GlobalMouseCapture_MouseDown);
            //    AutoCad.Status.isRightClicked = false;
            //}

            //private static void GlobalMouseCapture_MouseDown(object sender, GlobalMouseCapture.MouseCaptureEventArgs e)
            //{
            //    if (e.Button != GlobalMouseCapture.MouseButtons.Right)
            //        return;

            //    var autocadHandle = WindowController.GetAutoCadHandle();
            //    var topWindowHandle = WindowController.GetTopWindowHandle();

            //    if (autocadHandle != topWindowHandle)
            //        return;

            //    var rect = WindowController.GetWindowSize(autocadHandle);
            //    if (!(rect.Left <= e.X && e.X <= rect.Right && rect.Bottom <= e.Y && e.Y <= rect.Top))
            //        return;

            //    AutoCad.Status.isRightClicked = true;
            //    e.Cancel = true; //Windowsの右クリック押下を破棄する。
            //}

            #endregion

            #endregion

            #region ごみ
            /// <summary>コマンドプロンプトが「ｺﾏﾝﾄﾞ: 」か「Command: 」になるまで待つ</summary>
            //public static bool WaitCommandFinish()
            //{
            //    return AutoCad.Status.WaitCommand("ｺﾏﾝﾄﾞ: ", "Command: ");
            //}

            /// <summary>コマンドプロンプトの文字列が指定したものになるまで待つ。待っている間にキャンセルされたらfalseを返す</summary>
            //public static bool WaitCommand(string japaneseText, string englishText)
            //{
            //    //コマンドプロンプト（コマンド入力欄）のハンドルを取得する
            //    var promptHandle = WindowController.GetPromptHandle();

            //    while (true)
            //    {
            //        string text = WindowController.GetText(promptHandle);

            //        if (text.Contains(japaneseText) || text.Contains(englishText))
            //            return true;

            //        if (text.Contains("ｺﾏﾝﾄﾞ: ") || text.Contains("Command: "))
            //            return false;

            //        System.Threading.Thread.Sleep(100);
            //    }
            //}

            /// <summary>コマンド履歴の最終行に指定した文字列が現れるまで待つ。完全一致か部分一致かを指定できる</summary>
            //public static bool WaitHistory(string japaneseText, string englishText, bool fullMatch)
            //{
            //    while (true)
            //    {
            //        List<string> histories = AutoCad.Status.GetCommandHistories();
            //        if (1 <= histories.Count)
            //        {
            //            string lastHistory = histories[histories.Count - 1];

            //            if (fullMatch)
            //            {
            //                if (lastHistory == japaneseText || lastHistory == englishText)
            //                    return true;
            //            }
            //            else
            //            {
            //                if (lastHistory.Contains(japaneseText) || lastHistory.Contains(englishText))
            //                    return true;
            //            }

            //            if (AutoCad.Status.IsCanceled())
            //                return false;
            //        }

            //        System.Threading.Thread.Sleep(100);
            //    }
            //}
            #endregion
        }
    }
}
