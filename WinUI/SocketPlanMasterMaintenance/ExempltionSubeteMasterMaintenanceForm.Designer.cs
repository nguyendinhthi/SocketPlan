namespace SocketPlan.WinUI
{
    partial class SubeteExemptionMasterMaintenanceForm
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
            this.equipmentGrid = new System.Windows.Forms.DataGridView();
            this.equipmentIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.equipmentNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveButton = new System.Windows.Forms.Button();
            this.addbutton = new System.Windows.Forms.Button();
            this.serialGrid = new System.Windows.Forms.DataGridView();
            this.hinbanColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.equipmentGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serialGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // equipmentGrid
            // 
            this.equipmentGrid.AllowUserToAddRows = false;
            this.equipmentGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.equipmentGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.equipmentGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.equipmentIdColumn,
            this.equipmentNameColumn});
            this.equipmentGrid.Location = new System.Drawing.Point(12, 37);
            this.equipmentGrid.Name = "equipmentGrid";
            this.equipmentGrid.ReadOnly = true;
            this.equipmentGrid.RowTemplate.Height = 21;
            this.equipmentGrid.Size = new System.Drawing.Size(300, 396);
            this.equipmentGrid.TabIndex = 2;
            // 
            // equipmentIdColumn
            // 
            this.equipmentIdColumn.DataPropertyName = "EquipmentId";
            this.equipmentIdColumn.HeaderText = "ID";
            this.equipmentIdColumn.Name = "equipmentIdColumn";
            this.equipmentIdColumn.ReadOnly = true;
            this.equipmentIdColumn.Width = 60;
            // 
            // equipmentNameColumn
            // 
            this.equipmentNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.equipmentNameColumn.DataPropertyName = "EquipmentName";
            this.equipmentNameColumn.HeaderText = "Name";
            this.equipmentNameColumn.Name = "equipmentNameColumn";
            this.equipmentNameColumn.ReadOnly = true;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(512, 439);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "&Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // addbutton
            // 
            this.addbutton.Location = new System.Drawing.Point(113, 12);
            this.addbutton.Name = "addbutton";
            this.addbutton.Size = new System.Drawing.Size(75, 23);
            this.addbutton.TabIndex = 1;
            this.addbutton.Text = "&Add";
            this.addbutton.UseVisualStyleBackColor = true;
            this.addbutton.Click += new System.EventHandler(this.addbutton_Click);
            // 
            // serialGrid
            // 
            this.serialGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.serialGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.serialGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.hinbanColumn});
            this.serialGrid.Location = new System.Drawing.Point(318, 37);
            this.serialGrid.Name = "serialGrid";
            this.serialGrid.RowTemplate.Height = 21;
            this.serialGrid.Size = new System.Drawing.Size(269, 396);
            this.serialGrid.TabIndex = 4;
            this.serialGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.serialGrid_CellValidating);
            // 
            // hinbanColumn
            // 
            this.hinbanColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.hinbanColumn.DataPropertyName = "Hinban";
            this.hinbanColumn.HeaderText = "Serial";
            this.hinbanColumn.Name = "hinbanColumn";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Switch equipment";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(320, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Switch serial";
            // 
            // SubeteExemptionMasterMaintenanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 474);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addbutton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.serialGrid);
            this.Controls.Add(this.equipmentGrid);
            this.Name = "SubeteExemptionMasterMaintenanceForm";
            this.Text = "Exemption of subete comment Maintenance";
            ((System.ComponentModel.ISupportInitialize)(this.equipmentGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serialGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView equipmentGrid;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button addbutton;
        private System.Windows.Forms.DataGridView serialGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn hinbanColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn equipmentIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn equipmentNameColumn;
    }
}