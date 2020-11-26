using System.Collections.Generic;

namespace SocketPlan.WebService
{
    public partial class CtBox
    {
        public static List<CtBox> Get(string constructionCode)
        {
            var code = constructionCode.Replace("'", "''");

            var sql = @"SELECT * FROM CtBoxes WHERE ConstructionCode = '" + code + "'";
            var db = CtBox.GetDatabase();
            return db.ExecuteQuery<CtBox>(sql);
        }

        public static List<CtBox> Get(List<ShipmentRequest> requests)
        {
            if (requests.Count == 0)
                return new List<CtBox>();

            string constructionCodes = "";
            foreach (var request in requests)
            {
                constructionCodes += "'" + request.ConstructionCode + "',";
            }
            constructionCodes = constructionCodes.TrimEnd(',');

            var sql = @"
SELECT * FROM CtBoxes
WHERE
    ConstructionCode IN (" + constructionCodes + ")";

            var db = CtBox.GetDatabase();
            var ctBoxes = db.ExecuteQuery<CtBox>(sql);

            return ctBoxes;
        }

        public static void Delete(string constructionCode)
        {
            string sql = @"
                DELETE FROM CtBoxes
                WHERE
                    ConstructionCode = '" + constructionCode + @"'";

            var db = CtBox.GetDatabase();
            db.ExecuteNonQuery(sql);
        }

        // DipSwitchに「蓄電」って固定表記になったからもういらない
        //public string BlockName1 { get; set; }
        //public string BlockName2 { get; set; }

        // DipSwitchで表示する画像につかうの
        public byte[] NumberSettingImage { get; set; }
        public byte[] ModeSettingImage { get; set; }
    }
}
