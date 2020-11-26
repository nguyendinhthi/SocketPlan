using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class SolarComposition
    {
        public static List<SolarComposition> GetComposition(string powerStr)
        {
            decimal requirePower = decimal.Parse(powerStr);

            string sql = @"
                SELECT * FROM SolarComposition
                WHERE
                    PowerMin <= " + requirePower + @" AND
                    PowerMax >= " + requirePower;

            var db = SolarComposition.GetDatabase();
            return db.ExecuteQuery<SolarComposition>(sql);
        }
        public static List<SolarComposition> GetComposition(List<string> powerConTexts)
        {
            string pc1, pc2, pc3, pc4, pc5;
            if (string.IsNullOrEmpty(powerConTexts[0]))
                pc1 = "IS NULL";
            else
                pc1 = "= " + powerConTexts[0].Replace("kw", "");

            if (string.IsNullOrEmpty(powerConTexts[1]))
                pc2 = "IS NULL";
            else
                pc2 = "= " + powerConTexts[1].Replace("kw", "");

            if (string.IsNullOrEmpty(powerConTexts[2]))
                pc3 = "IS NULL";
            else
                pc3 = "= " + powerConTexts[2].Replace("kw", "");

            if (string.IsNullOrEmpty(powerConTexts[3]))
                pc4 = "IS NULL";
            else
                pc4 = "= " + powerConTexts[3].Replace("kw", "");

            if (string.IsNullOrEmpty(powerConTexts[4]))
                pc5 = "IS NULL";
            else
                pc5 = "= " + powerConTexts[4].Replace("kw", "");

            string sql = @"
                SELECT * FROM SolarComposition
                WHERE
                    PowerBox1 " + pc1 + @" AND
                    PowerBox2 " + pc2 + @" AND
                    PowerBox3 " + pc3 + @" AND
                    PowerBox4 " + pc4 + @" AND
                    PowerBox5 " + pc5;

            var db = SolarComposition.GetDatabase();
            return db.ExecuteQuery<SolarComposition>(sql);
        }

        public static decimal GetMaxPower() 
        {
            string sql = "SELECT MAX(PowerMax) FROM SolarComposition";
            var db = SolarComposition.GetDatabase();
            return (decimal)db.ExecuteScalar(sql);
        }

        public static decimal GetMinPower()
        {
            string sql = "SELECT MIN(PowerMin) FROM SolarComposition";
            var db = SolarComposition.GetDatabase();
            return (decimal)db.ExecuteScalar(sql);
        }
    }
}
