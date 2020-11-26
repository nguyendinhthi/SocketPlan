namespace SocketPlan.WinUI
{
    partial class SocketSpecificMasterMaintenanceForm
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.treeView = new System.Windows.Forms.TreeView();
            this.splitter = new System.Windows.Forms.Splitter();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.shapeCombo = new System.Windows.Forms.ComboBox();
            this.colorCombo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.depthCombo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label14 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.serialColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.blockPathButton = new System.Windows.Forms.Button();
            this.blockPathText = new System.Windows.Forms.TextBox();
            this.newSpecificButton = new System.Windows.Forms.Button();
            this.newCategoryButton = new System.Windows.Forms.Button();
            this.imagePathButton = new System.Windows.Forms.Button();
            this.categoryCombo = new System.Windows.Forms.ComboBox();
            this.sizeCombo = new System.Windows.Forms.ComboBox();
            this.imagePathText = new System.Windows.Forms.TextBox();
            this.serialText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView.HideSelection = false;
            this.treeView.LabelEdit = true;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(164, 447);
            this.treeView.TabIndex = 0;
            this.treeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_BeforeLabelEdit);
            this.treeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView_KeyDown);
            // 
            // splitter
            // 
            this.splitter.Location = new System.Drawing.Point(164, 0);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(3, 447);
            this.splitter.TabIndex = 1;
            this.splitter.TabStop = false;
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.shapeCombo);
            this.mainPanel.Controls.Add(this.colorCombo);
            this.mainPanel.Controls.Add(this.label8);
            this.mainPanel.Controls.Add(this.label7);
            this.mainPanel.Controls.Add(this.depthCombo);
            this.mainPanel.Controls.Add(this.label6);
            this.mainPanel.Controls.Add(this.pictureBox);
            this.mainPanel.Controls.Add(this.label14);
            this.mainPanel.Controls.Add(this.dataGridView);
            this.mainPanel.Controls.Add(this.addButton);
            this.mainPanel.Controls.Add(this.label3);
            this.mainPanel.Controls.Add(this.blockPathButton);
            this.mainPanel.Controls.Add(this.blockPathText);
            this.mainPanel.Controls.Add(this.newSpecificButton);
            this.mainPanel.Controls.Add(this.newCategoryButton);
            this.mainPanel.Controls.Add(this.imagePathButton);
            this.mainPanel.Controls.Add(this.categoryCombo);
            this.mainPanel.Controls.Add(this.sizeCombo);
            this.mainPanel.Controls.Add(this.imagePathText);
            this.mainPanel.Controls.Add(this.serialText);
            this.mainPanel.Controls.Add(this.label4);
            this.mainPanel.Controls.Add(this.label5);
            this.mainPanel.Controls.Add(this.label2);
            this.mainPanel.Controls.Add(this.label1);
            this.mainPanel.Controls.Add(this.saveButton);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(167, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(653, 447);
            this.mainPanel.TabIndex = 2;
            // 
            // shapeCombo
            // 
            this.shapeCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.shapeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shapeCombo.FormattingEnabled = true;
            this.shapeCombo.Items.AddRange(new object[] {
            "",
            "Round",
            "Square"});
            this.shapeCombo.Location = new System.Drawing.Point(363, 143);
            this.shapeCombo.MaximumSize = new System.Drawing.Size(140, 0);
            this.shapeCombo.Name = "shapeCombo";
            this.shapeCombo.Size = new System.Drawing.Size(136, 20);
            this.shapeCombo.TabIndex = 8;
            // 
            // colorCombo
            // 
            this.colorCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorCombo.FormattingEnabled = true;
            this.colorCombo.Items.AddRange(new object[] {
            "",
            "White",
            "Beigu"});
            this.colorCombo.Location = new System.Drawing.Point(103, 143);
            this.colorCombo.MaximumSize = new System.Drawing.Size(140, 0);
            this.colorCombo.Name = "colorCombo";
            this.colorCombo.Size = new System.Drawing.Size(137, 20);
            this.colorCombo.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(252, 146);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 12);
            this.label8.TabIndex = 39;
            this.label8.Text = "Shape:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 12);
            this.label7.TabIndex = 38;
            this.label7.Text = "Color:";
            // 
            // depthCombo
            // 
            this.depthCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.depthCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.depthCombo.FormattingEnabled = true;
            this.depthCombo.Items.AddRange(new object[] {
            "NORMAL",
            "DEEP"});
            this.depthCombo.Location = new System.Drawing.Point(363, 115);
            this.depthCombo.MaximumSize = new System.Drawing.Size(140, 0);
            this.depthCombo.Name = "depthCombo";
            this.depthCombo.Size = new System.Drawing.Size(136, 20);
            this.depthCombo.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(252, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 12);
            this.label6.TabIndex = 36;
            this.label6.Text = "Socket Box Depth:";
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox.Location = new System.Drawing.Point(505, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(136, 164);
            this.pictureBox.TabIndex = 35;
            this.pictureBox.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 184);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 12);
            this.label14.TabIndex = 12;
            this.label14.Text = "Related Specific";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serialColumn});
            this.dataGridView.Location = new System.Drawing.Point(3, 208);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 21;
            this.dataGridView.Size = new System.Drawing.Size(638, 198);
            this.dataGridView.TabIndex = 14;
            // 
            // serialColumn
            // 
            this.serialColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.serialColumn.DataPropertyName = "Serial";
            this.serialColumn.HeaderText = "Serial";
            this.serialColumn.Name = "serialColumn";
            this.serialColumn.ReadOnly = true;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(103, 179);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 10;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Block Path:";
            // 
            // blockPathButton
            // 
            this.blockPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.blockPathButton.Location = new System.Drawing.Point(479, 86);
            this.blockPathButton.Name = "blockPathButton";
            this.blockPathButton.Size = new System.Drawing.Size(20, 23);
            this.blockPathButton.TabIndex = 0;
            this.blockPathButton.TabStop = false;
            this.blockPathButton.Text = "...";
            this.blockPathButton.UseVisualStyleBackColor = true;
            this.blockPathButton.Click += new System.EventHandler(this.blockPathButton_Click);
            // 
            // blockPathText
            // 
            this.blockPathText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.blockPathText.Location = new System.Drawing.Point(103, 88);
            this.blockPathText.Name = "blockPathText";
            this.blockPathText.Size = new System.Drawing.Size(370, 19);
            this.blockPathText.TabIndex = 4;
            // 
            // newSpecificButton
            // 
            this.newSpecificButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.newSpecificButton.Location = new System.Drawing.Point(110, 412);
            this.newSpecificButton.Name = "newSpecificButton";
            this.newSpecificButton.Size = new System.Drawing.Size(104, 23);
            this.newSpecificButton.TabIndex = 22;
            this.newSpecificButton.Text = "New &Specific";
            this.newSpecificButton.UseVisualStyleBackColor = true;
            this.newSpecificButton.Click += new System.EventHandler(this.newSpecificButton_Click);
            // 
            // newCategoryButton
            // 
            this.newCategoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.newCategoryButton.Location = new System.Drawing.Point(6, 412);
            this.newCategoryButton.Name = "newCategoryButton";
            this.newCategoryButton.Size = new System.Drawing.Size(98, 23);
            this.newCategoryButton.TabIndex = 21;
            this.newCategoryButton.Text = "New &Category";
            this.newCategoryButton.UseVisualStyleBackColor = true;
            this.newCategoryButton.Click += new System.EventHandler(this.newCategoryButton_Click);
            // 
            // imagePathButton
            // 
            this.imagePathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imagePathButton.Location = new System.Drawing.Point(479, 61);
            this.imagePathButton.Name = "imagePathButton";
            this.imagePathButton.Size = new System.Drawing.Size(20, 23);
            this.imagePathButton.TabIndex = 0;
            this.imagePathButton.TabStop = false;
            this.imagePathButton.Text = "...";
            this.imagePathButton.UseVisualStyleBackColor = true;
            this.imagePathButton.Click += new System.EventHandler(this.imagePathButton_Click);
            // 
            // categoryCombo
            // 
            this.categoryCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.categoryCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryCombo.FormattingEnabled = true;
            this.categoryCombo.Location = new System.Drawing.Point(103, 12);
            this.categoryCombo.Name = "categoryCombo";
            this.categoryCombo.Size = new System.Drawing.Size(396, 20);
            this.categoryCombo.TabIndex = 1;
            // 
            // sizeCombo
            // 
            this.sizeCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sizeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sizeCombo.FormattingEnabled = true;
            this.sizeCombo.Location = new System.Drawing.Point(103, 115);
            this.sizeCombo.MaximumSize = new System.Drawing.Size(140, 0);
            this.sizeCombo.Name = "sizeCombo";
            this.sizeCombo.Size = new System.Drawing.Size(137, 20);
            this.sizeCombo.TabIndex = 5;
            // 
            // imagePathText
            // 
            this.imagePathText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.imagePathText.Location = new System.Drawing.Point(103, 63);
            this.imagePathText.Name = "imagePathText";
            this.imagePathText.Size = new System.Drawing.Size(370, 19);
            this.imagePathText.TabIndex = 3;
            this.imagePathText.Validated += new System.EventHandler(this.imagePathText_Validated);
            this.imagePathText.Validating += new System.ComponentModel.CancelEventHandler(this.imagePathText_Validating);
            // 
            // serialText
            // 
            this.serialText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.serialText.Location = new System.Drawing.Point(103, 38);
            this.serialText.Name = "serialText";
            this.serialText.Size = new System.Drawing.Size(396, 19);
            this.serialText.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Socket Box Size:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "Category:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Image Path:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Serial:";
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(566, 412);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 30;
            this.saveButton.Text = "&Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // SocketSpecificMasterMaintenanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 447);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.treeView);
            this.Name = "SocketSpecificMasterMaintenanceForm";
            this.Text = "Socket specific maintenance";
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Splitter splitter;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ComboBox sizeCombo;
        private System.Windows.Forms.TextBox imagePathText;
        private System.Windows.Forms.TextBox serialText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox categoryCombo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button imagePathButton;
        private System.Windows.Forms.Button newCategoryButton;
        private System.Windows.Forms.Button newSpecificButton;
        private System.Windows.Forms.Button blockPathButton;
        private System.Windows.Forms.TextBox blockPathText;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialColumn;
        private System.Windows.Forms.ComboBox depthCombo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox colorCombo;
        private System.Windows.Forms.ComboBox shapeCombo;
    }
}

