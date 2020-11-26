using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace SocketPlan.WebService
{
    public partial class LightSerialCategory
    {
        private List<LightSerial> lightSerials = new List<LightSerial>();
        public List<LightSerial> LightSerials
        {
            get { return this.lightSerials; }
            set { this.lightSerials = value; }
        }
    }
}
