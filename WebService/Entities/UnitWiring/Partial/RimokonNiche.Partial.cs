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
    public partial class RimokonNiche
    {
        private List<RimokonNicheDetail> details = new List<RimokonNicheDetail>();
        public List<RimokonNicheDetail> Details
        {
            get { return this.details; }
            set { this.details = value; }
        }

        public static void Delete(string constructionCode)
        {
            var sql = @"DELETE FROM RimokonNiches WHERE ConstructionCode = '" + constructionCode + "'";
            var db = RimokonNiche.GetDatabase();
            db.ExecuteNonQuery(sql);
        }

        public static List<RimokonNiche> Get(string constructionCode)
        {
            constructionCode = constructionCode.Replace("'", "''");

            var sql = @"SELECT * FROM RimokonNiches WHERE ConstructionCode = '" + constructionCode + "'";
            var db = RimokonNiche.GetDatabase();
            return db.ExecuteQuery<RimokonNiche>(sql);
        }
    }
}
