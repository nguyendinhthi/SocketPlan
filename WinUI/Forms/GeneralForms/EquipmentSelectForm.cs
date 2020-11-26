using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SocketPlan.WinUI.SocketPlanServiceReference;
using SocketPlan.WinUI;

namespace SocketPlan.WinUI
{
    public partial class EquipmentSelectForm : EquipmentSelectBaseForm
    {
        private static EquipmentSelectForm instance;
        public static EquipmentSelectForm Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new EquipmentSelectForm();

                return instance;
            }
        }

        public static void DisposeInstance()
        {
            if (instance == null)
                return;

            instance.Dispose();
            instance = null;
        }

        protected EquipmentSelectForm()
            : base()
        {
            InitializeComponent();
        }

        private void EquipmentSelectForm_Activated(object sender, EventArgs e)
        {
            this.categoryListView.Focus();
        }

        private void EquipmentSelectForm_Deactivate(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void EquipmentSelectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.Owner.Activate();
        }

        private void EquipmentSelectForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Hide();
        }

        protected override void OnSelectEquipment()
        {
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;

            //    var items = this.equipmentListView.SelectedItems;
            //    if (items.Count == 0)
            //        return;

            //    SymbolDrawer.DrawEquipments(items[0].Tag as Equipment);

            //    this.Show();
            //}
            //catch (Exception ex)
            //{
            //    MessageDialog.ShowError(ex);
            //}
            //finally
            //{
            //    this.Cursor = Cursors.Default;
            //}
        }

        protected override void OnSelectRelatedEquipment()
        {
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;

            //    var items = this.relatedEquipmentListView.SelectedItems;
            //    if (items.Count == 0)
            //        return;

            //    SymbolDrawer.DrawEquipments(items[0].Tag as Equipment);

            //    this.Show();
            //}
            //catch (Exception ex)
            //{
            //    MessageDialog.ShowError(ex);
            //}
            //finally
            //{
            //    this.Cursor = Cursors.Default;
            //}
        }
    }
}
