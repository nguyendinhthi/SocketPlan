namespace SocketPlan.WinUI
{
    partial class EquipmentSelectBaseForm
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
            this.equipmentListView = new System.Windows.Forms.ListView();
            this.equipmentImageList = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.itemNameText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.relatedEquipmentListView = new System.Windows.Forms.ListView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.categoryListView = new System.Windows.Forms.ListView();
            this.categoryImageList = new System.Windows.Forms.ImageList(this.components);
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // equipmentListView
            // 
            this.equipmentListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.equipmentListView.HideSelection = false;
            this.equipmentListView.LargeImageList = this.equipmentImageList;
            this.equipmentListView.Location = new System.Drawing.Point(0, 0);
            this.equipmentListView.MultiSelect = false;
            this.equipmentListView.Name = "equipmentListView";
            this.equipmentListView.Size = new System.Drawing.Size(541, 364);
            this.equipmentListView.SmallImageList = this.equipmentImageList;
            this.equipmentListView.TabIndex = 0;
            this.equipmentListView.UseCompatibleStateImageBehavior = false;
            this.equipmentListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.equipmentListView_MouseDoubleClick);
            this.equipmentListView.SelectedIndexChanged += new System.EventHandler(this.equipmentListView_SelectedIndexChanged);
            this.equipmentListView.Enter += new System.EventHandler(this.equipmentListView_Enter);
            this.equipmentListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.equipmentListView_KeyDown);
            // 
            // equipmentImageList
            // 
            this.equipmentImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.equipmentImageList.ImageSize = new System.Drawing.Size(48, 48);
            this.equipmentImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.equipmentListView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(541, 364);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.itemNameText);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 195);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(541, 25);
            this.panel4.TabIndex = 2;
            // 
            // itemNameText
            // 
            this.itemNameText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.itemNameText.Location = new System.Drawing.Point(71, 3);
            this.itemNameText.Name = "itemNameText";
            this.itemNameText.ReadOnly = true;
            this.itemNameText.Size = new System.Drawing.Size(467, 19);
            this.itemNameText.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Item Name:";
            // 
            // relatedEquipmentListView
            // 
            this.relatedEquipmentListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relatedEquipmentListView.HideSelection = false;
            this.relatedEquipmentListView.LargeImageList = this.equipmentImageList;
            this.relatedEquipmentListView.Location = new System.Drawing.Point(0, 18);
            this.relatedEquipmentListView.MultiSelect = false;
            this.relatedEquipmentListView.Name = "relatedEquipmentListView";
            this.relatedEquipmentListView.Size = new System.Drawing.Size(541, 177);
            this.relatedEquipmentListView.SmallImageList = this.equipmentImageList;
            this.relatedEquipmentListView.TabIndex = 1;
            this.relatedEquipmentListView.UseCompatibleStateImageBehavior = false;
            this.relatedEquipmentListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.relatedEquipmentListView_MouseDoubleClick);
            this.relatedEquipmentListView.SelectedIndexChanged += new System.EventHandler(this.relatedEquipmentListView_SelectedIndexChanged);
            this.relatedEquipmentListView.Enter += new System.EventHandler(this.relatedEquipmentListView_Enter);
            this.relatedEquipmentListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.relatedEquipmentListView_KeyDown);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(541, 18);
            this.panel3.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Related Pattern";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.relatedEquipmentListView);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 364);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(541, 220);
            this.panel1.TabIndex = 2;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.categoryListView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panel2);
            this.splitContainer.Panel2.Controls.Add(this.panel1);
            this.splitContainer.Size = new System.Drawing.Size(811, 584);
            this.splitContainer.SplitterDistance = 266;
            this.splitContainer.TabIndex = 2;
            this.splitContainer.TabStop = false;
            // 
            // categoryListView
            // 
            this.categoryListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoryListView.HideSelection = false;
            this.categoryListView.LargeImageList = this.categoryImageList;
            this.categoryListView.Location = new System.Drawing.Point(0, 0);
            this.categoryListView.MultiSelect = false;
            this.categoryListView.Name = "categoryListView";
            this.categoryListView.Size = new System.Drawing.Size(266, 584);
            this.categoryListView.SmallImageList = this.categoryImageList;
            this.categoryListView.TabIndex = 0;
            this.categoryListView.UseCompatibleStateImageBehavior = false;
            this.categoryListView.SelectedIndexChanged += new System.EventHandler(this.categoryListView_SelectedIndexChanged);
            this.categoryListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.categoryListView_KeyDown);
            // 
            // categoryImageList
            // 
            this.categoryImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.categoryImageList.ImageSize = new System.Drawing.Size(48, 48);
            this.categoryImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // EquipmentSelectBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 584);
            this.Controls.Add(this.splitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.Name = "EquipmentSelectBaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Equipment";
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList equipmentImageList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox itemNameText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ImageList categoryImageList;
        protected System.Windows.Forms.ListView categoryListView;
        protected System.Windows.Forms.ListView relatedEquipmentListView;
        protected System.Windows.Forms.ListView equipmentListView;
    }
}