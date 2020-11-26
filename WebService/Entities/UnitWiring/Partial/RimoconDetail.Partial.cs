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
    public partial class RemoconDetail
    {
        public static List<RemoconDetail> Get(string constructionCode)
        {
            constructionCode = constructionCode.Replace("'", "''");

            var sql = @"SELECT * FROM RemoconDetails WHERE ConstructionCode = '" + constructionCode + "'";
            var db = RemoconDetail.GetDatabase();
            return db.ExecuteQuery<RemoconDetail>(sql);
        }

        public static void Update(List<RemoconDetail> remocons)
        {
            if (remocons.Count == 0)
                return;

            var constructionCode = remocons[0].ConstructionCode;
            var deleteSql = @"DELETE FROM RemoconDetails WHERE ConstructionCode = '" + constructionCode + "'";

            var db = RemoconDetail.GetDatabase();
            db.ExecuteNonQuery(deleteSql);

            var now = DateTime.Now;

            foreach (var remocon in remocons)
            {
                remocon.UpdatedDateTime = now;
                db.Insert<RemoconDetail>(remocon);
            }
        }
    }
}
