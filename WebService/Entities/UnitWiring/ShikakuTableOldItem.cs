using System.Collections.Generic;

namespace SocketPlan.WebService
{
    public class ShikakuTableOldItem
    {
        public static List<ShikakuTableItem> GetAll()
        {
            var db = ShikakuTableItem.GetDatabase();
            return db.ExecuteQuery<ShikakuTableItem>("SELECT * FROM ShikakuTableOldItems");
        }
    }
}
