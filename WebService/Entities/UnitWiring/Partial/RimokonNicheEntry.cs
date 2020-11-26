using System.Collections.Generic;
using System.IO;
using SocketPlan.WebService.Properties;
using System.Drawing;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace SocketPlan.WebService
{
    public partial class RimokonNicheEntry
    {
        public static List<RimokonNicheEntry> Get(string constructionCode)
        {
            string sql = @"
                SELECT *
                FROM RimokonNicheEntries
                WHERE ConstructionCode =  '" + constructionCode + "'";

            var db = RimokonNicheEntry.GetDatabase();
            return db.ExecuteQuery<RimokonNicheEntry>(sql);
        }

        public static void Update(string constructionCode, List<RimokonNicheEntry> entries, int nicheSeq)
        {
            RimokonNicheEntry.Delete(constructionCode, nicheSeq);
            entries.ForEach(p => p.Store());
        }

        public static void Delete(string constructionCode, int nicheSeq)
        {
            string sql = @"
                DELETE FROM RimokonNicheEntries
                WHERE
                    ConstructionCode = '" + constructionCode + @"'
                AND NicheSeq = " + nicheSeq;

            var db = RimokonNicheEntry.GetDatabase();
            db.ExecuteNonQuery(sql);
        }

        public static void DeleteRemainedEntries(string constructionCode, int maxSeq)
        {
            string sql = @"
                DELETE FROM RimokonNicheEntries
                WHERE
                    ConstructionCode = '" + constructionCode + @"'
                AND NicheSeq > " + maxSeq;

            var db = RimokonNicheEntry.GetDatabase();
            db.ExecuteNonQuery(sql);
        }
    }
}
