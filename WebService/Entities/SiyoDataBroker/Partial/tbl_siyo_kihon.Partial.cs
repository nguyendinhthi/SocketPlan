namespace SocketPlan.WebService
{
    public partial class tbl_siyo_kihon
    {
        public static tbl_siyo_kihon Get(string constructionCode, int siyoCode, string kihonsiyoCd)
        {
            string sql = @"
                SELECT siyoDetailCd
                FROM tbl_siyo_kihon
                WHERE
	                customerCode = '" + constructionCode + @"' AND
	                siyoCode = " + siyoCode + @" AND
	                kihonsiyoCd = '" + kihonsiyoCd + "'";

            var db = tbl_siyo_kihon.GetDatabase();
            var list = db.ExecuteQuery<tbl_siyo_kihon>(sql);

            if (list.Count == 0)
                return null;
            else
                return list[0];
        }
    }
}
