using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SocketPlan.WinUI.SocketPlanServiceReference
{
    public partial class RimokonNicheDetail
    {
        [XmlIgnore]
        public Symbol Symbol { get; set; }
    }
}
