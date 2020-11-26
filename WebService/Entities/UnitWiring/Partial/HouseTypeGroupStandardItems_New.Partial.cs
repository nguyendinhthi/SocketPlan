using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketPlan.WebService
{
    public partial class HouseTypeGroupStandardItems_New
    {
        public static List<HouseTypeGroupStandardItems_New> Get(int houseTypeGroupId)
        {
            string sql = string.Empty;
            sql = @"SELECT * 
                    FROM HouseTypeGroupStandardItems_New
                    WHERE HouseTypeGroupId = " + houseTypeGroupId.ToString();
            var db = HouseTypeGroupStandardItems_New.GetDatabase();
            return db.ExecuteQuery<HouseTypeGroupStandardItems_New>(sql);
        }
    }
}
