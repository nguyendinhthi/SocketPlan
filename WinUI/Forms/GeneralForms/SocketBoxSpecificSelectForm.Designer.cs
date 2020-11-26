namespace SocketPlan.WinUI
{
    partial class SocketBoxSpecificSelectForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.categoryListView = new System.Windows.Forms.ListView();
            this.specificListView = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.relatedListView = new System.Windows.Forms.ListView();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.categoryListView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.specificListView);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(811, 584);
            this.splitContainer1.SplitterDistance = 266;
            this.splitContainer1.TabIndex = 0;
            // 
            // categoryListView
            // 
            this.categoryListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoryListView.Font = new System.Drawing.Font("MS UI Gothic", 14.25F);
            this.categoryListView.HideSelection = false;
            this.categoryListView.Location = new System.Drawing.Point(0, 0);
            this.categoryListView.MultiSelect = false;
            this.categoryListView.Name = "categoryListView";
            this.categoryListView.Size = new System.Drawing.Size(266, 584);
            this.categoryListView.TabIndex = 0;
            this.categoryListView.UseCompatibleStateImageBehavior = false;
            this.categoryListView.View = System.Windows.Forms.View.List;
            this.categoryListView.SelectedIndexChanged += new System.EventHandler(this.categoryListView_SelectedIndexChanged);
            // 
            // specificListView
            // 
            this.specificListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.specificListView.HideSelection = false;
            this.specificListView.LargeImageList = this.imageList;
            this.specificListView.Location = new System.Drawing.Point(0, 0);
            this.specificListView.MultiSelect = false;
            this.specificListView.Name = "specificListView";
            this.specificListView.Size = new System.Drawing.Size(541, 379);
            this.specificListView.SmallImageList = this.imageList;
            this.specificListView.TabIndex = 0;
            this.specificListView.UseCompatibleStateImageBehavior = false;
            this.specificListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.specificListView_MouseDoubleClick);
            this.specificListView.SelectedIndexChanged += new System.EventHandler(this.specificListView_SelectedIndexChanged);
            this.specificListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.equipmentListView_KeyDown);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(48, 48);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.relatedListView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 379);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(541, 205);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Related Specific";
            // 
            // relatedListView
            // 
            this.relatedListView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.relatedListView.LargeImageList = this.imageList;
            this.relatedListView.Location = new System.Drawing.Point(0, 18);
            this.relatedListView.Name = "relatedListView";
            this.relatedListView.Size = new System.Drawing.Size(541, 187);
            this.relatedListView.SmallImageList = this.imageList;
            this.relatedListView.TabIndex = 1;
            this.relatedListView.UseCompatibleStateImageBehavior = false;
            this.relatedListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.relatedListView_MouseDoubleClick);
            this.relatedListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.relatedListView_KeyDown);
            // 
            // SocketBoxSpecificSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 584);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SocketBoxSpecificSelectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SocketBoxSpecificSelectForm";
            this.Load += new System.EventHandler(this.SocketBoxSpecificSelectForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        protected System.Windows.Forms.ListView categoryListView;
        protected System.Windows.Forms.ListView specificListView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView relatedListView;
        private System.Windows.Forms.ImageList imageList;


    }
}