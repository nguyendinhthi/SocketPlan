namespace SocketPlan.WinUI
{
    partial class ErrorDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorDialog));
            this.detailsText = new System.Windows.Forms.TextBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.copyButton = new System.Windows.Forms.Button();
            this.iconPictureBox = new System.Windows.Forms.PictureBox();
            this.messageText = new System.Windows.Forms.TextBox();
            this.detailsButton = new System.Windows.Forms.Button();
            this.infoPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.infoLabel = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.approvalControl = new SocketPlan.WinUI.ApprovalControl();
            this.lineLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).BeginInit();
            this.infoPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // detailsText
            // 
            this.detailsText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.detailsText.Location = new System.Drawing.Point(12, 164);
            this.detailsText.Multiline = true;
            this.detailsText.Name = "detailsText";
            this.detailsText.ReadOnly = true;
            this.detailsText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.detailsText.Size = new System.Drawing.Size(327, 186);
            this.detailsText.TabIndex = 5;
            this.detailsText.Text = "スタックトレース1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n0\r\n1\r\n2\r\n3\r\n4\r\n5\r\n6";
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(234, 135);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(105, 23);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "C&lose";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // copyButton
            // 
            this.copyButton.Location = new System.Drawing.Point(88, 135);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(75, 23);
            this.copyButton.TabIndex = 3;
            this.copyButton.Text = "&Copy";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
            // 
            // iconPictureBox
            // 
            this.iconPictureBox.Location = new System.Drawing.Point(12, 12);
            this.iconPictureBox.Name = "iconPictureBox";
            this.iconPictureBox.Size = new System.Drawing.Size(32, 32);
            this.iconPictureBox.TabIndex = 1;
            this.iconPictureBox.TabStop = false;
            // 
            // messageText
            // 
            this.messageText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.messageText.Location = new System.Drawing.Point(50, 12);
            this.messageText.Multiline = true;
            this.messageText.Name = "messageText";
            this.messageText.ReadOnly = true;
            this.messageText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.messageText.Size = new System.Drawing.Size(289, 90);
            this.messageText.TabIndex = 0;
            this.messageText.Text = "エラー概要1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8";
            // 
            // detailsButton
            // 
            this.detailsButton.Location = new System.Drawing.Point(10, 135);
            this.detailsButton.Name = "detailsButton";
            this.detailsButton.Size = new System.Drawing.Size(75, 23);
            this.detailsButton.TabIndex = 2;
            this.detailsButton.Text = "▼&Details";
            this.detailsButton.UseVisualStyleBackColor = true;
            this.detailsButton.Click += new System.EventHandler(this.detailsButton_Click);
            // 
            // infoPanel
            // 
            this.infoPanel.AutoScroll = true;
            this.infoPanel.Controls.Add(this.infoLabel);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.infoPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.infoPanel.Location = new System.Drawing.Point(347, 0);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(217, 362);
            this.infoPanel.TabIndex = 1;
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
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.approvalControl);
            this.mainPanel.Controls.Add(this.lineLabel);
            this.mainPanel.Controls.Add(this.iconPictureBox);
            this.mainPanel.Controls.Add(this.detailsText);
            this.mainPanel.Controls.Add(this.copyButton);
            this.mainPanel.Controls.Add(this.detailsButton);
            this.mainPanel.Controls.Add(this.closeButton);
            this.mainPanel.Controls.Add(this.messageText);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(347, 362);
            this.mainPanel.TabIndex = 0;
            // 
            // approvalControl
            // 
            this.approvalControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.approvalControl.Location = new System.Drawing.Point(46, 105);
            this.approvalControl.Margin = new System.Windows.Forms.Padding(0);
            this.approvalControl.Message = null;
            this.approvalControl.MessageId = null;
            this.approvalControl.Name = "approvalControl";
            this.approvalControl.Size = new System.Drawing.Size(296, 29);
            this.approvalControl.TabIndex = 1;
            this.approvalControl.Approved += new System.EventHandler(this.approvalControl_Approved);
            // 
            // lineLabel
            // 
            this.lineLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lineLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lineLabel.Location = new System.Drawing.Point(345, 3);
            this.lineLabel.Name = "lineLabel";
            this.lineLabel.Size = new System.Drawing.Size(2, 356);
            this.lineLabel.TabIndex = 6;
            // 
            // ErrorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(564, 362);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.infoPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Error";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.ErrorDialog_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ErrorDialog_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).EndInit();
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox detailsText;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.PictureBox iconPictureBox;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.TextBox messageText;
        private System.Windows.Forms.Button detailsButton;
        private System.Windows.Forms.FlowLayoutPanel infoPanel;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label lineLabel;
        private ApprovalControl approvalControl;
    }
}