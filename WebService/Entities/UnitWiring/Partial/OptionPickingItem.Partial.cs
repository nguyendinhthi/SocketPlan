using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class OptionPickingItem
    {
        public static void Update(List<OptionPickingItem> items, string constructionCode)
        {
            OptionPickingItem.Delete(constructionCode);

            var now = DateTime.Now;
            int seq = 1;

            foreach (var item in items)
            {
                item.Seq = seq;
                item.UpdatedDateTime = now;
                
                item.Store();

                seq++;
            }
        }

        public static void Delete(string constructionCode)
        {
            string sql = @"
                DELETE FROM OptionPickingItems
                WHERE
                    ConstructionCode = '" + constructionCode + "'";

            var db = OptionPickingItem.GetDatabase();
            db.ExecuteNonQuery(sql);    
        }
    }
}
