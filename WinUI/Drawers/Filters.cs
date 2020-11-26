using System;
using System.Collections.Generic;
using System.Text;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    /// <summary>
    /// UnitWiring固有のFilter処理をまとめたクラス
    /// </summary>
    public class Filters
    {
        public static List<int> GetSpecificSymbolIds()
        {
            Filter filter = new Filter();
            filter.Add(new FilterOption.LayerName(Const.Layer.電気_SocketPlan_Specific));
            return filter.Execute();
        }

        public static List<int> GetConnectorIds()
        {
            Filter filter = new Filter();
            filter.Add(new FilterOption.LayerName(Const.Layer.電気_コネクタ));
            return filter.Execute();
        }

        public static List<int> GetClipLeaderIds()
        {
            Filter filter = new Filter();
            filter.Add(new FilterOption.LayerName(Const.Layer.電気_抜き出し));
            filter.Add(FilterOption.ObjectType.Leader);
            return filter.Execute();
        }

        public static List<int> GetClipRectangleIds()
        {
            Filter filter = new Filter();
            filter.Add(new FilterOption.LayerName(Const.Layer.電気_抜き出し));
            filter.Add(FilterOption.ObjectType.Polyline);
            filter.Add(FilterOption.Color.Orange);
            return filter.Execute();
        }

        public static List<int> GetRimokonNichePlateIds()
        {
            Filter filter = new Filter();
            filter.Add(new FilterOption.LayerName(Const.Layer.電気_リモコンニッチプレート));
            filter.Add(FilterOption.ObjectType.Polyline);
            return filter.Execute();
        }

        public static List<int> GetWireIds()
        {
            Filter filter = new Filter();
            filter.Add(FilterOption.ObjectType.Polyline);
            filter.Add(new FilterOption.LayerName(Const.Layer.電気_配線));
            var wireIds = filter.Execute();

            Filter filter2 = new Filter();
            filter2.Add(FilterOption.ObjectType.Polyline);
            filter2.Add(new FilterOption.LayerName(Const.Layer.電気_配線_非表示用));
            wireIds.AddRange(filter2.Execute());

            return wireIds;
        }

        public static List<int> GetDenkiWireIds()
        {
            Filter filter = new Filter();
            filter.Add(FilterOption.ObjectType.Polyline);
            filter.Add(new FilterOption.LayerName(Const.Layer.電気_電気図面配線));
            return filter.Execute();
        }

        public static List<int> GetKansenWireIds()
        {
            Filter filter = new Filter();
            filter.Add(FilterOption.ObjectType.Polyline);
            filter.Add(new FilterOption.LayerName(Const.Layer.電気_幹線));
            return filter.Execute();
        }

        public static List<int> GetSignalWireIds()
        {
            Filter filter = new Filter();
            filter.Add(FilterOption.ObjectType.Polyline);
            filter.Add(new FilterOption.LayerName(Const.Layer.Signal_Wire));
            return filter.Execute();
        }

        public static List<int> GetJboxWireIds()
        {
            Filter filter = new Filter();
            filter.Add(FilterOption.ObjectType.Polyline);
            filter.Add(new FilterOption.LayerName(Const.Layer.電気_JboxWire));
            return filter.Execute();
        }

        public static List<int> GetRedLineIds()
        {
            var filter = new Filter();
            filter.Add(FilterOption.ObjectType.Polyline);
            filter.Add(FilterOption.Color.Red);
            return filter.Execute();
        }

        public static List<int> GetPlateIds()
        {
            var filter = new Filter();
            filter.Add(FilterOption.ObjectType.Ellispe);
            filter.Add(new FilterOption.LayerName(Const.Layer.電気_プレート));
            return filter.Execute();
        }

        public static List<int> GetHouseOutlineIds()
        {
            Filter filter = new Filter();
            filter.Add(FilterOption.ObjectType.Polyline);
            filter.Add(new FilterOption.LayerName(Const.Layer.電気_外周));
            return filter.Execute();
        }

        public static List<int> GetRoomOutlineIds()
        {
            var list = new List<int>();

            {
                Filter filter = new Filter();
                filter.Add(FilterOption.ObjectType.Polyline);
                filter.Add(new FilterOption.LayerName(Const.Layer.電気_部屋));
                list.AddRange(filter.Execute());
            }

            {
                Filter filter = new Filter();
                filter.Add(FilterOption.ObjectType.Polyline);
                filter.Add(new FilterOption.LayerName(Const.Layer.電気_部屋_WithJyou));
                list.AddRange(filter.Execute());
            }

            {
                Filter filter = new Filter();
                filter.Add(FilterOption.ObjectType.Polyline);
                filter.Add(new FilterOption.LayerName(Const.Layer.電気_部屋_WithoutJyou));
                list.AddRange(filter.Execute());
            }

            return list;
        }

        public static List<int> GetNisetaiOutlineIds()
        {
            Filter filter = new Filter();
            filter.Add(FilterOption.ObjectType.Polyline);
            filter.Add(new FilterOption.LayerName(Const.Layer.電気_Kairo_Nisetai));
            return filter.Execute();
        }

        public static List<int> GetCeilingReceiverIds(string layerName)
        {
            Filter filter = new Filter();

            filter.Add(FilterOption.ObjectType.Polyline);
            filter.Add(new FilterOption.LayerName(layerName));
            filter.Add(new FilterOption.LineType(Const.Linetype.CeilingReceiver));
            return filter.Execute();
        }

        public static List<int> GetCeilingPanelLineIds(string layerName)
        {
            Filter filter = new Filter();

            filter.Add(FilterOption.ObjectType.Polyline);
            filter.Add(new FilterOption.LayerName(layerName));
            filter.Add(new FilterOption.LineType(Const.Linetype.CeilingPanel));
            return filter.Execute();
        }

        public static List<int> GetLeaderIds()
        {
            Filter filter = new Filter();
            filter.Add(FilterOption.ObjectType.Leader);
            return filter.Execute();
        }

        public static List<int> GetAll(string layerName)
        {
            Filter filter = new Filter();
            filter.Add(new FilterOption.LayerName(layerName));
            return filter.Execute();
        }
    }
}
