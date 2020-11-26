using System.Collections.Generic;
namespace SocketPlan.WebService
{
    public partial class HemsRoom
    {
        public static void Delete(string constructionCode)
        {
            string sql = @"
                DELETE FROM HemsRooms
                WHERE
                    ConstructionCode = '" + constructionCode + @"'";

            var db = HemsRoom.GetDatabase();
            db.ExecuteNonQuery(sql);
        }

        public static List<HemsRoom> Get(string constructionCode)
        {
            string sql = @"
                SELECT *
                FROM HemsRooms
                WHERE
                    ConstructionCode = '" + constructionCode + @"'";

            var db = HemsRoom.GetDatabase();
            return db.ExecuteQuery<HemsRoom>(sql);
        }
    }
}
