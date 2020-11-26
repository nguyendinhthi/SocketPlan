using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SocketPlan.WinUI
{
    public partial class Command0Form : CommandBaseForm
    {
        #region シングルトン

        private static Command0Form instance;
        public static Command0Form Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new Command0Form();

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

        private Command0Form()
        {
            InitializeComponent();
        }

        private void ControlEnabled(bool enable)
        {
            this.saveToLocalButton.Enabled = enable;
            this.saveToServerButton.Enabled = enable;
            this.closeButton.Enabled = enable;
            this.plotAndSaveDWGButton.Enabled = enable;
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new OpenDrawingForm();
                if (form.ShowDialog() == DialogResult.Cancel)
                    return;
                this.ControlEnabled(true);
                this.Owner.Activate();
            }
            catch (Exception ex)
            {
                MessageDialog.ShowError(ex);
            }
        }

        private void saveToLocalButton_Click(object sender, EventArgs e)
        {
            //Command0_2Form.Instance.Show(this.Owner, ((MainForm)this.Owner).command0Button);

            try
            {
                this.Cursor = Cursors.WaitCursor;

                var count = Drawing.SaveAll(Properties.Settings.Default.DrawingDirectory);

                this.Owner.Activate();
                MessageDialog.ShowInformation(this, Messages.FileSaved(count));
            }
            catch (Exception ex)
            {
                MessageDialog.ShowError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void saveToServerButton_Click(object sender, EventArgs e)
        {
            //Command0_2Form.Instance.Show(this.Owner, ((MainForm)this.Owner).command0Button);

            try
            {
                this.Cursor = Cursors.WaitCursor;

                int count;

                if (Static.Drawing.Location == "Local")
                    count = Drawing.SaveAll(Paths.GetServerDrawingDirectories()[0]);
                else
                    count = Drawing.SaveAll(System.IO.Directory.GetParent(Static.Drawing.Directory).FullName);

                this.Owner.Activate();
                MessageDialog.ShowInformation(this, Messages.FileSaved(count));
            }
            catch (Exception ex)
            {
                MessageDialog.ShowError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public DialogResult CloseDrawings()
        {
            var result = MessageDialog.ShowYesNoCancel(Messages.FileClosing());
            if (result == DialogResult.Cancel)
                return result;

            try
            {
                if (result == DialogResult.Yes)
                    Drawing.SaveAll(Properties.Settings.Default.DrawingDirectory);

                Drawing.CloseAll();

                Static.ConstructionCode = null;
                Static.Drawing = null;
                Static.HouseSpecs = null;
                Static.ConstructionTypeCode = null;
                Static.Schedule = null;
                this.ControlEnabled(false);
                this.Owner.Activate();
            }
            catch (Exception ex)
            {
                MessageDialog.ShowError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return result;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.CloseDrawings();
        }

        private void plotAndSaveDWGButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                new SaveAndPlot().Run();

                this.Cursor = Cursors.Default;
                MessageDialog.ShowInformation(this, Messages.Finished());
            }
            catch (Exception ex)
            {
                MessageDialog.ShowError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
