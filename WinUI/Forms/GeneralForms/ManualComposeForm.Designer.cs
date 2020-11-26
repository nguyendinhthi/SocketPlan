namespace SocketPlan.WinUI
{
    partial class ManualComposeForm
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
            this.resultListView = new System.Windows.Forms.ListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.relatedEquipmentListView = new System.Windows.Forms.ListView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.searchText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.clearButton = new System.Windows.Forms.Button();
            this.setButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
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
            this.equipmentListView.Size = new System.Drawing.Size(688, 238);
            this.equipmentListView.SmallImageList = this.equipmentImageList;
            this.equipmentListView.TabIndex = 0;
            this.equipmentListView.TabStop = false;
            this.equipmentListView.UseCompatibleStateImageBehavior = false;
            this.equipmentListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.equipmentListView_MouseDoubleClick);
            this.equipmentListView.SelectedIndexChanged += new System.EventHandler(this.equipmentListView_SelectedIndexChanged);
            this.equipmentListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.equipmentListView_KeyDown);
            // 
            // equipmentImageList
            // 
            this.equipmentImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.equipmentImageList.ImageSize = new System.Drawing.Size(64, 64);
            this.equipmentImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // resultListView
            // 
            this.resultListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultListView.HideSelection = false;
            this.resultListView.LargeImageList = this.equipmentImageList;
            this.resultListView.Location = new System.Drawing.Point(0, 21);
            this.resultListView.MultiSelect = false;
            this.resultListView.Name = "resultListView";
            this.resultListView.Size = new System.Drawing.Size(832, 110);
            this.resultListView.SmallImageList = this.equipmentImageList;
            this.resultListView.TabIndex = 1;
            this.resultListView.TabStop = false;
            this.resultListView.UseCompatibleStateImageBehavior = false;
            this.resultListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.resultListView_KeyDown);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.equipmentListView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(688, 238);
            this.panel2.TabIndex = 1;
            // 
            // relatedEquipmentListView
            // 
            this.relatedEquipmentListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relatedEquipmentListView.HideSelection = false;
            this.relatedEquipmentListView.LargeImageList = this.equipmentImageList;
            this.relatedEquipmentListView.Location = new System.Drawing.Point(0, 28);
            this.relatedEquipmentListView.MultiSelect = false;
            this.relatedEquipmentListView.Name = "relatedEquipmentListView";
            this.relatedEquipmentListView.Size = new System.Drawing.Size(688, 145);
            this.relatedEquipmentListView.SmallImageList = this.equipmentImageList;
            this.relatedEquipmentListView.TabIndex = 1;
            this.relatedEquipmentListView.TabStop = false;
            this.relatedEquipmentListView.UseCompatibleStateImageBehavior = false;
            this.relatedEquipmentListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.relatedEquipmentListView_MouseDoubleClick);
            this.relatedEquipmentListView.Enter += new System.EventHandler(this.relatedEquipmentListView_Enter);
            this.relatedEquipmentListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.relatedEquipmentListView_KeyDown);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.searchText);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(688, 28);
            this.panel3.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(472, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Search:";
            // 
            // searchText
            // 
            this.searchText.Location = new System.Drawing.Point(519, 4);
            this.searchText.Name = "searchText";
            this.searchText.Size = new System.Drawing.Size(159, 19);
            this.searchText.TabIndex = 1;
            this.searchText.TextChanged += new System.EventHandler(this.searchText_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Related Pattern";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.relatedEquipmentListView);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 238);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(688, 173);
            this.panel1.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.resultListView);
            this.splitContainer1.Panel2.Controls.Add(this.panel6);
            this.splitContainer1.Panel2.Controls.Add(this.panel5);
            this.splitContainer1.Size = new System.Drawing.Size(832, 586);
            this.splitContainer1.SplitterDistance = 411;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Panel1MinSize = 140;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel2);
            this.splitContainer2.Panel2.Controls.Add(this.panel1);
            this.splitContainer2.Size = new System.Drawing.Size(832, 411);
            this.splitContainer2.SplitterDistance = 140;
            this.splitContainer2.TabIndex = 2;
            this.splitContainer2.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(832, 21);
            this.panel6.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Composed Items";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.clearButton);
            this.panel5.Controls.Add(this.setButton);
            this.panel5.Controls.Add(this.closeButton);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 131);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(832, 40);
            this.panel5.TabIndex = 2;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(607, 6);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(98, 25);
            this.clearButton.TabIndex = 11;
            this.clearButton.Text = "&Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // setButton
            // 
            this.setButton.Location = new System.Drawing.Point(491, 6);
            this.setButton.Name = "setButton";
            this.setButton.Size = new System.Drawing.Size(98, 25);
            this.setButton.TabIndex = 10;
            this.setButton.Text = "&Set";
            this.setButton.UseVisualStyleBackColor = true;
            this.setButton.Click += new System.EventHandler(this.setButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(722, 6);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(98, 25);
            this.closeButton.TabIndex = 12;
            this.closeButton.Text = "Clos&e";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // ManualComposeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 586);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ManualComposeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manual Compose";
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList equipmentImageList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        protected System.Windows.Forms.ListView relatedEquipmentListView;
        protected System.Windows.Forms.ListView equipmentListView;
        protected System.Windows.Forms.ListView resultListView;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button setButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox searchText;
    }
}