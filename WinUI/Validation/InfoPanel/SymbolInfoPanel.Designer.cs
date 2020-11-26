namespace SocketPlan.WinUI
{
    partial class SymbolInfoPanel
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
            this.floorLabel = new System.Windows.Forms.Label();
            this.locationLabel = new System.Windows.Forms.Label();
            this.locationValueLabel = new System.Windows.Forms.Label();
            this.floorValueLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Size = new System.Drawing.Size(48, 12);
            this.titleLabel.Text = "Symbol";
            // 
            // floorLabel
            // 
            this.floorLabel.AutoSize = true;
            this.floorLabel.Location = new System.Drawing.Point(3, 23);
            this.floorLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.floorLabel.Name = "floorLabel";
            this.floorLabel.Size = new System.Drawing.Size(33, 12);
            this.floorLabel.TabIndex = 2;
            this.floorLabel.Text = "Floor:";
            // 
            // locationLabel
            // 
            this.locationLabel.AutoSize = true;
            this.locationLabel.Location = new System.Drawing.Point(3, 38);
            this.locationLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.locationLabel.Name = "locationLabel";
            this.locationLabel.Size = new System.Drawing.Size(50, 12);
            this.locationLabel.TabIndex = 3;
            this.locationLabel.Text = "Location:";
            // 
            // locationValueLabel
            // 
            this.locationValueLabel.AutoSize = true;
            this.locationValueLabel.Location = new System.Drawing.Point(59, 38);
            this.locationValueLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.locationValueLabel.Name = "locationValueLabel";
            this.locationValueLabel.Size = new System.Drawing.Size(99, 12);
            this.locationValueLabel.TabIndex = 9;
            this.locationValueLabel.Text = "12345.67, 98765.43";
            // 
            // floorValueLabel
            // 
            this.floorValueLabel.AutoSize = true;
            this.floorValueLabel.Location = new System.Drawing.Point(59, 23);
            this.floorValueLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.floorValueLabel.Name = "floorValueLabel";
            this.floorValueLabel.Size = new System.Drawing.Size(11, 12);
            this.floorValueLabel.TabIndex = 12;
            this.floorValueLabel.Text = "1";
            // 
            // SymbolInfoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.locationValueLabel);
            this.Controls.Add(this.locationLabel);
            this.Controls.Add(this.floorLabel);
            this.Controls.Add(this.floorValueLabel);
            this.Name = "SymbolInfoPanel";
            this.Size = new System.Drawing.Size(200, 50);
            this.Controls.SetChildIndex(this.findButton, 0);
            this.Controls.SetChildIndex(this.floorValueLabel, 0);
            this.Controls.SetChildIndex(this.titleValueLabel, 0);
            this.Controls.SetChildIndex(this.floorLabel, 0);
            this.Controls.SetChildIndex(this.locationLabel, 0);
            this.Controls.SetChildIndex(this.locationValueLabel, 0);
            this.Controls.SetChildIndex(this.titleLabel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label floorLabel;
        private System.Windows.Forms.Label locationLabel;
        private System.Windows.Forms.Label locationValueLabel;
        private System.Windows.Forms.Label floorValueLabel;
    }
}
