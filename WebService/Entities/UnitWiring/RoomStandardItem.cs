namespace SocketPlan.WebService
{
    using System;
    using System.Collections.Generic;

    public class RoomStandardItem
    {
        public int RoomId { get; set; }

        public decimal JyouConditionLower { get; set; }

        public decimal? JyouConditionUpper { get; set; }

        public string ItemName { get; set; }

        public int? Quantity { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public static List<RoomStandardItem> GetAll(string constructionCode, string planNo)
        {
            var items = new List<RoomStandardItem>();

            bool isUsingNew;
            if (string.IsNullOrEmpty(constructionCode) || string.IsNullOrEmpty(planNo))
            {
                isUsingNew = false;
            }
            else
            {
                var siyoCode = tbl_siyo_boss.GetSiyoCode(constructionCode, planNo);
                isUsingNew = IsUsingNew(constructionCode, siyoCode);
            }

            if (isUsingNew)
            {
                var newItems = RoomStandardNewItem.GetAll();
                newItems.ForEach(p => items.Add(Convert(p)));
            }
            else
            {
                var oldItems = RoomStandardOldItem.GetAll();
                oldItems.ForEach(p => items.Add(Convert(p)));
            }

            return items;
        }

        public static List<RoomStandardItem> GetAll(bool isUsingNew)
        {
            var items = new List<RoomStandardItem>();
            if (isUsingNew)
            {
                var newItems = RoomStandardNewItem.GetAll();
                newItems.ForEach(p => items.Add(Convert(p)));
            }
            else
            {
                var oldItems = RoomStandardOldItem.GetAll();
                oldItems.ForEach(p => items.Add(Convert(p)));
            }

            return items;
        }

        public static List<RoomStandardItem> GetAll(bool isUsingNew, int roomId)
        {
            var items = new List<RoomStandardItem>();
            if (isUsingNew)
            {
                var newItems = RoomStandardNewItem.Get(roomId);
                newItems.ForEach(p => items.Add(Convert(p)));
            }
            else
            {
                var oldItems = RoomStandardOldItem.Get(roomId);
                oldItems.ForEach(p => items.Add(Convert(p)));
            }

            return items;
        }

        public static void Delete(int roomId, bool isUsingNew)
        {
            if (isUsingNew)
            {
                var db = RoomStandardNewItem.GetDatabase();
                db.ExecuteNonQuery("DELETE FROM RoomStandardNewItems WHERE RoomId = " + roomId);
            }
            else
            {
                var db = RoomStandardOldItem.GetDatabase();
                db.ExecuteNonQuery("DELETE FROM RoomStandardOldItems WHERE RoomId = " + roomId);
            }
        }

        private static bool IsUsingNew(string constructionCode, int siyoCode)
        {
            if (BasicSpecificationDetail.IsIPalette(constructionCode, siyoCode))
                return false;

            return Construction.IsNewStandardQtyRule(constructionCode);
        }

        private static RoomStandardItem Convert(RoomStandardNewItem souce)
        {
            var item = new RoomStandardItem();
            item.RoomId = souce.RoomId;
            item.ItemName = souce.ItemName;
            item.JyouConditionLower = souce.JyouConditionLower;
            item.JyouConditionUpper = souce.JyouConditionUpper;
            item.Quantity = souce.Quantity;
            item.UpdatedDateTime = souce.UpdatedDateTime;

            return item;
        }

        private static RoomStandardItem Convert(RoomStandardOldItem souce)
        {
            var item = new RoomStandardItem();
            item.RoomId = souce.RoomId;
            item.ItemName = souce.ItemName;
            item.JyouConditionLower = souce.JyouConditionLower;
            item.JyouConditionUpper = souce.JyouConditionUpper;
            item.Quantity = souce.Quantity;
            item.UpdatedDateTime = souce.UpdatedDateTime;

            return item;
        }

        public void Store(bool isUsingNew)
        {
            if (isUsingNew)
            {
                var newItem = ConvertToNew(this);
                newItem.Store();
            }
            else
            {
                var oldItem = ConvertToOld(this);
                oldItem.Store();
            }
        }

        private static RoomStandardNewItem ConvertToNew(RoomStandardItem souce)
        {
            var newItem = new RoomStandardNewItem();
            newItem.RoomId = souce.RoomId;
            newItem.JyouConditionLower = souce.JyouConditionLower;
            newItem.JyouConditionUpper = souce.JyouConditionUpper;
            newItem.ItemName = souce.ItemName;
            newItem.Quantity = souce.Quantity;
            newItem.UpdatedDateTime = souce.UpdatedDateTime;

            return newItem;
        }

        private static RoomStandardOldItem ConvertToOld(RoomStandardItem souce)
        {
            var oldItem = new RoomStandardOldItem();
            oldItem.RoomId = souce.RoomId;
            oldItem.JyouConditionLower = souce.JyouConditionLower;
            oldItem.JyouConditionUpper = souce.JyouConditionUpper;
            oldItem.ItemName = souce.ItemName;
            oldItem.Quantity = souce.Quantity;
            oldItem.UpdatedDateTime = souce.UpdatedDateTime;

            return oldItem;
        }
    }
}
