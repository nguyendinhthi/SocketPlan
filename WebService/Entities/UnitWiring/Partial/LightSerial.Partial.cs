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
    public partial class LightSerial
    {
        /// <summary>
        /// テーブル名称
        /// </summary>
        private const string TABLE_NAME = "LightSerials";


        /// <summary>
        /// 在庫が無いLightSerialを取得する
        /// </summary>
        /// <returns></returns>
        public static List<LightSerial> GetEmptyStockLightSerials()
        {
            var db = LightSerialItem.GetDatabase();

            var list = db.ExecuteQuery<LightSerial>("SELECT * FROM " + TABLE_NAME + " WHERE IsStockEmpty = 'True' AND RequireApproval = 'False'");

            if (list.Count == 0)
                return new List<LightSerial>(0);
            else
                return list;
        }
    }
}
