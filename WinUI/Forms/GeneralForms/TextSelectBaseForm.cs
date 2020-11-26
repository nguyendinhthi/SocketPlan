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
    public partial class TextSelectBaseForm : Form
    {
        public TextSelectBaseForm()
        {
            InitializeComponent();

            //以下の処理のため、デザイナを表示できません。
            //デザイナで表示したい時は以下の処理をコメントアウトしてください。
            this.LoadMasters();
        }

        /// <summary>
        /// オーバーライドできるように、アクセス修飾子をprivate→protected virtualに変更 @sato 2015/06/26
        /// </summary>
        protected virtual void LoadMasters()
        {
            foreach (var category in UnitWiring.Masters.CommentCategories)
            {
                ListViewItem item = new ListViewItem();
                item.Text = category.Name;
                item.Tag = category;
                this.categoryListView.Items.Add(item);
            }
        }

        private void TextSelectBaseForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (this.categoryListView.Items.Count != 0)
                    //LoadMaster時コメントカテゴリが存在しない可能性があるので、ここでSelectedの処理をさせてください。 @sato
                    this.categoryListView.Items[0].Selected = true;
                else
                {
                    //コメントカテゴリが存在しなかったらエラーダイアログを出して終了
                    MessageDialog.ShowError(Messages.NotFoundCommentCategory());
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
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

        private void categoryListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var items = this.categoryListView.SelectedItems;
                if (items.Count == 0)
                    return;

                this.textListView.Items.Clear();

                //複数選択できないようプロパティを設定しているので、複数選択のケアはしない
                CommentCategory category = items[0].Tag as CommentCategory;

                foreach (var comment in category.Comments)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = comment.Text;
                    item.Tag = comment;
                    this.textListView.Items.Add(item);
                }
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

        private void categoryListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.textListView.Focus();
            }
        }

        private void textListView_Enter(object sender, EventArgs e)
        {
            if (this.textListView.FocusedItem == null)
            {
                if (this.textListView.Items.Count != 0)
                    this.textListView.Items[0].Selected = true;
            }
            else
                this.textListView.FocusedItem.Selected = true;
        }

        private void TextSelectBaseForm_Activated(object sender, EventArgs e)
        {
            this.categoryListView.Focus();
        }

        private void textListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.OnSelectText();
        }

        private void textListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.OnSelectText();
            }
        }

        protected virtual void OnSelectText()
        {
            MessageDialog.ShowError(new ApplicationException("プログラムのバグです。オーバーライドしてください。"));
        }

        private void textListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var items = this.textListView.SelectedItems;

            if (items.Count == 0)
            {
                this.englishMeaningText.Text = string.Empty;
                return;
            }

            //複数選択できないようプロパティを設定しているので、複数選択のケアはしない
            Comment comment = items[0].Tag as Comment;
            this.englishMeaningText.Text = comment.EnglishMeaning;
        }

        private void searchText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var items = this.categoryListView.SelectedItems;
                if (items.Count == 0)
                    return;

                this.textListView.Items.Clear();

                var category = items[0].Tag as CommentCategory;
                var comments = Array.FindAll(category.Comments, p => p.Text.Contains(this.searchText.Text));

                foreach (var comment in comments)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = comment.Text;
                    item.Tag = comment;
                    this.textListView.Items.Add(item);
                }
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }
    }
}
