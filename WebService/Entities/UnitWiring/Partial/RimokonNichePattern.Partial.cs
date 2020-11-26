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
    public partial class RimokonNichePattern
    {
        public static RimokonNichePattern Get(
            bool isJikugumi, string side, int interphoneCount, int mrCount, int switchCount, int rimokonCount, bool hasMT81_83)
        {
            var method = string.Empty;
            if (isJikugumi)
                method = "Jiku";
            else
                method = "Waku";

            var mt81_83 = string.Empty;
            if (hasMT81_83)
                mt81_83 = "1";
            else
                mt81_83 = "0";

            var sql = @"
            SELECT *
            FROM RimokonNichePatterns
            WHERE
                DeletedDateTime IS NULL AND
                Method = '" + method + @"' AND
                Side = '" + side + @"' AND
                InterphoneCount = " + interphoneCount + @" AND
                MRCount = " + mrCount + @" AND
                SwitchCount = " + switchCount + @" AND
                RimokonCount = " + rimokonCount + @" AND
                HasMT81_83 = " + mt81_83;

            var db = RimokonNichePattern.GetDatabase();
            var pattern = db.ExecuteQuery<RimokonNichePattern>(sql);

            if (pattern.Count == 0)
                return null;

            return pattern[0];
        }
    }
}
