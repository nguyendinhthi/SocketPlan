using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class ProcessLog
    {
        public static void Update(string constructionCode, string planNo, string revisionNo)
        {
            var log = ProcessLog.Get(constructionCode);
            if (log == null)
            {
                log = new ProcessLog();
                log.ConstructionCode = constructionCode;
            }

            var now = DateTime.Now;
            log.ExportedDateTime = now;
            log.DenkiOrderImportStatus = false;
            log.UpdatedDateTime = now;
            log.PlanNo = planNo;
            log.RevisionNo = revisionNo;
            log.IsExportedWithHems = false;

            log.Store();
        }

        public static bool ExistExportHemsData(string constructionCode)
        {
            string sql = @"
                SELECT * FROM ProcessLogs
                WHERE
                    ConstructionCode = '" + constructionCode + @"'
                    AND IsExportedWithHems = 'true'";

            var db = HemsLog.GetDatabase();
            var logs = db.ExecuteQuery<ProcessLog>(sql);

            if (logs.Count == 0)
                return false;
            else
                return true;
        }

        public static void UpdateProcessLogsForHems(string constructionCode)
        {
            string sql = string.Empty;
            sql = @"UPDATE ProcessLogs
                    SET IsExportedWithHems = 'true' 
                    WHERE ConstructionCode = '" + constructionCode + "'";

            var db = ProcessLog.GetDatabase();
            db.ExecuteNonQuery(sql);
        }
    }
}
