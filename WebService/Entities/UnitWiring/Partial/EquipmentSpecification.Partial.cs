using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class EquipmentSpecification
    {
        public static void Delete(int equipmentId)
        {
            var db = EquipmentSpecification.GetDatabase();
            db.ExecuteNonQuery("DELETE FROM EquipmentSpecifications WHERE EquipmentId = " + equipmentId);
        }

        public static List<Specification> Get(int equipmentId)
        {
            var sql = @"
            SELECT S.*
            FROM Specifications S
            JOIN EquipmentSpecifications ES ON
                S.Id = ES.SpecificationId AND
                ES.EquipmentId = " + equipmentId;

            var db = Specification.GetDatabase();
            return db.ExecuteQuery<Specification>(sql);
        }
    }
}
