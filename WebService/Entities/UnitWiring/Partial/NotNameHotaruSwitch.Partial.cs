using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketPlan.WebService
{
    public partial class NotNameHotaruSwitch
    {
        public Equipment Equipment { get; set; }

        public static void DeleteAll()
        {
            var sql = @"DELETE FROM NotNameHotaruSwitches";
            NotNameHotaruSwitch.GetDatabase().ExecuteNonQuery(sql);
        }
    }
}
