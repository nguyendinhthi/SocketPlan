using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class RelatedEquipment
    {
        public static void Delete(int equipmentId)
        {
            var db = RelatedEquipment.GetDatabase();
            db.ExecuteNonQuery("DELETE FROM RelatedEquipments WHERE EquipmentId = " + equipmentId);
        }

        public static void DeleteRelatedEquipment(int equipmentId)
        {
            var db = RelatedEquipment.GetDatabase();
            db.ExecuteNonQuery("DELETE FROM RelatedEquipments WHERE RelatedEquipmentId = " + equipmentId);
        }
    }
}
