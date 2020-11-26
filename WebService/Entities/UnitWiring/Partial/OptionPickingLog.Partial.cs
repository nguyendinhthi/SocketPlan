using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class OptionPickingLog
    {
        public static OptionPickingLog Get(string constructionCode)
        {
            string sql = @"SELECT TOP 1 *
              FROM OptionPickingLogs
              WHERE ConstructionCode = '" + constructionCode + @"' ORDER BY id DESC";

            var db = OptionPickingLog.GetDatabase();
            var list = db.ExecuteQuery<OptionPickingLog>(sql);
            if (list.Count == 0)
                return null;
            else
                return list[0];    
       }
    }
}
