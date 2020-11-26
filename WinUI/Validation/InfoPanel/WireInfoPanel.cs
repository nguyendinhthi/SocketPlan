using System;

namespace SocketPlan.WinUI
{
    public partial class WireInfoPanel : InfoPanelBase
    {
        #region コンストラクタ

        public WireInfoPanel() : base()
        {
            InitializeComponent();
        }

        public WireInfoPanel(Wire wire) : this()
        {
            this.floor = wire.Floor;
            this.location = wire.StartPoint;

            this.titleValueLabel.Text = string.Empty;

            this.floorValueLabel.Text = wire.Floor.ToString();
            this.startLocationValueLabel.Text = wire.StartPoint.X.ToString("0.00") + ", " + wire.StartPoint.Y.ToString("0.00");
            this.endLocationValueLabel.Text = wire.EndPoint.X.ToString("0.00") + ", " + wire.EndPoint.Y.ToString("0.00");
        }

        public WireInfoPanel(Wire wire, RisingWire risingWire) : this(wire)
        {
            this.titleValueLabel.Text = "Link to [" + risingWire.FloorLinkCode + "]";
        }

        #endregion

        #region publicメソッド

        public override string ToString()
        {
            var NL = Environment.NewLine;

            var text = string.Empty;
            text += this.titleLabel.Text + ":" + this.titleValueLabel.Text + NL;
            text += this.floorLabel.Text + this.floorValueLabel.Text + NL;
            text += this.startLabel.Text + this.startLocationValueLabel.Text + NL;
            text += this.endLabel.Text + this.endLocationValueLabel.Text;

            return text;
        }

        #endregion
    }
}
