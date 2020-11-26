using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SocketPlan.WinUI.SocketPlanServiceReference;

namespace SocketPlan.WinUI
{
    public partial class SelectableTextBox : UserControl
    {
        public bool ReadOnly { get; set; }
        public TextSelectForAttributeForm TextSelectForm { get; set; }

        public event EventHandler ButtonClickAfter;

        public SelectableTextBox()
        {
            InitializeComponent();

            this.valueText.DataBindings.Add(new Binding("Text", this, "Text"));
            this.valueText.DataBindings.Add(new Binding("ReadOnly", this, "ReadOnly"));
        }

        private void referenceButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.TextSelectForm.ShowDialog(this) == DialogResult.Cancel)
                    return;

                this.Text = this.TextSelectForm.Comment.Text;

                this.ButtonClickAfter(sender, e);
            }
            catch (Exception ex)
            {
                MessageDialog.ShowError(ex);
            }
        }
    }
}
