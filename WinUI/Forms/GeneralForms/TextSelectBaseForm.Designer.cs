namespace SocketPlan.WinUI
{
    partial class TextSelectBaseForm
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.categoryListView = new System.Windows.Forms.ListView();
            this.textListView = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.searchText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.englishMeaningText = new System.Windows.Forms.TextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer.Panel2.Controls.Add(this.textListView);
            this.splitContainer.Panel2.Controls.Add(this.panel1);
            this.splitContainer.Size = new System.Drawing.Size(663, 422);
            this.splitContainer.SplitterDistance = 142;
            this.splitContainer.TabIndex = 2;
            this.splitContainer.TabStop = false;
            // 
            // categoryListView
            // 
            this.categoryListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoryListView.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.categoryListView.HideSelection = false;
            this.categoryListView.Location = new System.Drawing.Point(0, 0);
            this.categoryListView.MultiSelect = false;
            this.categoryListView.Name = "categoryListView";
            this.categoryListView.Size = new System.Drawing.Size(142, 422);
            this.categoryListView.TabIndex = 0;
            this.categoryListView.UseCompatibleStateImageBehavior = false;
            this.categoryListView.View = System.Windows.Forms.View.List;
            this.categoryListView.SelectedIndexChanged += new System.EventHandler(this.categoryListView_SelectedIndexChanged);
            this.categoryListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.categoryListView_KeyDown);
            // 
            // textListView
            // 
            this.textListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textListView.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textListView.HideSelection = false;
            this.textListView.Location = new System.Drawing.Point(0, 0);
            this.textListView.MultiSelect = false;
            this.textListView.Name = "textListView";
            this.textListView.Size = new System.Drawing.Size(517, 360);
            this.textListView.TabIndex = 0;
            this.textListView.UseCompatibleStateImageBehavior = false;
            this.textListView.View = System.Windows.Forms.View.List;
            this.textListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textListView_MouseDoubleClick);
            this.textListView.SelectedIndexChanged += new System.EventHandler(this.textListView_SelectedIndexChanged);
            this.textListView.Enter += new System.EventHandler(this.textListView_Enter);
            this.textListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textListView_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.searchText);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.englishMeaningText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 360);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(517, 62);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Search:";
            // 
            // searchText
            // 
            this.searchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.searchText.Location = new System.Drawing.Point(99, 31);
            this.searchText.Name = "searchText";
            this.searchText.Size = new System.Drawing.Size(406, 19);
            this.searchText.TabIndex = 3;
            this.toolTip.SetToolTip(this.searchText, "Please input text and push \'Enter\' key.");
            this.searchText.TextChanged += new System.EventHandler(this.searchText_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "English Meaning:";
            // 
            // englishMeaningText
            // 
            this.englishMeaningText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.englishMeaningText.Location = new System.Drawing.Point(99, 6);
            this.englishMeaningText.Name = "englishMeaningText";
            this.englishMeaningText.ReadOnly = true;
            this.englishMeaningText.Size = new System.Drawing.Size(406, 19);
            this.englishMeaningText.TabIndex = 1;
            // 
            // TextSelectBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 422);
            this.Controls.Add(this.splitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.Name = "TextSelectBaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Text";
            this.Load += new System.EventHandler(this.TextSelectBaseForm_Load);
            this.Activated += new System.EventHandler(this.TextSelectBaseForm_Activated);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        protected System.Windows.Forms.ListView textListView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox englishMeaningText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox searchText;
        private System.Windows.Forms.ToolTip toolTip;
        protected System.Windows.Forms.ListView categoryListView;
    }
}