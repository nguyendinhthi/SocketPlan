namespace SocketPlan.WinUI
{
    public partial class StringInfoPanel : InfoPanelBase
    {
        #region コンストラクタ

        public StringInfoPanel() : base()
        {
            this.InitializeComponent();
        }

        public StringInfoPanel(string value) : this()
        {
            this.valueLabel.Text = value;
        }

        #endregion

        #region publicメソッド

        public override string ToString()
        {
            return this.valueLabel.Text;
        }

        #endregion
    }
}
