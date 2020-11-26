namespace SocketPlan.WinUI
{
    partial class SelectableTextBox
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
            this.referenceButton = new System.Windows.Forms.Button();
            this.valueText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // referenceButton
            // 
            this.referenceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.referenceButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.referenceButton.Location = new System.Drawing.Point(155, 0);
            this.referenceButton.Margin = new System.Windows.Forms.Padding(0);
            this.referenceButton.Name = "referenceButton";
            this.referenceButton.Size = new System.Drawing.Size(23, 19);
            this.referenceButton.TabIndex = 77;
            this.referenceButton.Text = "...";
            this.referenceButton.UseVisualStyleBackColor = true;
            this.referenceButton.Click += new System.EventHandler(this.referenceButton_Click);
            // 
            // valueText
            // 
            this.valueText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.valueText.Location = new System.Drawing.Point(0, 0);
            this.valueText.Margin = new System.Windows.Forms.Padding(0);
            this.valueText.Name = "valueText";
            this.valueText.Size = new System.Drawing.Size(155, 19);
            this.valueText.TabIndex = 76;
            // 
            // SelectableTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.referenceButton);
            this.Controls.Add(this.valueText);
            this.Name = "SelectableTextBox";
            this.Size = new System.Drawing.Size(178, 19);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button referenceButton;
        public System.Windows.Forms.TextBox valueText;
    }
}
