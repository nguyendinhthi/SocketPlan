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
    public partial class House
    {
        public static string GetConstructionTypeCode(string constructionCode, string planNo)
        {
            if(ConstructionSchedule.IsBeforeProcessRequest(constructionCode))
            {
                var kanri = tbl_siyo_kanri.Get(constructionCode, planNo);
                if (kanri == null)
                    return null;

                return kanri.typeCd;
            }
            else
            {
                var house = House.Get(constructionCode);
                if (house == null)
                    return null;

                return house.ConstructionTypeCode;
            }
        }
    }
}
