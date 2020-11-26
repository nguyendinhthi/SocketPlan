using System.ComponentModel;
using System.Windows.Forms;
using System;

namespace SocketPlan.WinUI
{
    public delegate void CadEventHandler();

    public class ProgressManager
    {
        #region 定数

        private const int PROGRESS_START = 0;
        private const int PROGRESS_FINISH = 100;

        #endregion

        #region イベント

        public event CadEventHandler Processes;

        #endregion

        #region メソッド

        public void Run()
        {
            var dialog = new ProgressDialog(this.StartProgress);
            dialog.ShowDialog();
            if (dialog.DialogResult == DialogResult.Abort)
            {
                //dialog.Errorをそのままthrowすると、StackTraceが現時点の情報に上書きされるので、
                //InnerExceptionに登録して返すことにしました。
                throw new Exception(null, dialog.Error);
            }
        }

        private void StartProgress(object sender, DoWorkEventArgs e)
        {
            var bw = (BackgroundWorker)sender;

            bw.ReportProgress(PROGRESS_START, "Loading...");

            var processes = this.Processes.GetInvocationList();
            decimal progressStep = (decimal)PROGRESS_FINISH / (processes.Length + 1); //Finishedの分を+1する
            decimal percentProgress = PROGRESS_START;

            foreach (CadEventHandler process in processes)
            {
                string message = this.GetMessage(process);

                percentProgress += progressStep;
                bw.ReportProgress((int)percentProgress, message);
                process.Invoke();
            }

            bw.ReportProgress(PROGRESS_FINISH, "Finished!");
        }

        private string GetMessage(CadEventHandler handler)
        {
            var atts = (ProgressMethodAttribute[])handler.Method.GetCustomAttributes(typeof(ProgressMethodAttribute), true);
            if (atts.Length == 0)
                return "Processing...";
            else
                return atts[0].Message;
        }

        #endregion
    }
}
