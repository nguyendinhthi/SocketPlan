using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SocketPlan.WinUI.SocketPlanServiceReference;
using System.IO;

namespace SocketPlan.WinUI
{
    public partial class SocketBoxSpecificSelectForm : Form
    {
        public static SocketBoxSpecific SelectedSpecific { get; set; }

        public SocketBoxSpecificSelectForm()
        {
            InitializeComponent();

            this.LoadMasters();
        }

        private void LoadMasters()
        {
            foreach (var category in UnitWiring.Masters.SocketBoxSpecificCategories)
            {
                ListViewItem item = new ListViewItem();
                item.Text = category.Name;
                item.Tag = category;
                this.categoryListView.Items.Add(item);
            }
        }

        private void SocketBoxSpecificSelectForm_Load(object sender, EventArgs e)
        {
            try
            {
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
        }

        private void categoryListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var items = this.categoryListView.SelectedItems;
                if (items.Count == 0)
                    return;

                this.specificListView.Items.Clear();

                //複数選択できないようプロパティを設定しているので、複数選択のケアはしない
                SocketBoxSpecificCategory category = items[0].Tag as SocketBoxSpecificCategory;

                foreach (var specific in category.Specifics)
                {
                    var imagePath = specific.ImagePath;
                    if (!File.Exists(imagePath))
                        imagePath = Path.Combine(Properties.Settings.Default.ImageDirectory, "NotFound.png");

                    ListViewItem item = new ListViewItem();
                    item.Text = specific.Serial;
                    item.Tag = specific;
                    item.ImageKey = imagePath;
                    this.specificListView.Items.Add(item);

                    if (!this.imageList.Images.ContainsKey(imagePath))
                        this.imageList.Images.Add(imagePath, Image.FromFile(imagePath));
                }

                if(this.specificListView.Items.Count != 0)
                    this.UpdateRelatedList(this.specificListView.Items[0].Tag as SocketBoxSpecific);
            }
            catch (Exception ex)
            {
                MessageDialog.ShowError(ex);
            }
        }

        private void specificListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.OnSelectEquipment();
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void equipmentListView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.OnSelectEquipment();
                }
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void OnSelectEquipment()
        {
            if (this.specificListView.SelectedItems.Count == 0)
                return;

            SelectedSpecific = this.specificListView.SelectedItems[0].Tag as SocketBoxSpecific;
            this.DialogResult = DialogResult.OK;
        }

        private void specificListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var items = this.specificListView.SelectedItems;
                if (items.Count == 0)
                    return;

                this.relatedListView.Items.Clear();

                //複数選択できないようプロパティを設定しているので、複数選択のケアはしない
                SocketBoxSpecific specific = items[0].Tag as SocketBoxSpecific;
                this.UpdateRelatedList(specific);
            }
            catch (Exception ex)
            {
                MessageDialog.ShowError(ex);
            }
        }

        private void UpdateRelatedList(SocketBoxSpecific specific)
        {
            this.relatedListView.Items.Clear();

            var relatedSpecifics = new List<SocketBoxSpecific>();

            foreach (var relation in specific.Relations)
            {
                var related = this.GetSpecifics(relation.RelatedSpecificId);
                if (related != null)
                    relatedSpecifics.Add(related);
            }

            foreach (var related in relatedSpecifics)
            {
                var imagePath = related.ImagePath;
                if (!File.Exists(imagePath))
                    imagePath = Path.Combine(Properties.Settings.Default.ImageDirectory, "NotFound.png");

                ListViewItem item = new ListViewItem();
                item.Text = related.Serial;
                item.Tag = related;
                item.ImageKey = imagePath;
                this.relatedListView.Items.Add(item);

                if (!this.imageList.Images.ContainsKey(imagePath))
                    this.imageList.Images.Add(imagePath, Image.FromFile(imagePath));
            }
        }

        private SocketBoxSpecific GetSpecifics(int specificId)
        {
            foreach (var category in UnitWiring.Masters.SocketBoxSpecificCategories)
            {
                var specific = Array.Find(category.Specifics, p => p.Id == specificId);
                if (specific != null)
                    return specific;
            }

            return null;
        }

        private void relatedListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.OnSelectRelatedSpecific();
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void relatedListView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.OnSelectRelatedSpecific();
                }
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void OnSelectRelatedSpecific()
        {
            if (this.relatedListView.SelectedItems.Count == 0)
                return;

            SelectedSpecific = this.relatedListView.SelectedItems[0].Tag as SocketBoxSpecific;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
