using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class HouseTypeGroup
    {
        public static HouseTypeGroup Get(string constructionCode, string planNo)
        {
            var detail = HouseTypeGroupDetail.Get(constructionCode, planNo);
            var group = HouseTypeGroup.Get(detail.HouseTypeGroupId);
            return group;
        }
    }
}
