using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlanMasterMaintenance.SocketPlanServiceReference
{
    public partial class SocketBoxCategory
    {
        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(this.Name))
                    return "None";

                return this.Name;
            }
        }
    }
}
