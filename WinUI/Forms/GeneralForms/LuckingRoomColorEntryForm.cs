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
    public partial class LuckingRoomColorEntryForm : Form
    {
        public List<PartsColorEntry> LuckEntries { get; set; }

        public LuckingRoomColorEntryForm(List<PartsColorEntry> luckEntries)
        {
            InitializeComponent();

            this.LuckEntries = luckEntries;

            this.dataGridView.AutoGenerateColumns = false;
        }

        #region イベント

        private void PartsColorEntryForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                this.LoadComboItems();
                this.dataGridView.DataSource = this.LuckEntries;
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

        private void LuckingRoomColorEntryForm_Shown(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                this.ValidateEntry();

                this.LuckEntries = this.dataGridView.DataSource as List<PartsColorEntry>;

                this.DialogResult = DialogResult.OK;
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

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.dataGridView.SelectedCells.Count == 0)
                return;

            var selectedCell = this.dataGridView.SelectedCells[0];
            if (selectedCell.ColumnIndex == this.floorColumn.Index ||
                selectedCell.ColumnIndex == this.roomNameColumn.Index)
                return;

            var column = this.dataGridView.Columns[selectedCell.ColumnIndex];
            if (!(column is DataGridViewComboBoxColumn))
                return;

            var combo = column as DataGridViewComboBoxColumn;
            var colors = combo.DataSource as List<PartsColor>;
            if (colors == null)
                return;

            var key = e.KeyCode.ToString();
            var number = this.ExtractNumber(key);
            if (number.HasValue && key.Contains("F"))
                return;

            if (number.HasValue)
                key = number.ToString();

            var color = colors.Find(p => p.Prefix == key);
            if (color == null)
                return;

            selectedCell.Value = color.Id;
        }

        #endregion

        #region プライベートメソッド

        private void LoadComboItems()
        {
            var items = new List<PartsColor>();
            using (var service = new SocketPlanServiceNoTimeout())
            {
                items.AddRange(service.GetPartsColors());
            }

            items.RemoveAll(p => p.Name.Contains("電"));

            this.wallColumn.DataSource = items;
            this.wallColumn.ValueMember = "Id";
            this.wallColumn.DisplayMember = "NameWithPrefix";
        }

        private void ValidateEntry()
        {
            var entries = this.dataGridView.DataSource as List<PartsColorEntry>;
            if (entries == null)
                throw new ApplicationException(Messages.UnexpectedProcessCalled("ValidateEntry"));

            foreach (var entry in entries)
            {
                if (entry.WallColorId == 0)
                    throw new ApplicationException(Messages.SelectPartsColor(entry.Floor + "F " + entry.RoomName));
            }
        }

        private int? ExtractNumber(string value)
        {
            if (value.Contains("0")) return 0;
            if (value.Contains("1")) return 1;
            if (value.Contains("2")) return 2;
            if (value.Contains("3")) return 3;
            if (value.Contains("4")) return 4;
            if (value.Contains("5")) return 5;
            if (value.Contains("6")) return 6;
            if (value.Contains("7")) return 7;
            if (value.Contains("8")) return 8;
            if (value.Contains("9")) return 9;
            return null;
        }

        #endregion
    }
}
