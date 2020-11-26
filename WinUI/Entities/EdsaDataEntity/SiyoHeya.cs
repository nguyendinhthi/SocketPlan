using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SocketPlan.WinUI.SocketPlanServiceReference
{
    public partial class SiyoHeya
    {
        [XmlIgnore]
        public int OrderNo { get; set; }

        public SiyoHeya()
        {
        }

        public SiyoHeya(string constructionCode, int floor, string roomCode, string roomName) : this()
        {
            this.ConstructionCode = constructionCode;
            this.Floor = floor;
            this.RoomCode = roomCode;
            this.RoomName = roomName;
        }
    }
}
