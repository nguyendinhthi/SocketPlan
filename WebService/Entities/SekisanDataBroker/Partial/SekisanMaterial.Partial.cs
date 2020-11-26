//using System.Collections.Generic;
//using System;
//namespace SocketPlan.WebService
//{
//    public partial class SekisanMaterial
//    {
//        public const int SYSTEM_NO = 21;

//        public SekisanMaterial()
//        {
//        }

//        public SekisanMaterial(SekisanItem sekisanItem, string constructionCode, string planNo, string revisionNo) : this()
//        {
//            this.Id = sekisanItem.Id;
//            this.ConstructionCode = constructionCode;
//            this.PlanNo = planNo;
//            this.ShiyoushoNo = revisionNo;
//            this.BasicGradeClassificationCode = sekisanItem.BasicGradeClassificationCode;
//            this.BasicGradeClassificationName = sekisanItem.BasicGradeClassificationName;
//            this.FirstGradeClassificationCode = sekisanItem.FirstGradeClassificationCode;
//            this.FirstGradeClassificationName = sekisanItem.FirstGradeClassificationName;
//            this.SecondGradeClassificationCode = sekisanItem.SecondGradeClassificationCode;
//            this.SecondGradeClassificationName = sekisanItem.SecondGradeClassificationName;
//            this.ConstructionItemType = sekisanItem.ConstructionItemType;
//            this.ConstructionItemCode = sekisanItem.ConstructionItemCode;
//            this.ConstructionItemNo = sekisanItem.ConstructionItemNo;

//            var item = Item.Get(this.ConstructionItemCode);
//            this.ConstructionItemName = item.ItemName;
//            this.ConstructionItemName2 = item.ItemName2;
//            this.UnitCode = item.UnitCode;

//            if (this.UnitCode != null)
//            {
//                var unit = Unit.Get(this.UnitCode);
//                this.UnitName = unit.UnitName;
//            }

//            this.ItemKindName = null;
//            this.ServiceType = 0;
//            this.Remarks = "HRD";
//            this.RenewId = "HRD";
//            this.SentFlag = false; //WebServiceで送信後、trueに変わる
//            this.SystemNo = SYSTEM_NO;

//            if (string.IsNullOrEmpty(sekisanItem.ItemCode))
//                this.Quantity = 0; //Detailがない時は数量を入れる
//            else
//                this.Quantity = 1; //Detailがある時は1固定
//        }

//        public int Id { get; set; }

//        private List<SekisanMaterialDetail> details = new List<SekisanMaterialDetail>();
//        public List<SekisanMaterialDetail> Details 
//        {
//            get
//            {
//                return this.details;
//            }

//            set
//            {
//                this.details = value;
//            }
//        }

//        public static void Update(List<SekisanMaterial> materials, string constructionCode, string planNo, string revisionNo)
//        {
//            var now = DateTime.Now;

//            SekisanMaterialDetail.Delete(constructionCode, planNo, revisionNo);
//            SekisanMaterial.Delete(constructionCode, planNo, revisionNo);

//            foreach (var material in materials)
//            {
//                material.UpdatedDate = now;
//                material.StoreSimple();

//                foreach (var detail in material.Details)
//                {
//                    detail.UpdatedDate = now;
//                    detail.Store();
//                }
//            }
//        }

//        public static void Delete(string constructionCode, string planNo, string revisionNo)
//        {
//            string sql = @"
//                DELETE FROM SekisanMaterials
//                WHERE
//                    ConstructionCode = '" + constructionCode + @"' AND
//                    PlanNo = '" + planNo + @"' AND
//                    ShiyoushoNo = '" + revisionNo + @"' AND
//                    SystemNo = " + SYSTEM_NO;

//            var db = SekisanMaterial.GetDatabase();
//            db.ExecuteNonQuery(sql);
//        }

//        public static List<SekisanMaterial> Get(string constructionCode, string planNo, string revisionNo)
//        {
//            var db = SekisanMaterial.GetDatabase();
//            var sql = @"
//                SELECT * FROM SekisanMaterials
//                WHERE
//                    ConstructionCode = '" + constructionCode + @"' AND
//                    PlanNo           = '" + planNo + @"' AND
//                    ShiyoushoNo      = '" + revisionNo + @"' AND
//                    SystemNo         =  " + SYSTEM_NO;

//            var materials = db.ExecuteQuery<SekisanMaterial>(sql);
//            return materials;
//        }
//    }
//}
