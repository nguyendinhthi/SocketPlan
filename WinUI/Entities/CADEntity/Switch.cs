using System;
using System.Collections.Generic;
using System.Text;
using SocketPlan.WinUI.SocketPlanServiceReference;

namespace SocketPlan.WinUI
{
    public enum SwitchType
    {
        None,
        Single,
        Double
    }

    public class Switch
    {
        public SwitchType SwitchType { get; set; }
        public int Vertical { get; set; }
        public Symbol Symbol { get; set; }

        public Switch()
        {
        }
    }
}
