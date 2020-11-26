using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SocketPlan.WinUI.SocketPlanServiceReference;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public partial class OtherColorSelectForm : Form
    {
        public SocketBoxPatternColor SelectedColor
        {
            get;
            private set;
        }

        public OtherColorSelectForm(List<SocketBoxPatternColor> colors)
        {
            InitializeComponent();

            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.DataSource = colors;
        }

        private void LuckingRoomColorEntryForm_Shown(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != this.buttonColumn.Index)
                    return;

                var color = this.dataGridView.Rows[e.RowIndex].DataBoundItem as SocketBoxPatternColor;
                if (color == null)
                    throw new ApplicationException("Invalid color is selected. Please close this form and open again.");

                this.SelectedColor = color;

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
