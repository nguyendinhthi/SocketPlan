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
    public partial class UsableRohmOfLightSerial
    {
        /// <summary>
        /// 指定した工事コードのデータを取得する
        /// </summary>
        /// <param name="constructionCode"></param>
        /// <returns></returns>
        public static List<UsableRohmOfLightSerial> Get(string constructionCode)
        {
            var db = UsableLightSerial.GetDatabase();

            var list = db.ExecuteQuery<UsableRohmOfLightSerial>("SELECT * FROM UsableRohmOfLightSerials WHERE ConstructionCode = '" + constructionCode + "'");

            if (list.Count == 0)
                return new List<UsableRohmOfLightSerial>();
            else
                return list;
        }
    }
}
