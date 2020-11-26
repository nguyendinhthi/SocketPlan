using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketPlan.WebService
{
    public partial class SocketBoxSpecificCategory
    {
        private List<SocketBoxSpecific> specifics = new List<SocketBoxSpecific>();
        public List<SocketBoxSpecific> Specifics
        {
            get { return this.specifics; }
            set { this.specifics = value; }
        }

        public static int GetNewId()
        {
            var sql = @"SELECT MAX(Id) FROM SocketBoxSpecificCategories";
            var db = SocketBoxSpecificCategory.GetDatabase();
            var obj = db.ExecuteScalar(sql);

            if (obj == null || obj == DBNull.Value)
                return 1;

            return (int)obj + 1;
        }
    }
}
