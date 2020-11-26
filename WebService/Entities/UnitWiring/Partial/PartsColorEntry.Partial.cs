using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class PartsColorEntry
    {
        public static List<PartsColorEntry> Get(string constructionCode)
        {
            string sql = @"
                SELECT * FROM PartsColorEntries
                WHERE
                    ConstructionCode = '" + constructionCode + @"'
                ORDER BY
                    ConstructionCode,
                    Seq";

            var db = PartsColorEntry.GetDatabase();
            return db.ExecuteQuery<PartsColorEntry>(sql);
        }

        public static void Delete(string constructionCode)
        {
            string sql = @"
                DELETE FROM PartsColorEntries
                WHERE
                    ConstructionCode = '" + constructionCode + "'";

            var db = PartsColorEntry.GetDatabase();
            db.ExecuteNonQuery(sql);
        }

    }
}
