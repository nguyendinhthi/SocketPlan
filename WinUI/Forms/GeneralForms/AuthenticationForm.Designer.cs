namespace SocketPlan.WinUI
{
    partial class AuthenticationForm
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.approvalControl = new ApprovalControl();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(308, 12);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // approvalControl
            // 
            this.approvalControl.Location = new System.Drawing.Point(9, 9);
            this.approvalControl.Margin = new System.Windows.Forms.Padding(0);
            this.approvalControl.Message = null;
            this.approvalControl.MessageId = null;
            this.approvalControl.Name = "approvalControl";
            this.approvalControl.NeedMessage = false;
            this.approvalControl.Size = new System.Drawing.Size(369, 29);
            this.approvalControl.TabIndex = 0;
            this.approvalControl.Approved += new System.EventHandler(this.approvalControl_Approved);
            // 
            // AuthenticationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(398, 47);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.approvalControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AuthenticationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Authentication";
            this.ResumeLayout(false);

        }

        #endregion

        private ApprovalControl approvalControl;
        private System.Windows.Forms.Button cancelButton;
    }
}