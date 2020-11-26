using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketPlan.WebService
{
    public partial class XGWSettingReportLog
    {
        public static List<XGWSettingReportLog> GetBeforeConstructionCodesforDistinct(int year, int week, List<string> constructionCodes)
        {
            var s = string.Empty;
            constructionCodes.ForEach(p => s += "'" + p + "',");
            s = s.Substring(0, s.Length - 1);

            string sql = @"
                SELECT DISTINCT ConstructionCode
                FROM XGWSettingReportLogs
                WHERE (" + (year - 1) + @"<= Year AND " + week + @" <= Week) AND Year < '" + year + @"' 
                OR ( Year = '" + year + @"' AND Week < '" + week + @"')
                AND ConstructionCode IN (" + s + @")";

            var db = XGWSettingReportLog.GetDatabase();
            return db.ExecuteQuery<XGWSettingReportLog>(sql);
        }

        public static void Delete(string constructionCode)
        {
            string sql = @"
                DELETE FROM XGWSettingReportLogs
                WHERE
                    ConstructionCode = '" + constructionCode + @"'";

            var db = XGWSettingReportLog.GetDatabase();
            db.ExecuteNonQuery(sql);
        }
    }
}
