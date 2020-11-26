using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class RoomStandardOldItem
    {
        private List<RoomStandardOldItem> standardOldItems = new List<RoomStandardOldItem>();
        public List<RoomStandardOldItem> StandardOldItems
        {
            get
            {
                return this.standardOldItems;
            }

            set
            {
                this.standardOldItems = value;
            }
        }

        public static List<RoomStandardOldItem> Get(int roomId)
        {
            string sql = @"
                SELECT * FROM RoomStandardOldItems
                WHERE
                    RoomId = '" + roomId + @"'
                ORDER BY
                    JyouConditionLower,
                    JyouConditionUpper";

            var db = RoomStandardOldItem.GetDatabase();
            return db.ExecuteQuery<RoomStandardOldItem>(sql);
        }

    }
}
