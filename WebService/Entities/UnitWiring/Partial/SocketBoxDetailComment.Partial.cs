using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketPlan.WebService
{
    public partial class SocketBoxDetailComment
    {
        public Comment Comment { get; set; }

        public static List<SocketBoxDetailComment> Get(int patternId)
        {
            var sql = @"
            SELECT * FROM SocketBoxDetailComments
            WHERE
                PatternId = " + patternId.ToString();

            var db = SocketBoxDetailComment.GetDatabase();
            return db.ExecuteQuery<SocketBoxDetailComment>(sql);
        }

        public static List<Comment> GetComments(List<SocketBoxDetailComment> socketComments)
        {
            if (socketComments.Count == 0)
                return new List<Comment>();

            var ids = string.Empty;
            socketComments.ForEach(p => ids += p.CommentId + ",");
            ids = ids.Substring(0, ids.Length - 1);

            var sql = @"
            SELECT * FROM Comments
            WHERE Id IN (" + ids + @")";

            var db = Comment.GetDatabase();
            return db.ExecuteQuery<Comment>(sql);
        }

        public static void Delete(int patternId)
        {
            var sql = @"
            DELETE FROM SocketBoxDetailComments
            WHERE PatternId = " + patternId;

            var db = SocketBoxDetailComment.GetDatabase();
            db.ExecuteNonQuery(sql);
        }

        public static void DeleteByCategory(int categoryId)
        {
            var sql = @"
            DELETE FROM SocketBoxDetailComments
            FROM SocketBoxDetailComments C
            JOIN SocketBoxPatternDetails D ON
                C.PatternId = D.PatternId AND C.DetailSeq = D.Seq
            JOIN SocketBoxPatterns P ON D.PatternId = P.Id AND
                P.CategoryId = " + categoryId;

            var db = SocketBoxDetailComment.GetDatabase();
            db.ExecuteNonQuery(sql);
        }
    }
}
