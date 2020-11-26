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
    public partial class SingleSymbolMasterMaintenanceForm : Form
    {
        public SingleSymbolMasterMaintenanceForm()
        {
            InitializeComponent();

            this.dataGridView.AutoGenerateColumns = false;
            this.UpdateGrid();
        }

        private void UpdateGrid()
        {
            this.UpdateGrid(UnitWiring.Masters.SingleSocketBoxEquipments);
        }

        private void UpdateGrid(List<SingleSocketBoxEquipment> equipments)
        {
            this.dataGridView.DataSource = null;

            var bs = new BindingSource();
            bs.DataSource = equipments;
            this.dataGridView.DataSource = bs;
        }

        private void addbutton_Click(object sender, EventArgs e)
        {
            try
            {
                EquipmentSelectionDialog.Instance.ShowDialog(this);
                if (EquipmentSelectionDialog.SelectedEquipment == null)
                    return;

                var equipments = (this.dataGridView.DataSource as BindingSource).DataSource as List<SingleSocketBoxEquipment>;
                if (equipments == null)
                    throw new ApplicationException("Faied to get list.");

                var selected = EquipmentSelectionDialog.SelectedEquipment;
                if (equipments.Exists(p => p.EquipmentId == selected.Id))
                {
                    MessageDialog.ShowWarning("That equipment is already selected.");
                    return;
                }
                
                var equipment = new SingleSocketBoxEquipment();
                equipment.EquipmentId = selected.Id;
                equipment.Equipment = selected;
                equipments.Add(equipment);

                this.UpdateGrid(equipments);
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
                var equipments = (this.dataGridView.DataSource as BindingSource).DataSource as List<SingleSocketBoxEquipment>;
                if (equipments == null)
                    throw new ApplicationException("Faied to get list.");

                using(var service = new SocketPlanService())
                {
                    service.RegisterSingleSocketBoxEquipments(equipments.ToArray());
                }

                UnitWiring.Masters.UpdateSingleSocketBoxEquipments();

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
