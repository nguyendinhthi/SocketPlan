using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class tbl_siyo_heya
    {
        public static List<tbl_siyo_heya> Get(string constructionCode, int siyoCode)
        {
            string sql = @"
                SELECT 
                    customerCode,
                    floorNum, 
	                roomCd,
                    roomName
                FROM tbl_siyo_heya
                WHERE
	                customerCode = '" + constructionCode + @"' AND
	                siyoCode = " + siyoCode + @" AND
	                floorNum <> 0
                GROUP BY
	                customerCode,
	                siyoCode,
	                floorNum,
	                roomCd,
                    roomName
                ORDER BY
                    customerCode,
                    floorNum,
                    roomCd";

            var db = tbl_siyo_heya.GetDatabase();
            return db.ExecuteQuery<tbl_siyo_heya>(sql);
        }
    }
}
