using System;
using System.Collections.Generic;
using System.Text;
using Edsa.AutoCadProxy;
using System.Windows.Forms;
using SocketPlan.WinUI.SocketPlanServiceReference;
using System.IO;

namespace SocketPlan.WinUI
{
    public class SymbolDrawer
    {
        public static PointD LeftBottom;
        public static PointD RightTop;
        public static void DrawSpecifics(ref List<SocketBoxSpecific> specifics, int seq)
        {
            if (specifics.Count == 0)
                return;
            
            WindowController2.BringAutoCadToTop();
            AutoCad.Command.Prepare();
            AutoCad.Command.SetCurrentLayoutToModel();

            AutoCad.Db.LayerTableRecord.Make(Const.Layer.電気_SocketPlan_Specific, CadColor.BlackWhite, Const.LineWeight._0_15);

            System.Threading.Thread.Sleep(100);
            AutoCad.Command.SetCurrentLayer(Const.Layer.電気_SocketPlan_Specific);
            System.Threading.Thread.Sleep(100);

            //直交モードを一時的に有効にする
            var orhtoMode = AutoCad.Db.Database.IsOrthogonalMode();
            if (!orhtoMode)
                AutoCad.Db.Database.SetOrthogonalMode(true);

            PointD bottomLeft = null;
            PointD topRight = null;
            List<int> drawnIds = new List<int>();
            var basePosition = new PointD();
            foreach (var specific in specifics) 
            {
                if (!File.Exists(specific.BlockPath))
                    throw new ApplicationException("a block file is not found.");

                if (specifics.IndexOf(specific) == 0)
                {
                    AutoCad.Command.InsertBlockWithRotation(specific.BlockPath);
                    AutoCad.Status.WaitFinish();
                    if (AutoCad.Status.IsCanceled())
                        return;
                }
                else 
                {
                    basePosition.X += 1500;

                    //2個目以降は自動で置く
                    AutoCad.Db.BlockReference.Insert(specific.BlockPath, basePosition);
                }
                var id = AutoCad.Selection.GetLastObjectId();
                if (id == null)
                    throw new ApplicationException("symbol drawing is failed. please retry.");
                specific.SocketBlockId = (int)id;
                List<PointD> pos = AutoCad.Db.BlockReference.GetBlockBound(specific.SocketBlockId);

                if (specifics.IndexOf(specific) == 0)
                    basePosition = AutoCad.Db.BlockReference.GetPosition(specific.SocketBlockId);

                //Seqを埋め込む
                //XData.Symbol.SetSocketBoxSeq(specific.SocketBlockId, seq);
                var attId = Attribute.Make(specific.SocketBlockId, "seq", seq.ToString(), new PointD(0, 0), true);
                AutoCad.Db.Attribute.SetVisible(attId, false);

                if (bottomLeft == null)
                {
                    bottomLeft = pos[0];
                }
                else
                {
                    if (bottomLeft.X > pos[0].X)
                        bottomLeft.X = pos[0].X;
                    if (bottomLeft.Y > pos[0].Y)
                        bottomLeft.Y = pos[0].Y;
                }

                if (topRight == null) 
                {
                    topRight = pos[1];
                }
                else
                {
                    if (topRight.X < pos[1].X)
                        topRight.X = pos[1].X;
                    if (topRight.Y < pos[1].Y)
                        topRight.Y = pos[1].Y;
                }

                drawnIds.Add(specific.SocketBlockId);
            }

            //位置調節(あとまわし)

            //枠を置く
            var rectId = AutoCad.Db.Polyline.MakeRectangle(bottomLeft, topRight);
            AutoCad.Db.Entity.SetColor(rectId, CadColor.Orange);

            //チェックボックス追加
            var lineId = SymbolDrawer.DrawCheckBox(bottomLeft, topRight);
            AutoCad.Db.Entity.SetColor(lineId, CadColor.Orange);

            //グループ化
            var groupingIds = new List<int>();
            groupingIds.AddRange(drawnIds);
            groupingIds.Add(rectId);
            groupingIds.Add(lineId);

            //Boxの座標取得
            LeftBottom = bottomLeft;
            RightTop = topRight;

            AutoCad.Db.Group.Make("SocketSpecific_" + DateTime.Now.ToString("yyyyMMddhhmmssfff"), groupingIds); //名前は何でもいいが被ると困るので現在時刻で作る

            PointD endPoint = new PointD();
            while (true) 
            {
                AutoCad.Command.Prepare();
                var ids = AutoCad.Selection.SelectObjects("Please select a socket symbol.");
                var blockIds = ids.FindAll(p => AutoCad.Db.BlockReference.IsType(p));
                if (blockIds.Count == 1)
                {
                    endPoint = AutoCad.Db.BlockReference.GetPosition(blockIds[0]);
                    specifics.ForEach(p => p.SymbolObjectId = blockIds[0]); //blockIdを持ち帰る
                    break;
                }
                else
                {
                    MessageBox.Show("You can choose only one symbol. "); //選び直させる
                }

                if (AutoCad.Status.IsCanceled())
                    return;
            }

            //矢印を引く
            AutoCad.Command.ZoomAll();
            var center = AutoCad.Db.ViewportTableRecord.GetCenterPointOfModelLayout();
            var direction = Utilities.GetSocketPlanDirection(basePosition, center);
            var points = new List<PointD>();

            if (direction == SocketPlanDirection.LeftDown ||
                direction == SocketPlanDirection.LeftUp)
                points.Add(new PointD(topRight.X, bottomLeft.Y));
            else
                points.Add(bottomLeft);

            points.Add(endPoint);
            points.Reverse();

            int leaderId = AutoCad.Db.Leader.Make(points, null);
            AutoCad.Db.Leader.SetColor(leaderId, CadColor.BlackWhite);
            AutoCad.Db.Leader.SetLineWeight(leaderId, Const.LineWeight._0_15);

            //直交モードを復元する
            if (!orhtoMode)
                AutoCad.Db.Database.SetOrthogonalMode(orhtoMode);

            AutoCad.Command.RefreshExEx();
        }

        public static int DrawCheckBox(PointD bottomleft, PointD topRight) 
        {
            List<PointD> points = new List<PointD>();
            points.Add(new PointD(bottomleft.X, topRight.Y));
            points.Add(new PointD(bottomleft.X - 400, topRight.Y));
            points.Add(new PointD(bottomleft.X - 400, bottomleft.Y));
            points.Add(new PointD(bottomleft.X, bottomleft.Y));
            points.Add(new PointD(bottomleft.X, topRight.Y - 400));
            points.Add(new PointD(bottomleft.X - 400, topRight.Y - 400));

            return AutoCad.Db.Polyline.Make(points);
        }

        //ブロック参照が循環しているとコマンドの途中で落ちる。
        //Edsaを使うと失敗時にリトライで暴走するので一旦分けた
        public static int InsertBlock(string path, PointD position) 
        {
            var blockName = Path.GetFileNameWithoutExtension(path);
            if (!AutoCad.Db.BlockTable.Exist(blockName))
            {
                ////たまに登録に失敗するので、最大10回試行する
                //for (int i = 0; i < 10; i++)
                //{
                        //AutoCad.Command.InsertBlock(path, new PointD(0, 0));
                        //AutoCad.Command.EraseLast();

                //    if (AutoCad.Db.BlockTable.Exist(blockName))
                //        break;
                //}
            }

            return AutoCad.Db.BlockReference.Make(blockName, position);
        }
    }
}
