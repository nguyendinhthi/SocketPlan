using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace SocketPlan.WebService
{
    public partial class ContractAmpere
    {
        public static ContractAmpere GetAmpere(string contract)
        {
            Regex reg = new Regex(@"[^0-9]");
            var contractNum = Int32.Parse(reg.Replace(contract, "").ToString());

            reg = new Regex(@"[^A-Z]");
            var unitStr = reg.Replace(contract, "").ToString();

            var sql = @"
SELECT TOP 1 * FROM ContractAmpere
WHERE Ampere = (

SELECT MIN(Ampere) FROM ContractAmpere
WHERE Contract >= " + contractNum + @" AND
      Unit = '" + unitStr + "')";

            var db = ContractAmpere.GetDatabase();
            var result = db.ExecuteQuery<ContractAmpere>(sql);
            if (result.Count > 0)
                return result[0];
            else
                return null;
        }

        public static string GetHinban(decimal ampere)
        {
            var sql = @"
SELECT Hinban FROM ContractAmpere
WHERE Ampere = (

SELECT MIN(Ampere) FROM ContractAmpere
WHERE Ampere >= " + ampere + ")";

            var db = ContractAmpere.GetDatabase();
            return db.ExecuteScalar(sql).ToString();
        }

        public static ContractAmpere GetExtensionAmpere(string ampere)
        {
            Regex reg = new Regex(@"[^0-9]");
            var ampereNum = Int32.Parse(reg.Replace(ampere, "").ToString());

            var sql = @"
SELECT TOP 1 * FROM ContractAmpere
WHERE Ampere = (

SELECT MIN(Ampere) FROM ContractAmpere
WHERE Ampere > " + ampereNum + ")";

            var db = ContractAmpere.GetDatabase();
            var result = db.ExecuteQuery<ContractAmpere>(sql);
            if (result.Count > 0)
                return result[0];
            else
                return null;
        }
    }
}
