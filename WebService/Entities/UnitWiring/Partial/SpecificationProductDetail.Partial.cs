using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

namespace SocketPlan.WebService
{
    public partial class SpecificationProductDetail
    {
        private const string CLASS1_屋根 = "0110";
        private const string CLASS2_太陽光発電システム = "0150";
        private const string CLASS2_パワーコンディショナー = "0153";

        public static bool ExistSolar(string constructionCode)
        {
            var detail = SpecificationProductDetail.Get(constructionCode, 0, 1, CLASS1_屋根, CLASS2_太陽光発電システム);
            return detail != null;
        }

        public static int GetPowerConditionerCount(string constructionCode)
        {
            if (!SpecificationProductDetail.ExistSolar(constructionCode))
                return 0;

            var detail = SpecificationProductDetail.Get(constructionCode, 0, 1, CLASS1_屋根, CLASS2_パワーコンディショナー);
            if (detail == null)
                return -1;

            var zenkaku = detail.ProductName.Remove(1);
            var hankaku = Strings.StrConv(zenkaku, VbStrConv.Narrow, 0x0411);

            int count;
            if (int.TryParse(hankaku, out count)) //1文字目から台数を取得できるか試みる
                return count;

            return -1;
        }

        private static SpecificationProductDetail Get(string constructionCode, int floor, int specificationType, string class1Code, string class2Code)
        {
            string sql = @"
                SELECT * FROM SpecificationProductDetails
                WHERE
                    ConstructionCode = '" + constructionCode + @"' AND
                    Floor = " + floor + @" AND
                    SpecificationType = " + specificationType + @" AND
                    Class1Code = '" + class1Code + @"' AND
                    Class2Code = '" + class2Code + @"'";

            var db = SpecificationProductDetail.GetDatabase();
            var list = db.ExecuteQuery<SpecificationProductDetail>(sql);

            if (list.Count == 0)
                return null;

            return list[0];
        }
    }
}
