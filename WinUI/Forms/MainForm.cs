using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using SocketPlan.WinUI.SocketPlanServiceReference;
using Edsa.AutoCadProxy;
using SocketPlan.WinUI.Properties;

namespace SocketPlan.WinUI
{
    public partial class MainForm : Form
    {
        HotKey hotKey;
        HotKey hotKeyCtrlP;

        #region BackgroundWorkerでClipboardを動かす為の仕組み

        /// <summary>
        ///メインスレッド以外ではClipboard.SetTextがエラーになる。実行するとThreadStateExceptionが発生します。
        ///なので、メインスレッドのMainFormをどこからでも参照できるようにし、MainFormにてコピーする。
        /// </summary>
        private static MainForm instance;
        public static MainForm Instance { get { return instance; } }

        public delegate void SetClipboardDataDelegate(string text);
        public void SetClipboardText(string text)
        {
            if (this.InvokeRequired)
                this.Invoke(new SetClipboardDataDelegate(SetClipboardText), text);
            else
                Clipboard.SetText(text);
        }

        #endregion

        public MainForm()
        {
            InitializeComponent();
            instance = this; //Clipboard用にインスタンスを保持します。
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.command0Button.Focus();

#if !DEBUG
            try
            {
                var dialog = new ProgressDialog(new MasterFileLoader().Run);
                dialog.ShowDialog();
                if (dialog.DialogResult == DialogResult.Abort)
                    throw dialog.Error;
            }
            catch (Exception ex)
            {
                MessageDialog.ShowWarning(ex.Message);
            }
#endif

#if DEBUG
            command1Button.BackColor = Color.HotPink;
            command2Button.BackColor = Color.HotPink;
            command4Button.BackColor = Color.HotPink;
            command5Button.BackColor = Color.HotPink;
            command4Button.BackColor = Color.HotPink;
            command0Button.BackColor = Color.HotPink;
            moveIcon.BackColor = Color.HotPink;
            transformButton.BackColor = Color.HotPink;
            closeButton.BackColor = Color.HotPink;
            drawingMenuPanel.BackColor = Color.HotPink;
#endif

            this.TopMost = true; //プロパティで設定すると、なぜか効かないことがある。
            this.hotKey = new HotKey(MOD_KEY.NONE, Keys.F1);
            this.hotKey.HotKeyPush += new EventHandler(hotKey_HotKeyPush);
            this.hotKeyCtrlP = new HotKey(MOD_KEY.CONTROL, Keys.P);
            this.hotKeyCtrlP.HotKeyPush += new EventHandler(hotKey_HotKeyPushAndShowMessage);
            Static.ConstructionChanged += new EventHandler(this.ConstructionChanged);
            Static.ConstructionCode = null; //これでイベント走らせて、Enableを設定する
        }

        //Static.ConstructionCodeが更新された時に走る処理
        private void ConstructionChanged(object sender, System.EventArgs e)
        {
            var enable = !string.IsNullOrEmpty(Static.ConstructionCode);

            this.command1Button.Enabled = enable;
            this.command2Button.Enabled = enable;
            this.command3Button.Enabled = enable;
            this.command4Button.Enabled = enable;
        }

        private void hotKey_HotKeyPush(object sender, EventArgs e)
        {
            //このフォームからモーダルダイアログを出した状態でこのフォームをActivateすると、
            //なぜかフォーカスできてしまう。
            //モーダルダイアログを出している時はCanFocusがfalseぽいので、それで制御する。

            if(this.CanFocus) 
                this.Activate();
        }

        private void hotKey_HotKeyPushAndShowMessage(object sender, EventArgs e)
        {
            try
            {
                if (this.CanFocus)
                {
                    this.Activate();
                    //図面が開かれていないときはメッセージを表示
                    if (!string.IsNullOrEmpty(Static.ConstructionCode))
                    {
                        this.Cursor = Cursors.WaitCursor;

                        var saveAndPlot = new SaveAndPlot();
                        saveAndPlot.Run();

                        this.Cursor = Cursors.Default;
                        MessageDialog.ShowInformation(this, Messages.Finished());
                    }
                    else
                        MessageDialog.ShowWarning(Messages.CanNotUseAutoCADPlot());
                }
            }
            catch (Exception ex)
            {
                MessageDialog.ShowError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        #region 作図メニュー

        /// <summary>自動配置</summary>
        private void command1Button_Click(object sender, EventArgs e)
        {
            AutoGenerateForm.Instance.Show(this, this.command1Button);
        }

        /// <summary>手動編集</summary>
        private void command2Button_Click(object sender, EventArgs e)
        {
            EditForm.Instance.Show(this, this.command2Button);
        }

        /// <summary>フレーミング</summary>
        private void command3Button_Click(object sender, EventArgs e)
        {
            FramingForm.Instance.Show(this, this.command3Button);
        }

        /// <summary>データ出力</summary>
        private void command4Button_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                var exporter = new Exporter();
                exporter.Run();

                MessageDialog.ShowInformation(this, Messages.Finished());
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>マスタメンテ</summary>
        private void command5Button_Click(object sender, EventArgs e)
        {
            MaintenanceForm.Instance.Show(this, this.command5Button);
        }

        /// <summary>ファイルメニュー</summary>
        private void command0Button_Click(object sender, EventArgs e)
        {
            Command0Form.Instance.Show(this, this.command0Button);
        }

        #endregion

        #region システムメニュー

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Static.ConstructionCode))
            {
                if (MessageDialog.ShowYesNo(Messages.Quit()) == DialogResult.No)
                    return;
            }
            else
            {
                if (Command0Form.Instance.CloseDrawings() == DialogResult.Cancel)
                    return;
            }

            //以下のフォームが残っているとCloseをキャンセルしてくる。
            EquipmentSelectForm.DisposeInstance();
            TextSelectForm.DisposeInstance();
            this.Close();
        }

        private void transformButton_Click(object sender, EventArgs e)
        {
            Size menuSize = new Size(this.Size.Height, this.Size.Width);
            Size systemMenuSize = new Size(this.systemMenuPanel.Size.Height, this.systemMenuPanel.Size.Width);

            this.Size = menuSize;

            if (this.Size.Width > this.Size.Height)
                this.systemMenuPanel.Dock = DockStyle.Right;
            else
                this.systemMenuPanel.Dock = DockStyle.Top;

            this.systemMenuPanel.Size = systemMenuSize;
        }

        #region タイトルバー以外でフォームをドラッグする
        const int WM_SYSCOMMAND = 0x112;
        const int SC_MOVE = 0xF010;
        [DllImport("User32.dll")]
        public static extern bool SetCapture(IntPtr hWnd);
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void moveIcon_MouseDown(object sender, MouseEventArgs e)
        {
            SetCapture(this.Handle);
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE | 2, 0);
        }
        #endregion

        #endregion

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.hotKey.Dispose(); //Disposeでホットキーを解除している
        }
    }
}
