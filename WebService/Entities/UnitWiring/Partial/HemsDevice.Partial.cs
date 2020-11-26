using System.Collections.Generic;

namespace SocketPlan.WebService
{
    public partial class HemsDevice
    {
        public static List<HemsDevice> Get(string constructionCode)
        {
            string sql = @"
                Select *  FROM HemsDevices
                WHERE
                    ConstructionCode = '" + constructionCode + @"'";

            var db = HemsDevice.GetDatabase();
            return db.ExecuteQuery<HemsDevice>(sql);
        }
        public static void Delete(string constructionCode)
        {
            string sql = @"
                DELETE FROM HemsDevices
                WHERE
                    ConstructionCode = '" + constructionCode + @"'";

            var db = HemsDevice.GetDatabase();
            db.ExecuteNonQuery(sql);
        }
    }
}
