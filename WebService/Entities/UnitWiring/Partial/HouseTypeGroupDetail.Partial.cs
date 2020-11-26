using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class HouseTypeGroupDetail
    {
        private const string HOSUE_TYPE_ERROR =
            "\n\n" +
            "==================== CAUSE ====================\n" +
            "Cannot get hosue type.\n" +
            "家タイプを取得できませんでした。\n" +
            "================================================\n";

        private const string SIYO_CODE_GROUP_ERROR =
            "\n\n" +
            "==================== CAUSE ====================\n" +
            "Cannot get hosue type group.\n" +
            "家タイプ区分を取得できませんでした。\n" +
            "================================================\n";

        public static HouseTypeGroupDetail Get(string constructionCode, string planNo)
        {
            var houseTypeCode = string.Empty;
            if (ConstructionSchedule.IsBeforeProcessRequest(constructionCode))
            {
                var kanri = tbl_siyo_kanri.Get(constructionCode, planNo);
                if (kanri == null || kanri.typeCd == null)
                    throw new ApplicationException(HOSUE_TYPE_ERROR);
                houseTypeCode = kanri.typeCd;
            }
            else
            {
                var house = House.Get(constructionCode);
                if (house == null || house.ConstructionTypeCode == null)
                    throw new ApplicationException(HOSUE_TYPE_ERROR);
                houseTypeCode = house.ConstructionTypeCode;
            }

            var detail = HouseTypeGroupDetail.Get(houseTypeCode);
            if (detail == null)
                throw new ApplicationException(SIYO_CODE_GROUP_ERROR);

            return detail;
        }
    }
}
