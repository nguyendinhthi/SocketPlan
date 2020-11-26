namespace SocketPlan.WinUI
{
    partial class EditForm
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
            Edsa.CustomControl.ShortcutRelation shortcutRelation4 = new Edsa.CustomControl.ShortcutRelation();
            Edsa.CustomControl.ShortcutRelation shortcutRelation5 = new Edsa.CustomControl.ShortcutRelation();
            Edsa.CustomControl.ShortcutRelation shortcutRelation6 = new Edsa.CustomControl.ShortcutRelation();
            this.moveButton = new System.Windows.Forms.Button();
            this.changeColorButton = new System.Windows.Forms.Button();
            this.manualComposeButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.ShowHideUnnecessaryItemsButton = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.moveButton);
            this.panel.Controls.Add(this.changeColorButton);
            this.panel.Controls.Add(this.manualComposeButton);
            this.panel.Controls.Add(this.deleteButton);
            this.panel.Controls.Add(this.ShowHideUnnecessaryItemsButton);
            this.panel.Size = new System.Drawing.Size(200, 230);
            this.panel.TabIndex = 0;
            // 
            // shortcutContainer
            // 
            shortcutRelation4.Button = this.moveButton;
            shortcutRelation4.Key = System.Windows.Forms.Keys.D1;
            shortcutRelation4.SecondKey = System.Windows.Forms.Keys.NumPad1;
            shortcutRelation4.ShortcutKeyDisplayString = "1";
            shortcutRelation5.Button = this.changeColorButton;
            shortcutRelation5.Key = System.Windows.Forms.Keys.D2;
            shortcutRelation5.SecondKey = System.Windows.Forms.Keys.NumPad2;
            shortcutRelation5.ShortcutKeyDisplayString = "2";
            shortcutRelation6.Button = this.manualComposeButton;
            shortcutRelation6.Key = System.Windows.Forms.Keys.D3;
            shortcutRelation6.SecondKey = System.Windows.Forms.Keys.NumPad3;
            shortcutRelation6.ShortcutKeyDisplayString = "3";
            this.shortcutContainer.Relations = new Edsa.CustomControl.ShortcutRelation[] {
        shortcutRelation4,
        shortcutRelation5,
        shortcutRelation6};
            // 
            // moveButton
            // 
            this.moveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.moveButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.moveButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.moveButton.ForeColor = System.Drawing.Color.Black;
            this.moveButton.Location = new System.Drawing.Point(0, 0);
            this.moveButton.Margin = new System.Windows.Forms.Padding(0);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(179, 23);
            this.moveButton.TabIndex = 0;
            this.moveButton.Text = "1. Move box location";
            this.moveButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.moveButton, "Move socket box location");
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // changeColorButton
            // 
            this.changeColorButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.changeColorButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.changeColorButton.ForeColor = System.Drawing.Color.Black;
            this.changeColorButton.Location = new System.Drawing.Point(0, 23);
            this.changeColorButton.Margin = new System.Windows.Forms.Padding(0);
            this.changeColorButton.Name = "changeColorButton";
            this.changeColorButton.Size = new System.Drawing.Size(179, 23);
            this.changeColorButton.TabIndex = 1;
            this.changeColorButton.Text = "2. Change box color";
            this.changeColorButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.changeColorButton, "Change to other color");
            this.changeColorButton.UseVisualStyleBackColor = true;
            this.changeColorButton.Click += new System.EventHandler(this.changeColorButton_Click);
            // 
            // manualComposeButton
            // 
            this.manualComposeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.manualComposeButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.manualComposeButton.ForeColor = System.Drawing.Color.Black;
            this.manualComposeButton.Location = new System.Drawing.Point(0, 46);
            this.manualComposeButton.Margin = new System.Windows.Forms.Padding(0);
            this.manualComposeButton.Name = "manualComposeButton";
            this.manualComposeButton.Size = new System.Drawing.Size(179, 23);
            this.manualComposeButton.TabIndex = 2;
            this.manualComposeButton.Text = "3. Manual compose";
            this.manualComposeButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.manualComposeButton, "Change to other color");
            this.manualComposeButton.UseVisualStyleBackColor = true;
            this.manualComposeButton.Click += new System.EventHandler(this.manualComposeButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deleteButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.deleteButton.ForeColor = System.Drawing.Color.Black;
            this.deleteButton.Location = new System.Drawing.Point(0, 69);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(0);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(179, 23);
            this.deleteButton.TabIndex = 4;
            this.deleteButton.Text = "4. Delete box item";
            this.deleteButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.deleteButton, "Delete a box on the drawing");
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // ShowHideUnnecessaryItemsButton
            // 
            this.ShowHideUnnecessaryItemsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ShowHideUnnecessaryItemsButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ShowHideUnnecessaryItemsButton.ForeColor = System.Drawing.Color.Black;
            this.ShowHideUnnecessaryItemsButton.Location = new System.Drawing.Point(0, 92);
            this.ShowHideUnnecessaryItemsButton.Margin = new System.Windows.Forms.Padding(0);
            this.ShowHideUnnecessaryItemsButton.Name = "ShowHideUnnecessaryItemsButton";
            this.ShowHideUnnecessaryItemsButton.Size = new System.Drawing.Size(179, 23);
            this.ShowHideUnnecessaryItemsButton.TabIndex = 5;
            this.ShowHideUnnecessaryItemsButton.Text = "5. Show/Hide Unnecessary";
            this.ShowHideUnnecessaryItemsButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.ShowHideUnnecessaryItemsButton, "Change Show/Hide for Unnecessary Items.");
            this.ShowHideUnnecessaryItemsButton.UseVisualStyleBackColor = true;
            this.ShowHideUnnecessaryItemsButton.Click += new System.EventHandler(this.ShowHideUnnecessaryItemsButton_Click);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 230);
            this.Name = "EditForm";
            this.Text = "SupportCommandForm";
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button changeColorButton;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Button manualComposeButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button ShowHideUnnecessaryItemsButton;

    }
}