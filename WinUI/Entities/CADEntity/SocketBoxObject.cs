using System;
using System.Collections.Generic;
using System.Text;
using Edsa.AutoCadProxy;
using System.Windows.Forms;
using SocketPlan.WinUI.SocketPlanServiceReference;

namespace SocketPlan.WinUI
{
    public class SocketBoxObject
    {
        public const double BOX_SCALE = 1.0d;

        public static void MoveLocation()
        {
            Initialize();

            while (true)
            {
                var boxIds = SelectSocketBox();
                if (boxIds.Count == 0)
                    return;

                var layerName = AutoCad.Db.Entity.GetLayerName(boxIds[0]);
                if (layerName == Const.Layer.電気_SocketPlan_Specific)
                {
                    //バラ品移動処理へ
                    return;
                }
                else if (layerName != Const.Layer.電気_SocketPlan)
                {
                    MessageDialog.ShowWarning("this is not a socket box.");
                    return;
                }

                int? boxId = boxIds[0];　//IsTypeだとなんかだめなので。。。
                var boxSize = AutoCad.Db.BlockReference.GetSize(boxId.Value);
                var beforePoint = AutoCad.Db.BlockReference.GetPosition(boxId.Value);
                var bounds = AutoCad.Db.BlockReference.GetBlockBound(boxId.Value);

                if (IsOSnap())
                    OffOSnap();

                var afterPoint = SocketBoxObject.GetClickPoint();
                if (afterPoint == null)
                    throw new ApplicationException(Messages.FailedToGetPoint());

                var leaderId = SocketBoxObject.GetLeaderObjectId(bounds);
                if (!leaderId.HasValue)
                    throw new ApplicationException("Failed to get leader.");

                var leaderFirstPoint = AutoCad.Db.Leader.GetFirstVertex(leaderId.Value);
                AutoCad.Db.Leader.Erase(leaderId.Value);
                AutoCad.Status.WaitFinish();
                AutoCad.Command.RefreshExEx();

                AutoCad.Command.SendLine("MOVE\n" + ToStringNoSpace(beforePoint) + "\n\n" +
                    ToStringNoSpace(beforePoint) + "\n" + ToStringNoSpace(afterPoint));
                AutoCad.Status.WaitFinish();
                AutoCad.Command.RefreshExEx();

                SocketBoxObject.MakeLeader(leaderFirstPoint, afterPoint, boxSize);
                AutoCad.Command.RefreshExEx();
            }
        }

        private static void Initialize()
        {
            WindowController2.BringAutoCadToTop();
            AutoCad.Command.Prepare();
            AutoCad.Command.SetCurrentLayoutToModel();

            AutoCad.Db.LayerTableRecord.Make(Const.Layer.電気_SocketPlan, CadColor.BlackWhite, Const.LineWeight._0_15);
            AutoCad.Command.SetCurrentLayer(Const.Layer.電気_SocketPlan);
        }

        public static void Delete()
        {
            WindowController2.BringAutoCadToTop();
            AutoCad.Command.Prepare();
            AutoCad.Command.SetCurrentLayoutToModel();

            AutoCad.Command.SendLineEsc("PICKSTYLE 1"); //グループ選択必須

            var dwg = Drawing.GetCurrent();
            var leaderIds = AutoCad.Db.Leader.GetAll(Const.Layer.電気_SocketPlan);
            leaderIds.AddRange(AutoCad.Db.Leader.GetAll(Const.Layer.電気_SocketPlan_Specific));

            var ids = SocketBoxObject.SelectSocketBoxes();
            var blockIds = new List<int>();
            List<PointD> bounds = new List<PointD>();

            //PointD bottomleft = new PointD(double.MaxValue, double.MaxValue);
            //PointD topRight = new PointD(double.MinValue, double.MinValue);
            double bottom = double.MaxValue;
            double top = double.MinValue;
            double left = double.MaxValue;
            double right = double.MinValue;

            var deleteSeqs = new List<string>();
            foreach (var id in ids)
            {
                var layerName = AutoCad.Db.Entity.GetLayerName(id);
                if (layerName != Const.Layer.電気_SocketPlan && layerName != Const.Layer.電気_SocketPlan_Specific)
                    continue;

                //Block削除
                if (AutoCad.Db.BlockReference.IsType(id))
                {
                    var socketboxAttribute = Attribute.GetAll(id);
                    var seqAttr = socketboxAttribute.Find(p => p.Tag == "seq");
                    if (seqAttr == null)
                    {
                        throw new ApplicationException("Please execute generating.");
                    }

                    var seq = seqAttr.Value.ToString();
                    if (!string.IsNullOrEmpty(seq))
                        deleteSeqs.Add(seq);

                    bounds = AutoCad.Db.BlockReference.GetBlockBound(id);

                    if (left > bounds[0].X)
                        left = bounds[0].X;
                    if (left > bounds[1].X)
                        left = bounds[1].X;

                    if (right < bounds[0].X)
                        right = bounds[0].X;
                    if (right < bounds[1].X)
                        right = bounds[1].X;

                    if (bottom > bounds[0].Y)
                        bottom = bounds[0].Y;
                    if (bottom > bounds[1].Y)
                        bottom = bounds[1].Y;

                    if (top < bounds[0].Y)
                        top = bounds[0].Y;
                    if (top < bounds[1].Y)
                        top = bounds[1].Y;

                    AutoCad.Db.BlockReference.Erase(id);
                    blockIds.Add(id);

                }
                //枠削除（バラ品のみ）
                else if (AutoCad.Db.Polyline.IsType(id))
                {
                    AutoCad.Db.Polyline.Erase(id);
                }
            }

            //LeadLine削除
            var leaderId = SocketBoxObject.GetLeaderObjectId(new List<PointD>() { new PointD(left, bottom), new PointD(right, top) }, leaderIds);
            PointD leaderStart = new PointD();
            PointD leaderEnd = new PointD();
            if (leaderId.HasValue) 
            {
                leaderStart = AutoCad.Db.Leader.GetStartPoint(Int32.Parse(leaderId.ToString()));
                leaderEnd = AutoCad.Db.Leader.GetEndPoint(Int32.Parse(leaderId.ToString()));
                AutoCad.Db.Leader.Erase(Int32.Parse(leaderId.ToString()));
            }

            //DB削除
            using (var service = new SocketPlanService())
            {
                service.DeleteSocketBoxes(Static.ConstructionCode, deleteSeqs.ToArray());
            }
            AutoCad.Command.RefreshEx();
        }

        /// <summary>
        /// ソケット選択（複数可）
        /// </summary>
        /// <returns></returns>
        private static List<int> SelectSocketBoxes()
        {
            var allGroups = AutoCad.Db.Dictionary.GetGroupIds();

            while (true)
            {
                var ids = AutoCad.Selection.SelectObjects("Select socket box DWG:");
                AutoCad.Status.WaitFinish();
                if (AutoCad.Status.IsCanceled())
                    return new List<int>();

                if (ids.Count == 0)
                {
                    MessageDialog.ShowWarning("No socket box is selected.");
                    continue;
                }
                else
                {
                    return ids;
                }
            }
        }

        private static List<int> SelectSocketBox()
        {
            var allGroups = AutoCad.Db.Dictionary.GetGroupIds();

            while (true)
            {
                var ids = AutoCad.Selection.SelectObjects("Select socket box DWG:");
                AutoCad.Status.WaitFinish();
                if (AutoCad.Status.IsCanceled())
                    return new List<int>();

                var blockIds = ids.FindAll(p => AutoCad.Db.BlockReference.IsType(p));
                if (blockIds.Count == 0)
                {
                    MessageDialog.ShowWarning("Please select just one socket box. Then, press the enter key.");
                    continue;
                }
                else
                {
                    var layerName = AutoCad.Db.Entity.GetLayerName(ids[0]);
                    if (layerName == Const.Layer.電気_SocketPlan_Specific)
                    {
                        MessageDialog.ShowWarning("Specific item is not able to be selected.");
                        continue;
                    }
                    else
                    {
                        if (blockIds.Count > 1)
                        {
                            MessageDialog.ShowWarning("Please select just one socket box. Then, press the enter key.");
                            continue;
                        }
                    }
                }
                return ids;
            }
        }

        private static bool IsOSnap()
        {
            return AutoCad.Db.Database.GetOSnapMode() == 4133;
        }

        private static void OffOSnap()
        {
            AutoCad.Db.Database.SetOSnapMode(20517);
        }

        private static PointD GetClickPoint()
        {
            AutoCad.Command.SendLineEsc("POINT");
            AutoCad.Status.WaitFinish();
            if (AutoCad.Status.IsCanceled())
                return null;

            var pointId = AutoCad.Selection.GetLastObjectId();
            if (!pointId.HasValue)
                return null;

            var point = AutoCad.Db.Point.GetPosition(pointId.Value);

            AutoCad.Db.Entity.Erase(pointId.Value);

            return point;
        }

        private static int? GetLeaderObjectId(List<PointD> bounds)
        {
            var ids = AutoCad.Db.Leader.GetAll(Const.Layer.電気_SocketPlan);
            ids.AddRange(AutoCad.Db.Leader.GetAll(Const.Layer.電気_SocketPlan_Specific));

            return GetLeaderObjectId(bounds, ids);
        }

        private static int? GetLeaderObjectId(List<PointD> bounds, List<int> ids)
        {
            foreach (var leaderId in ids)
            {
                try
                {
                    var leaderEndPosition = AutoCad.Db.Leader.GetEndPoint(leaderId);

                    if (bounds[0].X -10 <= leaderEndPosition.X && bounds[1].X + 10 >= leaderEndPosition.X &&
                        bounds[0].Y -10 <= leaderEndPosition.Y && bounds[1].Y + 10 >= leaderEndPosition.Y)
                    {
                        return leaderId;
                    }
                }
                catch
                {
                    // idsにSocktBoxのIDも入っててエラーになるので無視
                    continue;
                }
            }

            return null;
        }

        private static string ToStringNoSpace(PointD point)
        {
            return point.X.ToString("0.00") + "," + point.Y.ToString("0.00");
        }

        private static void MakeLeader(PointD first, PointD boxPosition, PointD boxSize)
        {
            PointD second;

            if (first.X < boxPosition.X)
            {
                second = boxPosition;
            }
            else
            {
                second = boxPosition.PlusX(boxSize.X);
            }

            var points = new List<PointD>() { first, second };
            AutoCad.Db.Leader.Make(points);
        }

        public static void ChangeColor()
        {
            Initialize();

            while (true)
            {
                var selectedIds = SelectSocketBox();
                if (selectedIds.Count == 0)
                    return;

                var layerName = AutoCad.Db.Entity.GetLayerName(selectedIds[0]);
                if (layerName == Const.Layer.電気_SocketPlan_Specific)
                {
                    //バラ品は不許可
                    throw new ApplicationException("The item that made by manual compose cannot change color.");
                }
                else if (layerName != Const.Layer.電気_SocketPlan)
                {
                    MessageDialog.ShowWarning("this is not a socket box.");
                    return;
                }

                int? boxId = selectedIds[0];

                // AutoGenerateのときにPatternNameでBOX作ってる
                var blockName = AutoCad.Db.BlockReference.GetBlockName(boxId.Value);
                var patternName = blockName.Substring(0, blockName.IndexOf("_"));
                var pattern = UnitWiring.Masters.SocketBoxPatterns.Find(p => p.Name == patternName);
                // 一度変えたら、戻せません
                if (pattern == null)
                    throw new ApplicationException("This box is already changed color. Please auto generate again.");

                var dialog = new OtherColorSelectForm(pattern.ColorsList);
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                foreach (var child in selectedIds)
                {
                    //Block削除
                    if (AutoCad.Db.BlockReference.IsType(child))
                        AutoCad.Db.BlockReference.Erase(child);
                    //CheckBox削除
                    else if (AutoCad.Db.Polyline.IsType(child))
                        AutoCad.Db.Polyline.Erase(child);
                }

                var boxPoint = AutoCad.Db.BlockReference.GetPosition(boxId.Value);
                var boxSize = AutoCad.Db.BlockReference.GetSize(boxId.Value);
                var bounds = AutoCad.Db.BlockReference.GetBlockBound(boxId.Value);
                var leaderId = GetLeaderObjectId(bounds);

                PointD firstVertex = null;
                if (leaderId.HasValue)
                {
                    firstVertex = AutoCad.Db.Leader.GetFirstVertex(leaderId.Value);
                    AutoCad.Db.Leader.Erase(leaderId.Value);
                }

                var path = dialog.SelectedColor.DwgPath;
                AutoCad.Command.InsertBlock(path, boxPoint);
                var id = Utilities.GetLastObjectId();
                AutoCad.Db.BlockReference.SetScaleFactor(id, BOX_SCALE);

                var name = AutoCad.Db.BlockReference.GetBlockName(id);
                var newName = GetNewName(pattern.Name + "_" + dialog.SelectedColor.ColorName);
                Utilities.Rename(name, newName);

                var newBoxSize = AutoCad.Db.BlockReference.GetSize(id);
                bounds = AutoCad.Db.BlockReference.GetBlockBound(id);
                MakeLeader(firstVertex, boxPoint, newBoxSize);

                //var lineId = SymbolDrawer.DrawCheckBox(bounds[0], bounds[1]);
                //AutoCad.Db.Entity.SetColor(lineId, CadColor.Blue);

                //グループ化
                var groupingIds = new List<int>();
                groupingIds.Add(id);
                //groupingIds.Add(lineId);
                AutoCad.Db.Group.Make("SocketPattern_" + DateTime.Now.ToString("yyyyMMddhhmmssfff"), groupingIds); //名前は何でもいいが被ると困るので現在時刻で作る

                using (var service = new SocketPlanService())
                {
                    service.ChangeSocketBoxColor(
                        Static.Drawing.ConstructionCode,
                        (decimal)firstVertex.X,
                        (decimal)firstVertex.Y,
                        pattern.Id,
                        dialog.SelectedColor.ColorName);
                }
            }
        }

        private static string GetNewName(string boxName)
        {
            List<string> names = new List<string>();
            var ids = AutoCad.Db.BlockTable.GetIds();
            foreach (var id in ids)
            {
                var currentName = AutoCad.Db.BlockTableRecord.GetBlockName(id);
                if (!currentName.Contains(boxName))
                    continue;

                names.Add(currentName);
            }

            if (names.Count == 0)
                return boxName + "_1";

            var index = 0;
            foreach (var name in names)
            {
                var s = name.Split('_');
                var last = s[s.Length - 1];

                int i;
                if (!int.TryParse(last, out i))
                    continue;

                if (index < i)
                    index = i;
            }

            return boxName + "_" + (index + 1);
        }
    }
}
