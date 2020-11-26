using System;
using System.Collections.Generic;
using System.Text;

namespace SocketPlan.WinUI.SocketPlanServiceReference
{
    public partial class SocketBoxPatternDetail
    {
        public List<SocketBoxDetailComment> CommentsList
        {
            get
            {
                if (this.Comments == null)
                    return new List<SocketBoxDetailComment>();

                return new List<SocketBoxDetailComment>(this.Comments);
            }
            set
            { this.Comments = value.ToArray(); }
        }

        public string EquipmentName
        {
            get
            {
                if (this.Equipment == null)
                    return string.Empty;

                return this.Equipment.Name;
            }
        }
    }
}
