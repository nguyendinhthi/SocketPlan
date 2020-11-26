using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketPlan.WebService
{
    public partial class SocketBox
    {
        public static List<SocketBox> Get(string constructionCode)
        {

            var sql = @"
            SELECT *
            FROM SocketBoxes
            WHERE
                ConstructionCode = '" + constructionCode + @"'";

            var db = SocketBox.GetDatabase();
            return db.ExecuteQuery<SocketBox>(sql);
        }

        public static void Delete(string constructionCode)
        {
            var sql = @"
            DELETE FROM SocketBoxes
            WHERE
                ConstructionCode = '" + constructionCode + @"'";

            var db = SocketBox.GetDatabase();
            db.ExecuteNonQuery(sql);
        }

        public static void DeleteByDrawing(string constructionCode, List<string> seqs)
        {

            var sql = @"
            DELETE FROM SocketBoxes
            WHERE ConstructionCode = '" + constructionCode + @"' AND
                  Seq  = " + seqs[0]; //どうせ１つずつしか消せないのでOK

            var db = SocketBox.GetDatabase();
            db.ExecuteNonQuery(sql);
        }

        public static void DeleteOld(string constructionCode)
        {
            var sql = @"
            DELETE FROM SocketBoxes
            WHERE ConstructionCode = '" + constructionCode + @"' AND
            PatternId != -1";

            var db = SocketBox.GetDatabase();
            db.ExecuteNonQuery(sql);
        }

        public static SocketBox Get(string constructionCode, decimal symbolX, decimal symbolY, int patternId)
        {

            var sql = @"
            SELECT *
            FROM SocketBoxes
            WHERE
                ConstructionCode = '" + constructionCode + @"' AND
                SymbolLocationX = " + symbolX.ToString("0.00") + @" AND
                SymbolLocationY = " + symbolY.ToString("0.00") + @" AND
                PatternId = " + patternId;

            var db = SocketBox.GetDatabase();
            var boxes = db.ExecuteQuery<SocketBox>(sql);

            if (boxes.Count == 0)
                return null;

            return boxes[0];
        }

        public static SocketBox Get(string constructionCode, int socketObjectId)
        {
            var sql = @"
            SELECT *
            FROM SocketBoxes
            WHERE
                ConstructionCode = '" + constructionCode + @"' AND
                SocketObjectId = " + socketObjectId;

            var db = SocketBox.GetDatabase();
            var boxes = db.ExecuteQuery<SocketBox>(sql);

            if (boxes.Count == 0)
                return null;

            return boxes[0];
        }

        public static int GetMaxSeq(string constructionCode)
        {

            var sql = @"
            SELECT COUNT(*)
            FROM SocketBoxes
            WHERE
            ConstructionCode = '" + constructionCode + @"'";

            var db = SocketBox.GetDatabase();

            if (Int32.Parse(db.ExecuteScalar(sql).ToString()) == 0)
                return 0;

            sql = @"
            SELECT MAX(Seq)
            FROM SocketBoxes
            WHERE
            ConstructionCode = '" + constructionCode + @"'";

            return Int32.Parse(db.ExecuteScalar(sql).ToString());
        }
    }
}
