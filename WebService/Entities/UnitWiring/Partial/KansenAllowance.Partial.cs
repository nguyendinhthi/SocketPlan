using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class KansenAllowance
    {
        public static KansenAllowance GetAllowance(string kind, string kanabakari, bool isAbove)
        {
            string sql = @"
                SELECT * FROM KansenAllowances
                WHERE
                    BundenbanKind = '" + kind + @"' AND
                    Kanabakari = '" + kanabakari + @"' AND
                    IsAboveBeam = " + ((isAbove == true) ? 1 : 0);

            var db = KansenAllowance.GetDatabase();
            if (db.ExecuteQuery<KansenAllowance>(sql).Count > 0)
                return db.ExecuteQuery<KansenAllowance>(sql)[0];
            else
                return new KansenAllowance();
        }
    }
}
