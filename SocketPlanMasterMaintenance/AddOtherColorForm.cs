using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HRD.CHOPPER.WinUI;
using SocketPlanMasterMaintenance.SocketPlanServiceReference;

namespace SocketPlanMasterMaintenance
{
    public partial class AddOtherColorForm : Form
    {
        //private bool isIndividual;
        private int patternId;
        private List<SocketBoxPatternColor> colors;

        public SocketBoxPatternColor NewColor { get; set; }

        public AddOtherColorForm(int patternId, List<SocketBoxPatternColor> colors)
        {
            InitializeComponent();

            this.patternId = patternId;
            this.colors = colors;
        }

        private void fileButton_Click(object sender, EventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.Title = "Select DWG file of socket box";
                dialog.Filter = "DWG Files (.dwg)|*.dwg";
                dialog.InitialDirectory = @"C:\UnitWiring\Blocks\SocketBox";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                this.dwgPathText.Text = dialog.FileName;
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(this.colorNameText.Text))
                {
                    MessageDialog.ShowWarning("Please entry color naqme.");
                    return;
                }

                if(string.IsNullOrEmpty(this.dwgPathText.Text))
                {
                    MessageDialog.ShowWarning("Please entry DWG path.");
                    return;
                }

                foreach (var c in this.colors)
                {
                    if (c.ColorName == this.colorNameText.Text)
                    {
                        MessageDialog.ShowWarning("This color name is already registered.");
                        return;
                    }
                }

                var color = new SocketBoxPatternColor();
                color.PatternId = this.patternId;
                color.ColorName = this.colorNameText.Text;
                color.DwgPath = this.dwgPathText.Text;
                this.NewColor = color;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }
    }
}
