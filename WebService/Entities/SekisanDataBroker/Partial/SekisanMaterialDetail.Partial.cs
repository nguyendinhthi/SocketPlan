//namespace SocketPlan.WebService
//{
//    public partial class SekisanMaterialDetail
//    {
//        public SekisanMaterialDetail()
//        {
//        }

//        public SekisanMaterialDetail(SekisanItem sekisanItem, string constructionCode, string planNo, string revisionNo) : this()
//        {
//            this.ConstructionCode = constructionCode;
//            this.PlanNo = planNo;
//            this.ShiyoushoNo = revisionNo;
//            this.ConstructionItemType = sekisanItem.ConstructionItemType;
//            this.ConstructionItemCode = sekisanItem.ConstructionItemCode;
//            this.ConstructionItemNo = sekisanItem.ConstructionItemNo;
//            this.ItemCode = sekisanItem.ItemCode;

//            var item = Item.Get(this.ItemCode);
//            this.ItemName = item.ItemName;
//            this.ItemName2 = item.ItemName2;
//            this.UnitCode = item.UnitCode;

//            if (this.UnitCode != null)
//            {
//                var unit = Unit.Get(this.UnitCode);
//                this.UnitName = unit.UnitName;
//            }

//            this.Floor = null;
//            this.RoomCode = null;
//            this.RoomName = null;
//            this.Remarks = "HRD";
//            this.RenewId = "HRD";

//            this.Quantity = 0;
//        }

//        public static void Delete(string constructionCode, string planNo, string revisionNo)
//        {
//            var materials = SekisanMaterial.Get(constructionCode, planNo, revisionNo);

//            var db = SekisanMaterialDetail.GetDatabase();

//            foreach (var material in materials)
//            {
//                string sql = @"
//                DELETE FROM SekisanMaterialDetails
//                WHERE
//                    ConstructionCode     = '" + constructionCode + @"' AND
//                    PlanNo               = '" + planNo + @"' AND
//                    ShiyoushoNo          = '" + revisionNo + @"' AND
//                    ConstructionItemType =  " + material.ConstructionItemType + @" AND
//                    ConstructionItemCode = '" + material.ConstructionItemCode + @"' AND
//                    ConstructionItemNo   =  " + material.ConstructionItemNo;

//                db.ExecuteNonQuery(sql);
//            }
//        }
//    }
//}
