namespace SocketPlan.WebService
{
    public partial class HemsItemDetail
    {
        public static void Delete(string constructionCode)
        {
            string sql = @"
                DELETE FROM HemsItemDetails
                WHERE
                    ConstructionCode = '" + constructionCode + @"'";

            var db = HemsItemDetail.GetDatabase();
            db.ExecuteNonQuery(sql);
        }
    }
}
