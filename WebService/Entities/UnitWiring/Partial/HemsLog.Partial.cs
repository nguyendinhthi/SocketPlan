using System;
namespace SocketPlan.WebService
{
    public partial class HemsLog
    {
        public static string GetPlanNoWithHyphen(string planNo)
        {
            if (planNo.Length < 4)
                return string.Empty;

            var mae = Convert.ToInt32(planNo.Substring(0, 2)).ToString();
            var ato = Convert.ToInt32(planNo.Substring(2, 2)).ToString();

            if (planNo.Length > 4)
                return mae + "-" + ato + planNo.Substring(4);
            else
                return mae + "-" + ato;
        }
    }
}
