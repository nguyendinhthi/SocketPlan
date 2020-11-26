using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using SocketPlan.WinUI.SocketPlanServiceReference;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public class RoomObject : CadObject
    {
        public RoomObject() : base()
        {
        }
        
        public RoomObject(int roomOutlineId, int floor, List<SiyoHeya> siyoHeyas)
            : base(roomOutlineId, floor)
        {
            this.NameInDrawing = XData.Room.GetRoomName(roomOutlineId);
            this.Name = RoomObject.GetInteriorRoomNameOrDefault(this.NameInDrawing);
            this.CenterPoint = AutoCad.Db.Entity.GetCenter(roomOutlineId);

            this.StartPoint = AutoCad.Db.Polyline.GetStartPoint(roomOutlineId);
            this.EndPoint = AutoCad.Db.Polyline.GetEndPoint(roomOutlineId);

            //始点と終点が重なっていない線はエラーにする
            if (!AutoCad.Db.Polyline.IsClosed(roomOutlineId) && !this.StartPoint.EqualsRound(this.EndPoint))
                throw new ApplicationException(Messages.NotClosedRoomLine(floor, this.StartPoint, this.EndPoint));

            //部屋名を取得できない線はエラーにする
            if (string.IsNullOrEmpty(this.Name))
                throw new ApplicationException(Messages.InvalidRoomline(this.StartPoint, this.EndPoint));

            //仕様書と図面で表記の異なる部屋があるので、（iｼﾘｰｽﾞ→システムバスなど）
            //仕様書で使われている部屋名も持っておく
            this.CodeInSiyo = RoomObject.GetRoomCodeInSiyo(this, this.Name, siyoHeyas);
        }

        #region プロパティ

        public string NameInDrawing { get; set; }
        public string Name { get; set; }
        public string CodeInSiyo { get; set; }
        public PointD CenterPoint { get; set; }
        public PointD StartPoint { get; set; }
        public PointD EndPoint { get; set; }

        #endregion

        #region メソッド

        public double GetJyou()
        {
            var jyouString = XData.Room.GetRoomJyou(this.ObjectId);

            if (string.IsNullOrEmpty(jyouString))
            {
                //図面上の面積から求める
                var areaMM = AutoCad.Db.Curve.GetArea(this.ObjectId);

                //GetAreaは㎜2で返ってくるので㎡に換算する
                var areaM = areaMM / 1000000;

                //1帖は1.656200㎡（910x1820）
                return Math.Round(areaM / 1.656200, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                //正規表現を使い、「（14.7 帖）」から「14.7」を抽出する
                Regex regex = new Regex(@"[0-9]+\.?[0-9]*");
                Match match = regex.Match(jyouString);
                if (match.Success)
                    jyouString = match.Value;

                double value;
                if (!double.TryParse(jyouString, out value))
                    throw new ApplicationException(Messages.InvalidJyou(jyouString));

                return value;
            }
        }

        public double GetTarekabeJyou()
        {
            var jyouString = XData.Room.GetRoomTarekabeJyou(this.ObjectId);

            if (string.IsNullOrEmpty(jyouString))
                return 0;

            double value;
            if (!double.TryParse(jyouString, out value))
                throw new ApplicationException(Messages.InvalidTarekabeJyou(jyouString));

            return value;
        }

        public bool HasCeiling(List<TextObject> texts)
        {
            if (this.Name == Const.Room.外部) //屋外に天井パネルがあるわけないよね～
                return false;

            if (this.Name.StartsWith(Const.Room.階段下))
                return false;

            if (this.IsFukinuke(texts))
                return false;

            return true;
        }

        private bool IsFukinuke(List<TextObject> texts)
        {
            var fukinukeTexts = texts.FindAll(p => p.Text == Const.Text.上部吹抜);
            foreach (var fukinukeText in fukinukeTexts)
            {
                var textPosition = AutoCad.Db.Text.GetPosition(fukinukeText.ObjectId);
                if (textPosition.IsIn(this.ObjectId))
                    return true;
            }

            return false;
        }

        public bool IsKoubai(List<TextObject> texts)
        {
            var koubaiTexts = texts.FindAll(p => p.Text == Const.Text.勾配天井);
            foreach (var koubaiText in koubaiTexts)
            {
                var textPosition = AutoCad.Db.Text.GetPosition(koubaiText.ObjectId);
                if (textPosition.IsIn(this.ObjectId))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// RoomObjectの領域内に「吹抜」コメントが存在したらTrueを返却
        /// </summary>
        /// <param name="texts"></param>
        /// <returns></returns>
        public bool HasFukinukeComment(List<TextObject> texts)
        {
            List<TextObject> fukinukeTexts = texts.FindAll(p => p.Text == Const.Text.吹抜 && p.Floor == this.Floor);

            foreach (TextObject fukinukeText in fukinukeTexts)
            {
                PointD textPosition = AutoCad.Db.Text.GetPosition(fukinukeText.ObjectId);
                if (textPosition.IsIn(this.ObjectId))
                    return true;
            }

            return false;
        }

        #endregion

        #region staticメソッド

        public static string GetRoomNameInSiyo(string roomName, List<SiyoHeya> siyoHeyas)
        {
            var siyoHeya = RoomObject.FindSiyoHeya(roomName, siyoHeyas);
            if (siyoHeya != null)
                return siyoHeya.RoomName;

            var interiorHeya = RoomObject.FindInteriorEstimateRoom(roomName);
            if (interiorHeya != null)
                return interiorHeya.RoomName;

            return null;
        }

        public static string GetRoomCodeInSiyo(RoomObject room, string roomName, List<SiyoHeya> siyoHeyas)
        {
            if (room != null)
            {
                var roomCode = XData.Room.GetRoomCode(room.ObjectId);
                if (!string.IsNullOrEmpty(roomCode))
                    return roomCode;
            }

            var siyoHeya = RoomObject.FindSiyoHeya(roomName, siyoHeyas);
            if (siyoHeya != null)
                return siyoHeya.RoomCode;

            var interiorHeya = RoomObject.FindInteriorEstimateRoom(roomName);
            if (interiorHeya != null)
                return interiorHeya.RoomCode;

            return null;
        }

        public static string GetRoomCodeInSiyo(string roomName, int floor, List<SiyoHeya> siyoHeyas)
        {
            var siyoHeyasFloor = siyoHeyas.FindAll(p => p.Floor == floor);
            var siyoHeya = RoomObject.FindSiyoHeya(roomName, siyoHeyasFloor);
            if (siyoHeya != null)
                return siyoHeya.RoomCode;

            var interiorHeya = RoomObject.FindInteriorEstimateRoom(roomName);
            if (interiorHeya != null)
                return interiorHeya.RoomCode;

            return null;
        }

        //public static string GetRoomCodeInSiyo(string roomName, List<SiyoHeya> siyoHeyas)
        //{
        //    var siyoHeya = RoomObject.FindSiyoHeya(roomName, siyoHeyas);
        //    if (siyoHeya != null)
        //        return siyoHeya.RoomCode;

        //    var interiorHeya = RoomObject.FindInteriorEstimateRoom(roomName);
        //    if (interiorHeya != null)
        //        return interiorHeya.RoomCode;

        //    return null;
        //}

        /// <summary>仕様書の部屋リストから、指定した部屋名と一致するデータを取得する</summary>
        public static SiyoHeya FindSiyoHeya(string roomName, List<SiyoHeya> siyoHeyas)
        {
            return siyoHeyas.Find(p => Utilities.EqualIgnoreWidthCase(p.RoomName, roomName));
        }

        /// <summary>インテリア見積りの部屋リストから、指定した部屋名と一致するデータを取得する</summary>
        public static InteriorEstimateRoom FindInteriorEstimateRoom(string roomName)
        {
            foreach (var interiorHeya in UnitWiring.Masters.InteriorEstimateRooms)
            {
                foreach (var aliasName in interiorHeya.AliasName.Split(';'))
                {
                    if (Utilities.EqualIgnoreWidthCase(aliasName, roomName))
                        return interiorHeya;
                }
            }

            return null;
        }

        /// <summary>インテリア見積りの部屋マスタに対応するデータがあればそれを使う。なければそのまま使う。</summary>
        public static string GetInteriorRoomNameOrDefault(string roomName)
        {
            var interiorHeya = RoomObject.FindInteriorEstimateRoom(roomName);
            if (interiorHeya != null)
                return interiorHeya.RoomName;

            return roomName;
        }

        public static List<RoomObject> GetAll(int floor)
        {
            var list = new List<RoomObject>();

            List<SiyoHeya> siyoHeyas;
            using (var service = new SocketPlanServiceNoTimeout())
            {
                siyoHeyas = new List<SiyoHeya>(service.GetSiyoHeyas(Static.ConstructionCode, Static.Drawing.PlanNoWithHyphen));
            }

            var ids = Filters.GetRoomOutlineIds();
            foreach (var id in ids)
            {
                if (AutoCad.Db.Polyline.GetVertex(id).Count < 4)
                    continue; //頂点が4つ以上ないと空間は表せない。4つ未満の線は無視する。線が閉じている時はありえるのか。ま、いいや。

                list.Add(new RoomObject(id, floor, siyoHeyas));
            }

            return list;
        }

        public static DrawResult DrawRoomOutline0Jyou(int floor)
        {
            WindowController2.BringAutoCadToTop();
            AutoCad.Command.Prepare();
            AutoCad.Command.SetCurrentLayoutToModel();

            AutoCad.Db.LayerTableRecord.Make(Const.Layer.電気_部屋_WithoutJyou, CadColor.Green, Const.LineWeight._0_70);
            AutoCad.Command.SetCurrentLayer(Const.Layer.電気_部屋_WithoutJyou);

            List<SiyoHeya> siyoHeyas;
            using (var service = new SocketPlanServiceNoTimeout())
            {
                siyoHeyas = new List<SiyoHeya>(service.GetSiyoHeyas(Static.ConstructionCode, Static.Drawing.PlanNoWithHyphen));
            }

            //先に線を引いてもらう
            var result = AutoCad.Drawer.DrawPolyline(true);
            if (result.Status == DrawStatus.Canceled)
                return result;

            if (result.Status == DrawStatus.Failed)
                throw new ApplicationException("Failed to get line info.\nPlease draw line again.");

            var lineId = result.ObjectId;

            if (!AutoCad.Db.Entity.GetLayerName(lineId).Contains(Const.Layer.電気_部屋))
                throw new ApplicationException("Invalid layer.\nPlease draw line again.");

            //線の始点と終点をくっつける
            if (!AutoCad.Db.Polyline.IsClosed(lineId))
                AutoCad.Db.Polyline.SetClose2(lineId);

            //部屋を選んでもらう
            var roomName = RoomObject.GetRoomNameText("Select Room Name:", true);
            if (roomName == null)
            {
                result.Status = DrawStatus.Canceled;
                AutoCad.Db.Polyline.Erase(lineId);
                return result;
            }
            else if (roomName == string.Empty)
                roomName = "外部";

            var roomNameInSiyo = RoomObject.GetRoomNameInSiyo(roomName, siyoHeyas);
            var roomCode = RoomObject.GetRoomCode(roomName, floor, siyoHeyas);
            if (string.IsNullOrEmpty(roomNameInSiyo))
            {
                RoomSelectForm form = new RoomSelectForm(siyoHeyas, roomName);
                var dr = form.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.Cancel)
                {
                    result.Status = DrawStatus.Canceled;
                    AutoCad.Db.Polyline.Erase(lineId);
                    return result;
                }

                roomName = form.CorrectRoomName;
                roomCode = form.CorrectRoomCode;
            }

            XData.Room.SetRoomName(lineId, roomName);
            XData.Room.SetRoomJyou(lineId, "0");
            XData.Room.SetRoomTarekabeJyou(lineId, "0");
            XData.Room.SetRoomCode(lineId, roomCode);

            return result;
        }

        public static void DrawRoomOutline(bool isPolyline, bool withJo)
        {
            WindowController2.BringAutoCadToTop();
            AutoCad.Command.Prepare();
            AutoCad.Command.SetCurrentLayoutToModel();

            if (withJo)
            {
                AutoCad.Db.LayerTableRecord.Make(Const.Layer.電気_部屋_WithJyou, CadColor.Magenta, Const.LineWeight._0_70);
                AutoCad.Command.SetCurrentLayer(Const.Layer.電気_部屋_WithJyou);
            }
            else
            {
                AutoCad.Db.LayerTableRecord.Make(Const.Layer.電気_部屋_WithoutJyou, CadColor.Green, Const.LineWeight._0_70);
                AutoCad.Command.SetCurrentLayer(Const.Layer.電気_部屋_WithoutJyou);
            }

            List<SiyoHeya> siyoHeyas;
            using (var service = new SocketPlanServiceNoTimeout())
            {
                siyoHeyas = new List<SiyoHeya>(service.GetSiyoHeyas(Static.ConstructionCode, Static.Drawing.PlanNoWithHyphen));
            }

            var drawing = Drawing.GetCurrent();

            while (true)
            {
                //部屋名を選択してもらう。
                var roomName = RoomObject.GetRoomNameText("Select Room Name:", false);
                if (string.IsNullOrEmpty(roomName))
                    break; //入力をキャンセルしていたら終了

                if (!withJo && UnitWiring.Masters.Rooms.Exists(p => p.Name == roomName && p.WithJyou))
                    MessageDialog.ShowWarning(Messages.ShouldHaveJyou()); //警告のみで処理続行

                var roomNameInSiyo = RoomObject.GetRoomNameInSiyo(roomName, siyoHeyas);
                if (string.IsNullOrEmpty(roomNameInSiyo))
                {
                    RoomSelectForm form = new RoomSelectForm(siyoHeyas, roomName);
                    var dr = form.ShowDialog();
                    if (dr == System.Windows.Forms.DialogResult.Cancel)
                        break;

                    roomName = form.CorrectRoomName;
                }

                var roomCode = RoomObject.GetRoomCode(roomName, drawing.Floor, siyoHeyas);

                DrawResult result;

                //線を引いてもらう。
                if (isPolyline)
                    result = AutoCad.Drawer.DrawPolyline(true);
                else
                    result = AutoCad.Drawer.DrawRectangle("");

                if (result.Status == DrawStatus.Canceled)
                    break;

                if (result.Status == DrawStatus.Failed)
                    throw new ApplicationException(Messages.RoomNotSet());

                var lineId = result.ObjectId;

                if (!AutoCad.Db.Entity.GetLayerName(lineId).Contains(Const.Layer.電気_部屋))
                    throw new ApplicationException(Messages.RoomNotSet());

                //線の始点と終点をくっつける
                if (!AutoCad.Db.Polyline.IsClosed(lineId))
                {
                    AutoCad.Db.Polyline.SetClose2(lineId);
                }

                //帖数を選択してもらう。
                string jo = string.Empty;
                if (withJo)
                {
                    jo = RoomObject.SelectJoIndication();

                    if (string.IsNullOrEmpty(jo))
                    {
                        AutoCad.Db.Object.Erase(lineId);
                        AutoCad.Command.Refresh();
                        break; //入力をキャンセルしていたら書いた図形を削除して終了
                    }
                }

                //引いた線に部屋名を拡張データとして持たせる。
                XData.Room.SetRoomName(lineId, roomName);
                XData.Room.SetRoomCode(lineId, roomCode);

                if (withJo)
                    XData.Room.SetRoomJyou(lineId, jo);

                //タレ壁情報を入力する画面を表示する
                var roomObj = new RoomObject(lineId, 0, siyoHeyas); //階数は適当
                var roomInfoForm = new RoomInfoForm(roomObj);
                var dr2 = roomInfoForm.ShowDialog();
                if (dr2 == System.Windows.Forms.DialogResult.Cancel)
                {
                    AutoCad.Db.Object.Erase(lineId);
                    AutoCad.Command.Refresh();
                    break; //入力をキャンセルしていたら書いた図形を削除して終了
                }

                AutoCad.Status.WaitFinish();

                if (result.Status == DrawStatus.DrawnAndCanceled)
                    break; //線をひいてキャンセルしていたら拡張データを設定して終了
            }
        }

        private static string GetRoomNameText(string prompt, bool permitNonSelect)
        {
            while (true)
            {
                var ids = AutoCad.Selection.SelectObjects(prompt);
                if (AutoCad.Status.IsCanceled())
                    return null;

                var textIds = ids.FindAll(p => AutoCad.Db.Text.IsType(p));
                if (textIds.Count == 0)
                {
                    if (permitNonSelect)
                        return string.Empty;

                    MessageDialog.ShowWarning(Messages.PleaseRoomNameText());
                    continue;
                }

                if (textIds.Count == 1)
                    return AutoCad.Db.Text.GetText(textIds[0]);

                var list = new List<KeyValuePair<int, PointD>>();
                foreach (var textId in textIds)
                {
                    list.Add(new KeyValuePair<int, PointD>(textId, AutoCad.Db.Text.GetPosition(textId)));
                }

                list.Sort((a, b) => b.Value.Y.CompareTo(a.Value.Y));

                var roomName = string.Empty;
                list.ForEach(p => roomName += AutoCad.Db.Text.GetText(p.Key));
                return roomName;
            }
        }

        private static string GetRoomCode(string roomName, int floor, List<SiyoHeya> siyoHeyas)
        {
            var siyoHeya = siyoHeyas.Find(p => p.RoomName == roomName && p.Floor == floor);
            if (siyoHeya == null)
                return string.Empty;

            return siyoHeya.RoomCode;
        }

        private static string SelectJoIndication()
        {
            while (true)
            {
                string jo = RoomObject.GetSelectedText("Select Jyou Indication:");

                if (string.IsNullOrEmpty(jo))
                    return null;

                if (jo.Contains("帖"))
                    return jo;

                MessageDialog.ShowWarning(Messages.PleaseSelectJoIndication());
            }
        }

        private static string GetSelectedText(string prompt)
        {
            while (true)
            {
                var ids = AutoCad.Selection.SelectObjects(prompt);
                if (AutoCad.Status.IsCanceled())
                    return null;

                if (ids.Count != 1 || ids.FindAll(p => AutoCad.Db.Text.IsType(p)).Count != 1)
                {
                    MessageDialog.ShowWarning(Messages.PleaseSelect1Text());
                    continue;
                }

                var id = ids.Find(p => AutoCad.Db.Text.IsType(p));

                return AutoCad.Db.Text.GetText(id);
            }
        }

        #endregion
    }
}
