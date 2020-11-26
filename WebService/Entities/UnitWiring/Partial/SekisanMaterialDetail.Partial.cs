namespace SocketPlan.WebService
{
    public partial class SekisanMaterialDetail
    {
        public SekisanMaterialDetail()
        {
        }

        public static void Delete(string constructionCode, string planNo, string revisionNo)
        {
            var materials = SekisanMaterial.Get(constructionCode, planNo, revisionNo);

            var db = SekisanMaterialDetail.GetDatabase();

            foreach (var material in materials)
            {
                string sql = @"
                DELETE FROM SekisanMaterialDetails
                WHERE
                    ConstructionCode     = '" + constructionCode + @"' AND
                    PlanNo               = '" + planNo + @"' AND
                    ShiyoushoNo          = '" + revisionNo + @"' AND
                    ConstructionItemType =  " + material.ConstructionItemType + @" AND
                    ConstructionItemCode = '" + material.ConstructionItemCode + @"' AND
                    ConstructionItemNo   =  " + material.ConstructionItemNo;

                db.ExecuteNonQuery(sql);
            }
        }
    }
}
