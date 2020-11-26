using System.Collections.Generic;
using System;
namespace SocketPlan.WebService
{
    public partial class SekisanMaterial
    {
        public const int SYSTEM_NO = 21;

        public SekisanMaterial()
        {
        }

        private List<SekisanMaterialDetail> details = new List<SekisanMaterialDetail>();
        public List<SekisanMaterialDetail> Details
        {
            get
            {
                return this.details;
            }

            set
            {
                this.details = value;
            }
        }

        public static void Update(SekisanMaterial material)
        {
            var now = DateTime.Now;

            SekisanMaterialDetail.Delete(material.ConstructionCode, material.PlanNo, material.ShiyoushoNo);
            SekisanMaterial.Delete(material.ConstructionCode, material.PlanNo, material.ShiyoushoNo);

            if (material.Details.Count == 0)
                return;

            material.UpdatedDate = now;
            material.StoreSimple();

            foreach (var detail in material.Details)
            {
                detail.UpdatedDate = now;
                detail.StoreSimple();
            }
        }

        public static void Delete(string constructionCode, string planNo, string revisionNo)
        {
            string sql = @"
                DELETE FROM SekisanMaterials
                WHERE
                    ConstructionCode = '" + constructionCode + @"' AND
                    PlanNo = '" + planNo + @"' AND
                    ShiyoushoNo = '" + revisionNo + @"' AND
                    SystemNo = " + SYSTEM_NO;

            var db = SekisanMaterial.GetDatabase();
            db.ExecuteNonQuery(sql);
        }

        public static List<SekisanMaterial> Get(string constructionCode, string planNo, string revisionNo)
        {
            var db = SekisanMaterial.GetDatabase();
            var sql = @"
                SELECT * FROM SekisanMaterials
                WHERE
                    ConstructionCode = '" + constructionCode + @"' AND
                    PlanNo           = '" + planNo + @"' AND
                    ShiyoushoNo      = '" + revisionNo + @"' AND
                    SystemNo         =  " + SYSTEM_NO;

            var materials = db.ExecuteQuery<SekisanMaterial>(sql);
            return materials;
        }
    }
}
