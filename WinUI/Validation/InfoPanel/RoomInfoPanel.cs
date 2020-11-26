using System;

namespace SocketPlan.WinUI
{
    public partial class RoomInfoPanel : InfoPanelBase
    {
        #region コンストラクタ

        public RoomInfoPanel() : base()
        {
            InitializeComponent();
        }

        public RoomInfoPanel(RoomObject roomOutline) : this()
        {
            this.floor = roomOutline.Floor;
            this.location = roomOutline.CenterPoint;

            this.titleValueLabel.Text = roomOutline.Name;
            this.floorValueLabel.Text = roomOutline.Floor.ToString();
            this.locationValueLabel.Text = roomOutline.CenterPoint.X.ToString("0.00") + ", " + roomOutline.CenterPoint.Y.ToString("0.00");
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
