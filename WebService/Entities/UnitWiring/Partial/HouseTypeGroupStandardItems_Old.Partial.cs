using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketPlan.WebService
{
    public partial class HouseTypeGroupStandardItems_Old
    {
        public static List<HouseTypeGroupStandardItems_Old> Get(int houseTypeGroupId)
        {
            string sql = string.Empty;
            sql = @"SELECT * 
                    FROM HouseTypeGroupStandardItems_Old
                    WHERE HouseTypeGroupId = " + houseTypeGroupId.ToString();
            var db = HouseTypeGroupStandardItems_Old.GetDatabase();
            return db.ExecuteQuery<HouseTypeGroupStandardItems_Old>(sql);
            
        }
    }
}
