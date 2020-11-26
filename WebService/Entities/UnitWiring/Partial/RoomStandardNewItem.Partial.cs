using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class RoomStandardNewItem
    {
        private List<RoomStandardNewItem> standardNewItems = new List<RoomStandardNewItem>();
        public List<RoomStandardNewItem> StandardNewItems
        {
            get
            {
                return this.standardNewItems;
            }

            set
            {
                this.standardNewItems = value;
            }
        }

        public static List<RoomStandardNewItem> Get(int roomId)
        {
            string sql = @"
                SELECT * FROM RoomStandardNewItems
                WHERE
                    RoomId = '" + roomId + @"'
                ORDER BY
                    JyouConditionLower,
                    JyouConditionUpper";

            var db = RoomStandardNewItem.GetDatabase();
            return db.ExecuteQuery<RoomStandardNewItem>(sql);
        }

    }
}
