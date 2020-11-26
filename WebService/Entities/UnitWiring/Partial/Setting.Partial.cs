using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class Setting
    {
        private const string KANABAKARI_ERROR =
            "\n\n" +
            "==================== CAUSE ====================\n" +
            "Unsupported Kanabakari.\n" +
            "未対応の矩計です。\n" +
            "================================================\n";

        public static List<Setting> Get(string constructionCode, int siyoCode)
        {
            int houseType;
            if (BasicSpecificationDetail.IsISmart(constructionCode, siyoCode)) //頻繁に呼ぶわけじゃないし、見やすさ優先で。
                houseType = 4;
            else if (BasicSpecificationDetail.IsICubeOrISmileOrIPalette(constructionCode, siyoCode))
                houseType = 3;
            else if (BasicSpecificationDetail.IsIHead(constructionCode, siyoCode))
            {
                var kanabakari = BasicSpecificationDetail.GetKanabakari(constructionCode, siyoCode);
                if (kanabakari == "265")
                    houseType = 1;
                else if (kanabakari == "240")
                    houseType = 2;
                else if (kanabakari == "260")
                    houseType = 5;
                else
                    throw new ApplicationException(KANABAKARI_ERROR);
            }
            else
                houseType = 2; //一般工法はI-HEAD(240)と同等に扱う

            var db = Setting.GetDatabase();
            var sql = @"
                SELECT * FROM Settings
                WHERE
                    HouseTypeId IS NULL OR
                    HouseTypeId = " + houseType;

            return db.ExecuteQuery<Setting>(sql);
        }
    }
}
