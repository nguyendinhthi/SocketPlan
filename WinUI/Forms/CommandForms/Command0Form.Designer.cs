namespace SocketPlan.WinUI
{
    partial class Command0Form
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
            Edsa.CustomControl.ShortcutRelation shortcutRelation7 = new Edsa.CustomControl.ShortcutRelation();
            Edsa.CustomControl.ShortcutRelation shortcutRelation8 = new Edsa.CustomControl.ShortcutRelation();
            Edsa.CustomControl.ShortcutRelation shortcutRelation9 = new Edsa.CustomControl.ShortcutRelation();
            Edsa.CustomControl.ShortcutRelation shortcutRelation10 = new Edsa.CustomControl.ShortcutRelation();
            Edsa.CustomControl.ShortcutRelation shortcutRelation11 = new Edsa.CustomControl.ShortcutRelation();
            Edsa.CustomControl.ShortcutRelation shortcutRelation12 = new Edsa.CustomControl.ShortcutRelation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Command0Form));
            this.openButton = new System.Windows.Forms.Button();
            this.saveToLocalButton = new System.Windows.Forms.Button();
            this.saveToServerButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.plotAndSaveDWGButton = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.openButton);
            this.panel.Controls.Add(this.saveToLocalButton);
            this.panel.Controls.Add(this.saveToServerButton);
            this.panel.Controls.Add(this.closeButton);
            this.panel.Controls.Add(this.plotAndSaveDWGButton);
            this.panel.Size = new System.Drawing.Size(245, 49);
            // 
            // shortcutContainer
            // 
            shortcutRelation7.Button = this.openButton;
            shortcutRelation7.Key = System.Windows.Forms.Keys.D1;
            shortcutRelation7.SecondKey = System.Windows.Forms.Keys.NumPad1;
            shortcutRelation7.ShortcutKeyDisplayString = "1";
            shortcutRelation8.Button = this.saveToLocalButton;
            shortcutRelation8.Key = System.Windows.Forms.Keys.D2;
            shortcutRelation8.SecondKey = System.Windows.Forms.Keys.NumPad2;
            shortcutRelation8.ShortcutKeyDisplayString = "2";
            shortcutRelation9.Button = this.saveToServerButton;
            shortcutRelation9.Key = System.Windows.Forms.Keys.D3;
            shortcutRelation9.SecondKey = System.Windows.Forms.Keys.NumPad3;
            shortcutRelation9.ShortcutKeyDisplayString = "3";
            shortcutRelation10.Button = this.closeButton;
            shortcutRelation10.Key = System.Windows.Forms.Keys.D4;
            shortcutRelation10.SecondKey = System.Windows.Forms.Keys.NumPad4;
            shortcutRelation10.ShortcutKeyDisplayString = "4";
            shortcutRelation11.Key = System.Windows.Forms.Keys.D5;
            shortcutRelation11.SecondKey = System.Windows.Forms.Keys.NumPad5;
            shortcutRelation11.ShortcutKeyDisplayString = "5";
            shortcutRelation12.Key = System.Windows.Forms.Keys.D6;
            shortcutRelation12.SecondKey = System.Windows.Forms.Keys.NumPad6;
            shortcutRelation12.ShortcutKeyDisplayString = "6";
            this.shortcutContainer.Relations = new Edsa.CustomControl.ShortcutRelation[] {
        shortcutRelation7,
        shortcutRelation8,
        shortcutRelation9,
        shortcutRelation10,
        shortcutRelation11,
        shortcutRelation12};
            // 
            // openButton
            // 
            this.openButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("openButton.BackgroundImage")));
            this.openButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.openButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.openButton.ForeColor = System.Drawing.Color.Red;
            this.openButton.Location = new System.Drawing.Point(0, 0);
            this.openButton.Margin = new System.Windows.Forms.Padding(0);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(48, 48);
            this.openButton.TabIndex = 0;
            this.openButton.Text = "1";
            this.openButton.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.toolTip.SetToolTip(this.openButton, "Open（開く）");
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // saveToLocalButton
            // 
            this.saveToLocalButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("saveToLocalButton.BackgroundImage")));
            this.saveToLocalButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.saveToLocalButton.Enabled = false;
            this.saveToLocalButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.saveToLocalButton.ForeColor = System.Drawing.Color.Red;
            this.saveToLocalButton.Location = new System.Drawing.Point(48, 0);
            this.saveToLocalButton.Margin = new System.Windows.Forms.Padding(0);
            this.saveToLocalButton.Name = "saveToLocalButton";
            this.saveToLocalButton.Size = new System.Drawing.Size(48, 48);
            this.saveToLocalButton.TabIndex = 1;
            this.saveToLocalButton.Text = "2";
            this.saveToLocalButton.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.toolTip.SetToolTip(this.saveToLocalButton, "Save to local（ローカルに保存する）");
            this.saveToLocalButton.UseVisualStyleBackColor = true;
            this.saveToLocalButton.Click += new System.EventHandler(this.saveToLocalButton_Click);
            // 
            // saveToServerButton
            // 
            this.saveToServerButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("saveToServerButton.BackgroundImage")));
            this.saveToServerButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.saveToServerButton.Enabled = false;
            this.saveToServerButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.saveToServerButton.ForeColor = System.Drawing.Color.Red;
            this.saveToServerButton.Location = new System.Drawing.Point(96, 0);
            this.saveToServerButton.Margin = new System.Windows.Forms.Padding(0);
            this.saveToServerButton.Name = "saveToServerButton";
            this.saveToServerButton.Size = new System.Drawing.Size(48, 48);
            this.saveToServerButton.TabIndex = 2;
            this.saveToServerButton.Text = "3";
            this.saveToServerButton.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.toolTip.SetToolTip(this.saveToServerButton, "Save to server（サーバーに保存する）");
            this.saveToServerButton.UseVisualStyleBackColor = true;
            this.saveToServerButton.Click += new System.EventHandler(this.saveToServerButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("closeButton.BackgroundImage")));
            this.closeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeButton.Enabled = false;
            this.closeButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.closeButton.ForeColor = System.Drawing.Color.Red;
            this.closeButton.Location = new System.Drawing.Point(144, 0);
            this.closeButton.Margin = new System.Windows.Forms.Padding(0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(48, 48);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "4";
            this.closeButton.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.toolTip.SetToolTip(this.closeButton, "Close（図面を閉じる）");
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // plotAndSaveDWGButton
            // 
            this.plotAndSaveDWGButton.AllowDrop = true;
            this.plotAndSaveDWGButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.plotAndSaveDWGButton.Enabled = false;
            this.plotAndSaveDWGButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.plotAndSaveDWGButton.ForeColor = System.Drawing.Color.Red;
            this.plotAndSaveDWGButton.Location = new System.Drawing.Point(192, 0);
            this.plotAndSaveDWGButton.Margin = new System.Windows.Forms.Padding(0);
            this.plotAndSaveDWGButton.Name = "plotAndSaveDWGButton";
            this.plotAndSaveDWGButton.Size = new System.Drawing.Size(48, 48);
            this.plotAndSaveDWGButton.TabIndex = 12;
            this.plotAndSaveDWGButton.Text = "Save and Plot";
            this.plotAndSaveDWGButton.UseVisualStyleBackColor = true;
            this.plotAndSaveDWGButton.Click += new System.EventHandler(this.plotAndSaveDWGButton_Click);
            // 
            // Command0Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 49);
            this.ColumnCount = 7;
            this.Name = "Command0Form";
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button saveToLocalButton;
        private System.Windows.Forms.Button saveToServerButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button plotAndSaveDWGButton;



    }
}