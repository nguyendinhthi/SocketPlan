using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SocketPlan.WinUI
{
    public partial class SwitchSizeEntryPanel : UserControl
    {
        private Symbol symbol = null;
        public int rowIdx = -1;
        public int colIdx = -1;

        public Switch Switch
        {
            get
            {
                var sw = new Switch();
                sw.Symbol = this.symbol;
                sw.SwitchType = (SwitchType)this.typeCombo.SelectedItem;
                sw.Vertical = Convert.ToInt32(this.verticalSizeCombo.SelectedItem);
                return sw;
            }
        }

        public SwitchSizeEntryPanel()
        {
            InitializeComponent();
        }

        public SwitchSizeEntryPanel(Symbol symbol)
            : this()
        {
            this.SetCombo();

            this.symbol = symbol;
            this.UpdateSymbolInfo(symbol);
        }

        public SwitchSizeEntryPanel(Symbol symbol, int row, int col)
            : this()
        {
            this.SetCombo();

            this.rowIdx = row;
            this.colIdx = col;
            this.symbol = symbol;
            this.UpdateSymbolInfo(symbol);
        }

        private void SetCombo()
        {
            this.typeCombo.DataSource = Enum.GetValues(typeof(SwitchType));
            this.typeCombo.SelectedIndex = 0;
            this.verticalSizeCombo.SelectedIndex = 0;
        }

        private void UpdateSymbolInfo(Symbol symbol)
        {
            this.infoPanel.Controls.Clear();
            this.infoPanel.Controls.Add(new SymbolInfoPanel(symbol));
        }

        public void SetType(SwitchType type)
        {
            this.typeCombo.SelectedItem = type;
        }

        public void SetVerticalSize(int size)
        {
            if(!this.verticalSizeCombo.Items.Contains(size.ToString()))
                return;

            this.verticalSizeCombo.SelectedItem = size.ToString();
        }

        public new string GetType()
        {
            return this.typeCombo.SelectedItem.ToString();
        }

        public int GetVerticalSize()
        {
            return Int32.Parse(this.verticalSizeCombo.SelectedItem.ToString() == string.Empty ? "0" : this.verticalSizeCombo.SelectedItem.ToString());
        }

    }
}
