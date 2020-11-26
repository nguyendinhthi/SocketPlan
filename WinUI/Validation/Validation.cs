using System;
using System.Collections.Generic;
using SocketPlan.WinUI.SocketPlanServiceReference;
using Edsa.AutoCadProxy;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SocketPlan.WinUI.Entities.CADEntity;
using Edsa.MSOffice;

namespace SocketPlan.WinUI
{
    public partial class Validation
    {
        #region 承認可能

        /// <summary>図面上にないSocketBoxDataがあったらエラー</summary>
        public static void ValidateSiyoHeyas(string constructionCode, string planNo, string revisionNo, List<Symbol> symbols)
        {
            string messageId =
@"The socket box on the drawing had been illegally changed. please regenerate the socket plan.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var errorsockets = new List<string>();
                var socketBoxes = new List<SocketBox>();
                using (var service = new SocketPlanService())
                {
                    socketBoxes = new List<SocketBox>(service.GetSocketBoxes(constructionCode));
                }

                foreach (var box in socketBoxes) 
                {
                    if (box.SocketObjectId == null)
                        continue;

                    var socket = symbols.Find(p => p.Floor == box.Floor && p.ObjectId == box.SocketObjectId);
                    if (socket == null)
                        errorsockets.Add(box.Floor + "F:" + box.patternName);
                }

                if (errorsockets.Count > 0)
                {
                    var error = new ErrorDialog(messageId);
                    errorsockets.ForEach(p => error.AddInfo(p));
                    return error;
                }

                return null;
            };

            validator.Run(messageId);
        }

        /// <summary>仕様書に登録されている部屋が図面上になかったらエラー</summary>
        public static void ValidateSiyoHeyas(List<RoomObject> pickedRooms, List<SiyoHeya> siyoHeyas)
        {
            string messageId =
@"There are rooms without outline.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix == "HZA")
                    return null;

                List<SiyoHeya> extendedSiyoHeyas = new List<SiyoHeya>(siyoHeyas);
                extendedSiyoHeyas.AddRange(GetAdditionalRooms(siyoHeyas));
                List<SiyoHeya> notFoundRooms = new List<SiyoHeya>();

                foreach (var siyoHeya in extendedSiyoHeyas)
                {
                    //外部と共通は作図しないので除外する
                    if (siyoHeya.RoomName == Const.Room.外部 || siyoHeya.RoomName == Const.Room.共通)
                        continue;

                    if (pickedRooms.Exists(pickedRoom => siyoHeya.RoomCode == pickedRoom.CodeInSiyo))
                        continue;

                    notFoundRooms.Add(siyoHeya);
                }

                if (notFoundRooms.Count != 0)
                {
                    var error = new ErrorDialog(messageId);
                    notFoundRooms.ForEach(p => error.AddInfo(p.Floor + "F/" + p.RoomName));
                    return error;
                }

                return null;
            };

            validator.Run(messageId);
        }

        private static List<SiyoHeya> GetAdditionalRooms(List<SiyoHeya> siyoHeyas)
        {
            var rooms = new List<SiyoHeya>();

            //２階建て以上の家には階段があるべき
            string storyCode;
            using (var service = new SocketPlanServiceNoTimeout())
            {
                storyCode = service.GetConstructionStory(Static.ConstructionCode, Static.Drawing.PlanNoWithHyphen);
            }

            if (storyCode == "0010")
            {
            }
            else if (storyCode == "0020")
            {
                rooms.Add(new SiyoHeya(string.Empty, 1, Const.RoomCode.階段室, Const.Room.階段室));
                rooms.Add(new SiyoHeya(string.Empty, 2, Const.RoomCode.階段室, Const.Room.階段室));
            }
            else if (storyCode == "0030")
            {
                rooms.Add(new SiyoHeya(string.Empty, 1, Const.RoomCode.階段室, Const.Room.階段室));
                rooms.Add(new SiyoHeya(string.Empty, 2, Const.RoomCode.階段室, Const.Room.階段室));
                rooms.Add(new SiyoHeya(string.Empty, 3, Const.RoomCode.階段室, Const.Room.階段室));
            }
            else
            {
                //特殊な階数が出た時に業務が止まったらイヤなので、何もしないでおこう。
            }

            //ポーチは全ての家にあるべきなので、仕様書にポーチがなくてもチェックする
            if (!siyoHeyas.Exists(p => p.RoomName == Const.Room.ポーチ))
                rooms.Add(new SiyoHeya(string.Empty, 1, Const.RoomCode.ポーチ, Const.Room.ポーチ));

            return rooms;
        }

        /// <summary>プレートに付けた数量付きのコメントとシンボルに付けたコメントの数が異なったらエラー</summary>
        public static void ValidateCommentCount(List<Symbol> symbols, List<TextObject> texts)
        {
            ValidateCommentCount(symbols, texts, Const.Text.ﾈｰﾑ);
            ValidateCommentCount(symbols, texts, Const.Text.ﾎﾀﾙ);
        }

        private static void ValidateCommentCount(List<Symbol> symbols, List<TextObject> texts, string targetComment)
        {
            string messageId =
@"Comment and drawing quantity not match.
図面上のコメント数と集計数が異なります。" + Environment.NewLine + targetComment;

            var validator = new Validator();

            validator.Validate = delegate()
            {
                var targetSymbols = symbols.FindAll(p => p.HasComment(targetComment));
                var targetTexts = texts.FindAll(p => p.TextWithoutQty == targetComment);

                var symbolCount = targetSymbols.Count; //targetCommentを設定しているシンボルの数
                var commentCount = 0; //図面上に表記されているtargetCommentの数
                targetTexts.ForEach(p => commentCount += p.Qty);
                targetSymbols.ForEach(p =>
                {
                    p.OtherAttributes.ForEach(q =>
                    {
                        if (q.Value == targetComment && AutoCad.Db.Attribute.GetVisible(q.ObjectId))
                        {
                            commentCount++;
                            targetTexts.Add(new TextObject(q.ObjectId, p.Floor));
                        }
                    });
                });

                if (symbolCount != commentCount)
                {
                    var error = new ErrorDialog(messageId);
                    error.AddInfo(targetComment + " -> Drawing:" + symbolCount + " Comment:" + commentCount);
                    targetSymbols.ForEach(p => error.AddInfo(p));
                    targetTexts.ForEach(p => error.AddInfo(p));
                    return error;
                }

                return null;
            };

            validator.Run(messageId);
        }

        /// <summary>1現場に2種類以上のライコンがあったらエラー</summary>
        public static void ValidateConflictLightControl(List<Symbol> symbols)
        {
            string messageId =
@"Comment is incorrect.
Need to check item comment below.
異なる品番のライコンが存在します。";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                //ライコンを探す

                var lightControls = new List<Symbol>();
                foreach (var symbol in symbols)
                {
                    if (symbol.ContainsComment(Const.Text.LEDﾗｲｺﾝ))
                        lightControls.Add(symbol);
                }

                if (lightControls.Count <= 1)
                    return null;

                //異なる品番のライコンがあったらエラーを出す

                string hinban = null;
                foreach (var lightControl in lightControls)
                {
                    var att = lightControl.OtherAttributes.Find(p => p.Value.Contains(Const.Text.LEDﾗｲｺﾝ));
                    if (string.IsNullOrEmpty(hinban))
                        hinban = att.Value;

                    if (hinban == att.Value)
                        continue;

                    var error = new ErrorDialog(messageId);
                    error.AddInfo(hinban);
                    error.AddInfo(att.Value);
                    lightControls.ForEach(p => error.AddInfo(p));
                    return error;
                }

                return null;
            };

            validator.Run(messageId);
        }

        /// <summary>2世帯だったらTVA帳票が対応していないのでエラー</summary>
        public static void Validate2setaiTVA()
        {
            string messageId =
@"Cannot generate TVA because Nisetai plan. Please ask Optional Team to make TVA.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                ShikakuTableEntry nisetai;
                using (var service = new SocketPlanService())
                {
                    nisetai = service.GetShikakuTableEntry(Static.ConstructionCode, Const.ShikakuItemId.NISETAI_ARI);
                }

                if (nisetai == null)
                    return null;

                if (!Convert.ToBoolean(nisetai.Value))
                    return null;

                var error = new ErrorDialog(messageId);
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>配線番号の取得に失敗しているシンボルが存在したらエラー</summary>
        public static void ValidateFailedWireNo(List<Symbol> breakers)
        {
            string messageId =
@"These symbols failed to get a wire number.
Please check floor plan and kairo plan.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                var errorSymbols = new List<Symbol>();
                //分電盤直結だけチェックする
                foreach (var breaker in breakers)
                {
                    errorSymbols.AddRange(breaker.Children.FindAll(p => p.SequenceNo == -1));
                }

                if (errorSymbols.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                errorSymbols.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>同じ配線番号のシンボルが存在したらエラー</summary>
        public static void ValidateDuplicatedWireNo(List<Symbol> breakers)
        {
            string messageId =
@"There are item with same item number.
Check error and generate the denki plan again.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                var symbols = Symbol.ConvertToSymbolList(breakers);
                var duplicatedWireNoSymbols = new List<Symbol>();

                foreach (var symbol in symbols)
                {
                    var wireNo = symbol.GetWireNo();
                    var sameNoSymbols = symbols.FindAll(p => p.GetWireNo() == wireNo);

                    if (2 <= sameNoSymbols.Count)
                        duplicatedWireNoSymbols.Add(symbol);
                }

                if (duplicatedWireNoSymbols.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                duplicatedWireNoSymbols.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>仕様書の階数と同じ階までの図面が開かれていなかったらエラー</summary>
        public static void ValidateLackOfDrawing(List<Drawing> drawings)
        {
            string messageId =
@"There is missing floor compared to story of shiyousho.
Please check all floor dwg are opened.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                string storyCode;
                using (var service = new SocketPlanServiceNoTimeout())
                {
                    storyCode = service.GetConstructionStory(Static.ConstructionCode, Static.Drawing.PlanNoWithHyphen);
                }

                int story = 0;
                switch (storyCode)
                {
                    case "0010": story = 1; break;
                    case "0020": story = 2; break;
                    case "0030": story = 3; break;
                }

                var draings = Drawing.GetAll(true);

                List<int> lackFloors = new List<int>();
                for (int i = 1; i <= story; i++)
                {
                    if (!drawings.Exists(p => p.Floor == i))
                        lackFloors.Add(i);
                }

                if (lackFloors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                error.AddInfo("Shiyousho Story : " + story);
                foreach (var lackFloor in lackFloors)
                {
                    error.AddInfo("Lacking Floor : " + lackFloor);
                }

                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>家タイプに合った火災警報器じゃなかったらエラー(Proposal:153)</summary>
        public static void ValidateConflictFireAlarmType(List<Symbol> symbols)
        {
            string messageId = @"Type of Fire Alarm and House type is conflict, need to check fire alarm pattern.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix == "HZA")
                    return null;

                var fireAlarmCategory = UnitWiring.Masters.SelectionCategories.Find(p => p.Name.Contains("Fire Alarm"));
                if (fireAlarmCategory == null)
                    return null; //火災警報器のカテゴリが無くなってたら、どうしようもないわ～

                //Fire Alarmカテゴリに含まれるシンボルに絞る
                var alarms = symbols.FindAll(symbol => Array.Exists(fireAlarmCategory.Equipments, q => symbol.Equipment.Id == q.Id));
                if (alarms.Count == 0)
                    return null;

                bool shouldBeBatteryTypeFireAlarm;
                var basedDate = new DateTime(2016, 9, 1);
                using (var service = new SocketPlanServiceNoTimeout())
                {
                    //2016/9/1以降のI-HEADはチェックしない
                    var construction = service.GetConstruction(Static.ConstructionCode);
                    if (construction != null &&
                        (construction.ContractDate >= basedDate || !construction.ContractDate.HasValue) &&
                        Static.HouseSpecs.IsIHead)
                        return null;

                    shouldBeBatteryTypeFireAlarm = service.IsBatteryTypeFireAlarm(Static.ConstructionCode, Static.Drawing.PlanNoWithHyphen);
                }

                var dameAlarms = new List<Symbol>();
                foreach (var alarm in alarms)
                {
                    if (shouldBeBatteryTypeFireAlarm == alarm.IsBatteryTypeFireAlarm)
                        continue;

                    dameAlarms.Add(alarm);
                }

                if (dameAlarms.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                dameAlarms.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>全部材の部屋が外部になっていたらエラー</summary>
        public static void ValidateAllGaibu(List<Symbol> symbols)
        {
            string messageId = @"All items are set in 外部. Please check the following floor.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                var dic = new Dictionary<int, List<Symbol>>();
                foreach (var symbol in symbols)
                {
                    if (!dic.ContainsKey(symbol.Floor))
                        dic.Add(symbol.Floor, new List<Symbol>());

                    dic[symbol.Floor].Add(symbol);
                }

                var badFloors = new List<int>();
                foreach (var floor in dic.Keys)
                {
                    if (!(floor == 1 || floor == 2 || floor == 3)) //やっつけ対応
                        continue;

                    if (dic[floor].Exists(p => !p.RoomName.Contains(Const.Room.外部)))
                        continue;

                    badFloors.Add(floor);
                }

                if (badFloors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                badFloors.ForEach(p => error.AddInfo("Floor : " + p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>ACコメントあるのにSleeveCapが無かったらエラー</summary>
        public static void ValidateNoSleeveCap(List<TextObject> texts, List<Symbol> symbols)
        {
            string messageId = @"Missing sleeve cap with AC（ｽ) in floor plan.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                //「AC⑥(ｽ)」という文字を探す（数字は任意）
                var acSus = texts.FindAll(p => p.Text.StartsWith(Const.Text.AC) && p.Text.EndsWith(Const.Text.ス));
                if (acSus.Count == 0)
                    return null;

                var sleeveCaps = symbols.FindAll(p => p.Floor == 0 && p.Equipment.Name == Const.EquipmentName.SLEEVE_CAP_H2669);
                if (0 < sleeveCaps.Count)
                    return null;

                var error = new ErrorDialog(messageId);
                error.AddInfo("Elevation");
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>AC(ｽ)コメントの数とSleeveCapの数が一致していなかったらエラー。↑のエラーと被ってる気がする・・・</summary>
        public static void ValidateExcessSleeveCap(List<Symbol> symbols, List<TextObject> texts)
        {
            string messageId = @"Sleeve cap is not tallied in elevation vs. floor plan.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix == "HZA")
                    return null;

                var acTexts = texts.FindAll(p => p.Text.StartsWith(Const.Text.AC) && p.Text.EndsWith(Const.Text.ス));
                var sleeveCaps = symbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.SLEEVE_CAP_H2669);

                if (acTexts.Count == sleeveCaps.Count)
                    return null;

                var dialog = new ErrorDialog(messageId);
                acTexts.ForEach(p => dialog.AddInfo(p));
                sleeveCaps.ForEach(p => dialog.AddInfo(p));
                return dialog;
            };

            validator.Run(messageId);
        }

        /// <summary>ACコメントあるのに品番記入枠が図面中に無かったらエラー</summary>
        public static void ValidateMissingSleevePipeSerial(List<TextObject> texts, List<Symbol> symbols)
        {
            string messageId = @"Missing Sleeve pipe serial in floor plan.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix != "HJA")
                    return null;

                //「AC⑥(ｽ)」という文字を探す（数字は任意）
                var acSus = texts.FindAll(p => p.Text.StartsWith("AC") && p.Text.EndsWith("(ｽ)"));
                if (acSus.Count == 0)
                    return null;

                var serials = symbols.FindAll(p => p.Equipment.Name == "Frame2" || p.Equipment.Name == "Frame3");
                if (acSus.Count <= serials.Count)
                    return null;

                var error = new ErrorDialog(messageId);
                acSus.ForEach(p => error.AddInfo(p));
                serials.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>ロスガードのコメントがあるのに、コンセントが無かったらエラー</summary>
        public static void ValidateLossGuard(List<TextObject> texts, List<Symbol> symbols)
        {
            string messageId = @"Missing socket for rosugado.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix != "HJA")
                    return null;

                var comments = texts.FindAll(p => p.Text.Contains(Const.Text.ロスガード) || p.Text.Contains(Const.Text.デシカント));
                var sockets = symbols.FindAll(p => p.Equipment.Name == "専用E付-10");

                if (comments.Count == sockets.Count)
                    return null;

                var error = new ErrorDialog(messageId);
                comments.ForEach(p => error.AddInfo(p));
                sockets.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>ロスガードのコメントがあるのに、リモコンが無かったらエラー</summary>
        public static void ValidateLossGuardRemocon(List<TextObject> texts, List<Symbol> symbols)
        {
            string messageId = @"Please add CV remote in floor plan.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix == "HJA" || Static.Drawing.Prefix == "HZA")
                    return null;

                var lossGuards = texts.FindAll(p => p.Text == Const.Text.ロスガード || p.Text == Const.Text.デシカント);
                if (lossGuards.Count == 0)
                    return null;

                var remocons = symbols.FindAll(p => Utilities.In(p.Equipment.Name, Const.EquipmentName.ﾘﾓｺﾝ01,
                                                                                   Const.EquipmentName.ﾘﾓｺﾝ02,
                                                                                   Const.EquipmentName.ﾘﾓｺﾝ03,
                                                                                   Const.EquipmentName.ﾘﾓｺﾝ04));

                if (0 < remocons.Count)
                    return null;

                var error = new ErrorDialog(messageId);
                lossGuards.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>ロスガードのコンセントがあるのに、RAシンボルが2つ無かったらエラー</summary>
        public static void ValidateLossGuardRA(List<Symbol> symbols)
        {
            string messageId = @"Missing RA in floor plan.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                var sockets = symbols.FindAll(p => p.Equipment.Name == "専用E付-10");

                //「RA3-ほげ」「RA4 - ほげ」のような文字を探す
                Regex regex = new Regex(@"RA[0-9]+\s*-.*");
                var ras = symbols.FindAll(p => regex.IsMatch(p.Equipment.Name));

                if (sockets.Count * 2 <= ras.Count)
                    return null;

                var error = new ErrorDialog(messageId);
                sockets.ForEach(p => error.AddInfo(p));
                ras.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>ひとりぼっちの照明は許さん！</summary>
        public static void ValidateNoConnectionLight(List<Symbol> symbols, List<Wire> wires)
        {
            string messageId = @"There is no wiring connection on switches/items which should be wire connectable.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix == "HZA")
                    return null;

                var bocchi = new List<Symbol>();

                foreach (var symbol in symbols)
                {
                    if (!symbol.IsConnectableForLightElectrical)
                        continue;

                    if (!symbol.IsLight)
                        continue;

                    if (symbol.Equipment.Name == Const.EquipmentName.照明_04 ||
                        symbol.Equipment.Name == Const.EquipmentName.照明_05 ||
                        symbol.Equipment.Name == Const.EquipmentName.照明_08 ||
                        symbol.Equipment.Name == Const.EquipmentName.照明_09 ||
                        symbol.Equipment.Name == Const.EquipmentName.照明_10 ||
                        symbol.Equipment.Name == Const.EquipmentName.ShowerLight)
                        continue;

                    if (wires.Exists(p => p.IsConnected(symbol)))
                        continue;

                    bocchi.Add(symbol);
                }

                if (bocchi.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                bocchi.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>シンボルが電気_設備レイヤーになかったらエラー</summary>
        public static void ValidateSymbolLayer(List<Symbol> symbols)
        {
            string messageId = @"Wrong layer.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var badSymbols = symbols.FindAll(p =>
                    p.Layer != Const.Layer.電気_設備 &&
                    p.Layer != Const.Layer.電気_抜き出し &&
                    p.Layer != Const.Layer.電気_SocketPlan &&
                    p.Layer != Const.Layer.電気_SocketPlan_Specific);
                if (badSymbols.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                badSymbols.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>矩計に適さないSA、RAシンボルが使われていたらエラー</summary>
        public static void ValidateKanabakariRASANotTally(List<Symbol> symbols)
        {
            string messageId = @"SA and RA not tally to kanabakari.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var kanabakari = Static.HouseSpecs.Kanabakari; //265or240or260が入ってる

                //「RA3-ほげ」「SA4 - ほげ」のような文字を探す
                Regex regex = new Regex(@"(RA|SA|FVSA)[0-9]+\s*-.*");
                var rasas = symbols.FindAll(p => regex.IsMatch(p.Equipment.Name));

                var badSymbols = new List<Symbol>();
                foreach (var rasa in rasas)
                {
                    //シンボル名の末尾の数字3文字をゲット
                    Regex re = new Regex(@"\d{3}$");
                    var match = re.Match(rasa.Equipment.Name);
                    if (!match.Success)
                        continue;

                    string baka = match.Value;
                    if (kanabakari != baka)
                        badSymbols.Add(rasa);
                }

                if (badSymbols.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                badSymbols.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>HTUのコメントがあるのに、コンセントが無かったらエラー</summary>
        public static void ValidateHTU(List<TextObject> texts, List<Symbol> symbols)
        {
            string messageId = @"Missing socket for HTU.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix != "HJA")
                    return null;

                var comments = texts.FindAll(p => p.Text.Contains("HTU"));
                var sockets = symbols.FindAll(p => p.Equipment.Name == "専用E付-24");

                if (comments.Count == sockets.Count)
                    return null;

                var error = new ErrorDialog(messageId);
                comments.ForEach(p => error.AddInfo(p));
                sockets.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>ﾌﾞｰｽﾀｰ用ｺﾝｾﾝﾄありで設定してるのに、情報BOXがあったらエラー</summary>
        public static void ValidateJBD(List<Symbol> symbols)
        {
            string messageId = @"Busuta portion in shikaku table must be nashi.";

            var validator = new Validator();

            validator.Validate = delegate()
            {

                if (Static.HouseSpecs.IsIPalette)
                    return null;//i-paletteだった場合,ﾌﾞｰｽﾀｰ用ｺﾝｾﾝﾄをShikakuTableで設定しない為

                var outSymbols = symbols.FindAll(p => p.IsJBox || p.Equipment.Name == Const.EquipmentName.JB_MAIN);
                if (outSymbols.Count == 0)
                    return null;

                ShikakuTableEntry ari;
                ShikakuTableEntry nashi;
                using (var service = new SocketPlanServiceNoTimeout())
                {
                    ari = service.GetShikakuTableEntry(Static.ConstructionCode, Const.ShikakuItemId.BOOSTER_SOCKET_ARI);
                    nashi = service.GetShikakuTableEntry(Static.ConstructionCode, Const.ShikakuItemId.BOOSTER_SOCKET_NASHI);
                }

                bool a = ari != null && Convert.ToBoolean(ari.Value);
                bool n = nashi != null && Convert.ToBoolean(nashi.Value);

                if (!a && !n)
                {
                    var error = new ErrorDialog(messageId);
                    outSymbols.ForEach(p => error.AddInfo(p));
                    return error;
                }

                if (a)
                {
                    var error = new ErrorDialog(messageId);
                    outSymbols.ForEach(p => error.AddInfo(p));
                    return error;
                }

                return null;
            };

            validator.Run(messageId);
        }

        /// <summary>BSEシリアルの数だけBSEスイッチが無かったらエラー</summary>
        public static void ValidateBSE(List<Symbol> symbols, List<TextObject> texts)
        {
            string messageId = @"Add switch for BSE.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                //ItemNameがｽｲｯﾁ-01で(BSE)を含むTextItemがBSESerialらしい。DWM～はnicchi or wood decorのシリアルらしい
                var targetSerials = UnitWiring.Masters.TextItems.FindAll(p =>
                    p.ItemName == Const.EquipmentName.ｽｲｯﾁ_01 &&
                    p.Text != Const.Text.融雪用 &&
                    p.Text != Const.Text.ﾄﾞﾚｲﾝ用 &&
                    !p.Text.Contains("DWM") &&
                    p.Text.Contains("(BSE)"));

                var bseTexts = texts.FindAll(p => targetSerials.Exists(q => q.Text == p.Text));
                if (bseTexts.Count == 0)
                    return null;

                var bseSymbols = symbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.ｽｲｯﾁ_11); //11はBSE専用スイッチ

                if (bseTexts.Count <= bseSymbols.Count)
                    return null;

                var dialog = new ErrorDialog(messageId);
                bseTexts.ForEach(p => dialog.AddInfo(p));
                bseSymbols.ForEach(p => dialog.AddInfo(p));
                return dialog;
            };

            validator.Run(messageId);
        }

        /// <summary>Individual ventシリアルの数だけIndividual ventスイッチが無かったらエラー</summary>
        public static void ValidateIndividualVent(List<Symbol> symbols, List<TextObject> texts)
        {
            string messageId = @"Add switch for individual vent.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                //ItemNameがｽｲｯﾁ-01で(BSE)を含まないTextItemがIndividual Ventらしい
                var targetSerials = UnitWiring.Masters.TextItems.FindAll(p =>
                    p.ItemName == Const.EquipmentName.ｽｲｯﾁ_01 &&
                    p.Text != Const.Text.融雪用 &&
                    p.Text != Const.Text.ﾄﾞﾚｲﾝ用 &&
                    !p.Text.Contains("DWM") &&
                    !p.Text.Contains("(BSE)"));

                var ventTexts = texts.FindAll(p => targetSerials.Exists(q => q.Text == p.Text));
                if (ventTexts.Count == 0)
                    return null;

                var ventSymbols = symbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.ｽｲｯﾁ_12); //12はvent専用スイッチ

                if (ventTexts.Count <= ventSymbols.Count)
                    return null;

                var dialog = new ErrorDialog(messageId);
                ventTexts.ForEach(p => dialog.AddInfo(p));
                ventSymbols.ForEach(p => dialog.AddInfo(p));
                return dialog;
            };

            validator.Run(messageId);
        }

        /// <summary>EVの配線の仕方を間違っていたらエラー</summary>
        public static void ValidateEvBadConnection(List<Symbol> symbols)
        {
            string messageId = @"Wiring of EV may not be correct.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var evSwitches = symbols.FindAll(p => p.IsEVSwitch);

                var bads = evSwitches.FindAll(p => p.Children.Count == 0);

                if (bads.Count == 0)
                    return null;

                var dialog = new ErrorDialog(messageId);
                bads.ForEach(p => dialog.AddInfo(p));
                return dialog;
            };

            validator.Run(messageId);
        }

        /// <summary>融雪用ルーフヒーターの配線の仕方を間違っていたらエラー</summary>
        public static void ValidateRoofHeaterBadConnection(List<Symbol> symbols)
        {
            string messageId = @"Wiring of Roof Heater may not be correct.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var roofHeaterSwitches = symbols.FindAll(p => p.IsRoofHeaterSwitch);

                var bads = roofHeaterSwitches.FindAll(p => p.Children.Count != 0);

                if (bads.Count == 0)
                    return null;

                var dialog = new ErrorDialog(messageId);
                bads.ForEach(p => dialog.AddInfo(p));
                return dialog;
            };

            validator.Run(messageId);

        }

        /// <summary>幹線引込と全量買取分電盤がある時は全量買取幹線引込が必要</summary>
        public static void ValidateZenryouKansenHikikomi(List<Symbol> symbols)
        {
            string messageId = @"Please add zenryou kaitori kansen hikikomi.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix != "HZA")
                    return null;

                var kansens = symbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.幹線引込);
                var bundenbans = symbols.FindAll(p => Utilities.In(p.Equipment.Name, Const.EquipmentName.分電盤_3, Const.EquipmentName.分電盤_4));

                if (kansens.Count == 0 || bundenbans.Count == 0)
                    return null;

                var zenryouKansens = symbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.全量買取幹線引込);

                if (zenryouKansens.Count != 0)
                    return null;

                var dialog = new ErrorDialog(messageId);
                kansens.ForEach(p => dialog.AddInfo(p));
                bundenbans.ForEach(p => dialog.AddInfo(p));
                return dialog;
            };

            validator.Run(messageId);
        }

        /// <summary>スイッチのみ、照明のみの配線があったらエラー</summary>
        public static void ValidateSingleSwitchOrLight(List<Symbol> symbols)
        {
            string messageId = @"Check wiring connection of item.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                var targets = symbols.FindAll(p => p.IsSwitch || p.IsLight);

                var unkos = new List<Symbol>();

                foreach (var target in targets)
                {
                    var groupSymbols = target.GetSameGroupSymbols();
                    var parent = target.Parent;

                    //parentは分電盤やジョイントボックスに繋がっているシンボルのみに絞るための条件です。
                    if (parent == null)
                        continue;

                    //照明だけのグループ、スイッチだけのグループはエラー対象
                    if (groupSymbols.TrueForAll(p => p.IsLight) || groupSymbols.TrueForAll(p => p.IsSwitch))
                        unkos.Add(target);
                }

                if (unkos.Count == 0)
                    return null;

                var dialog = new ErrorDialog(messageId);
                unkos.ForEach(p => dialog.AddInfo(p));
                return dialog;
            };

            validator.Run(messageId);
        }

        /// <summary>RAYがあるのに、床暖房エアコンのシンボルが無かったらエラー</summary>
        public static void ValidateRAYKairo(List<Symbol> symbols, List<TextObject> texts)
        {
            string messageId =
@"RAY AC symbol quantity is less than RAY comment.
Please add RAY comment to RAY AC symbol.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                var rayACs = symbols.FindAll(p => p.ContainsComment(Const.Text.RAY));
                var rayTexts = texts.FindAll(p => p.Text.Contains(Const.Text.RAY) && p.Text.Contains("(ｾ)"));

                if (rayTexts.Count <= rayACs.Count)
                    return null;

                var dialog = new ErrorDialog(messageId);
                rayACs.ForEach(p => dialog.AddInfo(p));
                rayTexts.ForEach(p => dialog.AddInfo(p));
                return dialog;
            };

            validator.Run(messageId);
        }

        /// <summary>配線が間違ったレイヤーに乗っていたらエラー</summary>
        public static void ValidateWireLayer(List<Wire> redLines)
        {
            string messageId = @"Layer of wire is wrong. It will have wrong wiring connection, so please correct it.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                var wrongLines = redLines.FindAll(p => !Utilities.In(p.Layer, Const.Layer.電気_電気図面配線,
                                                                             Const.Layer.電気_配線,
                                                                             Const.Layer.電気_配線_非表示用));

                if (wrongLines.Count == 0)
                    return null;

                var dialog = new ErrorDialog(messageId);
                wrongLines.ForEach(p => dialog.AddInfo(p));
                return dialog;
            };

            validator.Run(messageId);
        }

        /// <summary>HJAプランで無印コネクタがあったらエラー</summary>
        public static void ValidateConnectorNoAlphabet(List<Symbol> symbols)
        {
            string messageId = @"Cannot use connector without alphabet in HJA plan.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix != "HJA")
                    return null;

                var arrows = symbols.FindAll(p => p.IsArrow);
                var badArrows = new List<Symbol>();

                foreach (var arrow in arrows)
                {
                    var att = arrow.Attributes.Find(p => p.Tag == Const.AttributeTag.LINK_CODE);
                    if (att == null)
                        continue;

                    if (att.Value == Const.LINK_CODE_BLANK)
                        badArrows.Add(arrow);
                }

                if (badArrows.Count == 0)
                    return null;

                var dialog = new ErrorDialog(messageId);
                badArrows.ForEach(p => dialog.AddInfo(p));
                return dialog;
            };

            validator.Run(messageId);
        }

        /// <summary>シンボルに繋がっていない配線があったらエラー（GetGroupedSymbols(加工依頼前)用）</summary>
        public static void UnconnectedWire(List<Wire> wires, List<Symbol> symbols)
        {
            string messageId = @"Unconnected wire found..";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                var lonelyWires = new List<Wire>();
                foreach (var wire in wires)
                {
                    //無印コネクタに繋がっている配線は、両サイドがシンボルに繋がっていなくても良しとする
                    if (wire is RisingWire)
                    {
                        var risingWire = wire as RisingWire;
                        if (risingWire.FloorLinkCode == Const.LINK_CODE_BLANK)
                            continue;
                    }

                    lonelyWires.Add(wire);
                }

                if (lonelyWires.Count == 0)
                    return null;

                var realLonelyWires = GetUnconnectedWires(lonelyWires, symbols);
                if (realLonelyWires.Count == 0)
                    return null;

                var dialog = new ErrorDialog(messageId);
                realLonelyWires.ForEach(p => dialog.AddInfo(p));
                return dialog;
            };

            validator.Run(messageId);
        }

        /// <summary>ライコンのシリアルテキストがあるのに、ライコンスイッチがなかったらエラー</summary>
        public static void ValidateLightControlPlate(List<Plate> lightControlPlates, List<TextObject> texts)
        {
            string messageId = @"Please add light control serial using function 5.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                var lcTexts = texts.FindAll(p => p.Text.StartsWith(Const.Text.LIGHT_CONTROL_SERIAL_PREFIX));
                if (lcTexts.Count == 0)
                    return null;

                if (lightControlPlates.Count != 0)
                    return null;

                var dialog = new ErrorDialog(messageId);
                lcTexts.ForEach(p => dialog.AddInfo(p));
                return dialog;
            };

            validator.Run(messageId);
        }

        /// <summary>矩計240(+)で照明の高さが不正な範囲ならエラー</summary>
        public static void ValidateLightHightInFukinuke(string constructionCode, string planNo, List<RoomObject> rooms, List<Symbol> symbols)
        {
            string messageId = @"Height used is wrong.
(240K: Height should be below 2450/above 2750.
240+K: Height should be below 2488/above 2788)";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.HouseSpecs.Kanabakari != Const.Kanabakari._240)
                    return null;

                bool is240 = false;
                bool is240Plus = false;

                using (var service = new SocketPlanService())
                {
                    var siyoCode = service.GetSiyoCode(Static.ConstructionCode, Static.Drawing.PlanNo);

                    is240 = service.IsKanabakari240(Static.ConstructionCode, siyoCode);
                    if (!is240)
                        is240Plus = service.IsKanabakari240Plus(Static.ConstructionCode, siyoCode);
                }

                var fukinukeRooms = rooms.FindAll(p => p.Name.Contains(Const.Room.吹抜));
                var invalid = new List<Symbol>();

                foreach (var fukinuke in fukinukeRooms)
                {
                    var lights = symbols.FindAll(p => p.IsLight && p.Room != null && p.Room.ObjectId == fukinuke.ObjectId);

                    if (is240)
                        invalid.AddRange(lights.FindAll(p => 2450 < p.Height && p.Height < 2750));
                    else if (is240Plus)
                        invalid.AddRange(lights.FindAll(p => 2488 < p.Height && p.Height < 2788));
                }

                if (invalid.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                invalid.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>LightとSwitchが同じグループ内に存在していないとエラー</summary>
        public static void ValidateSwitchAndLightConnection(CadObjectContainer container)
        {
            var messageId = "There are unconnected light and switch.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix == "HZA")
                    return null;

                var invalidConnectSymbols = new List<Symbol>();

                var symbols = new List<Symbol>(container.Symbols);
                symbols.RemoveAll(p => !p.IsLight && !p.IsSwitch);
                symbols.RemoveAll(p => p.Equipment.Name == Const.EquipmentName.照明_10);
                var wires = new List<Wire>(container.DenkiWires);

                while (0 < symbols.Count)
                {
                    var currentSymbol = symbols[0];
                    var sameGroupSymbols = new List<Symbol>();
                    sameGroupSymbols.Add(currentSymbol);
                    symbols.Remove(currentSymbol);
                    sameGroupSymbols.AddRange(container.FindChainedGroupedSymbols(currentSymbol, ref symbols, ref wires));

                    if (sameGroupSymbols.Exists(p => !p.IsLight))
                        continue;

                    if (sameGroupSymbols.Exists(p => p.IsSwitch))
                        continue;

                    invalidConnectSymbols.AddRange(sameGroupSymbols);
                }

                if (invalidConnectSymbols.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                invalidConnectSymbols.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        public static void ValidateNoConnectionSwitch(List<Symbol> symbols, List<Wire> wires)
        {
            string messageId = @"There is no connection switches.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix == "HZA")
                    return null;

                var bocchi = new List<Symbol>();

                foreach (var symbol in symbols)
                {
                    if (!symbol.IsSwitch)
                        continue;

                    if (wires.Exists(p => p.IsConnected(symbol)))
                        continue;

                    bocchi.Add(symbol);
                }

                if (bocchi.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                bocchi.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        public static void ValidateYRExists(CadObjectContainer container)
        {
            var messageId = @"Please add YR remote in floor plan.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (!container.Texts.Exists(p =>
                    p.Text.Contains(Const.Text.エコウィル) ||
                    p.Text.Contains(Const.Text.ｴｺｳｨﾙ) ||
                    p.Text.Contains(Const.Text.ｶﾞｽﾎﾞｲﾗｰ) ||
                    p.Text.Contains(Const.Text.灯油ﾎﾞｲﾗｰ) ||
                    p.Text.Contains(Const.Text.RAY1) ||
                    p.Text.Contains(Const.Text.HCU_HU) ||
                    p.Text.Contains(Const.Text.ECO_ONE)))
                    return null;

                if (container.Symbols.Exists(p => p.IsYR))
                    return null;

                var error = new ErrorDialog(messageId);
                return error;
            };

            validator.Run(messageId);
        }

        public static void ValidateBoilerForYRExists(CadObjectContainer container)
        {
            var messageId = @"Need to check if YR is necessary, if not delete the item.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix == "HZA")
                    return null;

                if (!container.Symbols.Exists(p => p.IsYR && !p.Clipped))
                    return null;

                if (container.Texts.Exists(p =>
                    p.Text.Contains(Const.Text.エコウィル) ||
                    p.Text.Contains(Const.Text.ｴｺｳｨﾙ) ||
                    p.Text.Contains(Const.Text.ｶﾞｽﾎﾞｲﾗｰ) ||
                    p.Text.Contains(Const.Text.灯油ﾎﾞｲﾗｰ) ||
                    p.Text.Contains(Const.Text.RAY1) ||
                    p.Text.Contains(Const.Text.HCU_HU) ||
                    p.Text.Contains(Const.Text.ECO_ONE)))
                    return null;

                var error = new ErrorDialog(messageId);
                return error;
            };

            validator.Run(messageId);
        }

        //public static void ValidateCSCount(int csHaisenCount, int csToridashiCount)
        //{
        //    var messageId = @"Invalid CS and CSｱﾝﾃﾅ取出 count.";

        //    var validator = new Validator();

        //    validator.Validate = delegate()
        //    {
        //        if (csHaisenCount == 1 && csToridashiCount == 1) return null;
        //        if (csHaisenCount == 2 && csToridashiCount == 1) return null;
        //        if (csHaisenCount == 2 && csToridashiCount == 2) return null;
        //        if (csHaisenCount == 4 && csToridashiCount == 2) return null;
        //        if (csHaisenCount == 4 && csToridashiCount == 1) return null;
        //        if (csHaisenCount == 3 && csToridashiCount == 1) return null;
        //        if (csHaisenCount == 4 && csToridashiCount == 3) return null;

        //        var error = new ErrorDialog(messageId);
        //        return error;
        //    };

        //    validator.Run(messageId);
        //}

        public static void ValidateShikakuTableSukkriPoru(List<Control> controls)
        {
            var messageId = @"Sukkiri po-ru is ARI, please check kaiden and baiden portion if NASHI.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                var sukkiriAri1 = controls.Find(p => p.Name == "control61") as CheckBox;
                var sukkiriAri2 = controls.Find(p => p.Name == "control63") as CheckBox;

                if (!sukkiriAri1.Checked && !sukkiriAri2.Checked)
                    return null;

                var kaiAri = controls.Find(p => p.Name == "control90") as CheckBox;
                var kaiNashi = controls.Find(p => p.Name == "control89") as CheckBox;

                var uriAri = controls.Find(p => p.Name == "control92") as CheckBox;
                var uriNashi = controls.Find(p => p.Name == "control91") as CheckBox;

                if ((kaiNashi.Checked && !kaiAri.Checked) &&
                    (uriNashi.Checked && !uriAri.Checked))
                    return null;

                var error = new ErrorDialog(messageId);
                error.AddInfo("Elevation");
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary> RAYテキストとRAY用シンボルの数が同じゃなかったらエラー</summary>
        public static void ValidateRayAndSoket(List<Symbol> symbols, List<TextObject> texts, List<RoomObject> rooms)
        {
            string messageId = "Number of RAY socket and RAY machine is not tally, please check.";
            Validator validator = new Validator();
            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix != "HZA")
                    return null;

                var maxFloor = 0;
                foreach (var text in texts)
                {
                    if (maxFloor < text.Floor)
                        maxFloor = text.Floor;
                }

                var error = new ErrorDialog(messageId);
                var errorSymbols = new List<Symbol>();
                var errorTexts = new List<TextObject>();
                for (int i = 1; i <= maxFloor; i++)
                {
                    //外部にある場合は除外する。
                    var rayTexts = texts.FindAll(p => p.Text.StartsWith("RAY") && p.Floor == i);
                    var targetRays = new List<TextObject>();

                    foreach (var ray in rayTexts)
                    {
                        ray.FillRoom(rooms);
                        //Rayテキストが家の外だったら除外
                        if (ray.RoomName == Const.Room.外部)
                            continue;

                        var seq = ray.Text.Substring(3);
                        int result;
                        //rayの後ろが数字のみではないか、(ｾ)じゃなければ除外
                        if (!int.TryParse(seq, out result) && !seq.Contains("(ｾ)"))
                            continue;

                        targetRays.Add(ray);
                    }

                    var aircons = symbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.ｴｱｺﾝ_3 && p.Floor == i);

                    if (targetRays.Count == aircons.Count)
                        continue;

                    errorTexts.AddRange(targetRays);
                    errorSymbols.AddRange(aircons);
                }
                if (errorTexts.Count == 0 && errorSymbols.Count == 0)
                {
                    return null;
                }
                else
                {
                    errorTexts.ForEach(p => error.AddInfo(p));
                    errorSymbols.ForEach(p => error.AddInfo(p));
                }

                return error;
            };

            validator.Run(messageId);
        }

        /// <summary> 3wayスイッチと4wayスイッチが不正に接続されていたらエラー </summary>
        public static void Validate3wayTo4wayConnection(CadObjectContainer container)
        {
            string messageId = Messages.Invalid3Wayto4WaySwitch();
            List<Wire> unconnectedWires;
            var groupedSymbols = container.GetGroupedSymbols(out unconnectedWires);

            Validator validator = new Validator();
            validator.Validate = delegate()
            {
                var errorSymbols = new List<Symbol>();
                foreach (var group in groupedSymbols)
                {
                    var _3ways = group.FindAll(p => p.IsNormal3WaySwitch);
                    var _4ways = group.FindAll(p => p.Is4WaySwitch);

                    if (_3ways.Count == 0 && _4ways.Count == 0)
                        continue;

                    //3wayが2ではない、もしくは4wayが5つ以上存在したらエラー
                    if (_3ways.Count != 2 || 5 < _4ways.Count)
                    {
                        errorSymbols.AddRange(_3ways);
                        errorSymbols.AddRange(_4ways);
                    }
                }

                if (errorSymbols.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                errorSymbols.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary> E_断_テキストがあるのに、一般_41シンボルが配置されていなかったらエラー </summary>
        public static void ValidateElectricHoneycomb(List<Symbol> symbols, List<TextObject> texts)
        {
            string messageId = "Add item for Electric honeycomb.";

            Validator validator = new Validator();
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

                var error = new ErrorDialog(messageId);
                var errorSymbols = new List<Symbol>();
                var errorTexts = new List<TextObject>();
                for (int i = 1; i <= maxFloor; i++)
                {
                    var ippan41 = symbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.一般_41 && p.Floor == i);
                    var edanTexts = texts.FindAll(p => (p.Text.Contains(Const.Text.E_断) ||
                                                        p.Text.Contains(Const.Text.E_遮) ||
                                                        p.Text.Contains(Const.Text.E_レ)) && p.Floor == i);

                    int honeycomTextCount = 0;
                    foreach (var edanText in edanTexts)
                    {
                        if (edanText.Text.Contains("1,2,3") || edanText.Text.Contains("2,3,4"))
                            honeycomTextCount += 3;
                        else if (edanText.Text.Contains("1,2") || edanText.Text.Contains("2,3") || edanText.Text.Contains("3,4"))
                            honeycomTextCount += 2;
                        else
                            honeycomTextCount += 1;
                    }

                    if (ippan41.Count == honeycomTextCount)
                        continue;

                    errorSymbols.AddRange(ippan41);
                    errorTexts.AddRange(edanTexts);
                }

                if (errorSymbols.Count == 0 && errorTexts.Count == 0)
                {
                    return null;
                }
                else
                {
                    errorSymbols.ForEach(p => error.AddInfo(p));
                    errorTexts.ForEach(p => error.AddInfo(p));
                }

                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>DLに３本以上の電気配線がある場合エラー</summary>
        public static void ValidateOver3CountDL(List<Symbol> symbols)
        {
            string messageId = @"Your series connection of down light is wrong,so please check and correct it.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var errorLights = symbols.FindAll(p => p.IsDownLight && p.Children.Count >= 2);
                if (errorLights.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                errorLights.ForEach(p => error.AddInfo(p));
                return error;
            };
            validator.Run(messageId);
        }
        /// <summary>CLに３本以上の電気配線がある場合エラー</summary>
        public static void ValidateOver3CountCL(List<Symbol> symbols)
        {
            string messageId = @"Your series connection of ceiling light is wrong,so please check and correct it.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var errorLights = symbols.FindAll(p => p.IsCeilingLight && p.Children.Count >= 2);
                if (errorLights.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                errorLights.ForEach(p => error.AddInfo(p));
                return error;
            };
            validator.Run(messageId);
        }
        #endregion

        #region 承認不可

        /// <summary>同じ場所に同じシンボルがあったらエラー</summary>
        public static void ValidateDuplicatedSymbol(List<Symbol> symbols)
        {
            string messageId =
@"Duplicated symbols found.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var targetSymbols = symbols.FindAll(p => 0 < p.Equipment.Id);
                var duplicatedSymbols = new List<Symbol>();
                foreach (var symbolA in targetSymbols)
                {
                    foreach (var symbolB in targetSymbols)
                    {
                        if (symbolA == symbolB)
                            continue;

                        if (symbolA.Floor != symbolB.Floor)
                            continue;

                        if (symbolA.BlockName != symbolB.BlockName)
                            continue;

                        var differenceX = Math.Abs(symbolA.Position.X - symbolB.Position.X);
                        var differenceY = Math.Abs(symbolA.Position.Y - symbolB.Position.Y);

                        if (differenceX <= 4 && differenceY <= 24)
                            duplicatedSymbols.Add(symbolA);
                        else if (differenceX <= 24 && differenceY <= 4)
                            duplicatedSymbols.Add(symbolA);
                    }
                }

                if (duplicatedSymbols.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                duplicatedSymbols.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>1つのシンボルに同じコメントが複数付いていたらエラー</summary>
        public static void ValidateDuplicatedAttribute(List<Symbol> symbols)
        {
            string messageId =
@"Duplicated symbol's comment found.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var targetSymbols = symbols.FindAll(p => 0 < p.Equipment.Id);
                var duplicatedSymbols = new List<Symbol>();
                foreach (var symbol in targetSymbols)
                {
                    var atts = symbol.OtherAttributes;

                    if (atts.Exists(p => 2 <= atts.FindAll(q => q.Value == p.Value).Count))
                        duplicatedSymbols.Add(symbol);
                }

                if (duplicatedSymbols.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                duplicatedSymbols.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>配線番号の振られていないシンボルがあったらエラー</summary>
        public static void ValidateNoWireNumberSymbol(List<Symbol> breakers)
        {
            string messageId =
@"Wire number is not set. Please use the function to draw number.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var noNumberSymbols = FindAllNoWireNumberSymbol(breakers);

                if (noNumberSymbols.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                noNumberSymbols.ForEach(p => error.AddInfo(p));

                return error;
            };

            validator.Run(messageId);
        }

        private static List<Symbol> FindAllNoWireNumberSymbol(List<Symbol> parentSymbols)
        {
            var list = new List<Symbol>();
            foreach (var symbol in parentSymbols)
            {
                if (symbol.SequenceNo < 0)
                    list.Add(symbol);

                //再帰呼び出し
                list.AddRange(FindAllNoWireNumberSymbol(symbol.Children));
            }
            return list;
        }

        /// <summary>サブスイッチVer　配線番号の振られていないシンボルがあったらエラー</summary>
        public static void ValidateNoWireNumberForSubSwitch(List<Symbol> symbols)
        {
            string messageId =
@"Wire number is not set. Please use the function to draw number.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var subs = symbols.FindAll(p => p.IsSubLightControlSwitch);
                var noNumberSymbols = new List<Symbol>();
                foreach (var sub in subs)
                {
                    var att = sub.Attributes.Find(p => p.Tag == Const.AttributeTag.WIRE_NO);

                    if (att != null)
                        continue;

                    noNumberSymbols.Add(sub);
                }

                if (noNumberSymbols.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                noNumberSymbols.ForEach(p => error.AddInfo(p));

                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>子配線を持たないJBがあったらエラー</summary>
        public static void ValidateNoWireJointBox(List<Symbol> symbols)
        {
            string messageId = @"Joint box has no wire found.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var unconnectedJointBoxes = symbols.FindAll(p => p.IsJointBox && p.Children.Count == 0);
                if (unconnectedJointBoxes.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                unconnectedJointBoxes.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>シンボルに繋がっていない配線があったらエラー（GetWiredSymbols(加工依頼後)用）</summary>
        public static void ValidateUnconnectedWire(List<Wire> wires, List<Symbol> symbols)
        {
            string messageId = @"Unconnected wire found.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var unconnectedWires = wires.FindAll(p => p.ChildSymbol == null);
                if (unconnectedWires.Count == 0)
                    return null;

                var realUnconnectedWires = GetUnconnectedWires(unconnectedWires, symbols);
                if (realUnconnectedWires.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                realUnconnectedWires.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>未接続の配線の中から、IUBのシンボルにさえ繋がっていない配線を返す</summary>
        private static List<Wire> GetUnconnectedWires(List<Wire> unconnectedWires, List<Symbol> symbols)
        {
            //一条ユニットバスに置くシンボルは接続不可に設定されています。
            //IUBの電気設備は初めからユニットに組み込まれていてユニット配線しないのでそういう設定にしています。

            var unconnectableSymbols = symbols.FindAll(p => !p.IsConnectableForLightElectrical && (p.IsLight || p.IsSwitch || p.IsOutlet));
            var realUnconnectedWires = new List<Wire>();
            foreach (var unconnectedWire in unconnectedWires)
            {
                if (unconnectableSymbols.Exists(p => unconnectedWire.IsConnected(p)))
                    continue;

                //弱電対応
                if (unconnectedWire.IsLightElectric)
                    continue;

                realUnconnectedWires.Add(unconnectedWire);
            }

            return realUnconnectedWires;
        }

        /// <summary>配線されていないシンボルがあったらエラー</summary>
        public static void ValidateUnconnectedSymbol(List<Symbol> symbols)
        {
            string messageId = @"No wire connected to symbol.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                // 弱電は除外するぜ
                // リビングサブライコンも除外するぜ
                var unconnectedSymbols = symbols.FindAll(p =>
                    p.IsConnectableForLightElectrical &&
                    p.Wire == null &&
                    !p.IsLightElectrical &&
                    !p.IsSubLightControlSwitch);

                unconnectedSymbols.RemoveAll(p => p.IsSolarWireTop);

                if (unconnectedSymbols.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                unconnectedSymbols.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>天井パネル上を通っていない配線があったらエラー</summary>
        public static void ValidateWireNotOnCeilingPanel(List<Symbol> symbols)
        {
            string messageId = @"Wire is not passing on the ceiling panel.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var notOnPanelSymbols = symbols.FindAll(p => p.IsConnectableForLightElectrical && p.CeilingPanel == null);

                if (notOnPanelSymbols.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                notOnPanelSymbols.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>1600VA超えの回路があったらエラー</summary>
        public static void Validate1600VAOver(List<Symbol> circuitBreakesOver1600VA, int vaLimit)
        {
            string messageId = "Ordinary kairo is over " + vaLimit + "VA.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                if (circuitBreakesOver1600VA.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                circuitBreakesOver1600VA.ForEach(p => error.AddInfo("CircuitNo:" + p.SequenceNo.ToString()));

                return error;
            };

            validator.Run(messageId);
        }

        public static void NotFoundArrowOfClip(Symbol clip)
        {
            string messageId = @"Not found arrow of clip.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var error = new ErrorDialog(messageId, false);
                error.AddInfo(clip);
                return error;
            };

            validator.Run(messageId);
        }

        public static void NotFoundFrameOfClip(Symbol clip)
        {
            string messageId = @"Not found frame of clip.
Please check arrow is connecting to frame.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var error = new ErrorDialog(messageId, false);
                error.AddInfo(clip);
                return error;
            };

            validator.Run(messageId);
        }

        public static void NotFoundKairo(int familyNumber)
        {
            string messageId = @"Not found kairo.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix != "HZA")
                    return null;

                var error = new ErrorDialog(messageId, false);
                error.AddInfo("FamilyNumber:" + familyNumber);
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>
        /// batteryのテキストが３つ以上存在したらエラー
        /// </summary>
        /// <param name="batteryTexts">batteryのテキスト</param>
        public static void ManyBattery(List<TextObject> batteryTexts)
        {
            string messageId = @"3 or more batteries found.";

            var validator = new Validator();
            validator.Validate = delegate()
            {

                if (batteryTexts.Count < 3)
                    return null;

                var error = new ErrorDialog(messageId, false);
                batteryTexts.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>
        /// エコウィルと蓄電池の数合わせて３つ以上あったらエラー
        /// </summary>
        /// <param name="ecowills">エコウィルのシンボル群</param>
        /// <param name="batteryTexts">蓄電池のテキスト群</param>
        public static void ManyBatteryAndEcowill(List<Symbol> ecowills, List<TextObject> batteryTexts)
        {
            string messageId = @"Total of 3 or more batteries and ecowills found.";

            var validator = new Validator();
            validator.Validate = delegate()
            {

                if (batteryTexts.Count + ecowills.Count < 3)
                    return null;

                var error = new ErrorDialog(messageId, false);
                batteryTexts.ForEach(p => error.AddInfo(p));
                ecowills.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>
        /// エコウィルが３つ以上付いていたらエラー
        /// </summary>
        /// <param name="ecowills">エコウィルのシンボル群</param>
        public static void ManyEcowill(List<Symbol> ecowills)
        {
            string messageId = @"3 or more ecowills found.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                if (ecowills.Count < 3)
                    return null;

                var error = new ErrorDialog(messageId, false);
                ecowills.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        public static void ValidateInterphoneNumber(List<Symbol> symbols)
        {
            var messageId = @"Not found interphone pair.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                using (var server = new SocketPlanServiceNoTimeout())
                {
                    if (server.IsBeforeProcessRequest(Static.ConstructionCode))
                        return null;
                }

                var errors = new List<Symbol>();
                var interphones = symbols.FindAll(p => p.IsInterphone);
                var rimokons = symbols.FindAll(p => p.IsInterphoneRimokon);
                var denSymbols = symbols.FindAll(p => p.Equipment.Name.StartsWith(Const.EquipmentName.電ｺﾒﾝﾄPrefix)
                                                        && (p.HasComment("①") || p.HasComment("②") || p.HasComment("③")));

                if (interphones.Count <= 1)
                    return null;

                foreach (var interphone in interphones)
                {
                    var number = interphone.Attributes.Find(p => p.Value == "①" || p.Value == "②" || p.Value == "③");
                    if (number == null)
                    {
                        errors.Add(interphone);
                        continue;
                    }

                    var overlap = interphones.FindAll(p => p != interphone && p.Attributes.Exists(q => q.Value == number.Value));
                    if (overlap.Count >= 1)
                    {
                        errors.Add(interphone);
                        continue;
                    }

                    if (rimokons.Exists(p => p.Attributes.Exists(q => q.Value == number.Value)))
                        continue;

                    errors.Add(interphone);
                }

                foreach (var rimokon in rimokons)
                {
                    var number = rimokon.Attributes.Find(p => p.Value == "①" || p.Value == "②" || p.Value == "③");
                    if (number == null)
                    {
                        errors.Add(rimokon);
                        continue;
                    }

                    if (interphones.Exists(p => p.Attributes.Exists(q => q.Value == number.Value)))
                        continue;

                    if (denSymbols.Exists(p => p.Attributes.Exists(q => q.Value == number.Value)))
                        continue;

                    errors.Add(rimokon);
                }

                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                errors.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        public static void ValidateOldSymbols()
        {
            string messageId = @"Old symbol is put.
Please re-put by using new symbol.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var errorSymbols = new List<Symbol>();
                var drawings = Drawing.GetAll(false);
                var currentDrawing = Drawing.GetCurrent();
                foreach (var drawing in drawings)
                {
                    if (drawing.Floor != currentDrawing.Floor)
                        drawing.Focus();

                    var allBlocks = AutoCad.Db.BlockTable.GetIds();
                    foreach (var blockId in allBlocks)
                    {
                        var blockName = AutoCad.Db.BlockTableRecord.GetBlockName(blockId);

                        foreach (var objectId in AutoCad.Db.BlockTableRecord.GetBlockReferenceIds(blockId))
                        {
                            //ﾓﾃﾞﾙレイアウト以外に置いたブロックは無視する
                            var ownerId = AutoCad.Db.Object.GetOwnerId(objectId);
                            var ownerBlockName = AutoCad.Db.BlockTableRecord.GetBlockName(ownerId);
                            if (!ownerBlockName.Contains("Model"))
                                continue;

                            if (blockName == Const.BlockName.forWashing
                                || blockName == Const.BlockName.forRef
                                || blockName == Const.BlockName.denkiRimokonKey
                                || blockName == Const.BlockName.newAshimottou
                                || blockName == Const.BlockName.forSecurityAlarm
                                || blockName == Const.BlockName.individualVentSwitch)
                            {
                                var symbol = new Symbol(objectId);
                                symbol.Floor = drawing.Floor;
                                errorSymbols.Add(symbol);
                            }
                        }
                    }
                }

                if (errorSymbols.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                errorSymbols.ForEach(p => error.AddInfo(p));

                return error;
            };

            validator.Run(messageId);
        }

        public static void ValidateAlrearyOptionPickingWithHems(string constructionCode)
        {
            string messageId = @"This construction was already generating option picking with hems,
so please generate option picking with hems.";
            var validator = new Validator();
            validator.Validate = delegate()
            {
                bool isExporting;
                using (var service = new SocketPlanServiceNoTimeout())
                {
                    isExporting = service.IsExportingXMlData(constructionCode);
                }

                if (!isExporting)
                    return null;

                var error = new ErrorDialog(messageId, false);
                return error;
            };
            validator.Run(messageId);
        }

        /// <summary>
        /// ROHM製を使用している現場であるなら使用している照明のチェック
        /// </summary>
        /// <param name="constructionCode"></param>
        /// <param name="symbols"></param>
        public static void ValidateRohmLightCountIsMissMatch(string constructionCode, List<Symbol> symbols)
        {
            string messageId = @"Light serial of out of stock is used.
Please check current light serial qty below and ask for information to SMD.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                List<Symbol> lightSymbols = symbols.FindAll(p => p.IsLight);
                if (lightSymbols.Count == 0)
                    return null;

                var usableLights = new List<UsableRohmOfLightSerial>();
                using (var service = new SocketPlanService())
                {
                    usableLights.AddRange(service.GetUsableRohmOfLightSerial(constructionCode));
                }

                if (usableLights.Count == 0)
                    return null;

                //エラー検出用のリスト
                var dwgUsabletLights = new List<UsableLightSerial>();
                foreach (var usableLight in usableLights)
                {
                    var temp = new UsableLightSerial();
                    temp.ConstructionCode = usableLight.ConstructionCode;
                    temp.LightSerial = usableLight.LightSerial;
                    temp.UsableCount = 0;
                    temp.UpdatedDateTime = usableLight.UpdatedDateTime;
                    dwgUsabletLights.Add(temp);
                }

                foreach (var lightSymbol in lightSymbols)
                {
                    var light = new Light(lightSymbol);
                    var serials = light.GetSerialsForCount();

                    foreach (var serial in serials)
                    {
                        var dwgUsableLight = dwgUsabletLights.Find(p => p.LightSerial == serial);
                        if (dwgUsableLight == null)
                            continue;

                        dwgUsableLight.UsableCount++;
                    }
                }

                var errorMessages = new List<string>();
                foreach (var usableLight in usableLights)
                {
                    var errorLight = dwgUsabletLights.Find(p => p.LightSerial == usableLight.LightSerial && p.UsableCount != usableLight.UsableCount);
                    if (errorLight == null)
                        continue;

                    errorMessages.Add(errorLight.LightSerial + " is " + errorLight.UsableCount + ": currentQty :" + usableLight.UsableCount);
                }

                if (errorMessages.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                errorMessages.ForEach(p => error.AddInfo(p));

                return error;
            };

            validator.Run(messageId);
        }

        public static void ValidateDupliactedCheckboxNo(List<ConnectorCheckBox> connectorBoxs)
        {
            string messageId =
@"Check box name is duplicated, 
please check.";
            var validator = new Validator();
            validator.Validate = delegate()
            {
                var errors = new List<ConnectorCheckBox>();

                foreach (var connectorBox in connectorBoxs)
                {
                    var box = connectorBoxs.FindAll(p => p.ItemNo == connectorBox.ItemNo);
                    if (box.Count > 1)
                    {
                        errors.Add(connectorBox);
                    }
                }
                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                errors.ForEach(p => error.AddInfo(p.ItemNo));
                return error;

            };
            validator.Run(messageId);
        }

        public static void ValidatePendantDownLightSerials(List<Symbol> symbols)
        {
            string messageId =
@"Invalid light serial was found. 
please check a light.";
            var validator = new Validator();
            validator.Validate = delegate()
            {
                var errors = new List<Symbol>();
                var downLights = symbols.FindAll(p => p.IsDownLight);
                var pendantLightSerials = new List<PendantLightSerial>();

                if (downLights.Count == 0)
                    return null;
                using (var service = new SocketPlanServiceNoTimeout())
                {
                    pendantLightSerials = new List<PendantLightSerial>(service.GetPendantLightSerialsAll());
                }
                foreach (var downLight in downLights)
                {
                    var light = new Light(downLight);
                    var serials = light.GetSerialsForCount();

                    foreach (var pendantLightSerial in pendantLightSerials)
                    {
                        if (serials.Exists(p => p == pendantLightSerial.LightSerial))
                        {
                            if (pendantLightSerial.IsImplant == false)
                                errors.Add(downLight);
                        }
                    }
                }
                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                errors.ForEach(p => error.AddInfo(p));
                return error;

            };
            validator.Run(messageId);
        }

        public static void ValidateWirePickingReportNullCells(ExcelSheetProxy sheet)
        {
            string messageId =
@"There is a missing information in WPR, 
Please check and fill it up and try to generate again.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                var errors = new List<string>();
                int rowNum = 2;
                int maxColumnNum = 0;
                List<int> continueColumnNum = new List<int>();
                int ceilingPanelWirePassesCellCount = 0;
                int cellChar = (int)'A';
                List<string> nullPosition = new List<string>();
                switch (sheet.Name)
                {
                    case "Wire Picking Report":
                        maxColumnNum = 12;
                        continueColumnNum.Add(4);
                        ceilingPanelWirePassesCellCount = 1;
                        break;
                    case "Light Electric for Haikan":
                        maxColumnNum = 14;
                        continueColumnNum.Add(6);
                        ceilingPanelWirePassesCellCount = 1;
                        break;
                    case "Light Electric for Cable":
                        maxColumnNum = 12;
                        break;
                    default:
                        maxColumnNum = 0;
                        break;
                }

                while (true)
                {
                    for (int i = 1; i <= maxColumnNum; i++)
                    {
                        if (continueColumnNum.Exists(p => p == i))
                            continue;
                        if (sheet.Get(rowNum, i) == null)
                        {
                            nullPosition.Add(Convert.ToChar(cellChar + i - 1) + rowNum.ToString());
                        }
                    }

                    if (nullPosition.Count > 0)
                    {
                        if (nullPosition.Count == maxColumnNum - ceilingPanelWirePassesCellCount)
                            break;
                        else
                        {
                            errors.AddRange(nullPosition);
                            nullPosition.Clear();
                        }
                    }
                    rowNum++;
                }

                if (errors.Count == 0)
                    return null;
                var error = new ErrorDialog(messageId, false);
                error.AddInfo(sheet.Name);
                errors.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        #endregion

        #region 警告

        public static void WarnHikikomiAriFor売電(List<Symbol> symbols)
        {
            //機能9-3,9-4,9-5連続実行すると、この警告が連発してしまう・・・ま、いっか。言われたら修正しよう。

            string messageId1 = @"Bundenban and Meter is more than 7 grids, check if 引き込み(売電用) is 有.";
            string messageId2 = @"Bundenban and Meter is more than 4 grids, check if 引き込み(売電用) is 有.";

            ShikakuTableEntry entry = null;
            using (var service = new SocketPlanService())
            {
                entry = service.GetShikakuTableEntry(Static.ConstructionCode, Const.ShikakuItemId.HIKIKOMI_BAIDEN_ARI);
            }

            if (entry != null)
            {
                if (Convert.ToBoolean(entry.Value))
                    return;
            }

            var bundenban = symbols.Find(p => Utilities.In(p.Equipment.Name, Const.EquipmentName.分電盤_3,
                                                                             Const.EquipmentName.分電盤_4));

            var meter = symbols.Find(p => p.Equipment.Name == Const.EquipmentName.全量買取メーター
                                        || p.Equipment.Name == Const.EquipmentName.全量買取用メーター);

            if (bundenban == null || meter == null)
                return;

            var distanceXY = bundenban.ActualPosition.Minus(meter.ActualPosition);
            var distance = Convert.ToDecimal(Math.Abs(distanceXY.X) + Math.Abs(distanceXY.Y));

            if (bundenban.Floor == meter.Floor)
            {
                if (bundenban.Floor == 1)
                {
                    if (910 * 7 <= distance) //7グリッド以上なら「売電用あり」にチェックすべき
                        MessageDialog.ShowWarning(messageId1);
                }
                else
                {
                    if (910 * 4 <= distance) //4グリッド以上なら「売電用あり」にチェックすべき
                        MessageDialog.ShowWarning(messageId2);
                }
            }
        }

        public static void WarnHikikomiAriFor買電(List<Symbol> symbols)
        {
            string messageId1 = @"Bundenban and Meter is more than 7 grids, check if 引き込み(買電用) is 有.";
            string messageId2 = @"Bundenban and Meter is more than 4 grids, check if 引き込み(買電用) is 有.";

            ShikakuTableEntry entry = null;
            using (var service = new SocketPlanService())
            {
                entry = service.GetShikakuTableEntry(Static.ConstructionCode, Const.ShikakuItemId.HIKIKOMI_KAIDEN_ARI);
            }

            if (entry != null)
            {
                if (Convert.ToBoolean(entry.Value))
                    return;
            }

            var bundenban = symbols.Find(p => Utilities.In(p.Equipment.Name, Const.EquipmentName.分電盤,
                                                                             Const.EquipmentName.分電盤_2,
                                                                             Const.EquipmentName.分電盤_5));

            var meter = symbols.Find(p => p.Equipment.Name == Const.EquipmentName.Meter
                                        || p.Equipment.Name == Const.EquipmentName.Meter_01);

            if (bundenban == null || meter == null)
                return;

            var distanceXY = bundenban.ActualPosition.Minus(meter.ActualPosition);
            var distance = Convert.ToDecimal(Math.Abs(distanceXY.X) + Math.Abs(distanceXY.Y));

            if (bundenban.Floor == meter.Floor)
            {
                if (bundenban.Floor == 1)
                {
                    if (910 * 7 <= distance) //7グリッド以上なら「売電用あり」にチェックすべき
                        MessageDialog.ShowWarning(messageId1);
                }
                else
                {
                    if (910 * 4 <= distance) //4グリッド以上なら「売電用あり」にチェックすべき
                        MessageDialog.ShowWarning(messageId2);
                }
            }
        }

        #endregion

        #region SignalWire用Validation
        public static void ValidateUnconnectedSignalWire(List<Symbol> symbols, List<Wire> signalWires)
        {
            string messageId = "Unconnected Signal Wire";
            var validator = new Validator();
            validator.Validate = delegate()
            {
                var errorWires = new List<Wire>();

                var switches = symbols.FindAll(p => p.IsSwitch && p.IsSignalWireConnectable);
                var lights = symbols.FindAll(p => p.IsLight);
                foreach (var signalWire in signalWires)
                {
                    var connectedSymbols = symbols.FindAll(p => signalWire.IsConnected(p));

                    //接続されているシンボルが2個ではなかったらエラー
                    if (connectedSymbols.Count != 2)
                        errorWires.Add(signalWire);

                    //2個だったらライコンシリアル持ちのスイッチか、ライトである。
                    foreach (var symbol in connectedSymbols)
                    {
                        if (symbol.IsSignalWireConnectable)
                            continue;

                        if (symbol.IsLight)
                            continue;

                        errorWires.Add(signalWire);
                    }
                }

                if (errorWires.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                errorWires.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }
        #endregion

        #region
        public static void ValidateNotFoundCheckbox(List<TextObject> texts, List<Symbol> symbols)
        {
            string messageId = @"There are some connector without check box, 
please check and generate again.";
            var validator = new Validator();
            validator.Validate = delegate()
            {
                var symbolTexts = new List<string>();
                foreach (var symbol in symbols)
                {
                    var others = symbol.OtherAttributes;
                    if (others.Count == 0)
                        continue;
                    others.ForEach(p => symbolTexts.Add(p.Value));
                }

                foreach (var attribute in symbolTexts)
                {
                    var text = texts.Find(p => p.Text == attribute);
                    if (text != null)
                        texts.Remove(text);
                }

                if (texts.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                texts.ForEach(p => error.AddInfo(p.Text));
                return error;
            };

            validator.Run(messageId);
        }
        #endregion
    }
}