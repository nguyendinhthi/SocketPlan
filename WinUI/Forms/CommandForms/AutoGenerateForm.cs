using System;
using System.Windows.Forms;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public partial class AutoGenerateForm : CommandBaseForm
    {
        #region シングルトン

        private static AutoGenerateForm instance;
        public static AutoGenerateForm Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new AutoGenerateForm();

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

        private AutoGenerateForm()
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
                var plan = new SocketPlanCreator(SocketPlanType.Individual);
                plan.Run();

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
                var plan = new SocketPlanCreator(SocketPlanType.Pattern);
                plan.Run();

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
