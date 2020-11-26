using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class ConstructionSchedule
    {
        public static bool IsBeforeProcessRequest(string constructionCode)
        {
            //長期物件区分、0：通常物件 1：長期物件（待機） 2：長期物件（再開）
            //加工依頼送信日があっても、長期物件区分が
            //「1：長期物件（待機）」の場合は、加工依頼前と同じ処理にする。
            var construction = Construction.Get(constructionCode);
            if (construction.ProlongedPlanStatus == 1)
                return true;

            var schedule = ConstructionSchedule.Get(constructionCode);
            return !schedule.SentProcessRequestDate.HasValue;
        }
    }
}
