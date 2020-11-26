using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class Comment
    {
        private List<Specification> specifications = new List<Specification>();
        public List<Specification> Specifications
        {
            get { return this.specifications; }
            set { this.specifications = value; }
        }

        private List<CommentStandardRoom> commentStandardRooms = new List<CommentStandardRoom>();
        public List<CommentStandardRoom> CommentStandardRooms
        {
            get { return this.commentStandardRooms; }
            set { this.commentStandardRooms = value; }
        }



        public static void DeleteAll()
        {
            var db = Comment.GetDatabase();
            db.ExecuteNonQuery("DELETE FROM Comments");
        }
    }
}
