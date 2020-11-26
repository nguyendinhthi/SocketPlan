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

namespace SocketPlan.WebService
{
    public partial class SpcadCLInfo
    {
        public static int GetPowerConditionerCount(string constructionCode)
        {
            var info = SpcadCLInfo.Get(constructionCode);
            if(info == null)
                return 0;

            var count = 0;

            if (!string.IsNullOrEmpty(info.PC1ModelNo))
                count++;

            if (!string.IsNullOrEmpty(info.PC2ModelNo))
                count++;

            if (!string.IsNullOrEmpty(info.PC3ModelNo))
                count++;

            if (!string.IsNullOrEmpty(info.PC4ModelNo))
                count++;

            if (!string.IsNullOrEmpty(info.PC5ModelNo))
                count++;

            return count;
        }
    }
}
