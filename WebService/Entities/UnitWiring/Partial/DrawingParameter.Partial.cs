using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class DrawingParameter
    {
        public static DrawingParameter GetParameter(string constructionCode, string planNo, string revisionNo, string keyString)
        {
            var seq = GetMaxSeq(constructionCode, planNo, revisionNo, keyString);

            return DrawingParameter.Get(constructionCode, planNo, revisionNo, keyString, seq);
        }

        private static int GetMaxSeq(string constructionCode, string planNo, string revisionNo, string keyString)
        {
            string sql = @"
                SELECT MAX(KeySeq) FROM DrawingParameter
                WHERE
                    ConstructionCode = '" + constructionCode + @"' AND
                    PlanNo = '" + planNo + @"' AND
                    RevNo = '" + revisionNo + @"' AND
                    KeyString = '" + keyString + "'";

            var db = DrawingParameter.GetDatabase();
            var sqlResult =db.ExecuteScalar(sql);

            if (sqlResult == null)
                return 1;
            else
                return int.Parse(sqlResult.ToString());
        }

        public static void Insert(string constructionCode, string planNo, string revisionNo, string keyString, string value)
        {
            var status = new DrawingParameter();
            status.ConstructionCode = constructionCode;
            status.PlanNo = planNo;
            status.RevNo = revisionNo;
            status.KeySeq = GetMaxSeq(constructionCode, planNo, revisionNo, keyString);
            status.KeyString = keyString;
            status.Value = value;
            status.Store();
        }
    }
}
