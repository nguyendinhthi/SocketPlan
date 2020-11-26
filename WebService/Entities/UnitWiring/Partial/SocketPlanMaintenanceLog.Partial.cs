using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketPlan.WebService
{
    public partial class SocketPlanMaintenanceLog
    {
        public static int GetNewId()
        {
            var sql = @"SELECT MAX(Id) FROM SocketPlanMaintenanceLogs";
            var db = SocketBoxPattern.GetDatabase();
            var obj = db.ExecuteScalar(sql);

            if (obj == null || obj == DBNull.Value)
                return 1;

            return (int)obj + 1;
        }
    }
}
