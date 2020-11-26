using System;
namespace SocketPlan.WebService
{
    public partial class HouseConstructionMaterial
    {
        public static bool IsUnitWiring(string constructionCode)
        {
            var sql = string.Empty;

            sql = @"SELECT ConstructionCode 
                    FROM HouseConstructionMaterials
                    WHERE ConstructionCode = '" + constructionCode + @"'
                        AND ConstructionMaterialCode = 'UW'
                        AND ConstructionMaterialDetailCode IN (1,2)";

            var dataBase = HouseConstructionMaterial.GetDatabase();
            var code = dataBase.ExecuteScalar(sql);

            if (code == null)
                return false;

            if(string.IsNullOrEmpty(code.ToString()))
                return false;

            return true;
        }
    }
}
