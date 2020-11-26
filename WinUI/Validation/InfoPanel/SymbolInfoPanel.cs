using System;

namespace SocketPlan.WinUI
{
    public partial class SymbolInfoPanel : InfoPanelBase
    {
        #region コンストラクタ

        private SymbolInfoPanel() : base()
        {
            InitializeComponent();
        }

        public SymbolInfoPanel(Symbol symbol) : this()
        {
            this.floor = symbol.Floor;
            this.location = symbol.Position;

            this.titleValueLabel.Text = symbol.BlockName;
            this.floorValueLabel.Text = symbol.Floor.ToString();
            this.locationValueLabel.Text = symbol.Position.X.ToString("0.00") + ", " + symbol.Position.Y.ToString("0.00");
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
