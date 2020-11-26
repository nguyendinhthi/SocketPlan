using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketPlan.WebService
{
    public partial class JboxColorEntry
    {
        public Equipment Equipment { get; set; }

        public static List<JboxColorEntry> Get(string constructionCode, string orderColumn)
        {
            string sql = @"
                SELECT * FROM JboxColorEntries
                WHERE
                    ConstructionCode = '" + constructionCode + @"'
                ORDER BY " + orderColumn;                                     

            var db = JboxColorEntry.GetDatabase();
            return db.ExecuteQuery<JboxColorEntry>(sql);
        }

        public static void Delete(string constructionCode)
        {
            string sql = @"
                DELETE FROM JboxColorEntries
                WHERE
                    ConstructionCode = '" + constructionCode + "'";

            var db = JboxColorEntry.GetDatabase();
            db.ExecuteNonQuery(sql);
        }
    }
}
