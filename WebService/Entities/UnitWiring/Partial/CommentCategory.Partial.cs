using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class CommentCategory
    {
        private List<Comment> comments = new List<Comment>();
        public List<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public static void Update(List<CommentCategory> categories)
        {
            //必要なプロパティを埋める
            int categoryId = 1;
            int commentId = 1;
            foreach (var category in categories)
            {
                category.Id = categoryId;
                category.SortNo = categoryId;

                int commentSortNo = 1;
                foreach (var comment in category.Comments)
                {
                    comment.Id = commentId;
                    comment.SortNo = commentSortNo;
                    comment.CategoryId = categoryId;

                    commentId++;
                    commentSortNo++;
                }

                categoryId++;
            }

            //全消し全書き
            CommentSpecification.DeleteAll();
            Comment.DeleteAll();
            CommentCategory.DeleteAll();
            CommentStandardRoom.DeleteAll();
            
            foreach (var category in categories)
            {
                category.Store();

                foreach (var comment in category.Comments)
                {
                    comment.Store();

                    foreach (var spec in comment.Specifications)
                    {
                        var commentSpec = new CommentSpecification();
                        commentSpec.CommentId = comment.Id;
                        commentSpec.SpecificationId = spec.Id;
                        commentSpec.UpdatedDateTime = comment.UpdatedDateTime;

                        commentSpec.Store();
                    }

                    foreach (var roomName in comment.CommentStandardRooms)
                    {
                        var commentStadard = new CommentStandardRoom();
                        commentStadard.CommentId = comment.Id;
                        commentStadard.StandardRoomName = roomName.StandardRoomName;
                        commentStadard.UpdatedDateTime = comment.UpdatedDateTime;

                        commentStadard.Store();
                    }
                }
            }
        }

        public static void DeleteAll()
        {
            var db = CommentCategory.GetDatabase();
            db.ExecuteNonQuery("DELETE FROM CommentCategories");
        }
    }
}
