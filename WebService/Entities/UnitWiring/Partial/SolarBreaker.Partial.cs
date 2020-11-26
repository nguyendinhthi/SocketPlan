using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class SolarBreaker
    {
        public static List<SolarBreaker> GetBreaker(decimal main, decimal pcSize)
        {
            var db = SolarBreaker.GetDatabase();

            var sql = @"SELECT * FROM SolarBreakers WHERE MainAmpereFrom <= " + main + @" AND
                        MainAmpereTo >= " + main + @" AND
                        PcSize = " + pcSize;

            return db.ExecuteQuery<SolarBreaker>(sql);
        }
    }
}
