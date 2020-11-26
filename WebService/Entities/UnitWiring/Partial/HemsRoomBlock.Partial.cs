using System.Collections.Generic;
namespace SocketPlan.WebService
{
    public partial class HemsRoomBlock
    {
        public static void Delete(string constructionCode)
        {
            string sql = @"
                DELETE FROM HemsRoomBlocks
                WHERE
                    ConstructionCode = '" + constructionCode + @"'";

            var db = HemsRoomBlock.GetDatabase();
            db.ExecuteNonQuery(sql);
        }
        public static List<HemsRoomBlock> Get(string constructionCode)
        {
            string sql = @"
                SELECT *
                FROM HemsRoomBlocks
                WHERE
                    ConstructionCode = '" + constructionCode + @"'";

            var db = HemsRoomBlock.GetDatabase();
            return db.ExecuteQuery<HemsRoomBlock>(sql);
        }
    }
}
