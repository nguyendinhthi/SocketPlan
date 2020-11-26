using System;
using System.Drawing;
using System.Windows.Forms;
using SocketPlan.WinUI.Entities.CADEntity;

namespace SocketPlan.WinUI
{
    public partial class ErrorDialog : Form
    {
        #region フィールド変数

        private bool isHidingDetails;
        private int currenHeight;
        private Size minSize;
        private Size maxSize;

        #endregion

        #region コンストラクタ

        private ErrorDialog()
        {
            this.InitializeComponent();
        }

        public ErrorDialog(string messageId)
            : this()
        {
            this.messageText.Text = messageId;
            this.detailsText.Text = Environment.StackTrace;

            this.InitializeSize();
            this.DrawIcon(SystemIcons.Error);
        }

        public ErrorDialog(string messageId, bool canApprove)
            : this(messageId)
        {
            this.approvalControl.Visible = canApprove;
        }

        /// <summary>処理継続可能な警告用コンストラクタ。引数のintは意味ありません。</summary>
        public ErrorDialog(string messageId, int forWarning)
            : this(messageId)
        {
            this.approvalControl.Visible = false;
            this.Text = "Warning";
            this.closeButton.Text = "C&ontinue";
            this.DrawIcon(SystemIcons.Warning); //上書きしてしまえ。
        }

        private void InitializeSize()
        {
            var minX = this.Width;
            var minY = this.Height - (this.detailsText.Height + this.detailsText.Margin.Vertical);
            this.minSize = new Size(minX, minY);
            this.MinimumSize = this.minSize;

            var screen = Screen.PrimaryScreen.WorkingArea;
            this.maxSize = new Size(screen.Width, screen.Height);

            this.HideDetails();
        }

        private void DrawIcon(Icon icon)
        {
            this.iconPictureBox.Image = icon.ToBitmap();
        }

        #endregion

        #region publicメソッド

        public void AddInfo(Symbol symbol)
        {
            this.infoPanel.Controls.Add(new SymbolInfoPanel(symbol));
        }

        public void AddInfo(Wire wire)
        {
            var infoWires = wire.GetAllChildrenWire();
            foreach (var infoWire in infoWires)
            {
                if (wire is RisingWire)
                    this.infoPanel.Controls.Add(new WireInfoPanel(infoWire, (wire as RisingWire)));
                else
                    this.infoPanel.Controls.Add(new WireInfoPanel(infoWire));
            }
        }

        public void AddInfo(RoomObject room)
        {
            this.infoPanel.Controls.Add(new RoomInfoPanel(room));
        }

        public void AddInfo(TextObject text)
        {
            this.infoPanel.Controls.Add(new TextInfoPanel(text));
        }

        public void AddInfo(string value)
        {
            this.infoPanel.Controls.Add(new StringInfoPanel(value));
        }

        #endregion

        #region イベントハンドラ

        private void ErrorDialog_Shown(object sender, EventArgs e)
        {
            this.approvalControl.MessageId = this.messageText.Text;
            this.approvalControl.Message = this.CreateMessage();
            this.closeButton.Focus(); //警告モードの時は、approvalControlが無効なので、こちらにフォーカスされる
            this.approvalControl.Focus();

#if DEBUG
            this.approvalControl.Enter押すだけで承認できるように整える();
#endif
        }

        private void detailsButton_Click(object sender, EventArgs e)
        {
            if (this.isHidingDetails)
                this.ShowDetails();
            else
                this.HideDetails();
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            MainForm.Instance.SetClipboardText(this.approvalControl.Message);
        }

        private void ErrorDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
                return;

            this.DialogResult = DialogResult.Cancel;
        }

        private void approvalControl_Approved(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        #endregion

        private void closeButton_Click(object sender, EventArgs e)
        {
            //警告のときは続行させたい
            if (this.closeButton.Text == "C&ontinue")
                this.DialogResult = DialogResult.OK;
        }
        #region privateメソッド

        private void HideDetails()
        {
            this.currenHeight = this.Height;
            this.MaximumSize = this.minSize;
            this.detailsButton.Text = "▼&Details";
            this.isHidingDetails = true;
        }

        private void ShowDetails()
        {
            this.MaximumSize = this.maxSize;
            this.Height = this.currenHeight;
            this.detailsButton.Text = "▲&Details";
            this.isHidingDetails = false;
        }

        private string CreateMessage()
        {
            var NL = Environment.NewLine;

            var message = string.Empty;

            message += Environment.MachineName + " " + DateTime.Now.ToString() + NL;
            message += Static.Drawing.ConstructionCodePlanRevisionNo + NL;

            message += "=== Message ===>" + NL;
            message += this.messageText.Text + NL + NL;

            message += "=== Information ===>" + NL;
            foreach (var control in this.infoPanel.Controls)
            {
                if (control is InfoPanelBase)
                    message += control.ToString() + NL + NL;
            }

            message += "=== StackTrace ===>" + NL;
            message += this.detailsText.Text;

            return message;
        }

        #endregion
    }
}