using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SocketPlan.WinUI
{
    public partial class ProgressDialog : Form
    {
        private object workerArgument = null;

        private Exception error = null;
        public Exception Error { get { return this.error; } }

        public ProgressDialog()
        {
            InitializeComponent();
        }

        public ProgressDialog(DoWorkEventHandler doWork, params object[] arguments) : this()
        {
            this.backgroundWorker.DoWork += doWork;

            if (arguments.Length == 0)
                this.workerArgument = null;
            else if (arguments.Length == 1)
                this.workerArgument = arguments[0];
            else
                this.workerArgument = new List<object>(arguments);
        }

        private void ProgressDialog_Shown(object sender, EventArgs e)
        {
            this.backgroundWorker.RunWorkerAsync(this.workerArgument);
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage < this.progressBar.Minimum)
                this.progressBar.Value = this.progressBar.Minimum;
            else if (this.progressBar.Maximum < e.ProgressPercentage)
                this.progressBar.Value = this.progressBar.Maximum;
            else
                this.progressBar.Value = e.ProgressPercentage;

            this.Text = "[" + this.progressBar.Value + "%] " + (string)e.UserState;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.error = e.Error;
                this.DialogResult = DialogResult.Abort;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }

            this.Close();
        }
    }
}
