using System.Collections.Generic;

namespace SocketPlan.WebService
{
    public partial class SpecificationProductDetail
    {
        public static List<SpecificationProductDetail> Get(string constructionCode, string class1Code, string class2Code)
        {
            var db = SpecificationProductDetail.GetDatabase();
            string sql = @"
                SELECT * FROM SpecificationProductDetails
                WHERE
                    ConstructionCode = '" + constructionCode + @"' AND
                    Class1Code = '" + class1Code + @"' AND
                    Class2Code = '" + class2Code + @"'";

            return db.ExecuteQuery<SpecificationProductDetail>(sql);
        }

        public static List<SpecificationProductDetail> Get(string constructionCode, int specificationType, string class1Code, string class2Code)
        {
            var db = SpecificationProductDetail.GetDatabase();
            string sql = @"
                SELECT * FROM SpecificationProductDetails
                WHERE
                    ConstructionCode = '" + constructionCode + @"' AND
                    SpecificationType = " + specificationType + @" AND
                    Class1Code = '" + class1Code + @"' AND
                    Class2Code = '" + class2Code + @"'";

            return db.ExecuteQuery<SpecificationProductDetail>(sql);
        }

        public static List<SpecificationProductDetail> GetRimokonNicheSA(string constructionCode)
        {
            var db = SpecificationProductDetail.GetDatabase();
            string sql = @"
                SELECT * FROM SpecificationProductDetails
                WHERE
                    ConstructionCode = '" + constructionCode + @"' AND
                    SpecificationType = 3 AND
                    Class1Code IS NULL AND
                    Class2Code = '0035' AND
                    (ProductCode = '0400107' OR ProductCode = '0400108')";

            return db.ExecuteQuery<SpecificationProductDetail>(sql);
        }

        public static List<SpecificationProductDetail> Get(string constructionCode, int specificationType)
        {
            var db = SpecificationProductDetail.GetDatabase();
            string sql = @"
                SELECT * FROM SpecificationProductDetails
                WHERE
                    ConstructionCode = '" + constructionCode + @"' AND
                    SpecificationType = " + specificationType;

            return db.ExecuteQuery<SpecificationProductDetail>(sql);
        }

        public static string GetRoomName(string roomCode)
        {
            var db = SpecificationProductDetail.GetDatabase();
            string sql = @"
                SELECT RoomNameJapanese FROM Rooms WHERE RoomCode ='"+ roomCode +"'";

            var obj = db.ExecuteScalar(sql);
            if(obj == null)
                return string.Empty;

            return obj.ToString();
        }

        public static List<SpecificationProductDetail> Get(string constructionCode)
        {
            var db = SpecificationProductDetail.GetDatabase();

            string sql = @"
                SELECT * FROM SpecificationProductDetails
                WHERE
                    ConstructionCode = '" + constructionCode + @"'";

            return db.ExecuteQuery<SpecificationProductDetail>(sql);
        }
    }
}
