namespace SocketPlan.WinUI
{
    partial class CommandBaseForm
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
            this.components = new System.ComponentModel.Container();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.shortcutContainer = new Edsa.CustomControl.ShortcutContainer(this.components);
            this.panel = new SocketPlan.WinUI.InheritableFlowLayoutPanel();
            this.SuspendLayout();
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 0;
            this.toolTip.ReshowDelay = 0;
            // 
            // shortcutContainer
            // 
            this.shortcutContainer.ParentForm = this;
            this.shortcutContainer.Relations = new Edsa.CustomControl.ShortcutRelation[0];
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.SystemColors.Window;
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(240, 96);
            this.panel.TabIndex = 5;
            // 
            // CommandBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 96);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "CommandBaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Deactivate += new System.EventHandler(this.CommandBaseForm_Deactivate);
            this.Load += new System.EventHandler(this.CommandBaseForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CommandBaseForm_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        protected InheritableFlowLayoutPanel panel;
        protected Edsa.CustomControl.ShortcutContainer shortcutContainer;
        protected System.Windows.Forms.ToolTip toolTip;
    }
}