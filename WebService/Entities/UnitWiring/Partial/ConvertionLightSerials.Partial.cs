namespace SocketPlan.WebService
{
    using System;
    using System.Collections.Generic;
    using Edsa.Data;
    using Edsa.Data.Attributes;

    public partial class ConvertionLightSerial
    {
        public static List<ConvertionLightSerial> GetSerials(string constructionCode)
        {
            var result = new List<ConvertionLightSerial>();
            var sql = @"
            SELECT *
            FROM  ConvertionLightSerials
            WHERE ConstructionCode = '" + constructionCode + @"'";

            var db = ConvertionLightSerial.GetDatabase();
            return db.ExecuteQuery<ConvertionLightSerial>(sql);
        }

        public static void Update(string constructionCode)
        {
            var sql = string.Empty;
            sql = @"UPDATE ConvertionLightSerials 
                    SET ConfirmedFlag = 'true' WHERE ConstructionCode = '" + constructionCode + "'";

            var database = ConvertionLightSerial.GetDatabase();
            database.ExecuteNonQuery(sql);
        }
    }
}
