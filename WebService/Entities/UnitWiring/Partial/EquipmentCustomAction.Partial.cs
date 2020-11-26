using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class EquipmentCustomAction
    {
        public static void Delete(int equipmentId)
        {
            var db = EquipmentCustomAction.GetDatabase();
            db.ExecuteNonQuery("DELETE FROM EquipmentCustomActions WHERE EquipmentId = " + equipmentId);
        }
    }
}
