using System.Collections.Generic;

namespace SocketPlan.WebService
{
    public partial class RoomLayout
    {
        public static List<RoomLayout> Get(string constructionCode)
        {
            var db = RoomLayout.GetDatabase();

            string sql = @"
                SELECT * FROM RoomLayouts
                WHERE
                    ConstructionCode = '" + constructionCode + @"'
                    AND Floor <> 0";

            return db.ExecuteQuery<RoomLayout>(sql);
        }
    }
}
