using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class Text
    {
        public static void Delete(int equipmentId)
        {
            var db = Text.GetDatabase();
            db.ExecuteNonQuery("DELETE FROM Texts WHERE EquipmentId = " + equipmentId);
        }
    }
}
