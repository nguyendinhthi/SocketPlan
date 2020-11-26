namespace SocketPlan.WinUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            Edsa.CustomControl.ShortcutRelation shortcutRelation1 = new Edsa.CustomControl.ShortcutRelation();
            Edsa.CustomControl.ShortcutRelation shortcutRelation2 = new Edsa.CustomControl.ShortcutRelation();
            Edsa.CustomControl.ShortcutRelation shortcutRelation3 = new Edsa.CustomControl.ShortcutRelation();
            Edsa.CustomControl.ShortcutRelation shortcutRelation4 = new Edsa.CustomControl.ShortcutRelation();
            Edsa.CustomControl.ShortcutRelation shortcutRelation5 = new Edsa.CustomControl.ShortcutRelation();
            Edsa.CustomControl.ShortcutRelation shortcutRelation6 = new Edsa.CustomControl.ShortcutRelation();
            this.command1Button = new System.Windows.Forms.Button();
            this.command2Button = new System.Windows.Forms.Button();
            this.command3Button = new System.Windows.Forms.Button();
            this.command4Button = new System.Windows.Forms.Button();
            this.command5Button = new System.Windows.Forms.Button();
            this.command0Button = new System.Windows.Forms.Button();
            this.transformButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.drawingMenuPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.moveIcon = new System.Windows.Forms.PictureBox();
            this.systemMenuPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.shortcutContainer = new Edsa.CustomControl.ShortcutContainer(this.components);
            this.drawingMenuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moveIcon)).BeginInit();
            this.systemMenuPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // command1Button
            // 
            this.command1Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("command1Button.BackgroundImage")));
            this.command1Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.command1Button.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.command1Button.ForeColor = System.Drawing.Color.Black;
            this.command1Button.Location = new System.Drawing.Point(0, 0);
            this.command1Button.Margin = new System.Windows.Forms.Padding(0);
            this.command1Button.Name = "command1Button";
            this.command1Button.Size = new System.Drawing.Size(48, 48);
            this.command1Button.TabIndex = 0;
            this.command1Button.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.toolTip.SetToolTip(this.command1Button, "Auto Generate");
            this.command1Button.UseVisualStyleBackColor = true;
            this.command1Button.Click += new System.EventHandler(this.command1Button_Click);
            // 
            // command2Button
            // 
            this.command2Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("command2Button.BackgroundImage")));
            this.command2Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.command2Button.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.command2Button.ForeColor = System.Drawing.Color.Black;
            this.command2Button.Location = new System.Drawing.Point(48, 0);
            this.command2Button.Margin = new System.Windows.Forms.Padding(0);
            this.command2Button.Name = "command2Button";
            this.command2Button.Size = new System.Drawing.Size(48, 48);
            this.command2Button.TabIndex = 1;
            this.command2Button.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.toolTip.SetToolTip(this.command2Button, "Manual Edit");
            this.command2Button.UseVisualStyleBackColor = true;
            this.command2Button.Click += new System.EventHandler(this.command2Button_Click);
            // 
            // command3Button
            // 
            this.command3Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("command3Button.BackgroundImage")));
            this.command3Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.command3Button.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.command3Button.ForeColor = System.Drawing.Color.Black;
            this.command3Button.Location = new System.Drawing.Point(96, 0);
            this.command3Button.Margin = new System.Windows.Forms.Padding(0);
            this.command3Button.Name = "command3Button";
            this.command3Button.Size = new System.Drawing.Size(48, 48);
            this.command3Button.TabIndex = 2;
            this.command3Button.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.command3Button.UseVisualStyleBackColor = true;
            this.command3Button.Click += new System.EventHandler(this.command3Button_Click);
            // 
            // command4Button
            // 
            this.command4Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("command4Button.BackgroundImage")));
            this.command4Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.command4Button.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.command4Button.ForeColor = System.Drawing.Color.Black;
            this.command4Button.Location = new System.Drawing.Point(144, 0);
            this.command4Button.Margin = new System.Windows.Forms.Padding(0);
            this.command4Button.Name = "command4Button";
            this.command4Button.Size = new System.Drawing.Size(48, 48);
            this.command4Button.TabIndex = 3;
            this.command4Button.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.toolTip.SetToolTip(this.command4Button, "Export CSV");
            this.command4Button.UseVisualStyleBackColor = true;
            this.command4Button.Click += new System.EventHandler(this.command4Button_Click);
            // 
            // command5Button
            // 
            this.command5Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("command5Button.BackgroundImage")));
            this.command5Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.command5Button.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.command5Button.ForeColor = System.Drawing.Color.Black;
            this.command5Button.Location = new System.Drawing.Point(192, 0);
            this.command5Button.Margin = new System.Windows.Forms.Padding(0);
            this.command5Button.Name = "command5Button";
            this.command5Button.Size = new System.Drawing.Size(48, 48);
            this.command5Button.TabIndex = 4;
            this.command5Button.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.toolTip.SetToolTip(this.command5Button, "Master Maintenance");
            this.command5Button.UseVisualStyleBackColor = true;
            this.command5Button.Click += new System.EventHandler(this.command5Button_Click);
            // 
            // command0Button
            // 
            this.command0Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("command0Button.BackgroundImage")));
            this.command0Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.command0Button.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.command0Button.ForeColor = System.Drawing.Color.Black;
            this.command0Button.Location = new System.Drawing.Point(240, 0);
            this.command0Button.Margin = new System.Windows.Forms.Padding(0);
            this.command0Button.Name = "command0Button";
            this.command0Button.Size = new System.Drawing.Size(48, 48);
            this.command0Button.TabIndex = 5;
            this.command0Button.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.toolTip.SetToolTip(this.command0Button, "File");
            this.command0Button.UseVisualStyleBackColor = true;
            this.command0Button.Click += new System.EventHandler(this.command0Button_Click);
            // 
            // transformButton
            // 
            this.transformButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("transformButton.BackgroundImage")));
            this.transformButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.transformButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.transformButton.Location = new System.Drawing.Point(0, 16);
            this.transformButton.Margin = new System.Windows.Forms.Padding(0);
            this.transformButton.Name = "transformButton";
            this.transformButton.Size = new System.Drawing.Size(16, 16);
            this.transformButton.TabIndex = 0;
            this.toolTip.SetToolTip(this.transformButton, "Transform this window");
            this.transformButton.UseVisualStyleBackColor = true;
            this.transformButton.Click += new System.EventHandler(this.transformButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("closeButton.BackgroundImage")));
            this.closeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Location = new System.Drawing.Point(0, 32);
            this.closeButton.Margin = new System.Windows.Forms.Padding(0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(16, 16);
            this.closeButton.TabIndex = 1;
            this.toolTip.SetToolTip(this.closeButton, "Quit");
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // drawingMenuPanel
            // 
            this.drawingMenuPanel.BackColor = System.Drawing.SystemColors.Window;
            this.drawingMenuPanel.Controls.Add(this.command1Button);
            this.drawingMenuPanel.Controls.Add(this.command2Button);
            this.drawingMenuPanel.Controls.Add(this.command3Button);
            this.drawingMenuPanel.Controls.Add(this.command4Button);
            this.drawingMenuPanel.Controls.Add(this.command5Button);
            this.drawingMenuPanel.Controls.Add(this.command0Button);
            this.drawingMenuPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawingMenuPanel.Location = new System.Drawing.Point(0, 0);
            this.drawingMenuPanel.Name = "drawingMenuPanel";
            this.drawingMenuPanel.Size = new System.Drawing.Size(291, 48);
            this.drawingMenuPanel.TabIndex = 1;
            // 
            // moveIcon
            // 
            this.moveIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("moveIcon.BackgroundImage")));
            this.moveIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.moveIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.moveIcon.InitialImage = null;
            this.moveIcon.Location = new System.Drawing.Point(0, 0);
            this.moveIcon.Margin = new System.Windows.Forms.Padding(0);
            this.moveIcon.Name = "moveIcon";
            this.moveIcon.Size = new System.Drawing.Size(16, 16);
            this.moveIcon.TabIndex = 3;
            this.moveIcon.TabStop = false;
            this.toolTip.SetToolTip(this.moveIcon, "Move this window");
            this.moveIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moveIcon_MouseDown);
            // 
            // systemMenuPanel
            // 
            this.systemMenuPanel.BackColor = System.Drawing.SystemColors.Window;
            this.systemMenuPanel.Controls.Add(this.moveIcon);
            this.systemMenuPanel.Controls.Add(this.transformButton);
            this.systemMenuPanel.Controls.Add(this.closeButton);
            this.systemMenuPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.systemMenuPanel.Location = new System.Drawing.Point(291, 0);
            this.systemMenuPanel.Name = "systemMenuPanel";
            this.systemMenuPanel.Size = new System.Drawing.Size(16, 48);
            this.systemMenuPanel.TabIndex = 1;
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
            shortcutRelation1.Button = this.command1Button;
            shortcutRelation1.Key = System.Windows.Forms.Keys.D1;
            shortcutRelation1.SecondKey = System.Windows.Forms.Keys.NumPad1;
            shortcutRelation1.ShortcutKeyDisplayString = "1";
            shortcutRelation2.Button = this.command2Button;
            shortcutRelation2.Key = System.Windows.Forms.Keys.D2;
            shortcutRelation2.SecondKey = System.Windows.Forms.Keys.NumPad2;
            shortcutRelation2.ShortcutKeyDisplayString = "2";
            shortcutRelation3.Button = this.command3Button;
            shortcutRelation3.Key = System.Windows.Forms.Keys.D3;
            shortcutRelation3.SecondKey = System.Windows.Forms.Keys.NumPad3;
            shortcutRelation3.ShortcutKeyDisplayString = "3";
            shortcutRelation4.Button = this.command4Button;
            shortcutRelation4.Key = System.Windows.Forms.Keys.D4;
            shortcutRelation4.SecondKey = System.Windows.Forms.Keys.NumPad4;
            shortcutRelation4.ShortcutKeyDisplayString = "4";
            shortcutRelation5.Button = this.command5Button;
            shortcutRelation5.Key = System.Windows.Forms.Keys.D5;
            shortcutRelation5.SecondKey = System.Windows.Forms.Keys.NumPad5;
            shortcutRelation5.ShortcutKeyDisplayString = "5";
            shortcutRelation6.Button = this.command0Button;
            shortcutRelation6.Key = System.Windows.Forms.Keys.D0;
            shortcutRelation6.SecondKey = System.Windows.Forms.Keys.NumPad0;
            shortcutRelation6.ShortcutKeyDisplayString = "0";
            this.shortcutContainer.Relations = new Edsa.CustomControl.ShortcutRelation[] {
        shortcutRelation1,
        shortcutRelation2,
        shortcutRelation3,
        shortcutRelation4,
        shortcutRelation5,
        shortcutRelation6};
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 48);
            this.Controls.Add(this.drawingMenuPanel);
            this.Controls.Add(this.systemMenuPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(100, 100);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Unit Wiring System";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.drawingMenuPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.moveIcon)).EndInit();
            this.systemMenuPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox moveIcon;
        private System.Windows.Forms.FlowLayoutPanel systemMenuPanel;
        private System.Windows.Forms.Button transformButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ToolTip toolTip;
        private Edsa.CustomControl.ShortcutContainer shortcutContainer;
        public System.Windows.Forms.FlowLayoutPanel drawingMenuPanel;
        private System.Windows.Forms.Button command4Button;
        public System.Windows.Forms.Button command0Button;
        public System.Windows.Forms.Button command2Button;
        private System.Windows.Forms.Button command5Button;
        private System.Windows.Forms.Button command1Button;
        private System.Windows.Forms.Button command3Button;
    }
}