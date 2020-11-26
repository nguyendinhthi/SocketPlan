using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlanMasterMaintenance.SocketPlanServiceReference
{
    public partial class SocketBoxPattern
    {
        public string NodeName
        {
            get
            {
                if (string.IsNullOrEmpty(this.Name))
                    return "No Name";

                return this.Name;
            }
        }

        public List<SocketBoxPatternDetail> DetailsList
        {
            get
            {
                if (this.Details == null)
                    return new List<SocketBoxPatternDetail>();

                return this.Details.ToList();
            }
            set { this.Details = value.ToArray(); }
        }

        public List<SocketBoxPatternColor> ColorsList
        {
            get
            {
                if (this.Colors == null)
                    return new List<SocketBoxPatternColor>();

                return this.Colors.ToList();
            }
            set { this.Colors = value.ToArray(); }
        }
    }
}
