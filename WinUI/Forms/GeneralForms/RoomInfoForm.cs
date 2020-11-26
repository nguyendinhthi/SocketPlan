using System;
using System.Windows.Forms;

namespace SocketPlan.WinUI
{
    public partial class RoomInfoForm : Form
    {
        private RoomObject roomObj;

        private RoomInfoForm()
        {
            InitializeComponent();
        }

        public RoomInfoForm(RoomObject roomObj) : this()
        {
            this.roomObj = roomObj;
            this.roomNameText.Text = roomObj.Name;
            this.roomJyouText.Text = roomObj.GetJyou().ToString();
        }

        private void RoomInfoForm_Shown(object sender, EventArgs e)
        {
            this.tarekabeJyouText.Focus();
        }

        private void withButton_Click(object sender, EventArgs e)
        {
            try
            {
                //入力の書式を確かめる
                double jyou;
                if (!Double.TryParse(this.tarekabeJyouText.Text, out jyou))
                    throw new ApplicationException(Messages.InvalidTarekabeJyou(this.tarekabeJyouText.Text));

                //タレ壁帖数を設定してからフォームを閉じる
                XData.Room.SetRoomTarekabeJyou(roomObj.ObjectId, this.tarekabeJyouText.Text);

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageDialog.ShowError(ex);
            }
        }

        private void withoutButton_Click(object sender, EventArgs e)
        {
            //フォームを閉じるだけ
            this.DialogResult = DialogResult.OK;
        }

        private void tarekabeJyouText_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                this.withButton.PerformClick();
        }
    }
}
