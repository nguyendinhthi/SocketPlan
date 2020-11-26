using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketPlan.WebService
{
    public partial class SocketBoxSpecific
    {
        private List<SocketBoxSpecificRelation> relations = new List<SocketBoxSpecificRelation>();
        public List<SocketBoxSpecificRelation> Relations
        {
            get { return this.relations; }
            set { this.relations = value; }
        }

        public static int GetNewId()
        {
            var sql = @"
            SELECT MAX(Id) FROM SocketBoxSpecifics";

            var db = SocketBoxSpecific.GetDatabase();
            var obj = db.ExecuteScalar(sql);

            if (obj == null || obj == DBNull.Value)
                return 1;

            return (int)obj + 1;
        }

        public static SocketBoxSpecific GetSpecifics(int patternId)
        {
            var sql = @"
            SELECT * 
            FROM SocketBoxSpecifics
            WHERE Id = " + patternId;

            var db = SocketBoxSpecific.GetDatabase();
            var boxes = db.ExecuteQuery<SocketBoxSpecific>(sql);

            return boxes[0];
        }

        public static SocketBoxSpecific GetPatternName(int patternId)
        {
            var sql = @"
            SELECT * 
            FROM SocketBoxSpecifics
            WHERE Id = " + patternId;

            var db = SocketBoxSpecific.GetDatabase();
            var boxes = db.ExecuteQuery<SocketBoxSpecific>(sql);

            return boxes[0];
        }

//        public static List<SocketBoxSpecific> GetRelatedSpecifics(int specificId)
//        {
//            var sql = @"
//            SELECT S1.*
//            FROM SocketBoxSpecifics S1
//            JOIN SocketBoxSpecificRelations R ON S1.Id = R.SocketBoxSpecificId
//            JOIN SocketBoxSpecifics S2 ON R.RelatedSpecificId = S2.Id
//            WHERE S1.Id = " + specificId;

//            var db = SocketBoxSpecific.GetDatabase();
//            var specifics = db.ExecuteQuery<SocketBoxSpecific>(sql);

//            sql = @"
//            SELECT
//            FROM SocketBoxSpecifics S1
//            JOIN SocketBoxSpecificRelations R ON
//                S1.Id = R.SocketBoxSpecificId AND S1.Id = " + specificId + @"
//            JOIN SocketBoxSpecifics S2 ON R.RelatedSpecificId = S2.Id";

//            specifics.AddRange(db.ExecuteQuery<SocketBoxSpecific>(sql));

//            return specifics;
//        }


    }
}
