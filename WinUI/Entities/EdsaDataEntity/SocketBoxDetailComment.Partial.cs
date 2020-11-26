using System;
using System.Collections.Generic;
using System.Text;

namespace SocketPlan.WinUI.SocketPlanServiceReference
{
    public partial class SocketBoxDetailComment
    {
        public string CommentText
        {
            get
            {
                if (this.Comment == null)
                    return string.Empty;

                return this.Comment.Text;
            }
        }
    }
}
