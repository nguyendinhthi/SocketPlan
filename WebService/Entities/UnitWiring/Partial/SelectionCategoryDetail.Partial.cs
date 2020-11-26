using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class SelectionCategoryDetail
    {
        public static void Delete(int categoryId)
        {
            var db = SelectionCategoryDetail.GetDatabase();
            db.ExecuteNonQuery("DELETE FROM SelectionCategoryDetails WHERE SelectionCategoryId = " + categoryId);
        }

        public static void DeleteByEquipmentId(int equipmentId)
        {
            var db = SelectionCategoryDetail.GetDatabase();
            db.ExecuteNonQuery("DELETE FROM SelectionCategoryDetails WHERE EquipmentId = " + equipmentId);
        }

        public static int GetMaxSortNo(int categoryId)
        {
            var details = SelectionCategoryDetail.GetAll().FindAll(p => p.SelectionCategoryId == categoryId);
            if (details.Count != 0)
                return details.Max(p => p.SortNo);

            return 0;
        }

        public static List<SelectionCategoryDetail> Get(int equipmentId)
        {
            return SelectionCategoryDetail.GetAll().FindAll(p => p.EquipmentId == equipmentId);
        }

        public static List<SelectionCategoryDetail> GetSelect(int equipmentId)
        {
            var db = SelectionCategoryDetail.GetDatabase();
            string sql = @"
                SELECT * FROM SelectionCategoryDetails
                WHERE
                    EquipmentId = '" + equipmentId + @"'
                ORDER BY
                    SelectionCategoryId,
                    EquipmentId";

            return db.ExecuteQuery<SelectionCategoryDetail>(sql);
        }
    }
}
