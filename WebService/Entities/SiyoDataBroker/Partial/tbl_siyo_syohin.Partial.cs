using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class tbl_siyo_syohin
    {
        public static List<tbl_siyo_syohin> Get(string constructionCode, int siyoCode)
        {
            var db = tbl_siyo_syohin.GetDatabase();
            string sql = @"
                SELECT * FROM tbl_siyo_syohin
                WHERE
                    customerCode = '" + constructionCode + @"' AND
                    siyoCode = " + siyoCode + @" AND
                    notNeedsKoumoku2 = 0";

            return db.ExecuteQuery<tbl_siyo_syohin>(sql);
        }

        public static List<tbl_siyo_syohin> Get(string constructionCode, int siyoCode, string class1Code, string class2Code)
        {
            var db = tbl_siyo_syohin.GetDatabase();
            string sql = @"
                SELECT * FROM tbl_siyo_syohin
                WHERE
                    customerCode = '" + constructionCode + @"' AND
                    siyoCode = " + siyoCode + @" AND
                    komokuCd1 = '" + class1Code + @"' AND
                    komokuCd2 = '" + class2Code + @"' AND
                    notNeedsKoumoku2 = 0";

            return db.ExecuteQuery<tbl_siyo_syohin>(sql);
        }
        public static List<tbl_siyo_syohin> Get(string constructionCode, int siyoCode, int siyoKbn, string class1Code, string class2Code)
        {
            var db = tbl_siyo_syohin.GetDatabase();
            string sql = @"
                SELECT * FROM tbl_siyo_syohin
                WHERE
                    customerCode = '" + constructionCode + @"' AND
                    siyoCode = " + siyoCode + @" AND
                    siyoKbn = " + siyoKbn + @" AND
                    komokuCd1 = '" + class1Code + @"' AND
                    komokuCd2 = '" + class2Code + @"' AND
                    notNeedsKoumoku2 = 0";

            return db.ExecuteQuery<tbl_siyo_syohin>(sql);
        }
        public static List<tbl_siyo_syohin> GetRimokonNicheSA(string constructionCode, int siyoCode)
        {
            var db = tbl_siyo_syohin.GetDatabase();
            string sql = @"
                SELECT * FROM tbl_siyo_syohin
                WHERE
                    customerCode = '" + constructionCode + @"' AND
                    siyoCode = " + siyoCode + @" AND
                    siyoKbn = 3 AND
                    komokuCd1 IS NULL AND
                    komokuCd2 = '0035' AND
                    (shohinCd = '0400107' OR shohinCd = '0400108') AND
                    notNeedsKoumoku2 = 0";

            return db.ExecuteQuery<tbl_siyo_syohin>(sql);
        }
    }
}
