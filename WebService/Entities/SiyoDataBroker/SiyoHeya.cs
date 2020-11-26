using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public class SiyoHeya
    {
        public string ConstructionCode { get; set; }
        public int Floor { get; set; }
        public string RoomCode { get; set; }
        public string RoomName { get; set; }

        public static List<SiyoHeya> Get(string constructionCode, int siyoCode)
        {
            var rooms = new List<SiyoHeya>();
            if (ConstructionSchedule.IsBeforeProcessRequest(constructionCode))
            {
                var heyas = tbl_siyo_heya.Get(constructionCode, siyoCode);

                foreach (var heya in heyas)
                {
                    var siyoheya = new SiyoHeya();
                    siyoheya.ConstructionCode = heya.customerCode;
                    siyoheya.Floor = heya.floorNum;
                    siyoheya.RoomCode = heya.roomCd;
                    siyoheya.RoomName = heya.roomName;

                    rooms.Add(siyoheya);
                }
            }
            else
            {
                var heyas = RoomLayout.Get(constructionCode);

                foreach (var heya in heyas)
                {
                    var siyoheya = new SiyoHeya();
                    siyoheya.ConstructionCode = heya.ConstructionCode;
                    siyoheya.Floor = heya.Floor;
                    siyoheya.RoomCode = heya.RoomCode;
                    siyoheya.RoomName = heya.RoomName;

                    rooms.Add(siyoheya);
                }
            }

            return rooms;
        }
    }
}
