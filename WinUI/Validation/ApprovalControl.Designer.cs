namespace SocketPlan.WinUI
{
    partial class ApprovalControl
    {
        /// <summary> 
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.pwText = new System.Windows.Forms.TextBox();
            this.idText = new System.Windows.Forms.TextBox();
            this.pwLabel = new System.Windows.Forms.Label();
            this.approveButton = new System.Windows.Forms.Button();
            this.idLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pwText
            // 
            this.pwText.Location = new System.Drawing.Point(137, 5);
            this.pwText.Name = "pwText";
            this.pwText.PasswordChar = '*';
            this.pwText.Size = new System.Drawing.Size(75, 19);
            this.pwText.TabIndex = 9;
            this.pwText.TextChanged += new System.EventHandler(this.Text_TextChanged);
            this.pwText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Text_KeyDown);
            // 
            // idText
            // 
            this.idText.Location = new System.Drawing.Point(27, 5);
            this.idText.Name = "idText";
            this.idText.Size = new System.Drawing.Size(75, 19);
            this.idText.TabIndex = 7;
            this.idText.TextChanged += new System.EventHandler(this.Text_TextChanged);
            this.idText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Text_KeyDown);
            // 
            // pwLabel
            // 
            this.pwLabel.AutoSize = true;
            this.pwLabel.Location = new System.Drawing.Point(108, 8);
            this.pwLabel.Name = "pwLabel";
            this.pwLabel.Size = new System.Drawing.Size(23, 12);
            this.pwLabel.TabIndex = 8;
            this.pwLabel.Text = "&PW:";
            // 
            // approveButton
            // 
            this.approveButton.Location = new System.Drawing.Point(218, 3);
            this.approveButton.Name = "approveButton";
            this.approveButton.Size = new System.Drawing.Size(75, 23);
            this.approveButton.TabIndex = 10;
            this.approveButton.Text = "Approve";
            this.approveButton.UseVisualStyleBackColor = true;
            this.approveButton.Click += new System.EventHandler(this.approveButton_Click);
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(3, 8);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(18, 12);
            this.idLabel.TabIndex = 6;
            this.idLabel.Text = "&ID:";
            // 
            // ApprovalControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pwText);
            this.Controls.Add(this.idText);
            this.Controls.Add(this.pwLabel);
            this.Controls.Add(this.approveButton);
            this.Controls.Add(this.idLabel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ApprovalControl";
            this.Size = new System.Drawing.Size(296, 29);
            this.Load += new System.EventHandler(this.ApprovalControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.TextBox pwText;
        protected System.Windows.Forms.TextBox idText;
        protected System.Windows.Forms.Label pwLabel;
        protected System.Windows.Forms.Button approveButton;
        protected System.Windows.Forms.Label idLabel;

    }
}
