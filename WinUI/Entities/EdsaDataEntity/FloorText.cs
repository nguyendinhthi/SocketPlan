using System;
using System.Collections.Generic;
using System.Text;

namespace SocketPlan.WinUI.SocketPlanServiceReference
{
    public partial class FloorText
    {
        public bool IsKitchenSerial
        {
            get
            {
                return this.SpecificationId == Const.Specification.テキスト_キッチンシリアル;
            }
        }
    }
}
