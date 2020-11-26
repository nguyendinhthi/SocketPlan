namespace SocketPlan.WinUI
{
    partial class InfoPanelBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoPanelBase));
            this.titleLabel = new System.Windows.Forms.Label();
            this.findButton = new System.Windows.Forms.Button();
            this.titleValueLabel = new System.Windows.Forms.Label();
            this.lineLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.titleLabel.Location = new System.Drawing.Point(3, 8);
            this.titleLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(33, 12);
            this.titleLabel.TabIndex = 21;
            this.titleLabel.Text = "Title";
            // 
            // findButton
            // 
            this.findButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("findButton.BackgroundImage")));
            this.findButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.findButton.Location = new System.Drawing.Point(165, 8);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(32, 32);
            this.findButton.TabIndex = 14;
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // titleValueLabel
            // 
            this.titleValueLabel.AutoSize = true;
            this.titleValueLabel.Location = new System.Drawing.Point(59, 8);
            this.titleValueLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.titleValueLabel.Name = "titleValueLabel";
            this.titleValueLabel.Size = new System.Drawing.Size(57, 12);
            this.titleValueLabel.TabIndex = 19;
            this.titleValueLabel.Text = "TitleValue";
            // 
            // lineLabel
            // 
            this.lineLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lineLabel.Location = new System.Drawing.Point(3, 3);
            this.lineLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lineLabel.Name = "lineLabel";
            this.lineLabel.Size = new System.Drawing.Size(194, 2);
            this.lineLabel.TabIndex = 18;
            // 
            // InfoPanelBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.titleValueLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.findButton);
            this.Controls.Add(this.lineLabel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "InfoPanelBase";
            this.Size = new System.Drawing.Size(200, 200);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lineLabel;
        protected System.Windows.Forms.Label titleValueLabel;
        protected System.Windows.Forms.Label titleLabel;
        protected System.Windows.Forms.Button findButton;
    }
}
