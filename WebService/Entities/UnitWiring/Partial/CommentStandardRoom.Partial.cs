using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class CommentStandardRoom
    {

        public static void DeleteAll()
        {
            var db = CommentSpecification.GetDatabase();
            db.ExecuteNonQuery("DELETE FROM CommentStandardRooms");
        }
    }
}
