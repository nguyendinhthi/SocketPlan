using System;
using System.Collections.Generic;
using System.Text;

namespace SocketPlan.WinUI.SocketPlanServiceReference
{
    public partial class PartsColor
    {
        public string NameWithPrefix
        {
            get
            {
                return this.Prefix + ":" + this.Name;
            }
        }
    }
}
