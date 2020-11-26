using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketPlan.WebService
{
    public partial class SocketBoxSpecificRelation
    {
        public static void Delete(int specificId)
        {
            var sql = @"
            DELETE FROM SocketBoxSpecificRelations
            WHERE
                SocketBoxSpecificId = " + specificId;

            var db = SocketBoxSpecificRelation.GetDatabase();
            db.ExecuteNonQuery(sql);
        }

        public static void DeleteIncludeRelated(int specificId)
        {
            var sql = @"
            DELETE FROM SocketBoxSpecificRelations
            WHERE
                SocketBoxSpecificId = " + specificId + @" OR
                RelatedSpecificId = " + specificId;

            var db = SocketBoxSpecificRelation.GetDatabase();
            db.ExecuteNonQuery(sql);
        }
    }
}
