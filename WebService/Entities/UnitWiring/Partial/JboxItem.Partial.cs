using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketPlan.WebService
{
    public partial class JboxItem
    {
        public Equipment Jbox { get; set; }

        public static List<JboxItem> GetItems(string constructionCode)
        {
            string sql = String.Format(@"
                SELECT I.* 
                FROM JboxItems I INNER JOIN JboxColorEntries E 
                ON I.SymbolName = E.JboxEquipmentName AND E.JboxColorId = I.ColorId
                WHERE E.ConstructionCode = '{0}' 
                AND E.JboxEquipmentName = I.SymbolName
                AND I.LengthMax >= E.WireLength
                AND I.LengthMin <= E.WireLength", constructionCode);

            var db = JboxItem.GetDatabase();
            return db.ExecuteQuery<JboxItem>(sql);
        }

        public static List<JboxItem> GetItem(JboxColorEntry entry)
        {
            string sql = String.Format(@"
                SELECT I.* 
                FROM JboxItems I 
                WHERE I.SymbolName = '{0}'
                AND I.ColorId = '{1}'
                AND I.LengthMax >= {2}
                AND I.LengthMin <= {2}", entry.JboxEquipmentName, entry.JboxColorId, entry.WireLength);

            var db = JboxItem.GetDatabase();
            return db.ExecuteQuery<JboxItem>(sql);
        }

    }
}
