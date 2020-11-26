namespace SocketPlan.WinUI.SocketPlanServiceReference
{
    public partial class SocketPlanServiceNoTimeout : SocketPlanService
    {
        public SocketPlanServiceNoTimeout() : base()
        {
            this.Timeout = 600000; //＝600秒＝10分
        }
    }
}