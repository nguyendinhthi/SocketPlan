using System.Collections.Generic;

namespace SocketPlan.WebService
{
    public class ShikakuTableNewIPaletteItem
    {
        /// <summary>
        /// ShikakuTableNewIPaletteItemsテーブルの情報を全て取得する。
        /// </summary>
        /// <returns></returns>
        public static List<ShikakuTableItem> GetAll()
        {
            var db = ShikakuTableItem.GetDatabase();
            return db.ExecuteQuery<ShikakuTableItem>("SELECT * FROM ShikakuTableNewIPaletteItems");
        }
    }
}
