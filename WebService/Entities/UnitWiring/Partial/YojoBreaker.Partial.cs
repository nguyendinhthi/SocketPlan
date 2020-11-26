namespace SocketPlan.WebService
{
    using System;
    using System.Collections.Generic;
    using Edsa.Data;
    using Edsa.Data.Attributes;

    public partial class YojoBreaker
    {
        public static YojoBreaker Get(string categoryCode, string withHiraiki, decimal pcSmall, decimal pcLarge, bool IsShudenBan)
        {
            var db = YojoBreaker.GetDatabase();

            var sql = "SELECT * FROM YojoBreakers WHERE PcSmall = " + pcSmall +
                     " AND PcLarge = " + pcLarge +
                     " AND IsShudenBan = " + Convert.ToInt32(IsShudenBan).ToString();

            if (!string.IsNullOrEmpty(categoryCode))
                sql += " AND BreakerCategoryCode = '" + categoryCode + "'";

            if (!string.IsNullOrEmpty(withHiraiki))
                sql += " AND WithHiraiki = " + withHiraiki;

            var result = db.ExecuteQuery<YojoBreaker>(sql);

            if (result.Count > 0)
                return result[0];
            else
                return null;
        }
    }
}
