using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketPlan.WebService
{
    public partial class SocketBoxCategory
    {
        private List<SocketBoxPattern> patterns = new List<SocketBoxPattern>();
        public List<SocketBoxPattern> Patterns
        {
            get { return this.patterns; }
            set { this.patterns = value; }
        }

        public static int GetNewId()
        {
            var sql = @"SELECT MAX(Id) FROM SocketBoxCategories";
            var db = SocketBoxCategory.GetDatabase();
            var obj = db.ExecuteScalar(sql);

            if (obj == null || obj == DBNull.Value)
                return 1;

            return (int)obj + 1;
        }

        public static void Delete(int categoryId)
        {
            var sql = @"DELETE FROM SocketBoxCategories WHERE Id = " + categoryId;
            var db = SocketBoxCategory.GetDatabase();
            db.ExecuteNonQuery(sql);
        }
    }
}
