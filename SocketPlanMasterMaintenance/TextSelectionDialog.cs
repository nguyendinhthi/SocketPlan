using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRD.CHOPPER.WinUI;
using System.Windows.Forms;
using HRD.CHOPPER.WinUI.UnitWiringServiceReference;

namespace SocketPlanMasterMaintenance
{
    public class TextSelectionDialog : TextSelectForm
    {
        public static TextSelectionDialog instance;
        public static new TextSelectionDialog Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new TextSelectionDialog();

                return instance;
            }
        }

        public static Comment SelectedComment { get; set; }

        private TextSelectionDialog()
            : base()
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SelectedComment = null;
        }

        protected override void OnSelectText()
        {
            try
            {
                var texts = this.textListView.SelectedItems;
                if (texts.Count == 0)
                    return;

                TextSelectionDialog.SelectedComment = texts[0].Tag as Comment;
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageDialog.ShowError(ex);
            }
        }
    }
}
