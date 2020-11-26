using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketPlan.WebService
{
    public partial class SocketBoxPatternColor
    {
        public static List<SocketBoxPatternColor> Get(int patternId)
        {
            var sql = @"
            SELECT * FROM SocketBoxPatternColors
            WHERE
                PatternId = " + patternId.ToString();

            var db = SocketBoxPatternColor.GetDatabase();
            return db.ExecuteQuery<SocketBoxPatternColor>(sql);
        }

        public static void Delete(int patternId)
        {
            var sql = @"
            DELETE FROM SocketBoxPatternColors
            WHERE PatternId = " + patternId;

            var db = SocketBoxPatternColor.GetDatabase();
            db.ExecuteNonQuery(sql);
        }

        public static void DeleteByCategory(int categoryId)
        {
            var sql = @"
            DELETE FROM SocketBoxPatternColors
            FROM SocketBoxPatternColors C
            JOIN SocketBoxPatterns P ON C.PatternId = P.Id AND
                P.CategoryId = " + categoryId;

            var db = SocketBoxPatternDetail.GetDatabase();
            db.ExecuteNonQuery(sql);
        }
    }
}
