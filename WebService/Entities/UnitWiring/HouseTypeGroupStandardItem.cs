using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public class HouseTypeGroupStandardItem
    {
        public int HouseTypeGroupId { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public DateTime UpdatedDateTime { get; set; }

        public static List<HouseTypeGroupStandardItem> Get(string constructionCode, string planNo)
        {
            var detail = HouseTypeGroupDetail.Get(constructionCode, planNo);
            var items = HouseTypeGroupStandardItem.Get(constructionCode, detail.HouseTypeGroupId, planNo);

            return items;
        }

        private static List<HouseTypeGroupStandardItem> Get(string constructionCode, int houseTypeGroupId, string planNo)
        {
            var items = new List<HouseTypeGroupStandardItem>();
            var siyoCode = tbl_siyo_boss.GetSiyoCode(constructionCode, planNo);

            if (IsUsingNew(constructionCode, siyoCode))
            {
                var newItems = HouseTypeGroupStandardItems_New.Get(houseTypeGroupId);
                newItems.ForEach(p => items.Add(Convert(p)));
            }
            else
            {
                var oldItems = HouseTypeGroupStandardItems_Old.Get(houseTypeGroupId);
                oldItems.ForEach(p => items.Add(Convert(p)));

                var isHotaru0 = BasicSpecificationDetail.IsISmartICubeISmileIPaletteIHead4(constructionCode, siyoCode);

                if (isHotaru0)
                {
                    var hotaruItems = items.FindAll(p => p.ItemName == "House_ƒzƒ^ƒ‹");
                    hotaruItems.ForEach(p => p.Quantity = 0);
                }
            }

            return items;
        }

        private static bool IsUsingNew(string constructionCode, int siyoCode)
        {
            if (BasicSpecificationDetail.IsIPalette(constructionCode, siyoCode))
                return false;

            return Construction.IsNewStandardQtyRule(constructionCode);
        }

        private static HouseTypeGroupStandardItem Convert(HouseTypeGroupStandardItems_New source)
        {
            var item = new HouseTypeGroupStandardItem();
            item.HouseTypeGroupId = source.HouseTypeGroupId;
            item.ItemName = source.ItemName;
            item.Quantity = source.Quantity;
            item.UpdatedDateTime = source.UpdatedDateTime;
            
            return item;
        }

        private static HouseTypeGroupStandardItem Convert(HouseTypeGroupStandardItems_Old source)
        {
            var item = new HouseTypeGroupStandardItem();
            item.HouseTypeGroupId = source.HouseTypeGroupId;
            item.ItemName = source.ItemName;
            item.Quantity = source.Quantity;
            item.UpdatedDateTime = source.UpdatedDateTime;

            return item;
        }
    }


}
