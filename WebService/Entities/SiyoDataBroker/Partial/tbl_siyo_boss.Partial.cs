using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class tbl_siyo_boss
    {
        private const string SIYO_CODE_ERROR =
            "\n\n" +
            "==================== CAUSE ====================\n" +
            "Cannot get siyo code.\n" +
            "仕様コードを取得できませんでした。\n" +
            "================================================\n";

        public static int GetSiyoCode(string constructionCode, string planNo)
        {
            var siyoCode = GetSiyoCode1(constructionCode, planNo);
            if(siyoCode.HasValue)
                return siyoCode.Value;

            siyoCode = GetLatestSiyoCode(constructionCode);
            if (siyoCode.HasValue)
                return siyoCode.Value;

            throw new ApplicationException(SIYO_CODE_ERROR);
        }

        private static int? GetSiyoCode1(string constructionCode, string planNo)
        {
            string sql = @"
                SELECT MAX(siyoCode)
                FROM tbl_siyo_boss
                WHERE
	                customerCode = '" + constructionCode + @"' AND
	                zumenCode = '" + planNo + @"'";

            var db = tbl_siyo_boss.GetDatabase();
            var result = db.ExecuteScalar(sql);
            if (result == null)
                return null;

            return Convert.ToInt32(result);
        }

        private static int? GetLatestSiyoCode(string constructionCode)
        {
            string sql = @"
                SELECT MAX(siyoCode)
                FROM tbl_siyo_boss
                WHERE
	                customerCode = '" + constructionCode + @"'";

            var db = tbl_siyo_boss.GetDatabase();
            var result = db.ExecuteScalar(sql);
            if (result == null)
                return null;

            return Convert.ToInt32(result);
        }
        
    }
}
