using System;
using System.Collections.Generic;
using System.Text;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public class HouseObject : CadObject
    {
        public HouseObject(int objectId, int floor) : base(objectId, floor)
        {
        }

        public static List<HouseObject> GetAll(int floor)
        {
            var list = new List<HouseObject>();

            var ids = Filters.GetHouseOutlineIds();
            foreach (var id in ids)
            {
                list.Add(new HouseObject(id, floor));
            }

            return list;
        }

        public static void DrawHouseOutline()
        {
            WindowController2.BringAutoCadToTop();
            AutoCad.Command.Prepare();
            AutoCad.Command.SetCurrentLayoutToModel();

            AutoCad.Db.LayerTableRecord.Make(Const.Layer.電気_外周, CadColor.Cyan, Const.LineWeight._0_50);
            AutoCad.Command.SetCurrentLayer(Const.Layer.電気_外周);

            AutoCad.Command.DrawPolyline();
        }

    }
}
