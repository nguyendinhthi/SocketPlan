using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class CommentSpecification
    {
        public static void DeleteAll()
        {
            var db = CommentSpecification.GetDatabase();
            db.ExecuteNonQuery("DELETE FROM CommentSpecifications");
        }

        public static List<CommentSpecification> Get(int commentId)
        {
            var db = CommentSpecification.GetDatabase();
            string sql = @"
                SELECT * FROM CommentSpecifications
                WHERE
                    CommentId = '" + commentId + @"'
                ORDER BY
                    CommentId,
                    SpecificationId";

            return db.ExecuteQuery<CommentSpecification>(sql);

        }
    }
}
