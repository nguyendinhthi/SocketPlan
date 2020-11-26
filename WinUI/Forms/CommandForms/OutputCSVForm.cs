using System;
using System.Windows.Forms;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public partial class OutputCSVForm : CommandBaseForm
    {
        #region シングルトン

        private static OutputCSVForm instance;
        public static OutputCSVForm Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new OutputCSVForm();

                return instance;
            }
        }

        public static void DisposeInstance()
        {
            if (instance == null)
                return;

            instance.Dispose();
            instance = null;
        }

        #endregion

        private OutputCSVForm()
        {
            InitializeComponent();

            this.ColumnCount = 1;
            this.ButtonHeight = this.individualButton.Height;
            this.ButtonWidth = this.individualButton.Width;
        }

        private void individualButton_Click(object sender, EventArgs e)
        {
            try
            {
                var exporter = new Exporter();
                exporter.Run();

                MessageDialog.ShowInformation(this, Messages.Finished());
            }
            catch (Exception ex)
            {
                this.Owner.Show();
                this.Owner.Activate();

                MessageDialog.ShowError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void patternButton_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO Output Pattern CSV

                MessageDialog.ShowInformation(this, Messages.Finished());
            }
            catch (Exception ex)
            {
                this.Owner.Show();
                this.Owner.Activate();

                MessageDialog.ShowError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
