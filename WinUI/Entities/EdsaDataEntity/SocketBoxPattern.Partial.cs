using System;
using System.Collections.Generic;
using System.Text;

namespace SocketPlan.WinUI.SocketPlanServiceReference
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

                return new List<SocketBoxPatternDetail>(this.Details);
            }
            set { this.Details = value.ToArray(); }
        }

        public List<SocketBoxPatternColor> ColorsList
        {
            get
            {
                if (this.Colors == null)
                    return new List<SocketBoxPatternColor>();

                return new List<SocketBoxPatternColor>(this.Colors);
            }
            set { this.Colors = value.ToArray(); }
        }
    }
}
