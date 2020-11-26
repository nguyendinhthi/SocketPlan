using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public partial class EditForm : CommandBaseForm
    {
        #region シングルトン

        private static EditForm instance;
        public static EditForm Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new EditForm();

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

        private EditForm()
        {
            InitializeComponent();

            this.ColumnCount = 1;
            this.ButtonHeight = this.moveButton.Height;
            this.ButtonWidth = this.moveButton.Width;
        }

        private void moveButton_Click(object sender, EventArgs e)
        {
            try
            {
                SocketBoxObject.MoveLocation();
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

        private void changeColorButton_Click(object sender, EventArgs e)
        {
            try
            {
                SocketBoxObject.ChangeColor();
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

        private void manualComposeButton_Click(object sender, EventArgs e)
        {
            try
            {
                ManualComposeForm.Instance.Show(this);
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

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                SocketBoxObject.Delete();
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

        private void ShowHideUnnecessaryItemsButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.drawings = new List<Drawing>();
                this.drawings.Add(dwg);

                var progress = new ProgressManager();
                progress.Processes += new CadEventHandler(this.LoadCadObjectContainer);
                progress.Processes += new CadEventHandler(this.ShowHideUnnecessaryItems);
                progress.Run();
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

        public CadObjectContainer container;
        public List<Drawing> drawings;
        public bool buttonFlag = false;
        public Drawing dwg = Drawing.GetCurrent();


        [ProgressMethod("Loading CAD data...")]
        private void LoadCadObjectContainer()
        {
            this.container = new CadObjectContainer(this.drawings,
                                                   CadObjectTypes.Symbol |
                                                   CadObjectTypes.RoomOutline |
                                                   CadObjectTypes.Text);
        }

        [ProgressMethod("Processing...")]
        private void ShowHideUnnecessaryItems()
        {
            SocketPlanCreator.HideUnnecessaryItems(this.dwg, this.container, this.buttonFlag);
        }
    }
}
