using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class tbl_siyo_kanri
    {
        public static tbl_siyo_kanri Get(string constructionCode, string planNo)
        {
            var siyoCode = tbl_siyo_boss.GetSiyoCode(constructionCode, planNo);
            return tbl_siyo_kanri.Get(constructionCode, siyoCode);
        }
    }
}
