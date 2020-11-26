namespace SocketPlan.WinUI
{
    partial class TextSelectForm
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
            this.SuspendLayout();
            // 
            // TextSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 408);
            this.Name = "TextSelectForm";
            this.Deactivate += new System.EventHandler(this.TextSelectForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TextSelectForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextSelectForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}