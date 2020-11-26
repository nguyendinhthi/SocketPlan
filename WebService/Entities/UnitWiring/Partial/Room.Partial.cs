using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class Room
    {
        private List<RoomStandardItem> standardItems = new List<RoomStandardItem>();
        public List<RoomStandardItem> StandardItems
        {
            get
            {
                return this.standardItems;
            }

            set
            {
                this.standardItems = value;
            }
        }

        public void Store(bool isUsingNew)
        {
            var now = DateTime.Now;

            this.UpdatedDateTime = now;
            base.Store();

            RoomStandardItem.Delete(this.Id, isUsingNew);
            if (this.StandardItems != null)
            {
                foreach (var item in this.StandardItems)
                {
                    item.RoomId = this.Id;
                    item.UpdatedDateTime = now;
                    item.Store(isUsingNew);
                }
            }
        }

        public override void Drop()
        {
            RoomStandardItem.Delete(this.Id, true);
            RoomStandardItem.Delete(this.Id, false);
            base.Drop();
        }
    }
}
