using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketPlan.WebService
{
    public partial class SocketBoxPatternDetail
    {
        public Equipment Equipment { get; set; }

        private List<SocketBoxDetailComment> comments = new List<SocketBoxDetailComment>();
        public List<SocketBoxDetailComment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public static List<SocketBoxPatternDetail> Get(int patternId)
        {
            var sql = @"
            SELECT * FROM SocketBoxPatternDetails
            WHERE
                PatternId = " + patternId.ToString();

            var db = SocketBoxPatternDetail.GetDatabase();
            return db.ExecuteQuery<SocketBoxPatternDetail>(sql);
        }

        public static void Delete(int patternId)
        {
            var sql = @"
            DELETE FROM SocketBoxPatternDetails
            WHERE PatternId = " + patternId;

            var db = SocketBoxPatternDetail.GetDatabase();
            db.ExecuteNonQuery(sql);
        }

        public static void DeleteByCategory(int categoryId)
        {
            var sql = @"
            DELETE FROM SocketBoxPatternDetails
            FROM SocketBoxPatternDetails D
            JOIN SocketBoxPatterns P ON D.PatternId = P.Id AND
                P.CategoryId = " + categoryId;

            var db = SocketBoxPatternDetail.GetDatabase();
            db.ExecuteNonQuery(sql);
        }
    }
}
