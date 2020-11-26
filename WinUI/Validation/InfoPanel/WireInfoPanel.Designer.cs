namespace SocketPlan.WinUI
{
    partial class WireInfoPanel
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
            this.startLocationValueLabel = new System.Windows.Forms.Label();
            this.startLabel = new System.Windows.Forms.Label();
            this.floorLabel = new System.Windows.Forms.Label();
            this.floorValueLabel = new System.Windows.Forms.Label();
            this.endLocationValueLabel = new System.Windows.Forms.Label();
            this.endLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Size = new System.Drawing.Size(31, 12);
            this.titleLabel.Text = "Wire";
            // 
            // startLocationValueLabel
            // 
            this.startLocationValueLabel.AutoSize = true;
            this.startLocationValueLabel.Location = new System.Drawing.Point(59, 38);
            this.startLocationValueLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.startLocationValueLabel.Name = "startLocationValueLabel";
            this.startLocationValueLabel.Size = new System.Drawing.Size(99, 12);
            this.startLocationValueLabel.TabIndex = 24;
            this.startLocationValueLabel.Text = "12345.67, 98765.43";
            // 
            // startLabel
            // 
            this.startLabel.AutoSize = true;
            this.startLabel.Location = new System.Drawing.Point(3, 38);
            this.startLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(32, 12);
            this.startLabel.TabIndex = 23;
            this.startLabel.Text = "Start:";
            // 
            // floorLabel
            // 
            this.floorLabel.AutoSize = true;
            this.floorLabel.Location = new System.Drawing.Point(3, 23);
            this.floorLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.floorLabel.Name = "floorLabel";
            this.floorLabel.Size = new System.Drawing.Size(33, 12);
            this.floorLabel.TabIndex = 22;
            this.floorLabel.Text = "Floor:";
            // 
            // floorValueLabel
            // 
            this.floorValueLabel.AutoSize = true;
            this.floorValueLabel.Location = new System.Drawing.Point(59, 23);
            this.floorValueLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.floorValueLabel.Name = "floorValueLabel";
            this.floorValueLabel.Size = new System.Drawing.Size(11, 12);
            this.floorValueLabel.TabIndex = 25;
            this.floorValueLabel.Text = "1";
            // 
            // endLocationValueLabel
            // 
            this.endLocationValueLabel.AutoSize = true;
            this.endLocationValueLabel.Location = new System.Drawing.Point(59, 53);
            this.endLocationValueLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.endLocationValueLabel.Name = "endLocationValueLabel";
            this.endLocationValueLabel.Size = new System.Drawing.Size(99, 12);
            this.endLocationValueLabel.TabIndex = 27;
            this.endLocationValueLabel.Text = "12345.67, 98765.43";
            // 
            // endLabel
            // 
            this.endLabel.AutoSize = true;
            this.endLabel.Location = new System.Drawing.Point(3, 53);
            this.endLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.endLabel.Name = "endLabel";
            this.endLabel.Size = new System.Drawing.Size(26, 12);
            this.endLabel.TabIndex = 26;
            this.endLabel.Text = "End:";
            // 
            // WireInfoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.endLocationValueLabel);
            this.Controls.Add(this.endLabel);
            this.Controls.Add(this.startLocationValueLabel);
            this.Controls.Add(this.startLabel);
            this.Controls.Add(this.floorLabel);
            this.Controls.Add(this.floorValueLabel);
            this.Name = "WireInfoPanel";
            this.Size = new System.Drawing.Size(200, 65);
            this.Controls.SetChildIndex(this.titleValueLabel, 0);
            this.Controls.SetChildIndex(this.titleLabel, 0);
            this.Controls.SetChildIndex(this.floorValueLabel, 0);
            this.Controls.SetChildIndex(this.floorLabel, 0);
            this.Controls.SetChildIndex(this.startLabel, 0);
            this.Controls.SetChildIndex(this.startLocationValueLabel, 0);
            this.Controls.SetChildIndex(this.endLabel, 0);
            this.Controls.SetChildIndex(this.endLocationValueLabel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label startLocationValueLabel;
        private System.Windows.Forms.Label startLabel;
        private System.Windows.Forms.Label floorLabel;
        private System.Windows.Forms.Label floorValueLabel;
        private System.Windows.Forms.Label endLocationValueLabel;
        private System.Windows.Forms.Label endLabel;
    }
}
