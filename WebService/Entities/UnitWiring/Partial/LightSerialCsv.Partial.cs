using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketPlan.WebService
{
    public partial class LightSerialCsv
    {
        public static void Delete(string constructionCode)
        {
            string sql = @"
                DELETE FROM LightSerialCsvs
                WHERE
                    ConstructionCode = '" + constructionCode + "'";

            var db = LightSerialCsv.GetDatabase();
            db.ExecuteNonQuery(sql);
        }
    }
}
