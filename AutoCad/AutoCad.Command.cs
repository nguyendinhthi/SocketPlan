using System;
using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public partial class AutoCad
    {
        public class Command
        {
            //コマンド送信系の処理は、ユーザーが割り込むと処理が止まる可能性がある。
            //その為、Command.Sendの直前に[Esc][Esc]を送信し、コマンド欄をクリアすることを推奨する。

            /// <summary>AutoCADにコマンドを送信する</summary>
            private static void Send(string command)
            {
                IntPtr autoCADWindow = WindowController2.GetAutoCadHandle();
                WindowController2.SendCommand(autoCADWindow, command);
            }

            /// <summary>AutoCADにコマンドを送信する（改行付き）</summary>
            public static void SendLine(string command)
            {
                AutoCad.Command.Send(command + "\n");
            }

            /// <summary>AutoCADにコマンドを送信する。送りたいコマンドが1行で済む時はこちら推奨（[Esc]x2付き）（改行付き）</summary>
            public static void SendLineEsc(string command)
            {
                //PrepareToCommand.SendをしてからのCommand.SendLineでは、ユーザーが割り込む隙を与えてしまう。
                AutoCad.Command.Send("\x1b\x1b" + command + "\n");
            }

            public static void SendNewLine()
            {
                AutoCad.Command.SendLine(string.Empty);
            }

            /// <summary>Escキーを2回押して、実行中のコマンドをキャンセルします</summary>
            public static void Prepare()
            {
                AutoCad.Command.Send("\x1b\x1b");
            }

            public static void Esc()
            {
                AutoCad.Command.Send("\x1b");
            }

            //選択状態のオブジェクトのスケールを変更する
            public static void SetScaleAll(double scale)
            {
                AutoCad.Command.SendLineEsc("scale\nall\n\n0,0\n" + scale.ToString());
            }

            /// <summary>図面が裏で持っている不要なブロック図形情報を削除する。</summary>
            public static void PurgeBlocks()
            {
                AutoCad.Command.SendLineEsc("-purge\nB\n*\nN");
            }

            /// <summary>図形の描かれていないレイヤーを削除する</summary>
            public static void PurgeLayers()
            {
                AutoCad.Command.SendLineEsc("-purge\nLA\n*\nN");
            }

            /// <summary>図面を再描画する。</summary>
            public static void Refresh()
            {
                AutoCad.Command.SendLineEsc("redraw");
            }

            /// <summary>図面を再描画する。強力に再描画してくれるが、画面がちらつくので、Refreshで再描画できない時だけ使ってください</summary>
            public static void RefreshEx()
            {
                AutoCad.Command.SendLineEsc("regen");
            }

            /// <summary>図面を再描画する。超強力に再描画してくれるが、画面がちらつくので、Refreshで再描画できない時だけ使ってください</summary>
            public static void RefreshExEx()
            {
                AutoCad.Command.SendLineEsc("regenall");
            }

            /// <summary>挿入座標、回転はユーザーが指示する必要があります</summary>
            public static void InsertBlockWithRotation(string blockFilePath)
            {
                AutoCad.Command.SendLineEsc("-insert\n" + blockFilePath + "\ns\n1"); //尺度1
            }

            /// <summary>挿入座標はユーザーが指示する必要があります</summary>
            public static void InsertBlock(string blockFilePath)
            {
                AutoCad.Command.SendLineEsc("-insert\n" + blockFilePath + "\ns\n1\nr\n0");
            }

            /// <summary>指定した座標に指定したブロックを挿入します</summary>
            public static void InsertBlock(string blockFilePath, PointD point)
            {
                AutoCad.Command.SendLineEsc("-insert\n" + blockFilePath + "\n" + point.ToString2() + "\n1\n1\n0"); //尺度1
            }

            /// <summary>指定した座標から伸びるラバーハンド付きで最後に作図した図形を移動させる</summary>
            public static void MoveEntity(PointD basePoint)
            {
                string point = basePoint.X + "," + basePoint.Y;
                AutoCad.Command.SendLineEsc("MOVE\nLAST\n\n" + point);
                AutoCad.Status.WaitFinish();
            }

            /// <summary>全図形が画面内に収まるようズームします</summary>
            public static void ZoomAll()
            {
                AutoCad.Command.SendLineEsc("zoom\ne");
            }

            /// <summary>指定した範囲が画面内に収まるようズームします</summary>
            public static void Zoom(PointD corner, PointD oppositeCorner)
            {
                AutoCad.Command.SendLineEsc("zoom\n" + corner.X + "," + corner.Y + "\n" + oppositeCorner.X + "," + oppositeCorner.Y);
            }

            /// <summary>指定した座標を画面の中央にもってきます。</summary>
            public static void Pan(PointD center)
            {
                AutoCad.Command.SendLineEsc("zoom\nC\n" + center.X + "," + center.Y + "\n1x");
            }

            /// <summary>点線の間隔を調整するのに使います</summary>
            public static void SetGlobalScaleFactor(double scale, bool isPaparScale)
            {
                AutoCad.Command.SendLineEsc("LTSCALE\n" + scale);
                AutoCad.Command.SendLineEsc("PSLTSCALE\n" + (isPaparScale ? 1 : 0));
            }

            /// <summary>全図形を左右反転させます。文字は反転しない</summary>
            public static void InvertDrawing()
            {
                AutoCad.Command.SendLineEsc("mirrtext\n0");
                AutoCad.Command.SendLineEsc("mirror\nall\n\n0,0\n0,1\ny");
            }

            /// <summary>(0,0)を起点に全図形をコピーする</summary>
            public static void CopyDrawing()
            {
                AutoCad.Command.SendLineEsc("copybase\n0,0\nall\n");
            }

            /// <summary>(0,0)にペーストする</summary>
            public static void PasteDrawing()
            {
                AutoCad.Command.SendLineEsc("pasteclip\n0,0");
            }

            public static void SetColor(CadColor color)
            {
                AutoCad.Command.SendLineEsc("chprop\nall\n\nc\n" + color.Code + "\n");
            }

            /// <summary>指定したレイヤーを現在のレイヤーにします</summary>
            public static void SetCurrentLayer(string name)
            {
                AutoCad.Command.SendLineEsc("-layer\ns\n" + name + "\n");
            }

            /// <summary>フォーカスをモデルスペースに合わせる</summary>
            public static void SetCurrentSpaceToModel()
            {
                AutoCad.Command.SendLineEsc("mspace");
            }

            /// <summary>フォーカスをペーパースペースに合わせる</summary>
            public static void SetCurrentSpaceToPaper()
            {
                AutoCad.Command.SendLineEsc("pspace");
            }

            /// <summary>現在表示しているレイアウトのビューポート内の図形を、ビューポートいっぱいに表示させる。</summary>
            public static void ZoolAllInViewport()
            {
                AutoCad.Command.SetCurrentSpaceToModel();
                AutoCad.Command.ZoomAll();
                AutoCad.Command.SetCurrentSpaceToPaper();
            }

            /// <summary>モデルレイアウトを表示する</summary>
            public static void SetCurrentLayoutToModel()
            {
                AutoCad.Command.SendLineEsc("-layout s model");
            }

            /// <summary>指定したレイアウトを表示する</summary>
            public static void SetCurrentLayout(string layoutTabName)
            {
                AutoCad.Command.SendLineEsc("-layout s " + layoutTabName);
            }

            public static void InsertLayout(string templateFilePath, string layoutTabName)
            {
                AutoCad.Command.SendLineEsc("-layout\nt\n" + templateFilePath + "\n" + layoutTabName);
            }

            public static void RenameLayout(string oldTabName, string newTabName)
            {
                AutoCad.Command.SendLineEsc("-layout r");
                AutoCad.Command.SendLine(oldTabName);
                AutoCad.Command.SendLine(newTabName);
            }

            public static void AddLayout(string templatePath, string layoutTabName, string newTabName)
            {
                //レイアウトをﾃﾝﾌﾟﾚｰﾄから挿入する
                AutoCad.Command.InsertLayout(templatePath, layoutTabName);
                AutoCad.Command.RenameLayout(layoutTabName, newTabName);
                AutoCad.Command.SetCurrentLayout(newTabName);
            }

            public static void DropLayout(string layoutTabName)
            {
                AutoCad.Command.SendLineEsc("-layout d " + layoutTabName);
            }

            //public static void DropWasteLayout()
            //{
            //    var layoutIds = AutoCad.Db.Dictionary.GetLayoutIds();
            //    foreach (var id in layoutIds)
            //    {
            //        var name = AutoCad.Db.Layout.GetName(id);

            //        switch (name)
            //        {
            //            case "レイアウト1":
            //            case "レイアウト2":
            //            case "Layout1":
            //            case "Layout2":
            //                AutoCad.Command.SendLineEsc("-layout d " + name);
            //                break;
            //        }
            //    }
            //}

            public static void DropWasteLayout()
            {
                var layoutIds = AutoCad.Db.Dictionary.GetLayoutIds();
                foreach (var id in layoutIds)
                {
                    var name = AutoCad.Db.Layout.GetName(id);

                    if (name.Contains("レイアウト1") || name.Contains("レイアウト2") || name.Contains("Layout1") || name.Contains("Layout2"))
                        AutoCad.Command.SendLineEsc("-layout d " + name);
                }
            }

            public static void SetProxyNotice(bool on)
            {
                AutoCad.Command.SendLineEsc("PROXYNOTICE " + (on ? 1 : 0));
            }

            public static void DrawOrderToFront(List<int> objectIds)
            {
                if (objectIds.Count == 0)
                    return;

                var groupId = AutoCad.Db.Group.Make("TEMP"); //グループを作る
                AutoCad.Db.Group.Append(groupId, objectIds); //グループに対象図形を突っ込む

                AutoCad.Command.SendLineEsc("DRAWORDER G TEMP  F"); //TEMPグループを最前面に表示させる

                AutoCad.Db.Group.Erase(groupId); //グループを削除する
            }

            public static void DrawOrderToBack()
            {
                AutoCad.Command.SendLineEsc("DRAWORDER L  B");
            }

            public static void DrawOrderToBack(string groupName)
            {
                AutoCad.Command.SendLineEsc("DRAWORDER G " + groupName + "  B");
            }

            public static void ExplodeAll()
            {
                AutoCad.Command.SendLineEsc("EXPLODE\nALL\n");
            }

            public static void SetColorAll(CadColor color)
            {
                AutoCad.Command.SendLineEsc("CHPROP\nALL\n\nCOLOR\n" + color.CodeForCommand + "\n");
            }

            public static void SetLineWeightAll(int lineWeight)
            {
                AutoCad.Command.SendLineEsc("CHPROP\nALL\n\nLWEIGHT\n" + lineWeight + "\n");
            }

            public static void EraseLast()
            {
                AutoCad.Command.SendLine("erase\nlast\n");
            }

            public static void DisableSDI()
            {
                AutoCad.Command.SendLine("SDI 0");
            }

            /// <summary>最後に作図した図形にハッチング(塗りつぶし)をかける</summary>
            public static void HatchingLast()
            { //ハッチングタイプをSolidにして対象を最後に選択したオブジェクトにしている
                AutoCad.Command.SendLineEsc("-bhatch\np\ns\ns\nl\n\n");
            }

            /// <summary>指定したグループにハッチング(塗りつぶし)をかける</summary>
            public static void HatchingGroup(string groupName)
            {
                AutoCad.Command.SendLineEsc("-bhatch\np\ns\ns\ng\n" + groupName + "\n\n");
            }

            //AutoCAD 2013対応
            public static void SetInsertUnitToMM()
            {
                AutoCad.Command.SendLineEsc("INSUNITS 4");
            }

            //AutoCAD 2013対応
            public static void SmoothZoomOff()
            {
                //滑らかにズームするのをやめて、一瞬でズームするようにする(AutoCAD2006以降)
                AutoCad.Command.SendLineEsc("VTENABLE\n0");
            }

            //AutoCAD 2013対応
            public static void CloseLayerManager()
            {
                //画層管理画面を閉じる。開いていると、図面切り替え時にAutoCADがビジーと判定され、エラーが発生する。
                AutoCad.Command.SendLineEsc("LAYERCLOSE");
            }

            public static void DrawPolyline()
            {
                AutoCad.Command.SendLineEsc("pline");
                AutoCad.Status.WaitFinish();
            }

            public static void DrawRectangle(string additionalParams)
            {
                AutoCad.Command.SendLineEsc("rectang" + additionalParams);
                AutoCad.Status.WaitFinish();
            }
        }
    }
}
