using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SocketPlan.WinUI
{
    public partial class CommandBaseForm : Form
    {
        public int ColumnCount { get; set; }
        public int ButtonHeight { get; set; }
        public int ButtonWidth { get; set; }

        public CommandBaseForm()
        {
            InitializeComponent();

            //既定では、一段に6個、48x48のボタンを並べる。
            this.ColumnCount = 6;
            this.ButtonHeight = 48;
            this.ButtonWidth = 48;
        }

        private void CommandBaseForm_Load(object sender, EventArgs e)
        {
            this.AdjustFormSize();
        }

        private void CommandBaseForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Hide();
        }

        private void CommandBaseForm_Deactivate(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void Show(Form owner, Button pressedButton)
        {
            Point location = pressedButton.PointToScreen(pressedButton.ClientRectangle.Location);

            if (owner.Size.Width > owner.Size.Height)
                location.Y += pressedButton.Size.Height;
            else
                location.X += pressedButton.Size.Width;

            this.Location = location;
            base.Show(owner);
        }

        protected virtual void AdjustFormSize()
        {
            int height = Convert.ToInt32(decimal.Ceiling((decimal)this.panel.Controls.Count / this.ColumnCount)) * ButtonHeight;
            int width;
            if (this.panel.Controls.Count >= this.ColumnCount)
                width = this.ColumnCount * ButtonWidth;
            else
                width = this.panel.Controls.Count * this.ButtonWidth;

            this.Size = new Size(width, height);
        }
    }

    //FlowLayoutPanelを継承すると、継承先でFlowLayoutPanelのプロパティを変更できないというバグがある。
    //http://dobon.net/vb/dotnet/control/tlinheritable.html
    //以下のクラスを使うことでバグを回避できる。
    [Designer("System.Windows.Forms.Design.PanelDesigner, System.Design")]
    public class InheritableFlowLayoutPanel : FlowLayoutPanel
    {
        public InheritableFlowLayoutPanel()
        {
        }
    }
}
