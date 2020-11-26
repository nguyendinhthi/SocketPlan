using System;
using System.Windows.Forms;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public partial class FramingForm : CommandBaseForm
    {
        #region シングルトン

        private static FramingForm instance;
        public static FramingForm Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new FramingForm();

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

        private FramingForm()
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
                var framing = new Framing(0, SocketPlanType.Individual);
                framing.Run();
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
                var framing = new Framing(0, SocketPlanType.Pattern);
                framing.Run();
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
