using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class KansenBuzaiItem
    {
        public static string GetName(string buzaiCode)
        {
            string sql = @"
                SELECT BuzaiName FROM KansenBuzaiItems
                WHERE
                    BuzaiCode = '" + buzaiCode + "'";

            var db = KansenBuzaiItem.GetDatabase();
            return db.ExecuteScalar(sql).ToString();
        }
    }
}
