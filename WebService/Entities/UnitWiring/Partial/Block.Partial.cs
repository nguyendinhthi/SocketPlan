using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class Block
    {
        public static bool Exists(string name)
        {
            return Block.Find(name) != null;
        }

        public static Block Find(string name)
        {
            var db = Block.GetDatabase();

            var list = db.ExecuteQuery<Block>("SELECT * FROM Blocks WHERE Name = '" + name + "'");

            if (list.Count == 0)
                return null;
            else
                return list[0];
        }

        public static void Delete(int blockId)
        {
            var db = Block.GetDatabase();
            db.ExecuteNonQuery("DELETE FROM Blocks WHERE Id =" + blockId);
        }
    }
}
