using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketPlan.WebService
{
    public partial class NotNameHotaruSwitchSerial
    {
        public static void DeleteAll()
        {
            var sql = @"DELETE FROM NotNameHotaruSwitchSerials";
            NotNameHotaruSwitchSerial.GetDatabase().ExecuteNonQuery(sql);
        }
    }
}
