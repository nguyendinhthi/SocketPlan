using System;
using System.Collections.Generic;
using System.Text;

namespace SocketPlan.WinUI.SocketPlanServiceReference
{
    public partial class SingleSocketBoxEquipment
    {
        public string EquipmentName
        {
            get
            {
                if (this.Equipment == null)
                    return string.Empty;

                return this.Equipment.Name;
            }
        }
    }
}
