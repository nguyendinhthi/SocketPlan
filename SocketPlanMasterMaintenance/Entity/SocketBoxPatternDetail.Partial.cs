using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlanMasterMaintenance.SocketPlanServiceReference
{
    public partial class SocketBoxPatternDetail
    {
        public List<SocketBoxDetailComment> CommentsList
        {
            get
            {
                if (this.Comments == null)
                    return new List<SocketBoxDetailComment>();

                return this.Comments.ToList();
            }
            set
            { this.Comments = value.ToArray(); }
        }
    }
}
