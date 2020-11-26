namespace SocketPlan.WinUI
{
    partial class StringInfoPanel
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
            this.valueLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // titleValueLabel
            // 
            this.titleValueLabel.Visible = false;
            // 
            // titleLabel
            // 
            this.titleLabel.Visible = false;
            // 
            // findButton
            // 
            this.findButton.Visible = false;
            // 
            // valueLabel
            // 
            this.valueLabel.AutoSize = true;
            this.valueLabel.Location = new System.Drawing.Point(3, 8);
            this.valueLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(94, 12);
            this.valueLabel.TabIndex = 25;
            this.valueLabel.Text = "ここに値を代入する";
            // 
            // StringInfoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.valueLabel);
            this.Name = "StringInfoPanel";
            this.Size = new System.Drawing.Size(200, 20);
            this.Controls.SetChildIndex(this.titleValueLabel, 0);
            this.Controls.SetChildIndex(this.findButton, 0);
            this.Controls.SetChildIndex(this.titleLabel, 0);
            this.Controls.SetChildIndex(this.valueLabel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label valueLabel;
    }
}
