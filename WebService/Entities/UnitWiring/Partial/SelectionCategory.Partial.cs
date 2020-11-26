using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class SelectionCategory
    {
        private List<Equipment> equipments = new List<Equipment>();
        public List<Equipment> Equipments
        {
            get { return this.equipments; }
            set { this.equipments = value; }
        }

        public static SelectionCategory GetSelectionCategory(int equipmentId)
        {
            var sql = @"
            SELECT S.*
            FROM SelectionCategories S
            JOIN SelectionCategoryDetails SD ON
                S.Id = SD.SelectionCategoryId AND
                SD.EquipmentId = " + equipmentId;

            var db = SelectionCategory.GetDatabase();
            var list = db.ExecuteQuery<SelectionCategory>(sql);

            if (list.Count == 0)
                return null;
            else
                return list[0];
        }
    }
}
