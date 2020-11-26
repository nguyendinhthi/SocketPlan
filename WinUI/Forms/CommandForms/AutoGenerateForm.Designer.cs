namespace SocketPlan.WinUI
{
    partial class AutoGenerateForm
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
            this.individualButton = new System.Windows.Forms.Button();
            this.patternButton = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.individualButton);
            this.panel.Controls.Add(this.patternButton);
            this.panel.Size = new System.Drawing.Size(200, 230);
            this.panel.TabIndex = 0;
            // 
            // shortcutContainer
            // 
            shortcutRelation1.Button = this.individualButton;
            shortcutRelation1.Key = System.Windows.Forms.Keys.D1;
            shortcutRelation1.SecondKey = System.Windows.Forms.Keys.NumPad1;
            shortcutRelation1.ShortcutKeyDisplayString = "1";
            shortcutRelation2.Button = this.patternButton;
            shortcutRelation2.Key = System.Windows.Forms.Keys.D2;
            shortcutRelation2.SecondKey = System.Windows.Forms.Keys.NumPad2;
            shortcutRelation2.ShortcutKeyDisplayString = "2";
            this.shortcutContainer.Relations = new Edsa.CustomControl.ShortcutRelation[] {
        shortcutRelation1,
        shortcutRelation2};
            // 
            // individualButton
            // 
            this.individualButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.individualButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.individualButton.ForeColor = System.Drawing.Color.Black;
            this.individualButton.Location = new System.Drawing.Point(0, 0);
            this.individualButton.Margin = new System.Windows.Forms.Padding(0);
            this.individualButton.Name = "individualButton";
            this.individualButton.Size = new System.Drawing.Size(129, 23);
            this.individualButton.TabIndex = 0;
            this.individualButton.Text = "1. Individual";
            this.individualButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.individualButton, "Auto generate for individual");
            this.individualButton.UseVisualStyleBackColor = true;
            this.individualButton.Click += new System.EventHandler(this.individualButton_Click);
            // 
            // patternButton
            // 
            this.patternButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.patternButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.patternButton.ForeColor = System.Drawing.Color.Black;
            this.patternButton.Location = new System.Drawing.Point(0, 23);
            this.patternButton.Margin = new System.Windows.Forms.Padding(0);
            this.patternButton.Name = "patternButton";
            this.patternButton.Size = new System.Drawing.Size(129, 23);
            this.patternButton.TabIndex = 1;
            this.patternButton.Text = "2. Pattern";
            this.patternButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.patternButton, "Auto generate for pattern");
            this.patternButton.UseVisualStyleBackColor = true;
            this.patternButton.Click += new System.EventHandler(this.patternButton_Click);
            // 
            // AutoGenerateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 230);
            this.Name = "AutoGenerateForm";
            this.Text = "SupportCommandForm";
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button individualButton;
        private System.Windows.Forms.Button patternButton;

    }
}