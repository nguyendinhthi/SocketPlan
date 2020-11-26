using System.Collections.Generic;
using System.Windows.Forms;
using SocketPlan.WinUI.SocketPlanServiceReference;

namespace SocketPlan.WinUI
{
    public partial class RoomSelectForm : Form
    {
        public string CorrectRoomName
        {
            get
            {
                var row = this.dataGridView.SelectedRows[0];
                var cell = row.Cells[this.roomNameColumn.Name];
                return cell.Value.ToString();
            }
        }

        public string CorrectRoomCode
        {
            get
            {
                var row = this.dataGridView.SelectedRows[0];
                var cell = row.Cells[this.roomCodeColumn.Name];
                return cell.Value.ToString();
            }
        }

        private RoomSelectForm()
        {
            InitializeComponent();
            this.dataGridView.AutoGenerateColumns = false;
        }

        public RoomSelectForm(List<SiyoHeya> siyoHeyas, string selectedText) : this()
        {
            var list = new List<SiyoHeya>(siyoHeyas);
            foreach (var room in UnitWiring.Masters.InteriorEstimateRooms)
            {
                var siyoHeya = new SiyoHeya();
                siyoHeya.Floor = 0;
                siyoHeya.RoomCode = room.RoomCode;
                siyoHeya.RoomName = room.RoomName;
                list.Add(siyoHeya);
            }

            this.dataGridView.DataSource = list;
            this.selectedText.Text = selectedText;
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.okButton.PerformClick();
        }
    }
}
