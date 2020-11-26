using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace SocketPlan.WebService
{
    public partial class Item
    {
        public static Item GetBySerial(string serial)
        {
            var db = Item.GetDatabase();

            var sql = @"
                SELECT Items.*
                FROM Items
                JOIN ItemStructures ON
                    ItemStructures.ParentItemCode = '6200000' AND
                    ItemStructures.ChildItemCode = Items.ItemCode AND
                    Items.ItemName2 = '" + serial + @"'";

            var items = db.ExecuteQuery<Item>(sql);
            if (items.Count == 0)
                return null;

            return items[0];
        }
    }
}
