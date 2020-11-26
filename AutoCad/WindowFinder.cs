using System;

namespace Edsa.AutoCadProxy
{
    public partial class WindowController2
    {
        //WindowControllerからしか呼び出す事は無いので、
        //インナークラスにして見えなくしておこう・・・
        private class WindowFinder
        {
            public IntPtr FoundWindowHandle { get; set; }
            private string targetTitle;
            private string jpTitle;
            private string enTitle;

            public WindowFinder(string targetWindowTitle)
            {
                this.targetTitle = targetWindowTitle;
            }

            public WindowFinder(string jpTitle, string enTitle)
            {
                this.jpTitle = jpTitle;
                this.enTitle = enTitle;
            }

            /// <summary>
            /// EnumChildWindowsメソッドの引数に渡すコールバックメソッド
            /// </summary>
            public int FindChildWindow(IntPtr childWindowHandle, int lParam)
            {
                var title = WindowController2.GetWindowTitle(childWindowHandle);

                if (string.IsNullOrEmpty(this.targetTitle))
                {
                    if (title.Contains(this.jpTitle) || title.Contains(this.enTitle))
                    {
                        this.FoundWindowHandle = childWindowHandle;
                        return 0; //見つかったら処理中止
                    }
                }
                else
                {
                    if (title.Contains(this.targetTitle))
                    {
                        this.FoundWindowHandle = childWindowHandle;
                        return 0; //見つかったら処理中止
                    }
                }

                return 1; //処理続行

            }
        }
    }
}
