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
using System.Collections.Generic;

namespace SocketPlan.WebService
{
    public partial class OptionPickingItemsEx
    {
        public static void Update(List<OptionPickingItemsEx> items, string constructionCode)
        {
            OptionPickingItemsEx.Delete(constructionCode);

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
        public static List<OptionPickingItemsEx> Get(string constructionCode)
        {
            string sql = @"
                SELECT * FROM OptionPickingItemsEx
                WHERE
                    ConstructionCode = '" + constructionCode + "'";

            var db = OptionPickingItemsEx.GetDatabase();
            return db.ExecuteQuery<OptionPickingItemsEx>(sql);
        }

        public static void Delete(string constructionCode)
        {
            string sql = @"
                DELETE FROM OptionPickingItemsEx
                WHERE
                    ConstructionCode = '" + constructionCode + "'";

            var db = OptionPickingItem.GetDatabase();
            db.ExecuteNonQuery(sql);
        }
    }
}