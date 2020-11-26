using System;
using System.Windows.Forms;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public partial class MaintenanceForm : CommandBaseForm
    {
        #region シングルトン

        private static MaintenanceForm instance;
        public static MaintenanceForm Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new MaintenanceForm();

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

        private MaintenanceForm()
        {
            InitializeComponent();

            this.ColumnCount = 1;
            this.ButtonHeight = this.composeButton.Height;
            this.ButtonWidth = this.composeButton.Width;
        }

        private void composeButton_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new AuthenticationForm();
                if (form.ShowDialog(this) != DialogResult.OK)
                    return;

                var master = new SocketPlanMasterMaintenanceForm();
                master.Show(this.Owner);
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
                var form = new AuthenticationForm();
                if (form.ShowDialog(this) != DialogResult.OK)
                    return;

                var master = new SocketSpecificMasterMaintenanceForm();
                master.Show(this.Owner);
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

        private void singleButton_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new AuthenticationForm();
                if (form.ShowDialog(this) != DialogResult.OK)
                    return;

                var master = new SingleSymbolMasterMaintenanceForm();
                master.Show(this.Owner);
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

        private void exempleButton_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new AuthenticationForm();
                if (form.ShowDialog(this) != DialogResult.OK)
                    return;

                var master = new SubeteExemptionMasterMaintenanceForm();
                master.Show(this.Owner);
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
