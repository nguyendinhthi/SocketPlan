namespace SocketPlan.WebService
{
    public partial class InteriorEstimateRoom
    {
        public static void DeleteAll()
        {
            var db = InteriorEstimateRoom.GetDatabase();
            db.ExecuteNonQuery("DELETE FROM InteriorEstimateRooms");
        }
    }
}
