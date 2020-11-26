using System;
using System.Collections.Generic;
using System.Text;

namespace SocketPlan.WinUI.SocketPlanServiceReference
{
    public partial class KansenBuzaiItem
    {
        public string GetName(bool isYojo)
        {
            if (isYojo)
                return this.BuzaiNameYojo;
            else
                return this.BuzaiName;
        }
    }
}
