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
    public partial class EquipmentSelectBaseForm : Form
    {
        public EquipmentSelectBaseForm()
        {
            InitializeComponent();
            this.LoadMasters();
            this.categoryListView.Items[0].Selected = true;
        }

        private void LoadMasters()
        {
            foreach (var catalog in UnitWiring.Masters.SelectionCategories)
            {
                var imagePath = Paths.GetImagePath(catalog.ImagePath);

                ListViewItem item = new ListViewItem();
                item.Text = catalog.Name;
                item.Tag = catalog;
                item.ImageKey = imagePath;
                this.categoryListView.Items.Add(item);

                this.categoryImageList.Images.Add(imagePath, Image.FromFile(imagePath));
            }
        }

        private void categoryListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var items = this.categoryListView.SelectedItems;
                if (items.Count == 0)
                    return;

                this.equipmentListView.Items.Clear();

                //複数選択できないようプロパティを設定しているので、複数選択のケアはしない
                SelectionCategory category = items[0].Tag as SelectionCategory;

                foreach (var equipment in category.Equipments)
                {
                    var imagePath = Paths.GetImagePath(equipment.ImagePath);

                    ListViewItem item = new ListViewItem();
                    item.Text = equipment.NameAtSelection;
                    item.Tag = equipment;
                    item.ImageKey = imagePath;
                    this.equipmentListView.Items.Add(item);

                    if (!this.equipmentImageList.Images.ContainsKey(imagePath))
                        this.equipmentImageList.Images.Add(imagePath, Image.FromFile(imagePath));
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
                this.equipmentListView.Focus();
            }
        }

        private void equipmentListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.OnSelectEquipment();
        }

        private void equipmentListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.OnSelectEquipment();
            }
        }

        private void equipmentListView_Enter(object sender, EventArgs e)
        {
            if (this.equipmentListView.FocusedItem == null)
            {
                if(this.equipmentListView.Items.Count != 0)
                    this.equipmentListView.Items[0].Selected = true;
            }
            else
                this.equipmentListView.FocusedItem.Selected = true;
        }

        private void equipmentListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var items = this.equipmentListView.SelectedItems;
                if (items.Count == 0)
                    return;

                this.relatedEquipmentListView.Items.Clear();

                //複数選択できないようプロパティを設定しているので、複数選択のケアはしない
                Equipment equipment = items[0].Tag as Equipment;
                this.itemNameText.Text = equipment.Name;

                foreach (var related in equipment.RelatedEquipments)
                {
                    var relatedEquipment = UnitWiring.Masters.Equipments.Find(p => p.Id == related.RelatedEquipmentId);
                    var imagePath = Paths.GetImagePath(relatedEquipment.ImagePath);

                    ListViewItem item = new ListViewItem();
                    item.Text = relatedEquipment.NameAtSelection;
                    item.Tag = relatedEquipment;
                    item.ImageKey = imagePath;
                    this.relatedEquipmentListView.Items.Add(item);

                    if (!this.equipmentImageList.Images.ContainsKey(imagePath))
                        this.equipmentImageList.Images.Add(imagePath, Image.FromFile(imagePath));
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

        private void relatedEquipmentListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.OnSelectRelatedEquipment();
        }

        private void relatedEquipmentListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.OnSelectRelatedEquipment();
            }
        }

        private void relatedEquipmentListView_Enter(object sender, EventArgs e)
        {
            if (this.relatedEquipmentListView.FocusedItem == null)
            {
                if(this.relatedEquipmentListView.Items.Count != 0)
                    this.relatedEquipmentListView.Items[0].Selected = true;
            }
            else
                this.relatedEquipmentListView.FocusedItem.Selected = true;
        }

        private void relatedEquipmentListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var items = this.relatedEquipmentListView.SelectedItems;
                if (items.Count == 0)
                    return;

                //複数選択できないようプロパティを設定しているので、複数選択のケアはしない
                Equipment equipment = items[0].Tag as Equipment;
                this.itemNameText.Text = equipment.Name;
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

        protected virtual void OnSelectEquipment()
        {
            MessageDialog.ShowError(new ApplicationException("プログラムのバグです。オーバーライドしてください。"));
        }

        protected virtual void OnSelectRelatedEquipment()
        {
            MessageDialog.ShowError(new ApplicationException("プログラムのバグです。オーバーライドしてください。"));
        }
    }
}
