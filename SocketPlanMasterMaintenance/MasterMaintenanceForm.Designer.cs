namespace SocketPlanMasterMaintenance
{
    partial class MasterMaintenanceForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            this.treeView = new System.Windows.Forms.TreeView();
            this.splitter = new System.Windows.Forms.Splitter();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.patternBSButton = new System.Windows.Forms.Button();
            this.patternBSText = new System.Windows.Forms.TextBox();
            this.patternBSLabel = new System.Windows.Forms.Label();
            this.patternBRButton = new System.Windows.Forms.Button();
            this.patternBRText = new System.Windows.Forms.TextBox();
            this.patternBRLabel = new System.Windows.Forms.Label();
            this.patternWSButton = new System.Windows.Forms.Button();
            this.patternWSText = new System.Windows.Forms.TextBox();
            this.patternWSLabel = new System.Windows.Forms.Label();
            this.individualBSButton = new System.Windows.Forms.Button();
            this.individualBSText = new System.Windows.Forms.TextBox();
            this.individualBSLabel = new System.Windows.Forms.Label();
            this.individualBRButton = new System.Windows.Forms.Button();
            this.individualBRText = new System.Windows.Forms.TextBox();
            this.individualBRLabel = new System.Windows.Forms.Label();
            this.individualWSButton = new System.Windows.Forms.Button();
            this.individualWSText = new System.Windows.Forms.TextBox();
            this.individualWSLabel = new System.Windows.Forms.Label();
            this.newPatternButton = new System.Windows.Forms.Button();
            this.newCategoryButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.detailGrid = new System.Windows.Forms.DataGridView();
            this.equipmentNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.addEquipmentButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.colorGrid = new System.Windows.Forms.DataGridView();
            this.label14 = new System.Windows.Forms.Label();
            this.addColorButton = new System.Windows.Forms.Button();
            this.commentGrid = new System.Windows.Forms.DataGridView();
            this.commentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.addCommentButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.patternWRButton = new System.Windows.Forms.Button();
            this.individualWRButton = new System.Windows.Forms.Button();
            this.outputCheck = new System.Windows.Forms.CheckBox();
            this.categoryCombo = new System.Windows.Forms.ComboBox();
            this.sizeCombo = new System.Windows.Forms.ComboBox();
            this.patternWRText = new System.Windows.Forms.TextBox();
            this.individualWRText = new System.Windows.Forms.TextBox();
            this.nameText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.patternWRLabel = new System.Windows.Forms.Label();
            this.individualWRLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.colorNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mainPanel.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detailGrid)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commentGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView.HideSelection = false;
            this.treeView.LabelEdit = true;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(164, 695);
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
            this.splitter.Size = new System.Drawing.Size(3, 695);
            this.splitter.TabIndex = 1;
            this.splitter.TabStop = false;
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.label14);
            this.mainPanel.Controls.Add(this.colorGrid);
            this.mainPanel.Controls.Add(this.addColorButton);
            this.mainPanel.Controls.Add(this.patternBSButton);
            this.mainPanel.Controls.Add(this.patternBSText);
            this.mainPanel.Controls.Add(this.patternBSLabel);
            this.mainPanel.Controls.Add(this.patternBRButton);
            this.mainPanel.Controls.Add(this.patternBRText);
            this.mainPanel.Controls.Add(this.patternBRLabel);
            this.mainPanel.Controls.Add(this.patternWSButton);
            this.mainPanel.Controls.Add(this.patternWSText);
            this.mainPanel.Controls.Add(this.patternWSLabel);
            this.mainPanel.Controls.Add(this.individualBSButton);
            this.mainPanel.Controls.Add(this.individualBSText);
            this.mainPanel.Controls.Add(this.individualBSLabel);
            this.mainPanel.Controls.Add(this.individualBRButton);
            this.mainPanel.Controls.Add(this.individualBRText);
            this.mainPanel.Controls.Add(this.individualBRLabel);
            this.mainPanel.Controls.Add(this.individualWSButton);
            this.mainPanel.Controls.Add(this.individualWSText);
            this.mainPanel.Controls.Add(this.individualWSLabel);
            this.mainPanel.Controls.Add(this.newPatternButton);
            this.mainPanel.Controls.Add(this.newCategoryButton);
            this.mainPanel.Controls.Add(this.splitContainer1);
            this.mainPanel.Controls.Add(this.patternWRButton);
            this.mainPanel.Controls.Add(this.individualWRButton);
            this.mainPanel.Controls.Add(this.outputCheck);
            this.mainPanel.Controls.Add(this.categoryCombo);
            this.mainPanel.Controls.Add(this.sizeCombo);
            this.mainPanel.Controls.Add(this.patternWRText);
            this.mainPanel.Controls.Add(this.individualWRText);
            this.mainPanel.Controls.Add(this.nameText);
            this.mainPanel.Controls.Add(this.label4);
            this.mainPanel.Controls.Add(this.patternWRLabel);
            this.mainPanel.Controls.Add(this.individualWRLabel);
            this.mainPanel.Controls.Add(this.label5);
            this.mainPanel.Controls.Add(this.label1);
            this.mainPanel.Controls.Add(this.saveButton);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(167, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(626, 695);
            this.mainPanel.TabIndex = 2;
            // 
            // patternBSButton
            // 
            this.patternBSButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.patternBSButton.Location = new System.Drawing.Point(594, 236);
            this.patternBSButton.Name = "patternBSButton";
            this.patternBSButton.Size = new System.Drawing.Size(20, 23);
            this.patternBSButton.TabIndex = 27;
            this.patternBSButton.Text = "...";
            this.patternBSButton.UseVisualStyleBackColor = true;
            this.patternBSButton.Click += new System.EventHandler(this.OnFileSelectButtonClick);
            // 
            // patternBSText
            // 
            this.patternBSText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.patternBSText.Location = new System.Drawing.Point(145, 238);
            this.patternBSText.Name = "patternBSText";
            this.patternBSText.Size = new System.Drawing.Size(446, 19);
            this.patternBSText.TabIndex = 26;
            // 
            // patternBSLabel
            // 
            this.patternBSLabel.AutoSize = true;
            this.patternBSLabel.Location = new System.Drawing.Point(6, 241);
            this.patternBSLabel.Name = "patternBSLabel";
            this.patternBSLabel.Size = new System.Drawing.Size(122, 12);
            this.patternBSLabel.TabIndex = 25;
            this.patternBSLabel.Text = "Pattern(Beigu, Square):";
            // 
            // patternBRButton
            // 
            this.patternBRButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.patternBRButton.Location = new System.Drawing.Point(594, 211);
            this.patternBRButton.Name = "patternBRButton";
            this.patternBRButton.Size = new System.Drawing.Size(20, 23);
            this.patternBRButton.TabIndex = 24;
            this.patternBRButton.Text = "...";
            this.patternBRButton.UseVisualStyleBackColor = true;
            this.patternBRButton.Click += new System.EventHandler(this.OnFileSelectButtonClick);
            // 
            // patternBRText
            // 
            this.patternBRText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.patternBRText.Location = new System.Drawing.Point(145, 213);
            this.patternBRText.Name = "patternBRText";
            this.patternBRText.Size = new System.Drawing.Size(446, 19);
            this.patternBRText.TabIndex = 23;
            // 
            // patternBRLabel
            // 
            this.patternBRLabel.AutoSize = true;
            this.patternBRLabel.Location = new System.Drawing.Point(6, 216);
            this.patternBRLabel.Name = "patternBRLabel";
            this.patternBRLabel.Size = new System.Drawing.Size(119, 12);
            this.patternBRLabel.TabIndex = 22;
            this.patternBRLabel.Text = "Pattern(Beigu, Round):";
            // 
            // patternWSButton
            // 
            this.patternWSButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.patternWSButton.Location = new System.Drawing.Point(594, 186);
            this.patternWSButton.Name = "patternWSButton";
            this.patternWSButton.Size = new System.Drawing.Size(20, 23);
            this.patternWSButton.TabIndex = 21;
            this.patternWSButton.Text = "...";
            this.patternWSButton.UseVisualStyleBackColor = true;
            this.patternWSButton.Click += new System.EventHandler(this.OnFileSelectButtonClick);
            // 
            // patternWSText
            // 
            this.patternWSText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.patternWSText.Location = new System.Drawing.Point(145, 188);
            this.patternWSText.Name = "patternWSText";
            this.patternWSText.Size = new System.Drawing.Size(446, 19);
            this.patternWSText.TabIndex = 20;
            // 
            // patternWSLabel
            // 
            this.patternWSLabel.AutoSize = true;
            this.patternWSLabel.Location = new System.Drawing.Point(6, 191);
            this.patternWSLabel.Name = "patternWSLabel";
            this.patternWSLabel.Size = new System.Drawing.Size(121, 12);
            this.patternWSLabel.TabIndex = 19;
            this.patternWSLabel.Text = "Pattern(White, Square):";
            // 
            // individualBSButton
            // 
            this.individualBSButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.individualBSButton.Location = new System.Drawing.Point(594, 136);
            this.individualBSButton.Name = "individualBSButton";
            this.individualBSButton.Size = new System.Drawing.Size(20, 23);
            this.individualBSButton.TabIndex = 15;
            this.individualBSButton.Text = "...";
            this.individualBSButton.UseVisualStyleBackColor = true;
            this.individualBSButton.Click += new System.EventHandler(this.OnFileSelectButtonClick);
            // 
            // individualBSText
            // 
            this.individualBSText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.individualBSText.Location = new System.Drawing.Point(145, 138);
            this.individualBSText.Name = "individualBSText";
            this.individualBSText.Size = new System.Drawing.Size(446, 19);
            this.individualBSText.TabIndex = 14;
            // 
            // individualBSLabel
            // 
            this.individualBSLabel.AutoSize = true;
            this.individualBSLabel.Location = new System.Drawing.Point(6, 141);
            this.individualBSLabel.Name = "individualBSLabel";
            this.individualBSLabel.Size = new System.Drawing.Size(133, 12);
            this.individualBSLabel.TabIndex = 13;
            this.individualBSLabel.Text = "Individual(Beigu, Square):";
            // 
            // individualBRButton
            // 
            this.individualBRButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.individualBRButton.Location = new System.Drawing.Point(594, 111);
            this.individualBRButton.Name = "individualBRButton";
            this.individualBRButton.Size = new System.Drawing.Size(20, 23);
            this.individualBRButton.TabIndex = 12;
            this.individualBRButton.Text = "...";
            this.individualBRButton.UseVisualStyleBackColor = true;
            this.individualBRButton.Click += new System.EventHandler(this.OnFileSelectButtonClick);
            // 
            // individualBRText
            // 
            this.individualBRText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.individualBRText.Location = new System.Drawing.Point(145, 113);
            this.individualBRText.Name = "individualBRText";
            this.individualBRText.Size = new System.Drawing.Size(446, 19);
            this.individualBRText.TabIndex = 11;
            // 
            // individualBRLabel
            // 
            this.individualBRLabel.AutoSize = true;
            this.individualBRLabel.Location = new System.Drawing.Point(6, 116);
            this.individualBRLabel.Name = "individualBRLabel";
            this.individualBRLabel.Size = new System.Drawing.Size(130, 12);
            this.individualBRLabel.TabIndex = 10;
            this.individualBRLabel.Text = "Individual(Beigu, Round):";
            // 
            // individualWSButton
            // 
            this.individualWSButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.individualWSButton.Location = new System.Drawing.Point(594, 86);
            this.individualWSButton.Name = "individualWSButton";
            this.individualWSButton.Size = new System.Drawing.Size(20, 23);
            this.individualWSButton.TabIndex = 9;
            this.individualWSButton.Text = "...";
            this.individualWSButton.UseVisualStyleBackColor = true;
            this.individualWSButton.Click += new System.EventHandler(this.OnFileSelectButtonClick);
            // 
            // individualWSText
            // 
            this.individualWSText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.individualWSText.Location = new System.Drawing.Point(145, 88);
            this.individualWSText.Name = "individualWSText";
            this.individualWSText.Size = new System.Drawing.Size(446, 19);
            this.individualWSText.TabIndex = 8;
            // 
            // individualWSLabel
            // 
            this.individualWSLabel.AutoSize = true;
            this.individualWSLabel.Location = new System.Drawing.Point(6, 91);
            this.individualWSLabel.Name = "individualWSLabel";
            this.individualWSLabel.Size = new System.Drawing.Size(132, 12);
            this.individualWSLabel.TabIndex = 7;
            this.individualWSLabel.Text = "Individual(White, Square):";
            // 
            // newPatternButton
            // 
            this.newPatternButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.newPatternButton.Location = new System.Drawing.Point(110, 660);
            this.newPatternButton.Name = "newPatternButton";
            this.newPatternButton.Size = new System.Drawing.Size(104, 23);
            this.newPatternButton.TabIndex = 33;
            this.newPatternButton.Text = "New &Pattern";
            this.newPatternButton.UseVisualStyleBackColor = true;
            this.newPatternButton.Click += new System.EventHandler(this.newPatternButton_Click);
            // 
            // newCategoryButton
            // 
            this.newCategoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.newCategoryButton.Location = new System.Drawing.Point(6, 660);
            this.newCategoryButton.Name = "newCategoryButton";
            this.newCategoryButton.Size = new System.Drawing.Size(98, 23);
            this.newCategoryButton.TabIndex = 32;
            this.newCategoryButton.Text = "New &Category";
            this.newCategoryButton.UseVisualStyleBackColor = true;
            this.newCategoryButton.Click += new System.EventHandler(this.newCategoryButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 483);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.detailGrid);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.commentGrid);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(611, 171);
            this.splitContainer1.SplitterDistance = 306;
            this.splitContainer1.TabIndex = 31;
            // 
            // detailGrid
            // 
            this.detailGrid.AllowUserToAddRows = false;
            this.detailGrid.AllowUserToResizeColumns = false;
            this.detailGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle25.BackColor = System.Drawing.Color.Lavender;
            this.detailGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle25;
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.detailGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle26;
            this.detailGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.detailGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.equipmentNameColumn});
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.detailGrid.DefaultCellStyle = dataGridViewCellStyle27;
            this.detailGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailGrid.Location = new System.Drawing.Point(0, 24);
            this.detailGrid.MultiSelect = false;
            this.detailGrid.Name = "detailGrid";
            this.detailGrid.ReadOnly = true;
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle28.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.detailGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle28;
            this.detailGrid.RowTemplate.Height = 21;
            this.detailGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.detailGrid.Size = new System.Drawing.Size(306, 147);
            this.detailGrid.TabIndex = 1;
            this.detailGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.detailGrid_RowEnter);
            this.detailGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.detailGrid_RowsRemoved);
            // 
            // equipmentNameColumn
            // 
            this.equipmentNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.equipmentNameColumn.DataPropertyName = "EquipmentName";
            this.equipmentNameColumn.HeaderText = "Equipment Name";
            this.equipmentNameColumn.Name = "equipmentNameColumn";
            this.equipmentNameColumn.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.addEquipmentButton);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(306, 24);
            this.panel2.TabIndex = 0;
            // 
            // addEquipmentButton
            // 
            this.addEquipmentButton.Location = new System.Drawing.Point(57, 0);
            this.addEquipmentButton.Name = "addEquipmentButton";
            this.addEquipmentButton.Size = new System.Drawing.Size(123, 23);
            this.addEquipmentButton.TabIndex = 1;
            this.addEquipmentButton.Text = "Add Equipment";
            this.addEquipmentButton.UseVisualStyleBackColor = true;
            this.addEquipmentButton.Click += new System.EventHandler(this.addEquipmentButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "Equipment";
            // 
            // colorGrid
            // 
            this.colorGrid.AllowUserToAddRows = false;
            this.colorGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.colorGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colorNameColumn,
            this.pathColumn});
            this.colorGrid.Location = new System.Drawing.Point(3, 335);
            this.colorGrid.Name = "colorGrid";
            this.colorGrid.ReadOnly = true;
            this.colorGrid.RowTemplate.Height = 21;
            this.colorGrid.Size = new System.Drawing.Size(611, 142);
            this.colorGrid.TabIndex = 2;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 316);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "Other colors";
            // 
            // addColorButton
            // 
            this.addColorButton.Location = new System.Drawing.Point(80, 311);
            this.addColorButton.Name = "addColorButton";
            this.addColorButton.Size = new System.Drawing.Size(103, 23);
            this.addColorButton.TabIndex = 3;
            this.addColorButton.Text = "Add Color";
            this.addColorButton.UseVisualStyleBackColor = true;
            this.addColorButton.Click += new System.EventHandler(this.addColorButton_Click);
            // 
            // commentGrid
            // 
            this.commentGrid.AllowUserToAddRows = false;
            this.commentGrid.AllowUserToResizeColumns = false;
            this.commentGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle29.BackColor = System.Drawing.Color.Lavender;
            this.commentGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle29;
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle30.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle30.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle30.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle30.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle30.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle30.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.commentGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle30;
            this.commentGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.commentGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.commentColumn});
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle31.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle31.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle31.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.commentGrid.DefaultCellStyle = dataGridViewCellStyle31;
            this.commentGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commentGrid.Location = new System.Drawing.Point(0, 24);
            this.commentGrid.MultiSelect = false;
            this.commentGrid.Name = "commentGrid";
            this.commentGrid.ReadOnly = true;
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle32.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle32.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle32.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle32.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle32.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.commentGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle32;
            this.commentGrid.RowTemplate.Height = 21;
            this.commentGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.commentGrid.Size = new System.Drawing.Size(301, 147);
            this.commentGrid.TabIndex = 1;
            this.commentGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.commentGrid_RowsRemoved);
            // 
            // commentColumn
            // 
            this.commentColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.commentColumn.DataPropertyName = "CommentText";
            this.commentColumn.HeaderText = "Comment";
            this.commentColumn.Name = "commentColumn";
            this.commentColumn.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.addCommentButton);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 24);
            this.panel1.TabIndex = 0;
            // 
            // addCommentButton
            // 
            this.addCommentButton.Location = new System.Drawing.Point(55, 0);
            this.addCommentButton.Name = "addCommentButton";
            this.addCommentButton.Size = new System.Drawing.Size(123, 23);
            this.addCommentButton.TabIndex = 1;
            this.addCommentButton.Text = "Add Comment";
            this.addCommentButton.UseVisualStyleBackColor = true;
            this.addCommentButton.Click += new System.EventHandler(this.addCommentButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "Comment";
            // 
            // patternWRButton
            // 
            this.patternWRButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.patternWRButton.Location = new System.Drawing.Point(594, 161);
            this.patternWRButton.Name = "patternWRButton";
            this.patternWRButton.Size = new System.Drawing.Size(20, 23);
            this.patternWRButton.TabIndex = 18;
            this.patternWRButton.Text = "...";
            this.patternWRButton.UseVisualStyleBackColor = true;
            this.patternWRButton.Click += new System.EventHandler(this.OnFileSelectButtonClick);
            // 
            // individualWRButton
            // 
            this.individualWRButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.individualWRButton.Location = new System.Drawing.Point(594, 61);
            this.individualWRButton.Name = "individualWRButton";
            this.individualWRButton.Size = new System.Drawing.Size(20, 23);
            this.individualWRButton.TabIndex = 6;
            this.individualWRButton.Text = "...";
            this.individualWRButton.UseVisualStyleBackColor = true;
            this.individualWRButton.Click += new System.EventHandler(this.OnFileSelectButtonClick);
            // 
            // outputCheck
            // 
            this.outputCheck.AutoSize = true;
            this.outputCheck.Location = new System.Drawing.Point(145, 289);
            this.outputCheck.Name = "outputCheck";
            this.outputCheck.Size = new System.Drawing.Size(98, 16);
            this.outputCheck.TabIndex = 30;
            this.outputCheck.Text = "Output in CSV";
            this.outputCheck.UseVisualStyleBackColor = true;
            // 
            // categoryCombo
            // 
            this.categoryCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.categoryCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryCombo.FormattingEnabled = true;
            this.categoryCombo.Location = new System.Drawing.Point(145, 12);
            this.categoryCombo.Name = "categoryCombo";
            this.categoryCombo.Size = new System.Drawing.Size(469, 20);
            this.categoryCombo.TabIndex = 1;
            // 
            // sizeCombo
            // 
            this.sizeCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sizeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sizeCombo.FormattingEnabled = true;
            this.sizeCombo.Location = new System.Drawing.Point(145, 263);
            this.sizeCombo.Name = "sizeCombo";
            this.sizeCombo.Size = new System.Drawing.Size(469, 20);
            this.sizeCombo.TabIndex = 29;
            // 
            // patternWRText
            // 
            this.patternWRText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.patternWRText.Location = new System.Drawing.Point(145, 163);
            this.patternWRText.Name = "patternWRText";
            this.patternWRText.Size = new System.Drawing.Size(446, 19);
            this.patternWRText.TabIndex = 17;
            // 
            // individualWRText
            // 
            this.individualWRText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.individualWRText.Location = new System.Drawing.Point(145, 63);
            this.individualWRText.Name = "individualWRText";
            this.individualWRText.Size = new System.Drawing.Size(446, 19);
            this.individualWRText.TabIndex = 5;
            // 
            // nameText
            // 
            this.nameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nameText.Location = new System.Drawing.Point(145, 38);
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(469, 19);
            this.nameText.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 266);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "Socket Box Size:";
            // 
            // patternWRLabel
            // 
            this.patternWRLabel.AutoSize = true;
            this.patternWRLabel.Location = new System.Drawing.Point(6, 166);
            this.patternWRLabel.Name = "patternWRLabel";
            this.patternWRLabel.Size = new System.Drawing.Size(118, 12);
            this.patternWRLabel.TabIndex = 16;
            this.patternWRLabel.Text = "Pattern(White, Round):";
            // 
            // individualWRLabel
            // 
            this.individualWRLabel.AutoSize = true;
            this.individualWRLabel.Location = new System.Drawing.Point(6, 66);
            this.individualWRLabel.Name = "individualWRLabel";
            this.individualWRLabel.Size = new System.Drawing.Size(129, 12);
            this.individualWRLabel.TabIndex = 4;
            this.individualWRLabel.Text = "Individual(White, Round):";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name:";
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(539, 660);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 34;
            this.saveButton.Text = "&Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // colorNameColumn
            // 
            this.colorNameColumn.DataPropertyName = "ColorName";
            this.colorNameColumn.HeaderText = "Color";
            this.colorNameColumn.Name = "colorNameColumn";
            this.colorNameColumn.ReadOnly = true;
            this.colorNameColumn.Width = 120;
            // 
            // pathColumn
            // 
            this.pathColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.pathColumn.DataPropertyName = "DwgPath";
            this.pathColumn.HeaderText = "DWG Path";
            this.pathColumn.Name = "pathColumn";
            this.pathColumn.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 695);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.treeView);
            this.Name = "Form1";
            this.Text = "Socket plan maintenance";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.detailGrid)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commentGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Splitter splitter;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.DataGridView detailGrid;
        private System.Windows.Forms.ComboBox sizeCombo;
        private System.Windows.Forms.TextBox patternWRText;
        private System.Windows.Forms.TextBox individualWRText;
        private System.Windows.Forms.TextBox nameText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label patternWRLabel;
        private System.Windows.Forms.Label individualWRLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox outputCheck;
        private System.Windows.Forms.Button addEquipmentButton;
        private System.Windows.Forms.ComboBox categoryCombo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button patternWRButton;
        private System.Windows.Forms.Button individualWRButton;
        private System.Windows.Forms.DataGridView commentGrid;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn equipmentNameColumn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button addCommentButton;
        private System.Windows.Forms.Button newCategoryButton;
        private System.Windows.Forms.Button newPatternButton;
        private System.Windows.Forms.Button individualBSButton;
        private System.Windows.Forms.TextBox individualBSText;
        private System.Windows.Forms.Label individualBSLabel;
        private System.Windows.Forms.Button individualBRButton;
        private System.Windows.Forms.TextBox individualBRText;
        private System.Windows.Forms.Label individualBRLabel;
        private System.Windows.Forms.Button individualWSButton;
        private System.Windows.Forms.TextBox individualWSText;
        private System.Windows.Forms.Label individualWSLabel;
        private System.Windows.Forms.Button patternWSButton;
        private System.Windows.Forms.TextBox patternWSText;
        private System.Windows.Forms.Label patternWSLabel;
        private System.Windows.Forms.Button patternBSButton;
        private System.Windows.Forms.TextBox patternBSText;
        private System.Windows.Forms.Label patternBSLabel;
        private System.Windows.Forms.Button patternBRButton;
        private System.Windows.Forms.TextBox patternBRText;
        private System.Windows.Forms.Label patternBRLabel;
        private System.Windows.Forms.DataGridView colorGrid;
        private System.Windows.Forms.Button addColorButton;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridViewTextBoxColumn colorNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pathColumn;
    }
}

