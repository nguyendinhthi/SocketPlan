using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketPlan.WebService
{
    public partial class SocketBoxPickingItem
    {
        public static void Delete(string constructionCode)
        {
            var construction = constructionCode.Replace("'", "''");

            var sql = @"
            DELETE FROM SocketBoxPickingItems
            WHERE
                ConstructionCode = '" + construction + @"'";

            var db = SocketBoxPickingItem.GetDatabase();
            db.ExecuteNonQuery(sql);
        }
    }
}
