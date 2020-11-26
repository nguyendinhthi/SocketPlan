using System;
using System.Windows.Forms;
using SocketPlan.WinUI.SocketPlanServiceReference;

namespace SocketPlan.WinUI
{
    public partial class ApprovalControl : UserControl
    {
        #region コンストラクタ

        public ApprovalControl()
        {
            InitializeComponent();
            this.Approved += new EventHandler(ApprovalControl_Approved);
            this.Denied += new EventHandler(ApprovalControl_Denied);
            this.NeedMessage = true;
        }

        #endregion

        #region イベント

        /// <summary>ID・PWが承認された時に発生します。既定ではUnitWiring.approvedHistoryに承認ログを記録するだけです。</summary>
        public event EventHandler Approved;

        /// <summary>ID・PWの承認に失敗した時に発生します。既定では「Incorrect password!」とメッセージを表示します。</summary>
        public event EventHandler Denied;

        #endregion

        #region プロパティ

        /// <summary>サーバのログに残すメッセージを設定します。ボタンが押される前に必ず何かを設定してください。</summary>
        public string Message { get; set; }

        /// <summary>承認したメッセージを識別する文字列を設定します。ボタンが押される前に必ず何かを設定してください。</summary>
        public string MessageId { get; set; }

        /// <summary>上記2つが必要ないとき（falseなら承認履歴もサーバに登録しません）</summary>
        public bool NeedMessage { get; set; }

        #endregion

        #region publicメソッド

        public void SetButtonText(string text)
        {
            this.approveButton.Text = text;
        }

        /// <summary>デバッグ用</summary>
        public void Enter押すだけで承認できるように整える()
        {
            this.idText.Text = "1";
            this.pwText.Text = "1";
            this.approveButton.Focus();
        }

        #endregion

        #region イベントハンドラ

        private void ApprovalControl_Load(object sender, EventArgs e)
        {
            //ボタンのEnableを初期化したいのさ
            this.Text_TextChanged(null, null);
        }

        private void ApprovalControl_Approved(object sender, EventArgs e)
        {
            if(this.NeedMessage)
                UnitWiring.AddApprovedHistory(this.MessageId);

            this.approveButton.Enabled = false;
        }

        private void ApprovalControl_Denied(object sender, EventArgs e)
        {
            MessageBox.Show("Incorrect password!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Text_TextChanged(object sender, EventArgs e)
        {
            if (this.NeedMessage)
            {
                if (UnitWiring.AlreadyApproved(this.MessageId))
                {
                    this.approveButton.Enabled = false;
                    return;
                }
            }

            this.approveButton.Enabled =
                (!string.IsNullOrEmpty(this.idText.Text) &&
                 !string.IsNullOrEmpty(this.pwText.Text));
        }

        private void Text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.approveButton.PerformClick();
        }

        private void approveButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool isValidated;

                if (this.NeedMessage)
                {
                    if (string.IsNullOrEmpty(this.Message))
                        throw new ApplicationException("To Developer,\nApprovalControlのMessageを設定してください。");

                    if (string.IsNullOrEmpty(this.MessageId))
                        throw new ApplicationException("To Developer,\nApprovalControlのMessageIdを設定してください。");

                    using (var service = new SocketPlanServiceNoTimeout())
                    {
                        isValidated = service.ValidateUser(this.idText.Text, this.pwText.Text, Environment.MachineName, this.Message);
                    }
                }
                else
                {
                    using (var service = new SocketPlanServiceNoTimeout())
                    {
                        isValidated = service.ValidateUserWithoutMessage(this.idText.Text, this.pwText.Text, true);
                    }
                }

                if (isValidated)
                    this.Approved(sender, e);
                else
                    this.Denied(sender, e);
            }
            catch (Exception ex)
            {
                MessageDialog.ShowError(ex);
            }
        }

        #endregion
    }
}