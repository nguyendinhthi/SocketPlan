using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRD.CHOPPER.WinUI;
using System.Windows.Forms;
using HRD.CHOPPER.WinUI.UnitWiringServiceReference;

namespace SocketPlanMasterMaintenance
{
    public class EquipmentSelectionDialog : EquipmentSelectForm
    {
        public static EquipmentSelectionDialog instance;
        public static new EquipmentSelectionDialog Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new EquipmentSelectionDialog();

                return instance;
            }
        }

        public static Equipment SelectedEquipment { get; set; }

        private EquipmentSelectionDialog()
            : base()
        {
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SelectedEquipment = null;
        }

        protected override void OnSelectEquipment()
        {
            try
            {
                var items = this.equipmentListView.SelectedItems;
                if (items.Count == 0)
                    return;

                SelectedEquipment = items[0].Tag as Equipment;
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageDialog.ShowError(ex);
            }
        }

        protected override void OnSelectRelatedEquipment()
        {
            try
            {
                var items = this.relatedEquipmentListView.SelectedItems;
                if (items.Count == 0)
                    return;

                SelectedEquipment = items[0].Tag as Equipment;
                this.Hide();
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }
    }
}
