using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Edsa.AutoCadProxy
{
    public partial class WindowController2
    {
        #region WinAPI定義

        private const int GW_HWNDNEXT = 2;
        private const int GW_CHILD = 5;
        private const int WM_COPYDATA = 0x4A;
        private const int WM_GETTEXT = 0xD;
        private const int WM_GETTEXTLENGTH = 0xE;
        private const int WM_COMMAND = 0x0111;
        private const int SW_MAXIMIZE = 3;

        private struct COPYDATASTRUCT
        {
            public IntPtr dwData;   //受信側アプリケーションに渡される値データを指定します。
            public int cbData;      //lpDataメンバが指すデータのサイズをバイト単位で指定します。
            public IntPtr lpData;   //受信側アプリケーションに渡されるデータへのポインタを指定します。
        }

        public delegate int WNDENUMPROC(IntPtr hwnd, int lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpStr, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindowEx(IntPtr hWnd, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        private static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder buff);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref COPYDATASTRUCT lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

        [DllImport("user32.dll")]
        private static extern int EnumChildWindows(IntPtr hWndParent, WNDENUMPROC lpEnumFunc, int lParam);

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        #endregion

        /// <summary>指定したウィンドウに文字列を送る</summary>
        public static void SendCommand(IntPtr windowHandle, string command)
        {
            string str = command + '\0';
            var cds = new COPYDATASTRUCT();
            cds.dwData = new IntPtr(1);
            cds.lpData = Marshal.StringToHGlobalUni(str);
            cds.cbData = Encoding.Unicode.GetByteCount(str);

            SendMessage(windowHandle, WM_COPYDATA, new IntPtr(0), ref cds);

            Marshal.FreeHGlobal(cds.lpData);
        }

        /// <summary>指定したウィンドウにコマンドを送る</summary>
        public static void SendCommand(IntPtr windowHandle, int command)
        {
            WindowController2.SendMessage(windowHandle, WM_COMMAND, new IntPtr(command), IntPtr.Zero);
        }

        /// <summary>指定したウィンドウにコマンドを送る。返事を待たずに次の処理へ移る</summary>
        public static void PostCommand(IntPtr windowHandle, int command)
        {
            WindowController2.PostMessage(windowHandle, WM_COMMAND, new IntPtr(command), IntPtr.Zero);
        }

        /// <summary>指定したウィンドウのタイトルを取得する</summary>
        public static string GetWindowTitle(IntPtr windowHandle)
        {
            StringBuilder title = new StringBuilder(300);
            WindowController2.GetWindowText(windowHandle, title, title.Capacity);

            return title.ToString();
        }

        /// <summary>MDIの子ウィンドウのうち、最前面のウィンドウハンドルを取得する</summary>
        private static IntPtr GetTopChildHandle(IntPtr windowHandle)
        {
            while (true)
            {
                IntPtr mdiHandle = WindowController2.FindWindowEx(windowHandle, IntPtr.Zero, "MDIClient", null);
                if (mdiHandle == IntPtr.Zero)
                    continue;

                var activeChildHandle = WindowController2.GetWindow(mdiHandle, GW_CHILD);
                if (activeChildHandle == IntPtr.Zero)
                    throw new AutoCadException("MDIの子ウィンドウが見つかりませんでした。");

                return activeChildHandle;
            }
        }

        /// <summary>指定したウィンドウのテキストを取得する</summary>
        public static string GetText(IntPtr windowHandle)
        {
            var byteLength = WindowController2.SendMessage(windowHandle, WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero).ToInt32();

            var buff = new StringBuilder(byteLength + 1);

            WindowController2.SendMessage(windowHandle, WM_GETTEXT, byteLength + 1, buff);

            return buff.ToString();
        }

        public static void Maximize(IntPtr windowHandle)
        {
            WindowController2.ShowWindow(windowHandle, SW_MAXIMIZE);
         }

        #region AutoCAD用
        
        public static IntPtr GetAutoCadHandle()
        {
            var processes = System.Diagnostics.Process.GetProcessesByName("acadlt");
            if (processes.Length >= 2)
                throw new AutoCadException("2 or more AutoCAD LT windows found.");

            if (processes.Length == 0)
                throw new AutoCadException("Not found AutoCAD LT window.");

            return processes[0].MainWindowHandle;
        }

        public static bool ExistAutoCad()
        {
            var processes = System.Diagnostics.Process.GetProcessesByName("acadlt");
            return processes.Length != 0;
        }

        public static IntPtr GetTopDrawingHandle()
        {
            while (true)
            {
                var autocadWindowHandle = WindowController2.GetAutoCadHandle();
                IntPtr mdiHandle = WindowController2.FindWindowEx(autocadWindowHandle, IntPtr.Zero, "MDIClient", null);
                //AutoCadのハンドルをうまくとれない時がある。その時mdiHandleがZeroになる。
                if (mdiHandle == IntPtr.Zero)
                    continue;

                var activeChildHandle = WindowController2.GetWindow(mdiHandle, GW_CHILD);
                if (activeChildHandle == IntPtr.Zero)
                    throw new AutoCadException("MDIの子ウィンドウが見つかりませんでした。");

                return activeChildHandle;
            }

        }

        public static List<IntPtr> GetDrawingHandles()
        {
            while (true)
            {
                var autocadWindowHandle = WindowController2.GetAutoCadHandle();

                IntPtr mdi = FindWindowEx(autocadWindowHandle, IntPtr.Zero, "MDIClient", null);
                if (mdi == IntPtr.Zero)
                    continue;

                List<IntPtr> childList = new List<IntPtr>();
                IntPtr child = WindowController2.GetWindow(mdi, GW_CHILD);
                while (child != IntPtr.Zero)
                {
                    childList.Add(child);

                    child = WindowController2.GetWindow(child, GW_HWNDNEXT);
                }

                return childList;
            }
        }

        /// <summary>AutoCADのコマンドウィンドウのハンドルを取得する</summary>
        public static IntPtr GetCommandWindowHandle()
        {
            var textWindowHandle = WindowController2.GetTextWindowHandle();

            var finder = new WindowFinder("Marin");

            //EnumChildWindowsは指定したウィンドウ配下のコントロール全てを、順にコールバック関数に渡す
            var result = WindowController2.EnumChildWindows(textWindowHandle, finder.FindChildWindow, 0);
            if (result == 1)
                throw new ApplicationException("AutoCADのCommandWindowが見つかりませんでした。");

            return finder.FoundWindowHandle;
        }

        private static IntPtr GetTextWindowHandle()
        {
            var finder = new WindowFinder("AutoCAD LT テキスト ウィンドウ - ", "AutoCAD LT Text Window - ");

            //EnumChildWindowsは指定したウィンドウ配下のコントロール全てを、順にコールバック関数に渡す
            var result = WindowController2.EnumChildWindows(new IntPtr(0), finder.FindChildWindow, 0);
            if (result == 1)
                throw new ApplicationException("AutoCADのTextWindowが見つかりませんでした。");

            return finder.FoundWindowHandle;
        }

        public static IntPtr GetPromptHandle()
        {
            var commandWindowHandle = WindowController2.GetCommandWindowHandle();
            return WindowController2.GetDlgItem(commandWindowHandle, 2);
        }

        public static void BringAutoCadToTop()
        {
            var handle = WindowController2.GetAutoCadHandle();
            WindowController2.BringWindowToTop(handle);
        }

        public static void BringDrawingToTop(IntPtr drawingHandle)
        {
            var currentWindowHandle = WindowController2.GetTopDrawingHandle();

            if (currentWindowHandle == drawingHandle)
                return; //既に最前面に来ていたら抜ける

            var lastBlockTableId = AutoCad.Db.BlockTable.GetModelId();

            WindowController2.BringWindowToTop(drawingHandle);

            //タイミングによってか、図面を切り替えても取得するブロックテーブルが切り替わらないことがあるので、
            //前回のブロックテーブルと違うブロックテーブルを取得できるまでひたすら取得する(5回)。
            for (var i = 0; i < 5; i++)
            {
                var blockTableId = AutoCad.Db.BlockTable.GetModelId();
                System.Threading.Thread.Sleep(100);
                if (lastBlockTableId != blockTableId)
                    break;
            }
        }

        #endregion
    }
}
