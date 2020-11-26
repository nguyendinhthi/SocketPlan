namespace SocketPlan.WinUI
{
    partial class MaintenanceForm
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
            Edsa.CustomControl.ShortcutRelation shortcutRelation1 = new Edsa.CustomControl.ShortcutRelation();
            Edsa.CustomControl.ShortcutRelation shortcutRelation2 = new Edsa.CustomControl.ShortcutRelation();
            Edsa.CustomControl.ShortcutRelation shortcutRelation3 = new Edsa.CustomControl.ShortcutRelation();
            Edsa.CustomControl.ShortcutRelation shortcutRelation4 = new Edsa.CustomControl.ShortcutRelation();
            this.composeButton = new System.Windows.Forms.Button();
            this.specificButton = new System.Windows.Forms.Button();
            this.singleButton = new System.Windows.Forms.Button();
            this.exempleButton = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.composeButton);
            this.panel.Controls.Add(this.specificButton);
            this.panel.Controls.Add(this.singleButton);
            this.panel.Controls.Add(this.exempleButton);
            this.panel.Size = new System.Drawing.Size(250, 230);
            this.panel.TabIndex = 0;
            // 
            // shortcutContainer
            // 
            shortcutRelation1.Button = this.composeButton;
            shortcutRelation1.Key = System.Windows.Forms.Keys.D1;
            shortcutRelation1.SecondKey = System.Windows.Forms.Keys.NumPad1;
            shortcutRelation1.ShortcutKeyDisplayString = "1";
            shortcutRelation2.Button = this.specificButton;
            shortcutRelation2.Key = System.Windows.Forms.Keys.D2;
            shortcutRelation2.SecondKey = System.Windows.Forms.Keys.NumPad2;
            shortcutRelation2.ShortcutKeyDisplayString = "2";
            shortcutRelation3.Button = this.singleButton;
            shortcutRelation3.Key = System.Windows.Forms.Keys.D3;
            shortcutRelation3.SecondKey = System.Windows.Forms.Keys.NumPad3;
            shortcutRelation3.ShortcutKeyDisplayString = "3";
            shortcutRelation4.Button = this.exempleButton;
            shortcutRelation4.Key = System.Windows.Forms.Keys.D4;
            shortcutRelation4.SecondKey = System.Windows.Forms.Keys.NumPad4;
            shortcutRelation4.ShortcutKeyDisplayString = "4";
            this.shortcutContainer.Relations = new Edsa.CustomControl.ShortcutRelation[] {
        shortcutRelation1,
        shortcutRelation2,
        shortcutRelation3,
        shortcutRelation4};
            // 
            // composeButton
            // 
            this.composeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.composeButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.composeButton.ForeColor = System.Drawing.Color.Black;
            this.composeButton.Location = new System.Drawing.Point(0, 0);
            this.composeButton.Margin = new System.Windows.Forms.Padding(0);
            this.composeButton.Name = "composeButton";
            this.composeButton.Size = new System.Drawing.Size(240, 23);
            this.composeButton.TabIndex = 0;
            this.composeButton.Text = "1. Compose pattern maintenance";
            this.composeButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.composeButton, "Compose pattern maintenance");
            this.composeButton.UseVisualStyleBackColor = true;
            this.composeButton.Click += new System.EventHandler(this.composeButton_Click);
            // 
            // specificButton
            // 
            this.specificButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.specificButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.specificButton.ForeColor = System.Drawing.Color.Black;
            this.specificButton.Location = new System.Drawing.Point(0, 23);
            this.specificButton.Margin = new System.Windows.Forms.Padding(0);
            this.specificButton.Name = "specificButton";
            this.specificButton.Size = new System.Drawing.Size(240, 23);
            this.specificButton.TabIndex = 1;
            this.specificButton.Text = "2. Specific pattern maintenance";
            this.specificButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.specificButton, "Specific pattern maintenance");
            this.specificButton.UseVisualStyleBackColor = true;
            this.specificButton.Click += new System.EventHandler(this.patternButton_Click);
            // 
            // singleButton
            // 
            this.singleButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.singleButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleButton.ForeColor = System.Drawing.Color.Black;
            this.singleButton.Location = new System.Drawing.Point(0, 46);
            this.singleButton.Margin = new System.Windows.Forms.Padding(0);
            this.singleButton.Name = "singleButton";
            this.singleButton.Size = new System.Drawing.Size(240, 23);
            this.singleButton.TabIndex = 2;
            this.singleButton.Text = "3. Single symbol maintenance";
            this.singleButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.singleButton, "Single symbol maintenance");
            this.singleButton.UseVisualStyleBackColor = true;
            this.singleButton.Click += new System.EventHandler(this.singleButton_Click);
            // 
            // exempleButton
            // 
            this.exempleButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.exempleButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.exempleButton.ForeColor = System.Drawing.Color.Black;
            this.exempleButton.Location = new System.Drawing.Point(0, 69);
            this.exempleButton.Margin = new System.Windows.Forms.Padding(0);
            this.exempleButton.Name = "exempleButton";
            this.exempleButton.Size = new System.Drawing.Size(240, 23);
            this.exempleButton.TabIndex = 2;
            this.exempleButton.Text = "4. Exemption of subete comment maintenance";
            this.exempleButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.exempleButton, "Exemption of subete comment maintenance");
            this.exempleButton.UseVisualStyleBackColor = true;
            this.exempleButton.Click += new System.EventHandler(this.exempleButton_Click);
            // 
            // MaintenanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 230);
            this.Name = "MaintenanceForm";
            this.Text = "SupportCommandForm";
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button composeButton;
        private System.Windows.Forms.Button exempleButton;
        private System.Windows.Forms.Button specificButton;
        private System.Windows.Forms.Button singleButton;

    }
}