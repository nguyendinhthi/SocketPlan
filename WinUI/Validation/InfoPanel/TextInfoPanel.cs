using System;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public partial class TextInfoPanel : InfoPanelBase
    {
        #region コンストラクタ

        private TextInfoPanel() : base()
        {
            InitializeComponent();
        }

        public TextInfoPanel(TextObject text) : this()
        {
            var pos = AutoCad.Db.Text.GetPosition(text.ObjectId);

            this.floor = text.Floor;
            this.location = pos;

            this.titleValueLabel.Text = text.Text;
            this.floorValueLabel.Text = text.Floor.ToString();
            this.locationValueLabel.Text = pos.X.ToString("0.00") + ", " + pos.Y.ToString("0.00");
        }

        #endregion

        #region publicメソッド

        public override string ToString()
        {
            var NL = Environment.NewLine;

            var text = string.Empty;
            text += this.titleLabel.Text + ":" + this.titleValueLabel.Text + NL;
            text += this.floorLabel.Text + this.floorValueLabel.Text + NL;
            text += this.locationLabel.Text + this.locationValueLabel.Text;

            return text;
        }

        #endregion
    }
}
