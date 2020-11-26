using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlanMasterMaintenance.SocketPlanServiceReference
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
