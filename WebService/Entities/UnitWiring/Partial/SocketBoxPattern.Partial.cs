using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketPlan.WebService
{
    public partial class SocketBoxPattern
    {
        private List<SocketBoxPatternDetail> details = new List<SocketBoxPatternDetail>();
        public List<SocketBoxPatternDetail> Details
        {
            get { return this.details; }
            set { this.details = value; }
        }

        private List<SocketBoxPatternColor> colors = new List<SocketBoxPatternColor>();
        public List<SocketBoxPatternColor> Colors
        {
            get { return this.colors; }
            set { this.colors = value; }
        }

        public static int GetNewId()
        {
            var sql = @"SELECT MAX(Id) FROM SocketBoxPatterns";
            var db = SocketBoxPattern.GetDatabase();
            var obj = db.ExecuteScalar(sql);

            if (obj == null || obj == DBNull.Value)
                return 1;

            return (int)obj + 1;
        }

        public static void DeleteByCategory(int categoryId)
        {
            var sql = @"
            DELETE FROM SocketBoxPatterns
            WHERE CategoryId = " + categoryId;

            var db = SocketBoxPattern.GetDatabase();
            db.ExecuteNonQuery(sql);
        }

        public static void Delete(int patternId)
        {
            var sql = @"
            DELETE FROM SocketBoxPatterns
            WHERE Id = " + patternId;

            var db = SocketBoxPattern.GetDatabase();
            db.ExecuteNonQuery(sql);
        }
    }
}
