using System;
using System.Collections.Generic;
using System.Text;

namespace SocketPlan.WinUI
{
    public class CadObject
    {
        public int ObjectId { get; set; }
        public int Floor { get; set; }

        public CadObject()
        {

        }

        public CadObject(int objectId, int floor)
            : this()
        {
            this.ObjectId = objectId;
            this.Floor = floor;
        }
    }
}
