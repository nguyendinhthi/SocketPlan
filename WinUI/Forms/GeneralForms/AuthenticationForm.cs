using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SocketPlan.WinUI
{
    public partial class AuthenticationForm : Form
    {
        public AuthenticationForm()
        {
            InitializeComponent();

            this.approvalControl.SetButtonText("&OK");
            this.approvalControl.NeedMessage = false;
        }

        private void approvalControl_Approved(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }
    }
}
