using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SocketPlan.WinUI.SocketPlanServiceReference;

namespace SocketPlan.WinUI
{
    public partial class TextSelectForm : TextSelectBaseForm
    {
        #region シングルトン

        private static TextSelectForm instance;
        public static TextSelectForm Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new TextSelectForm();

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

        protected TextSelectForm() : base()
        {
            InitializeComponent();
        }

        private void TextSelectForm_Deactivate(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void TextSelectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void TextSelectForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Hide();
        }

        protected override void OnSelectText()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                var items = this.textListView.SelectedItems;
                if (items.Count == 0)
                    return;

                var comment = items[0].Tag as Comment;

                this.Hide();

                TextDrawer.DrawTexts(comment);

                this.Show();
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
