namespace SocketPlan.WebService
{
    public partial class TextItem
    {
        public static void DeleteAll()
        {
            var db = TextItem.GetDatabase();
            db.ExecuteNonQuery("DELETE FROM TextItems");
        }
    }
}
