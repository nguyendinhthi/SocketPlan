using System;
using System.Windows.Forms;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public partial class InfoPanelBase : UserControl
    {
        #region フィールド変数(プロパティ用を除く)

        protected int floor = 0;
        protected PointD location = null;

        #endregion

        #region コンストラクタ

        public InfoPanelBase()
        {
            InitializeComponent();
        }

        #endregion

        #region イベントハンドラ

        private void findButton_Click(object sender, EventArgs e)
        {
            if (this.location == null)
                return;

            var drawings = Drawing.GetAll(true);
            var drawing = drawings.Find(p => p.Floor == this.floor);
            if (drawing == null)
                return;

            drawing.Focus();

            AutoCad.Command.Pan(this.location);
        }

        #endregion
    }
}
