using System.Collections.Generic;

namespace SocketPlan.WebService
{
    public partial class RoomLayoutsForKibiroi
    {
        public static List<RoomLayoutsForKibiroi> Get(string constructionCode)
        {
            var db = RoomLayoutsForKibiroi.GetDatabase();
            string sql = "SELECT * FROM RoomLayoutsForKibiroi WHERE ConstructionCode = '" + constructionCode + "'";

            return db.ExecuteQuery<RoomLayoutsForKibiroi>(sql);
        }
    }
}
