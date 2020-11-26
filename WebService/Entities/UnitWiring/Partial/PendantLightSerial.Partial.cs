using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class PendantLightSerial
    {
        public static void Delete()
        {
            string sql = string.Empty;
            sql = "Delete From PendantLightSerials";

            PendantLightSerial.GetDatabase().ExecuteNonQuery(sql);
        }
    }
}
