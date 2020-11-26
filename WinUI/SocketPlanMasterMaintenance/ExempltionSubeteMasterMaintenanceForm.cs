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
    public partial class SubeteExemptionMasterMaintenanceForm : Form
    {
        public SubeteExemptionMasterMaintenanceForm()
        {
            InitializeComponent();

            this.equipmentGrid.AutoGenerateColumns = false;
            this.serialGrid.AutoGenerateColumns = false;
            this.UpdateGrids();
        }

        private void UpdateGrids()
        {
            this.UpdateEquipmentGrid(UnitWiring.Masters.NotNameHotaruSwitches);
            this.UpdateSerialGrid(UnitWiring.Masters.NotNameHotaruSwitchSerials);
        }

        private void UpdateEquipmentGrid(List<NotNameHotaruSwitch> equipments)
        {
            this.equipmentGrid.DataSource = null;

            var bs = new BindingSource();
            bs.DataSource = equipments;
            this.equipmentGrid.DataSource = bs;
        }

        private void UpdateSerialGrid(List<NotNameHotaruSwitchSerial> serials)
        {
            this.serialGrid.DataSource = null;

            var bs = new BindingSource();
            bs.DataSource = serials;
            this.serialGrid.DataSource = bs;
        }

        private void addbutton_Click(object sender, EventArgs e)
        {
            try
            {
                EquipmentSelectionDialog.Instance.ShowDialog(this);
                if (EquipmentSelectionDialog.SelectedEquipment == null)
                    return;

                var equipments = (this.equipmentGrid.DataSource as BindingSource).DataSource as List<NotNameHotaruSwitch>;
                if (equipments == null)
                    throw new ApplicationException("Faied to get list.");

                var selected = EquipmentSelectionDialog.SelectedEquipment;
                if (equipments.Exists(p => p.EquipmentId == selected.Id))
                {
                    MessageDialog.ShowWarning("That equipment is already selected.");
                    return;
                }
                
                var equipment = new NotNameHotaruSwitch();
                equipment.EquipmentId = selected.Id;
                equipment.Equipment = selected;
                equipments.Add(equipment);

                this.UpdateEquipmentGrid(equipments);
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void serialGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                if (this.serialGrid[e.ColumnIndex, e.RowIndex].Value != null &&
                    e.FormattedValue.ToString() == this.serialGrid[e.ColumnIndex, e.RowIndex].Value.ToString())
                    return;

                var serials = (this.serialGrid.DataSource as BindingSource).DataSource as List<NotNameHotaruSwitchSerial>;
                if (serials == null)
                    throw new ApplicationException("Faied to get list.");

                if (serials.Exists(p => p.Hinban == e.FormattedValue.ToString()))
                {
                    MessageDialog.ShowWarning("This serial is already entried.");
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                var equipments = (this.equipmentGrid.DataSource as BindingSource).DataSource as List<NotNameHotaruSwitch>;
                if (equipments == null)
                    throw new ApplicationException("Faied to get list.");

                var serials = (this.serialGrid.DataSource as BindingSource).DataSource as List<NotNameHotaruSwitchSerial>;
                if (serials == null)
                    throw new ApplicationException("Faied to get list.");

                using(var service = new SocketPlanService())
                {
                    service.RegisterNotNameHotaruSwitches(equipments.ToArray());
                    service.RegisterNotNameHotaruSwitchSerials(serials.ToArray());
                }

                UnitWiring.Masters.UpdateNotNameHotaruSwitches();
                UnitWiring.Masters.UpdateNotNameHotaruSwitchSerials();

                MessageDialog.ShowInformation(this, "Saved successfully.");
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
