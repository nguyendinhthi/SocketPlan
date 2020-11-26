using System;
using System.Collections.Generic;
using SocketPlan.WinUI.SocketPlanServiceReference;
using Edsa.AutoCadProxy;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SocketPlan.WinUI
{
    public partial class Validation
    {
        #region 承認可能

        //屋外用のNewLEDが屋内に配置されていたらエラー
        public static void ValidateNewLEDOutSide(List<Symbol> symbols)
        {
            var messageId = @"This type of LED light is not possible to use for inside light.";
            var validator = new Validator();

            validator.Validate = delegate()
            {
                var errors = new List<Symbol>();
                foreach (var symbol in symbols)
                {
                    if (!symbol.IsLight)
                        continue;

                    var light = new Light(symbol);
                    var serials = light.GetSerialsForCount();
                    if (serials.Exists(p => p.Contains(Const.LightSerial.MLS) ||
                                            p.Contains(Const.LightSerial.LLS) ||
                                            p.Contains(Const.LightSerial.PLS)))
                    {
                        if (!symbol.IsOutside)
                        {
                            errors.Add(symbol);
                            break;
                        }
                    }
                }

                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                errors.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        //屋外用のNewLEDがかってにスイッチと接続していたらエラー
        public static void ValidateNewLEDConnectKatteni(List<Symbol> symbols, List<Wire> denkiWires)
        {
            var messageId = @"This type of LED light is not possible to connect in katteni switch.";
            var validator = new Validator();

            validator.Validate = delegate()
            {
                var errors = new List<Symbol>();
                foreach (var symbol in symbols)
                {
                    if (!symbol.IsLight)
                        continue;

                    var light = new Light(symbol);
                    var serials = light.GetSerialsForCount();
                    if (serials.Exists(p => p.Contains(Const.LightSerial.MLS) ||
                                            p.Contains(Const.LightSerial.LLS) ||
                                            p.Contains(Const.LightSerial.PLS)))
                    {
                        var wires = denkiWires.FindAll(p => p.IsConnected(symbol));
                        foreach (var wire in wires)
                        {
                            if (symbols.Exists(p => wire.IsConnected(p) && p.IsKatteniSwitch))
                            {
                                errors.Add(symbol);
                                break;
                            }
                        }
                    }
                }

                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                errors.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        //特定のシンボルがパターンの中心から一定距離空いていなければエラー
        //加工依頼後だと、チェッカーが確認済みのためエラーを表示させる必要はない。7/28中山
        public static void ValidateFireAlarmDistance(CadObjectContainer container)
        {
            var messageId = @"There is the symbol which is too near to a fire alarm.";
            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix == "HJA" || Static.Drawing.Prefix == "HZA")
                    return null;

                var errors = new List<Symbol>();
                var errorTexts = new List<TextObject>();

                foreach (var pattern in container.IgnoreSymbols)
                {
                    //パターンの中心にある火災報知機シンボルを探す
                    var alarm = container.Symbols.Find(p => p.Floor == pattern.Floor &&
                                                      p.Room == pattern.Room &&
                                                      p.Equipment.Block.FileName.Contains("FireAlarm") && //火災報知機を一発で拾う方法がないのでやむなく・・・
                                                      p.Contains(pattern.Position));

                    //Supply Air, Nano Electric, circulator
                    var validateTexts = container.Texts.FindAll(p => p.Floor == pattern.Floor &&
                                                                     p.Room == pattern.Room &&
                                                                    !p.Text.Contains(" ") &&
                                                                    (p.Text.StartsWith("NE") || p.Text == Const.Text.ｻｰｷｭﾚｰﾀｰ || p.Text == Const.Text.サーキュレーター));

                    //Aircon
                    var airconTexts = container.Texts.FindAll(p => p.Floor == pattern.Floor &&
                                                                   p.Room == pattern.Room &&
                                                                  !p.Text.Contains(" ") &&
                                                                  (p.Text.StartsWith("AC")));
                    //AC①～AC⑳までをエアコンとして判定する
                    foreach (var aircon in airconTexts)
                    {
                        if (aircon.Text.Length < 3)
                            continue;

                        var wk = Convert.ToInt32(aircon.Text.Substring(2, 1)[0]);
                        if (wk < 9312 || wk > 9331)
                            continue;

                        validateTexts.Add(aircon);
                    }

                    //SAテキスト
                    var saTexts = container.Texts.FindAll(p => p.Floor == pattern.Floor &&
                                                                p.Room == pattern.Room &&
                                                                !p.Text.Contains(" ") &&
                                                                p.Text.Contains("SA"));

                    foreach (var sa in saTexts)
                    {
                        Regex regex = new System.Text.RegularExpressions.Regex(@"SA\d");
                        if (regex.IsMatch(sa.Text))
                            validateTexts.Add(sa);
                    }

                    foreach (var text in validateTexts)
                    {
                        var rotate = AutoCad.Db.Text.GetRotation(text.ObjectId) / Math.PI * 180;
                        int rad = 0;

                        var width = AutoCad.Db.Text.GetWidth(text.ObjectId);
                        var height = AutoCad.Db.Text.GetHeight(text.ObjectId);
                        var position = AutoCad.Db.Text.GetPosition(text.ObjectId);

                        //回転角とサイズから四隅を取得する
                        var points = new List<PointD>();
                        if (Int32.TryParse(rotate.ToString(), out rad))
                        {
                            switch (rad)
                            {
                                case 0:
                                    points.Add(new PointD(position.X, position.Y + height));
                                    points.Add(new PointD(position.X + width, position.Y + height));
                                    points.Add(new PointD(position.X + width, position.Y));
                                    break;
                                case 90:
                                    points.Add(new PointD(position.X, position.Y + height));
                                    points.Add(new PointD(position.X - width, position.Y));
                                    points.Add(new PointD(position.X - width, position.Y + height));
                                    break;
                                case 180:
                                    points.Add(new PointD(position.X - width, position.Y));
                                    points.Add(new PointD(position.X - width, position.Y - height));
                                    points.Add(new PointD(position.X, position.Y - height));
                                    break;
                                case 270:
                                    points.Add(new PointD(position.X, position.Y - height));
                                    points.Add(new PointD(position.X + width, position.Y - height));
                                    points.Add(new PointD(position.X + width, position.Y));
                                    break;
                                default:
                                    break;
                            }
                        }
                        points.Add(position);

                        foreach (var point in points)
                        {
                            var checkDistance = Edsa.AutoCadProxy.Calc.GetDistance(point, pattern.Position);
                            if (1500 > checkDistance)
                            {
                                errorTexts.Add(text);
                                break;
                            }
                        }

                    }

                    //壁付きのときは照明をチェックしない
                    if (alarm != null)
                    {
                        if (alarm.OtherAttributes.Exists(p => p.Value == Const.Text.壁付))
                            continue;
                    }

                    var validateSymbols = container.Symbols.FindAll(p => p.Floor == pattern.Floor &&
                                                                         p.Room == pattern.Room &&
                                                                        (p.Equipment.Name == Const.EquipmentName.照明 ||
                                                                         p.Equipment.Name == Const.EquipmentName.照明_01 ||
                                                                         p.Equipment.Name == Const.EquipmentName.照明_02 ||
                                                                         p.Equipment.Name == Const.EquipmentName.照明_05 ||
                                                                         p.Equipment.Name == Const.EquipmentName.照明_30));

                    foreach (var symbol in validateSymbols)
                    {
                        double distance;
                        switch (symbol.Equipment.Name)
                        {
                            case Const.EquipmentName.照明:
                                distance = 300;
                                break;
                            case Const.EquipmentName.照明_01:
                                distance = 700;
                                break;
                            case Const.EquipmentName.照明_02:
                                distance = 300;
                                break;
                            case Const.EquipmentName.照明_05:
                                distance = 700;
                                break;
                            case Const.EquipmentName.照明_30:
                                distance = 700;
                                break;
                            default:
                                continue;
                        }

                        //シンボルは基点だけをチェックする
                        var points = new List<PointD>();
                        points.Add(symbol.Position);
                        //points.Add(new PointD(symbol.PositionBottomLeft.X, symbol.PositionBottomLeft.Y));
                        //points.Add(new PointD(symbol.PositionTopRight.X, symbol.PositionBottomLeft.Y));
                        //points.Add(new PointD(symbol.PositionBottomLeft.X, symbol.PositionTopRight.Y));
                        //points.Add(new PointD(symbol.PositionTopRight.X, symbol.PositionTopRight.Y));

                        foreach (var point in points)
                        {
                            var checkDistance = Edsa.AutoCadProxy.Calc.GetDistance(point, pattern.Position);
                            if (distance > checkDistance)
                            {
                                errors.Add(symbol);
                                break;
                            }
                        }
                    }
                }

                if (errors.Count == 0 && errorTexts.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                errors.ForEach(p => error.AddInfo(p));
                errorTexts.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        //JBOXシンボルの高さが無い場合エラー
        public static void ValidateJboxHeight(List<Symbol> symbols)
        {
            var messageId = @"Wrong encoding of Height for JC, JCL, JCT or JB-D/JB-DA.";
            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix == "HZA")
                    return null;

                var errors = new List<Symbol>();
                foreach (var symbol in symbols)
                {
                    if (symbol.IsJBox == false &&
                        symbol.Equipment.Name != Const.EquipmentName.JC &&
                        symbol.Equipment.Name != Const.EquipmentName.JCL &&
                        symbol.Equipment.Name != Const.EquipmentName.JCT)
                        continue;

                    if (symbol.Height <= 0)
                        errors.Add(symbol);
                }

                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                errors.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>239:S指定があり、スマートシリアルがない場合はエラー</summary>
        public static void ValidateSmartSeriesSerial(List<Symbol> symbols)
        {
            var messageId = @"These symbols are smart series.
Please add smart series serial.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix != "HJA")
                    return null;

                var errors = new List<Symbol>();
                foreach (var symbol in symbols)
                {
                    if (symbol.Floor == 0) //立面は許可する
                        continue;

                    if (!symbol.IsSmartSeries)
                        continue;

                    if (!symbol.HasSmartSeriesSerial)
                        errors.Add(symbol);
                }

                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                errors.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>238:HCUコメントがあるのに、専用E付-27シンボルが無かったら場合はエラー</summary>
        public static void ValidateHCUSocket(List<TextObject> texts, List<Symbol> symbols)
        {
            var messageId = @"No HCU item with HCU/HU machine in floor plan.
Please add the item.
Check number of machine in floor plan.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix != "HJA")
                    return null;

                //最上階を取得
                var maxFloor = 0;
                foreach (var text in texts)
                {
                    if (maxFloor < text.Floor)
                        maxFloor = text.Floor;
                }

                //階毎にチェック
                var errors = new List<string>();
                for (var i = 1; i <= maxFloor; i++)
                {
                    var comments = texts.FindAll(p => p.Text.Contains(Const.Text.HCU_HU) && p.Floor == i);
                    var sockets = symbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.HCU && p.Floor == i);

                    if (sockets.Count < comments.Count)
                        errors.Add(i.ToString() + "F");
                }

                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                errors.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>245:出窓箇所に出窓用照明が無かったらエラー</summary>
        public static void ValidateUnitWindowItem(List<TextObject> texts, List<Symbol> symbols)
        {
            var messageId = @"No item installed for unit window.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                //最上階を取得
                var maxFloor = 0;
                foreach (var text in texts)
                {
                    if (maxFloor < text.Floor)
                        maxFloor = text.Floor;
                }

                //階毎にチェック
                var errorSymbols = new List<Symbol>();
                var errorTexts = new List<TextObject>();

                var comments = texts.FindAll(p => !p.IsBayWindowNoLight);
                for (int i = 1; i <= maxFloor; i++)
                {
                    var floorComments = comments.FindAll(p => p.IsBayWindow && p.Floor == i);
                    var items = symbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.uw_04 && p.Floor == i);

                    if (floorComments.Count > items.Count)
                    {
                        errorSymbols.AddRange(items);
                        errorTexts.AddRange(floorComments);
                    }
                }

                if (errorSymbols.Count == 0 && errorTexts.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                errorSymbols.ForEach(p => error.AddInfo(p));
                errorTexts.ForEach(p => error.AddInfo(p));
                return error;
            };
            validator.Run(messageId);
        }

        public static void ValidateNoNeedUnitWindowItem(List<TextObject> texts, List<Symbol> symbols)
        {
            var messageId = @"Please delete unit window pattern.
No need denki item.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var errorComments = new List<TextObject>();
                var comments = texts.FindAll(p => p.IsBayWindowNoLight);
                foreach (var comment in comments)
                {
                    var demadoLightSymbols = symbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.uw_04 && p.Floor == comment.Floor);
                    foreach (var symbol in demadoLightSymbols)
                    {
                        var distance = Utilities.GetDistance(symbol.ActualPosition, comment.Position);
                        if (910 * 2.5 > distance) //2.5グリッド（てきとう）
                        {
                            comment.Text = symbol.RoomName; //出窓コメントは外部にあるのでシンボルの部屋名に置き変えちゃう
                            errorComments.Add(comment);
                            break;
                        }
                    }
                }

                if (errorComments.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                errorComments.ForEach(p => error.AddInfo(p));
                return error;

            };
            validator.Run(messageId);
        }

        /// <summary>帖つきの部屋or階段上部に火災報知機が無い時エラー</summary>
        public static void ValidateFireAlarmWithJyou(List<RoomObject> rooms, List<Symbol> symbols)
        {
            var messageId = @"Missing fire alarm. Please add.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix == "HZA")
                    return null;

                var errors = new List<string>();
                foreach (var room in rooms)
                {
                    var jyouString = XData.Room.GetRoomJyou(room.ObjectId);

                    if (String.IsNullOrEmpty(jyouString))
                    {
                        if (room.Name != Const.Room.階段 && !room.Name.StartsWith(Const.Room.階段 + "("))
                            continue;
                    }

                    var alarms = symbols.FindAll(p => p.Floor == room.Floor && p.IsFireAlarm);

                    if (!alarms.Exists(p => p.ActualPosition.IsIn(room.ObjectId)))
                        errors.Add(room.Floor + "F" + room.Name);
                }

                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                errors.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>帖付きかつ勾配天井のある部屋に火災報知機が無い時エラー</summary>
        public static void ValidateFireAlarmWithKobai(List<RoomObject> rooms, List<TextObject> texts, List<Symbol> symbols)
        {
            var messageId = @"Missing fire alarm. Please check indication in elevation if fukiage. Please add.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                var errors = new List<string>();
                foreach (var room in rooms)
                {
                    if (string.IsNullOrEmpty(XData.Room.GetRoomJyou(room.ObjectId)))
                        continue;

                    if (!room.IsKoubai(texts.FindAll(p => p.Floor == room.Floor)))
                        continue;

                    var alarms = symbols.FindAll(p => p.Floor == room.Floor && p.IsFireAlarm);

                    if (!alarms.Exists(p => p.ActualPosition.IsIn(room.ObjectId)))
                        errors.Add(room.Floor + "F" + room.Name);
                }

                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                errors.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>かってにスイッチ不可コメントがかってにスイッチに含まれていたらエラー</summary>
        public static void ValidateDisapprovalKatteniSwitch(List<Symbol> symbols)
        {
            var messageId = @"Katteni switch is combined with other item, which is not possible.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                var cannotComment = UnitWiring.Masters.Comments.FindAll(p => p.CannotKatteniSwitch);
                var katteniSwitches = symbols.FindAll(p => p.IsKatteniSwitch);

                List<Symbol> errors = new List<Symbol>();

                foreach (var symbol in katteniSwitches)
                {
                    foreach (var comment in cannotComment)
                    {
                        if (symbol.HasComment(comment.Text))
                        {
                            if (!errors.Exists(p => p == symbol))
                            {
                                errors.Add(symbol);
                                break;
                            }
                        }
                    }
                }
                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                errors.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>C-VALUEの設定がなく、ロスガードのシリアルがないときエラー</summary>
        public static void ValidateLossGuardSerial(List<TextObject> texts, List<Symbol> symbols)
        {
            string messageId = @"There is no Central vent in this plan, please check if need to add C-value.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix != "HJA")
                    return null;

                if (symbols.Exists(p => p.BlockName == Const.BlockName.c_value || p.BlockName == Const.BlockName.CVALUE))
                    return null;

                if (texts.Exists(p => p.Text.Contains(Const.Text.ロスガード)))
                    return null;

                if (symbols.Exists(p => p.Equipment.Name == Const.EquipmentName.VHV18A) ||
                    symbols.Exists(p => p.Equipment.Name == Const.EquipmentName.DKI180) ||
                    symbols.Exists(p => p.Equipment.Name == Const.EquipmentName.DKI181) ||
                    symbols.Exists(p => p.Equipment.Name == Const.EquipmentName.ES1800DC))
                    return null;

                var error = new ErrorDialog(messageId);
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>センサースイッチor外部のノーマルスイッチでシリアルコメントの無いものはエラー</summary>
        public static void ValidateNoSerialSensorSwitch(List<Symbol> symbols)
        {
            string messageId = @"Sensor switch has no serial. Please check.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix == "HZA")
                    return null;

                List<Symbol> errors = new List<Symbol>();
                var sensorSwitches = symbols.FindAll(p => (p.Equipment.Name == Const.EquipmentName.ｽｲｯﾁ_01 && (p.RoomName == Const.Room.ポーチ || p.RoomName == Const.Room.外部 || p.RoomName.StartsWith(Const.Room.車庫)))
                                                        || p.Equipment.Name == Const.EquipmentName.ｽｲｯﾁ_03);

                foreach (var sw in sensorSwitches)
                {
                    if (sw.OtherAttributes.Exists(p => !p.Value.Contains("H=")))
                        continue;

                    errors.Add(sw);
                }

                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                errors.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>外部かつ同じ位置のシンボルで高さが一定未満の場合エラー</summary>
        public static void ValidateDuplicateSymbolHeight(List<Symbol> symbols)
        {
            string messageId = @"ATTENTION:
There is item outside with same height and location, need to secure 227.5 distance";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix == "HZA")
                    return null;

                const decimal limitDistance = 227.5M;
                List<Symbol> errors = new List<Symbol>();

                var targets = symbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.int_03 ||
                                                   p.Equipment.Name == Const.EquipmentName.照明_10 ||
                                                   p.Equipment.Name == Const.EquipmentName.専用E無_04 ||
                                                   p.Equipment.Name == Const.EquipmentName.専用E無_05 ||
                                                   p.Equipment.Name == Const.EquipmentName.専用E無_06 ||
                                                   p.Equipment.Name == Const.EquipmentName.専用E無_07 ||
                                                   p.Equipment.Name == Const.EquipmentName.専用E無_08 ||
                                                   p.Equipment.Name == Const.EquipmentName.専用E付_13 ||
                                                   p.Equipment.Name == Const.EquipmentName.一般_15 ||
                                                   p.Equipment.Name == Const.EquipmentName.専用E付_16 ||
                                                   p.Equipment.Name == Const.EquipmentName.専用E付_20 ||
                                                   p.Equipment.Name == Const.EquipmentName.専用E付_22 ||
                                                   p.Equipment.Name == Const.EquipmentName.専用E付_24 ||
                                                   p.Equipment.Name == Const.EquipmentName.防水専_01 ||
                                                   p.Equipment.Name == Const.EquipmentName.防水専_05 ||
                                                   p.Equipment.Name == Const.EquipmentName.防水専_06 ||
                                                   p.Equipment.Name == Const.EquipmentName.防水専_07 ||
                                                   p.Equipment.Name == Const.EquipmentName.防水E付_01 ||
                                                   p.Equipment.Name == Const.EquipmentName.防水E付_02 ||
                                                   p.Equipment.Name == Const.EquipmentName.防水E付_03 ||
                                                   p.Equipment.Name == Const.EquipmentName.防水E付_04 ||
                                                   p.Equipment.Name == Const.EquipmentName.防水E付_07 ||
                                                   p.Equipment.Name == Const.EquipmentName.E付_08 ||
                                                   p.Equipment.Name == Const.EquipmentName.有り);

                for (int i = 0; i < targets.Count; i++)
                {
                    if (!(targets[i].RoomName == Const.Room.ポーチ || targets[i].RoomName == Const.Room.外部 || targets[i].RoomName.StartsWith(Const.Room.車庫)))
                        continue;
                    if (targets[i].Floor == 0)
                        continue;

                    for (int j = 0; j < targets.Count; j++)
                    {
                        if (j <= i)
                            continue;
                        if (targets[j].Floor == 0)
                            continue;
                        if (!(targets[j].RoomName == Const.Room.ポーチ || targets[j].RoomName == Const.Room.外部 || targets[i].RoomName.StartsWith(Const.Room.車庫)))
                            continue;

                        if (targets[i].IsConnected(targets[j]) && targets[i].Floor == targets[j].Floor)
                        {
                            //高さ属性が付加されている場合はその値で比較する
                            var heightI = GetAdditionalHeight(targets[i]);
                            var heightJ = GetAdditionalHeight(targets[j]);

                            var distance = Math.Abs(heightI - heightJ);
                            if (distance < limitDistance)
                                errors.Add(targets[j]);
                        }
                    }
                }
                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                errors.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }
        private static decimal GetAdditionalHeight(Symbol symbol)
        {
            decimal ret = 0;
            foreach (var attr in symbol.OtherAttributes)
            {
                if (!attr.Value.Contains("H="))
                    continue;

                var idx = attr.Value.IndexOf("+");
                if (idx < 0)
                    idx = 2;

                var val = attr.Value.Substring(idx, attr.Value.Length - idx);
                if (Decimal.TryParse(val, out ret))
                    return ret;
            }
            return symbol.Height;
        }

        #endregion

        #region 承認不可

        /// <summary>パワコンのテキストがあるのに、ソーラーソケットがなければエラー</summary>
        public static void ValidateSolarSockets(List<Symbol> symbols, List<TextObject> texts)
        {
            var messageId = @"Please add original solar socket and please check if it is tally with the number of powercon.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                var powerCons = texts.FindAll(p => p.Text == Const.Text.ﾊﾟﾜｰｺﾝﾃﾞｨｼｮﾅ);
                if (powerCons.Count == 0)
                    return null;

                if (symbols.Exists(p => p.Equipment.Name == Const.EquipmentName.太陽光_1口 ||
                                        p.Equipment.Name == Const.EquipmentName.太陽光_2口 ||
                                        p.Equipment.Name == Const.EquipmentName.太陽光_3口))
                    return null;

                var error = new ErrorDialog(messageId, false);
                powerCons.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        #endregion

        #region 承認可or警告

        /// <summary>279:資格テーブルに未入力箇所があるか、重複入力があればエラー</summary>
        public static void ValidateShikakuTableInput(List<Control> controls)
        {
            var messageId = @"There's portion in shikaku table that has no shade correctly, please check and make attachement.";
            var dialog = new ErrorDialog(messageId, 0); //警告エラー用

            //HJAなら承認可エラー、それ以外は警告
            bool isBeforeProcessRequest = true;
            if (Static.Drawing.Prefix == "HJA")
                isBeforeProcessRequest = false;

            var validator = new Validator();

            validator.Validate = delegate()
            {
                //グループに振り分け
                List<List<Control>> groups = new List<List<Control>>();
                groups.Add(controls.FindAll(p => p.Name == "control69" || p.Name == "control70"));
                groups.Add(controls.FindAll(p => p.Name == "control14" || p.Name == "control15"));
                groups.Add(controls.FindAll(p => p.Name == "control16" || p.Name == "control17"));
                groups.Add(controls.FindAll(p => p.Name == "control18" || p.Name == "control19"));
                groups.Add(controls.FindAll(p => p.Name == "control20" || p.Name == "control21"));
                groups.Add(controls.FindAll(p => p.Name == "control23" || p.Name == "control24"));
                groups.Add(controls.FindAll(p => p.Name == "control26" || p.Name == "control27"));
                groups.Add(controls.FindAll(p => p.Name == "control29" || p.Name == "control30"));
                groups.Add(controls.FindAll(p => p.Name == "control56" || p.Name == "control57"));
                groups.Add(controls.FindAll(p => p.Name == "control46" || p.Name == "control43"));
                groups.Add(controls.FindAll(p => p.Name == "control54" || p.Name == "control55"));
                groups.Add(controls.FindAll(p => p.Name == "control47" || p.Name == "control50"));
                groups.Add(controls.FindAll(p => p.Name == "control89" || p.Name == "control90"));
                groups.Add(controls.FindAll(p => p.Name == "control91" || p.Name == "control92"));
                groups.Add(controls.FindAll(p => p.Name == "control64" || p.Name == "control68"));
                groups.Add(controls.FindAll(p => p.Name == "control95" || p.Name == "control96"));
                groups.Add(controls.FindAll(p => p.Name == "control97" || p.Name == "control98"));
                groups.Add(controls.FindAll(p => p.Name == "control81" || p.Name == "control88"));
                groups.Add(controls.FindAll(p => p.Name == "control60" || p.Name == "control61" || p.Name == "control63"));
                groups.Add(controls.FindAll(p => p.Name == "control111" || p.Name == "control112"));

                //グループ内のチェックが1個でない場合はエラー
                foreach (var group in groups)
                {
                    if (group.Count <= 1)
                        continue;

                    var isCheck = group.FindAll(p => ((CheckBox)p).Checked);
                    if (isCheck.Count == 1)
                        continue;
                    else if (isCheck.Count == 0 || isCheck.Count > 1)
                    {
                        if (isBeforeProcessRequest)
                            return new ErrorDialog(messageId, 0);
                        else
                            return new ErrorDialog(messageId);
                    }
                }

                //TV受信工事はチェックなしのみNG
                var checkTV = controls.FindAll(p => p.Name == "control7" ||
                                                    p.Name == "control8" ||
                                                    p.Name == "control9" ||
                                                    p.Name == "control11" ||
                                                    p.Name == "control12" ||
                                                    p.Name == "control13");
                if (checkTV.Find(p => ((CheckBox)p).Checked == true) == null)
                {
                    if (isBeforeProcessRequest)
                        return new ErrorDialog(messageId, 0);
                    else
                        return new ErrorDialog(messageId);
                }

                return null;
            };
            validator.Run(messageId);
        }

        /// <summary>凍結防止ソケットが必要な場合に存在していないとき警告</summary>
        public static void ValidateTouketsuBoushiSocket(List<TextObject> texts, List<Symbol> symbols)
        {
            string messageId = @"Need Touketsu Boushi. Please install same location of Boiler.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix != "HJA")
                    return null;

                //水まわり設備＝寒冷地仕様
                using (var service = new SocketPlanServiceNoTimeout())
                {
                    var siyoCode = service.GetSiyoCode(Static.ConstructionCode, Static.Drawing.PlanNo);

                    if (!service.IsKanreiArea(Static.ConstructionCode, siyoCode))
                        return null;
                }

                var sockets = symbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.防水E付_02);
                if (sockets.Count > 0)
                    return null;

                return new ErrorDialog(messageId, 0);
            };

            validator.Run(messageId);
        }

        /// <summary>凍結防止ソケットとボイラーの数が合わない場合警告</summary>
        public static void WarnTouketsuBoushiSocket(List<TextObject> texts, List<Symbol> symbols)
        {
            string messageId = @"REMINDER: INSTALL SAME NUMBER OF TOUEKTSU BOUSHI WITH BOILER.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix != "HJA")
                    return null;

                //水まわり設備＝寒冷地仕様
                using (var service = new SocketPlanServiceNoTimeout())
                {
                    var siyoCode = service.GetSiyoCode(Static.ConstructionCode, Static.Drawing.PlanNo);

                    if (!service.IsKanreiArea(Static.ConstructionCode, siyoCode))
                        return null;
                }

                var sockets = symbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.防水E付_02);
                if (sockets.Count == 0)
                    return new ErrorDialog(messageId, 0);

                var boilers = texts.FindAll(p => p.Text.Contains(Const.Text.ｶﾞｽﾎﾞｲﾗｰ) ||
                                                 p.Text.Contains(Const.Text.電気温水器) ||
                                                 p.Text.Contains(Const.Text.ｴｺｷｭｰﾄ));

                if (sockets.Count == boilers.Count)
                    return null;

                return new ErrorDialog(messageId, 0);
            };

            validator.Run(messageId);
        }

        //JBOXシンボルがJB-D間に接続されていない場合エラー
        public static void WarnJboxConnection(CadObjectContainer container)
        {
            var messageId = @"There is a JC, JCL, JCT which has no wire to JB-D/JB-DA.";
            var validator = new Validator();

            validator.Validate = delegate()
            {
                var errors = new List<Symbol>();
                foreach (var symbol in container.Symbols)
                {
                    if (symbol.IsJBox == false &&
                        symbol.Equipment.Name != Const.EquipmentName.JC &&
                        symbol.Equipment.Name != Const.EquipmentName.JCL &&
                        symbol.Equipment.Name != Const.EquipmentName.JCT)
                        continue;

                    if (!container.JboxWires.Exists(p => p.IsConnected(symbol)))
                        errors.Add(symbol);
                }

                var jboxSymbols = container.GetWiredJboxSymbols();
                foreach (var parent in jboxSymbols)
                {
                    if (parent.Children.Count == 0)
                        errors.Add(parent);

                    foreach (var child in parent.Children)
                    {
                        if (child.Parent == null)
                            errors.Add(child);
                        else if (child.Parent.IsJBox == false ||
                                (child.Equipment.Name != Const.EquipmentName.JC &&
                                child.Equipment.Name != Const.EquipmentName.JCL &&
                                child.Equipment.Name != Const.EquipmentName.JCT))
                        {
                            errors.Add(parent);
                            errors.Add(child);
                        }
                    }
                }

                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, 0);
                errors.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        //加工依頼後で、回路図面が存在しない場合警告
        public static void WarnNotExistsKairoFrame(List<Drawing> drawings)
        {
            var messageId = @"There is no 提案HEMS回路図面. Please create a 提案HEMS回路図面 before do the option picking.";
            var validator = new Validator();

            validator.Validate = delegate()
            {
                //加工依頼前はノーチェック
                using (var server = new SocketPlanServiceNoTimeout())
                {
                    if (server.IsBeforeProcessRequest(Static.ConstructionCode))
                        return null;
                }

                var error = new ErrorDialog(messageId, 0);

                //回路図面が見つからなかったらエラー
                var kairoLayoutName = "提案HEMS回路図面";
                foreach (var drawing in drawings)
                {
                    if (drawing.Floor < 1 || drawing.Floor > 3)
                        continue;

                    WindowController2.BringDrawingToTop(drawing.WindowHandle);
                    AutoCad.Command.SetCurrentLayoutToModel();
                    var modelLayout = AutoCad.Db.Layout.GetCurrent();
                    AutoCad.Command.SetCurrentLayout(kairoLayoutName);
                    var kairoLayout = AutoCad.Db.Layout.GetCurrent();
                    if (modelLayout == kairoLayout)
                        return error;
                }
                return null;
            };

            validator.Run(messageId);
        }
        #endregion
    }
}
