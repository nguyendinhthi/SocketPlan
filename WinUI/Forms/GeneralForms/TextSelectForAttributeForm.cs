using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SocketPlan.WinUI.SocketPlanServiceReference;

namespace SocketPlan.WinUI
{
    public partial class TextSelectForAttributeForm : TextSelectBaseForm
    {
        public Comment Comment { get; set; }

        public TextSelectForAttributeForm()
            : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 左側のカテゴリーリストに表示するデータを設定する。
        /// SerialForUnitWiringOnly以外のカテゴリーを表示する。
        /// </summary>
        protected override void LoadMasters()
        {
            List<CommentCategory> categorys = UnitWiring.Masters.CommentCategories.FindAll(p => p.Id != Const.CommentCategoryId.SerialForUnitWiringOnly);

            foreach (CommentCategory category in categorys)
            {
                ListViewItem item = new ListViewItem();
                item.Text = category.Name;
                item.Tag = category;
                this.categoryListView.Items.Add(item);
            }
        }

        protected override void OnSelectText()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                var items = this.textListView.SelectedItems;
                if (items.Count == 0)
                    return;

                this.Comment = items[0].Tag as Comment;
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageDialog.ShowError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void TextSelectForAttributeForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
