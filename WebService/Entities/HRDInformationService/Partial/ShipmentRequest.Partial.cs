using System;
using System.Collections.Generic;

namespace SocketPlan.WebService
{
    public partial class ShipmentRequest
    {
        public static List<ShipmentRequest> GetHEMS(int requestYear, int requestWeek)
        {
            //めんどくせえからCodeべたうちで。他にも必要になったら、Code指定できるメソッドつくれば？

            var db = ShipmentRequest.GetDatabase();
            string sql = @"
                SELECT * FROM ShipmentRequests S
                WHERE
                    S.ConstructionMaterialCode = 'HH' AND
                    S.ConstructionMaterialDetailCode = 1 AND
                    S.Floor = 0 AND
                    S.RequestYear = " + requestYear + @" AND
                    S.RequestWeek = " + requestWeek;

            return db.ExecuteQuery<ShipmentRequest>(sql);
        }

        public static List<ShipmentRequest> Get(int requestYear, int requestWeek, string code)
        {
            var db = ShipmentRequest.GetDatabase();
            string sql = @"
                SELECT * FROM ShipmentRequests S
                WHERE
                    S.ConstructionMaterialCode = '" + code + @"' AND
                    S.ConstructionMaterialDetailCode = 1 AND
                    S.Floor = 0 AND
                    S.RequestYear = " + requestYear + @" AND
                    S.RequestWeek = " + requestWeek;

            return db.ExecuteQuery<ShipmentRequest>(sql);
        }

        public static List<ShipmentRequest> Get(int requestYear, int requestWeek, List<string> codes)
        {
            var s = string.Empty;
            codes.ForEach(p => s += "'" + p + "',");
            s = s.Substring(0, s.Length - 1);

            var db = ShipmentRequest.GetDatabase();
            string sql = @"
                SELECT * FROM ShipmentRequests S
                WHERE
                    S.ConstructionMaterialCode IN (" + s + @") AND
                    S.ConstructionMaterialDetailCode = 1 AND
                    S.Floor = 0 AND
                    S.RequestYear = " + requestYear + @" AND
                    S.RequestWeek = " + requestWeek;

            return db.ExecuteQuery<ShipmentRequest>(sql);
        }

        public static ShipmentRequest Get(string constructionCode, string requestCode)
        {
            var code = constructionCode.Replace("'", "''");

            string sql = @"
                SELECT * FROM ShipmentRequests S
                WHERE
                    S.ConstructionCode = '" + code + @"' AND
                    S.ConstructionMaterialCode = '" + requestCode + @"' AND
                    S.ConstructionMaterialDetailCode = 1 AND
                    S.Floor = 0";

            var db = ShipmentRequest.GetDatabase();

            var requests = db.ExecuteQuery<ShipmentRequest>(sql);
            if (requests.Count == 0)
                return null;

            return requests[0];
        }
        public static int? GetMaxWeek(int requestYear)
        {
            var db = ShipmentRequest.GetDatabase();
            string sql = @"
                SELECT MAX(RequestWeek)
                FROM ShipmentRequests
                WHERE RequestYear = " + requestYear;

            var result = db.ExecuteScalar(sql);
            if (result == null)
                return null;

            return Convert.ToInt32(result);
        }

    }
}
