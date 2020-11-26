using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace SocketPlan.WebService
{
    public partial class Construction
    {
        public static bool IsNewBunpaikiStandardQtyRule(string constructionCode)
        {
            var construction = Construction.Get(constructionCode);
            if (construction == null)
                return true;

            var basedDate = new DateTime(2017, 5, 1);

            //値なしor2017/5/1以降のときtrue
            var isNew = false;
            if (construction.ContractDate.HasValue)
                isNew = basedDate <= construction.ContractDate.Value;
            else
                isNew = true;

            var isProvisionalNew = false;
            if (construction.ProvisionalContractDate.HasValue)
                isProvisionalNew = basedDate <= construction.ProvisionalContractDate.Value;
            else
                isProvisionalNew = true;

            return isNew && isProvisionalNew;
        }
        
        public static bool IsNewStandardQtyRule(string constructionCode)
        {
            var construction = Construction.Get(constructionCode);
            if (construction == null)
                return true;

            var basedDate = Properties.Settings.Default.StandardItemSwitchingDate;

            //値なしor2016/9/1以降のときtrue
            var isNew = false;
            if (construction.ContractDate.HasValue)
                isNew = basedDate <= construction.ContractDate.Value;
            else
                isNew = true;

            var isProvisionalNew = false;
            if (construction.ProvisionalContractDate.HasValue)
                isProvisionalNew = basedDate <= construction.ProvisionalContractDate.Value;
            else
                isProvisionalNew = true;

            return isNew && isProvisionalNew;
        }

        public static bool IsTenjijyo(string constructionCode)
        {
            var construction = Construction.Get(constructionCode);
            if (construction == null)
                return false;

            return (construction.ConstructionKind == 4 && construction.ConstructionDetailKind == 3);
        }

        // 物件情報一括取得
        public static List<Construction> Get(List<string> constructionCodes)
        {
            if (constructionCodes.Count == 0)
                return new List<Construction>();

            var codes = string.Empty;
            constructionCodes.ForEach(p => codes += "'" + p + "',");
            if (codes.Length == 0)
                return new List<Construction>();

            codes = codes.Substring(0, codes.Length - 1);
            var sql = "SELECT * FROM Constructions WHERE ConstructionCode IN (" + codes + ")";
            var db = Construction.GetDatabase();
            return db.ExecuteQuery<Construction>(sql);
        }
    }
}
