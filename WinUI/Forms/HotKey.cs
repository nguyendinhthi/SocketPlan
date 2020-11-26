using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
namespace SocketPlan.WinUI
{
    //ここからパクった：http://www.k4.dion.ne.jp/~anis7742/codevault/00140.html

    /// <summary>
    /// グローバルホットキーを登録するクラス。
    /// 使用後は必ずDisposeすること。
    /// </summary>
    public class HotKey : IDisposable
    {
        HotKeyForm form;

        /// <summary>
        /// ホットキーが押されると発生する。
        /// </summary>
        public event EventHandler HotKeyPush;

        /// <summary>
        /// ホットキーを指定して初期化する。
        /// 使用後は必ずDisposeすること。
        /// </summary>
        /// <param name="modKey">修飾キー</param>
        /// <param name="key">キー</param>
        public HotKey(MOD_KEY modKey, Keys key)
        {
            this.form = new HotKeyForm(modKey, key, raiseHotKeyPush);
        }

        private void raiseHotKeyPush()
        {
            if (this.HotKeyPush != null)
            {
                this.HotKeyPush(this, EventArgs.Empty);
            }
        }

        public void Dispose()
        {
            this.form.Dispose();
        }

        private class HotKeyForm : Form
        {
            [DllImport("user32.dll")]
            private extern static int RegisterHotKey(IntPtr HWnd, int ID, MOD_KEY MOD_KEY, Keys KEY);

            [DllImport("user32.dll")]
            private extern static int UnregisterHotKey(IntPtr HWnd, int ID);

            const int WM_HOTKEY = 0x0312;
            int id;
            ThreadStart proc;

            public HotKeyForm(MOD_KEY modKey, Keys key, ThreadStart proc)
            {
                this.proc = proc;
                for (int i = 0x0000; i <= 0xbfff; i++)
                {
                    if (RegisterHotKey(this.Handle, i, modKey, key) != 0)
                    {
                        this.id = i;
                        break;
                    }
                }
            }

            protected override void WndProc(ref System.Windows.Forms.Message m)
            {
                base.WndProc(ref m);

                if (m.Msg == WM_HOTKEY)
                {
                    if ((int)m.WParam == this.id)
                    {
                        this.proc();
                    }
                }
            }

            protected override void Dispose(bool disposing)
            {
                UnregisterHotKey(this.Handle, this.id);
                base.Dispose(disposing);
            }
        }
    }

    /// <summary>
    /// HotKeyクラスの初期化時に指定する修飾キー
    /// </summary>
    public enum MOD_KEY : int
    {
        NONE = 0x0000,
        ALT = 0x0001,
        CONTROL = 0x0002,
        SHIFT = 0x0004,
    }
}
