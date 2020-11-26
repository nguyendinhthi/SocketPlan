using System;
using System.Collections.Generic;
using System.Text;

namespace SocketPlan.WinUI.SocketPlanServiceReference
{
    public partial class SocketBoxSpecific
    {
        public int SymbolObjectId = 0;
        public int SocketBlockId = 0;
        private Symbol _symbol;
        public Symbol Symbol
        {
            get 
            {
                return this._symbol;
            }
            set 
            {
                this._symbol = value;
            }
        }
    }
}
