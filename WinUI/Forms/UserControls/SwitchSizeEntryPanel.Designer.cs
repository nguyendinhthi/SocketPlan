namespace SocketPlan.WinUI
{
    partial class SwitchSizeEntryPanel
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
            this.infoPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.infoLabel = new System.Windows.Forms.Label();
            this.verticalSizeCombo = new System.Windows.Forms.ComboBox();
            this.typeCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.infoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // infoPanel
            // 
            this.infoPanel.AutoScroll = true;
            this.infoPanel.Controls.Add(this.infoLabel);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.infoPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.infoPanel.Location = new System.Drawing.Point(194, 0);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(217, 61);
            this.infoPanel.TabIndex = 4;
            this.infoPanel.WrapContents = false;
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.infoLabel.Location = new System.Drawing.Point(3, 3);
            this.infoLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(90, 15);
            this.infoLabel.TabIndex = 0;
            this.infoLabel.Text = "Information";
            // 
            // verticalSizeCombo
            // 
            this.verticalSizeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "1",
            "2",
            "3"});
            this.verticalSizeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.verticalSizeCombo.FormattingEnabled = true;
            this.verticalSizeCombo.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.verticalSizeCombo.Location = new System.Drawing.Point(82, 32);
            this.verticalSizeCombo.Name = "verticalSizeCombo";
            this.verticalSizeCombo.Size = new System.Drawing.Size(98, 20);
            this.verticalSizeCombo.TabIndex = 3;
            // 
            // typeCombo
            // 
            this.typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeCombo.FormattingEnabled = true;
            this.typeCombo.Location = new System.Drawing.Point(82, 8);
            this.typeCombo.Name = "typeCombo";
            this.typeCombo.Size = new System.Drawing.Size(98, 20);
            this.typeCombo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "&VerticalSize:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "&Type:";
            // 
            // SwitchSizeEntryPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.infoPanel);
            this.Controls.Add(this.verticalSizeCombo);
            this.Controls.Add(this.typeCombo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "SwitchSizeEntryPanel";
            this.Size = new System.Drawing.Size(411, 61);
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel infoPanel;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.ComboBox verticalSizeCombo;
        private System.Windows.Forms.ComboBox typeCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

    }
}
