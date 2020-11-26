using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class ShikakuTableEntry
    {
        public const int HEMS_ARI = 81;

        public static List<ShikakuTableEntry> Get(string constructionCode)
        {
            string sql = @"
                SELECT * FROM ShikakuTableEntries
                WHERE
                    ConstructionCode = '" + constructionCode + "'";

            var db = ShikakuTableEntry.GetDatabase();
            return db.ExecuteQuery<ShikakuTableEntry>(sql);
        }

        public static void Delete(string constructionCode)
        {
            string sql = @"
                DELETE FROM ShikakuTableEntries
                WHERE
                    ConstructionCode = '" + constructionCode + "'";

            var db = ShikakuTableEntry.GetDatabase();
            db.ExecuteNonQuery(sql);            
        }

        public static bool HasHemsBundenban(string constructionCode)
        {
            var sql = @"
            SELECT * FROM ShikakuTableEntries
            WHERE
                ConstructionCode = '" + constructionCode + @"' AND
                ItemId = 81"; // 81:HEMS分電盤：有

            var db = ShikakuTableEntry.GetDatabase();
            var entries = db.ExecuteQuery<ShikakuTableEntry>(sql);

            if (entries.Count == 0)
                return false;

            var ari = entries[0];
            if (ari.Value == "True")
                return true;
            else
                return false;
        }

        public static string GetCompanyCode(string constructionCode)
        {
            var shikaku = ShikakuTableEntry.Get(constructionCode, 38);
            if (shikaku == null)
                return string.Empty;

            var contract = ElectricContract.Get(shikaku.Value);
            if (contract == null)
                return string.Empty;

            return contract.CompanyCode;

        }

        public static void UpdateElectricPower(string constructionCode, decimal power)
        {
            var electricPower = ConvertElectriPower(power);

            var sql = string.Empty;
            sql =
                @"UPDATE ShikakuTableEntries
                  SET Value = '" + electricPower + @"'
                  WHERE
                    ConstructionCode = '" + constructionCode + @"'
                     AND ItemId = 41";

            var dataBase = ShikakuTableItem.GetDatabase();
            dataBase.ExecuteNonQuery(sql);
        }

        private static string ConvertElectriPower(decimal power)
        {
            var sql = string.Empty;
            sql = @"SELECT ComboItems 
                    FROM ShikakuTableNewItems
                    WHERE Id = 41";

            var dataBase = ShikakuTableItem.GetDatabase();
            var combo = dataBase.ExecuteScalar(sql);

            if (combo == null)
                throw new ApplicationException("Not found ShikakuTableItems.");

            var items = new List<string>(combo.ToString().Split(';'));
            items.Sort();

            foreach (var item in items)
            {
                if (!item.Contains("KVA"))
                    continue;

                decimal kva = 0;
                if (!decimal.TryParse(item.Replace("KVA", ""), out kva))
                    continue;

                if (kva < power)
                    continue;

                return item;
            }

            return string.Empty;
        }

    }
}
