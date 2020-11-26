using System;
using System.Collections.Generic;
using SocketPlan.WinUI.SocketPlanServiceReference;
using Edsa.AutoCadProxy;
using System.Text.RegularExpressions;
using SocketPlan.WinUI.Entities.CADEntity;

namespace SocketPlan.WinUI
{
    public partial class Validation
    {

        #region 承認可能

        /// <summary>シャワールームに適さない品番が使われていた場合エラー</summary>
        public static void ValidateShowerRoomMissingSerial(List<TextObject> texts, List<Symbol> symbols)
        {
            string messageId = @"Wrong hontai serial used for shower room.";
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
                    var showerRooms = texts.FindAll(p => p.Text == Const.Room.ｼｬﾜｰﾙｰﾑ && p.Floor == i);
                    var iSeriesRooms = texts.FindAll(p => p.Text == Const.Room.iｼﾘｰｽﾞ && p.Floor == i);
                    var serials = symbols.FindAll(p => (p.Equipment.Name == Const.EquipmentName.ｼｬﾜｰ01 || p.Equipment.Name == Const.EquipmentName.IUB_01) && p.Floor == i);

                    //判定外のシリアルが使われている場合もあるので、シリアルを基準に判定する
                    foreach (var serial in serials)
                    {
                        if (serial.Equipment.Name == Const.EquipmentName.ｼｬﾜｰ01)
                        {
                            if (showerRooms.Count > 0)
                                showerRooms.RemoveAt(0);
                            else
                                errors.Add(i.ToString() + "F");
                        }
                        else
                        {
                            if (iSeriesRooms.Count > 0)
                                iSeriesRooms.RemoveAt(0);
                            else
                                errors.Add(i.ToString() + "F");
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

        //TODO ValidateNoConnectionSwitchを使うのが正しい？
        //TODO ValidateNoConnectionSwitchメソッドの内容にHJAチェックを付けただけ。
        /// <summary>スイッチと他のシンボルがつながっていなかった場合エラー(HJAのみ)</summary>
        public static void ValidateUnconnectedSwitch(List<Symbol> symbols, List<Wire> wires)
        {
            string messageId = @"Switch doesn't connect other symbol.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix != "HJA")
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

        /// <summary>FireAlermに壁付コメントがあるのに、高さコメントがなければエラー</summary>
        public static void ValidateInputWallFireAlermHeight(List<Symbol> symbols)
        {
            string messageId = @"There is no indication of height for wall kemuri.";

            var validator = new Validator();

            validator.Validate = delegate()
            {
                //Fire Alarmカテゴリに含まれるシンボルに絞る
                var fireAlarmCategory = UnitWiring.Masters.SelectionCategories.Find(p => p.Name.Contains("Fire Alarm"));
                if (fireAlarmCategory == null)
                    return null;

                var alarms = symbols.FindAll(symbol => Array.Exists(fireAlarmCategory.Equipments, q => symbol.Equipment.Id == q.Id));
                if (alarms.Count == 0)
                    return null;

                //壁付き属性をもったシンボルに絞る
                var wallAlarms = alarms.FindAll(p => p.Attributes.Exists(q => q.Value == Const.Text.壁付));
                if (wallAlarms.Count == 0)
                    return null;

                //壁付き属性をもったシンボルから、エラー対象のシンボルに絞る
                var noHeightComments = new List<Symbol>();
                foreach (var wallAlarm in wallAlarms)
                {
                    var height = wallAlarm.Attributes.Find(p => p.Tag == Const.AttributeTag.HEIGHT);
                    if (height == null)
                        continue;

                    if (string.IsNullOrEmpty(height.Value) || height.Value == "H=0")
                        noHeightComments.Add(wallAlarm);
                }

                if (noHeightComments.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                noHeightComments.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>NR3160-02とJB-Dが同時に存在したらエラー、NR3160-02とJB-MAINが同時に存在してもエラー</summary>
        public static void ValidateNR3160_02(List<Symbol> symbols)
        {
            string messageId = @"Need to delete NR3160 because there's JB-D/JB-DA/JB machine in floor plan.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                if (Static.Drawing.Prefix == "HZA")
                    return null;

                //NR3160-02のシンボルで絞る
                var nrSymbols = symbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.NR3160_02);
                if (nrSymbols.Count == 0)
                    return null;

                var jbSymbols = symbols.FindAll(p => p.IsJBox || p.Equipment.Name == Const.EquipmentName.JB_MAIN);

                if (jbSymbols.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                nrSymbols.ForEach(p => error.AddInfo(p));
                jbSymbols.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>重複したリモコンがあったらエラー</summary>
        public static void ValidateDuplicateRemocon(List<Symbol> symbols)
        {
            string messageId = @"Duplicate remote pattern, please cheeck.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var rimokonNames = new List<string>();
                rimokonNames.Add(Const.EquipmentName.ﾘﾓｺﾝ01);
                rimokonNames.Add(Const.EquipmentName.ﾘﾓｺﾝ02);
                rimokonNames.Add(Const.EquipmentName.ﾘﾓｺﾝ03);
                rimokonNames.Add(Const.EquipmentName.ﾘﾓｺﾝ04);
                rimokonNames.Add(Const.EquipmentName.ﾘﾓｺﾝ05);
                rimokonNames.Add(Const.EquipmentName.ﾘﾓｺﾝ06);
                rimokonNames.Add(Const.EquipmentName.ﾘﾓｺﾝ07);
                rimokonNames.Add(Const.EquipmentName.ﾘﾓｺﾝ08);
                rimokonNames.Add(Const.EquipmentName.ﾘﾓｺﾝ09);
                rimokonNames.Add(Const.EquipmentName.ﾘﾓｺﾝ10);
                rimokonNames.Add(Const.EquipmentName.ﾘﾓｺﾝ16);
                rimokonNames.Add(Const.EquipmentName.ﾘﾓｺﾝ17);
                rimokonNames.Add(Const.EquipmentName.ﾘﾓｺﾝ18);
                rimokonNames.Add(Const.EquipmentName.ﾘﾓｺﾝ19);

                var errors = new List<Symbol>();

                foreach (var name in rimokonNames)
                {
                    var rimokons = symbols.FindAll(p => p.Equipment.Name == name);
                    if (rimokons.Count >= 2)
                        errors.AddRange(rimokons);
                }

                //var remotes = symbols.FindAll(p => p.Equipment.Name.StartsWith("ﾘﾓｺﾝ"));
                //if (remotes.Count == 0)
                //    return null;

                //var errors = new List<Symbol>();
                //foreach (var remote in remotes)
                //{
                //    var duplicated = remotes.FindAll(p => p.Equipment.Name == remote.Equipment.Name);
                //    if (duplicated.Count >= 2)
                //        errors.Add(remote);
                //}

                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                errors.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }


        /// <summary>
        /// シンボルのSwitchSerial属性に未登録のスイッチシリアルが設定されていた場合エラー
        /// </summary>
        /// <param name="symbols"></param>
        public static void ValidateUnregisteredSwitchSerial(List<Symbol> symbols)
        {
            string messageId = @"There are switch serials that are not in the list of modified serials.
Please Check!";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                List<Symbol> switchSymbols = symbols.FindAll(p => p.IsSwitch);
                List<Comment> switchSerialComments = UnitWiring.Masters.Comments.FindAll(p => p.CategoryId == Const.CommentCategoryId.SerialForUnitWiringOnly);

                //未登録のスイッチシリアルが付与されているシンボルを詰めるリスト
                List<Symbol> unregisteredSwitches = new List<Symbol>();
                foreach (Symbol switchSymbol in switchSymbols)
                {
                    Attribute switchSerialAttr = switchSymbol.Attributes.Find(p => p.Tag == Const.AttributeTag.SWITCH_SERIAL);
                    if (switchSerialAttr == null)
                        continue;

                    if (!switchSerialComments.Exists(p => p.Text == switchSerialAttr.Value))
                        unregisteredSwitches.Add(switchSymbol);
                }

                if (unregisteredSwitches.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                unregisteredSwitches.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        #endregion 承認可能

        #region 承認不可


        #region 在庫チェック

        /// <summary>
        /// 在庫のない照明を使用してたらエラー
        /// </summary>
        /// <param name="constractionCode">工事コード</param>
        /// <param name="symbols">シンボル群</param>
        public static void ValidateLightSerialOutOfStock(List<Symbol> symbols)
        {

            string messageId =
@"Light serial of out of stock is used.
Please check light serial.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                List<Symbol> lightSymbols = symbols.FindAll(p => p.IsLight);
                if (lightSymbols.Count == 0)
                    return null;

                //在庫無しの品番情報
                List<LightSerial> emptyStockLightSerials;
                //邸ごとの使用できる品番情報
                List<UsableLightSerial> usableLightSerials;
                //加工前依頼か否か
                bool isBeforeProcessRequest;
                using (var service = new SocketPlanServiceNoTimeout())
                {
                    emptyStockLightSerials = new List<LightSerial>(service.GetEmptyStockLightSerials());
                    usableLightSerials = new List<UsableLightSerial>(service.GetUsableLightSerials(Static.ConstructionCode));
                    isBeforeProcessRequest = service.IsBeforeProcessRequest(Static.ConstructionCode);
                }

                //非許可の照明品番が設定されていた照明シンボルを設定するリスト
                List<Symbol> denialLightSymbols = new List<Symbol>();
                //非許可の品番名を設定するリスト
                List<String> deniialLightHinbanName = new List<string>();

                List<Light> lights = new List<Light>();
                lightSymbols.ForEach(p => lights.Add(new Light(p)));

                foreach (Light light in lights)
                {
                    List<string> hinbans = light.GetSerialsForDistinct();
                    foreach (string hinban in hinbans)
                    {
                        if (!emptyStockLightSerials.Exists(p => p.Name == hinban))
                            continue;

                        if (IsUsableHinban(hinban, isBeforeProcessRequest, usableLightSerials))
                            continue;

                        if (denialLightSymbols.IndexOf(light.Symbol) == -1)
                            denialLightSymbols.Add(light.Symbol);

                        if (deniialLightHinbanName.IndexOf(hinban) == -1)
                            deniialLightHinbanName.Add(hinban);
                    }
                }

                if (denialLightSymbols.Count == 0 && deniialLightHinbanName.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                denialLightSymbols.ForEach(a => error.AddInfo(a));
                deniialLightHinbanName.ForEach(a => error.AddInfo(a + " out of stock."));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>
        /// 指定の照明品番名が使用できるか否かを判定
        /// </summary>
        /// <param name="hinban">品番名</param>
        /// <param name="isBeforeProcessReques">加工依頼前か否か</param>
        /// <param name="usableLightSerials">UsableLightSerialテーブルのエンティティ</param>
        /// <returns>true = 使用可</returns>
        private static bool IsUsableHinban(string hinban, bool isBeforeProcessReques, List<UsableLightSerial> usableLightSerials)
        {
            //加工依頼前の物件であれば使用不可
            if (isBeforeProcessReques)
                return false;

            //加工依頼後の場合は、許可されていた場合のみ使える
            if (usableLightSerials.Exists(p => p.LightSerial == hinban))
                return true;

            return false;
        }

        #endregion 在庫チェック

        #region 数量チェック
        /// <summary>
        /// 登録されている数量と図面上の数量が一致しなかったらエラー
        /// </summary>
        /// <param name="lights">照明情報</param>
        /// <param name="emptyLightSerials">在庫が空である照明品番情報</param>
        /// <param name="usableLightSerials">使用可能な照明品番情報</param>
        public static void ValidateLightSerialCountNotMatch(List<Symbol> symbols)
        {
            string messageId =
@"The count of light serial and stocked light serial does not match.
Please inquire of SMD.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                List<Symbol> lightSymbols = symbols.FindAll(p => p.IsLight);

                if (lightSymbols.Count == 0)
                    return null;

                //在庫無しの品番情報
                List<LightSerial> emptyStockLightSerials;
                //邸ごとの使用できる品番情報
                List<UsableLightSerial> usableLightSerials;
                using (var service = new SocketPlanServiceNoTimeout())
                {
                    emptyStockLightSerials = new List<LightSerial>(service.GetEmptyStockLightSerials());
                    usableLightSerials = new List<UsableLightSerial>(service.GetUsableLightSerials(Static.ConstructionCode));
                }

                if (emptyStockLightSerials.Count == 0)
                    return null;

                List<Light> lights = new List<Light>();
                lightSymbols.ForEach(p => lights.Add(new Light(p)));

                //在庫数が一致しなかった照明シンボルを設定するリスト
                List<Symbol> notMatchLightSymbols = new List<Symbol>();
                //在庫数が一致しなかった品番名を設定するリスト
                List<String> notMatchLightHinbanNames = new List<string>();

                foreach (LightSerial emptyLightSerial in emptyStockLightSerials)
                {
                    UsableLightSerial usableLightSerial = usableLightSerials.Find(p => p.LightSerial == emptyLightSerial.Name);

                    if (usableLightSerial == null)//在庫数情報が取れなかった場合は在庫比較の判断ができない為。
                        continue;

                    List<Light> stockEmptyLights = new List<Light>();
                    int totalStockEmptyLightCount = 0;

                    //図面内に存在する在庫無し品番をカウント
                    foreach (Light light in lights)
                    {
                        List<string> hinbans = light.GetSerialsForCount();
                        int count = hinbans.FindAll(p => p == emptyLightSerial.Name).Count;
                        if (count == 0)
                            continue;

                        stockEmptyLights.Add(light);
                        totalStockEmptyLightCount += count;
                    }

                    if (totalStockEmptyLightCount == usableLightSerial.UsableCount)
                        continue;

                    //図面上と登録されている拾い数量が一致してなければエラーを表示
                    foreach (var light in stockEmptyLights)
                    {
                        if (notMatchLightSymbols.IndexOf(light.Symbol) == -1)
                            notMatchLightSymbols.Add(light.Symbol);
                    }

                    string infoMessage = emptyLightSerial.Name + " count " + totalStockEmptyLightCount + ", stock count " + usableLightSerial.UsableCount; ;
                    notMatchLightHinbanNames.Add(infoMessage);

                }

                if (notMatchLightSymbols.Count == 0 && notMatchLightHinbanNames.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);

                notMatchLightSymbols.ForEach(a => error.AddInfo(a));
                notMatchLightHinbanNames.ForEach(a => error.AddInfo(a));


                return error;
            };

            validator.Run(messageId);
        }
        #endregion 数量チェック


        #region マーキングシンボル系
        /// <summary>
        /// Wireが繋がっていないマーキングシンボルが有ったらエラー
        /// </summary>
        /// <param name="symbols">マーキングシンボル</param>
        /// <param name="wires"></param>
        public static void ValidateUnconnectedMarkingSymbol(List<Symbol> markingSymbols, List<Wire> wires)
        {
            string messageId =
@"Marking is unconnected to Wire";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                List<Symbol> unconnectedMarking = new List<Symbol>();

                foreach (Symbol marking in markingSymbols)
                {
                    List<Wire> connectedWire = wires.FindAll(p => p.IsConnected(marking));

                    if (connectedWire.Count == 0)
                        unconnectedMarking.Add(marking);
                }

                if (unconnectedMarking.Count == 0)
                    return null;


                var error = new ErrorDialog(messageId, false);
                unconnectedMarking.ForEach(a => error.AddInfo(a));
                return error;
            };

            validator.Run(messageId);
        }


        /// <summary>
        /// マーキングに一本のWireしか繋がっていなかったらエラー
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="wires"></param>
        public static void ValidateConnectedOnlyOneMarkingWire(List<Symbol> markingSymbols, List<Wire> wires)
        {
            string messageId =
@"Marking is connected only one to Wire";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                //一本のワイヤーしか接続されていないマーキングシンボル
                List<Symbol> onlyOneMarkings = new List<Symbol>();

                foreach (Symbol marking in markingSymbols)
                {
                    List<Wire> connectedWire = wires.FindAll(p => p.IsConnected(marking));

                    if (connectedWire.Count == 1)
                        onlyOneMarkings.Add(marking);
                }

                if (onlyOneMarkings.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                onlyOneMarkings.ForEach(a => error.AddInfo(a));
                return error;
            };

            validator.Run(messageId);
        }


        /// <summary>
        /// マーキングに三本以上のWireが繋がっていたらエラー
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="wires"></param>
        public static void ValidateConnectedThreeOrMoreMarkingWire(List<Symbol> markingSymbols, List<Wire> wires)
        {
            string messageId =
@"Marking is connected three Wire or more to Wire.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                List<Symbol> threeOrMoreMarkings = new List<Symbol>();
                foreach (Symbol marking in markingSymbols)
                {
                    List<Wire> connectedWire = wires.FindAll(p => p.IsConnected(marking));

                    if (3 <= connectedWire.Count)
                        threeOrMoreMarkings.Add(marking);
                }

                if (threeOrMoreMarkings.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                threeOrMoreMarkings.ForEach(a => error.AddInfo(a));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>
        /// マーキングに繋がるワイヤーが、「電気_配線」以外に存在していたらエラー
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="wires"></param>
        public static void ValidateConnectedMarkingWireLayer(List<Symbol> markingSymbols, List<Wire> wires)
        {
            string messageId =
@"It is not possible to connect it except Normal wire.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                //「電気_配線」以外のワイヤー
                List<Wire> exceptDenkiHaisenWires = new List<Wire>();

                foreach (Symbol marking in markingSymbols)
                {
                    List<Wire> connectedWires = wires.FindAll(p => p.IsConnected(marking) && p.Layer != Const.Layer.電気_配線);

                    if (connectedWires.Count != 0)
                        exceptDenkiHaisenWires.AddRange(connectedWires);
                }

                if (exceptDenkiHaisenWires.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                exceptDenkiHaisenWires.ForEach(a => error.AddInfo(a));
                return error;

            };

            validator.Run(messageId);
        }


        /// <summary>
        /// マーキングと階またぎアローがワイヤーと接続されてたら、存在していたらエラー
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="wires"></param>
        public static void ValidateConnectedMarkingToArrow(List<Symbol> markingSymbols, List<Symbol> arrows, List<Wire> wires)
        {
            string messageId =
@"Marking and Floor Arrow cannot be connected. ";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                //マーキングと階またぎアローが接続されているワイヤー
                List<Wire> connectedMarkingToArrowWires = new List<Wire>();

                foreach (Wire wire in wires)
                {
                    bool isMarkingConnected = markingSymbols.Exists(p => wire.IsConnected(p));
                    bool isArrowConnected = arrows.Exists(p => wire.IsConnected(p));

                    if (isMarkingConnected && isArrowConnected)
                        connectedMarkingToArrowWires.Add(wire);
                }

                if (connectedMarkingToArrowWires.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                connectedMarkingToArrowWires.ForEach(a => error.AddInfo(a));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>
        /// 青マーキングを使用して天井設置ワイヤー同士を結合しようとしていたらエラー
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="wires"></param>
        public static void ValidateSameWireCombine(List<Symbol> markingSymbols, List<Wire> wires)
        {
            string messageId =
@"Under Floor Drop function is combine of Normal wire cannot be done by using.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                List<Symbol> buleMarkings = markingSymbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.MarkingBule);
                List<Wire> errorWires = new List<Wire>();
                List<Symbol> errorSymbols = new List<Symbol>();

                foreach (Symbol buleMarking in buleMarkings)
                {
                    List<Wire> connectedWires = wires.FindAll(p => p.IsConnected(buleMarking));

                    int normalCount = 0;

                    foreach (Wire connectedWire in connectedWires)
                    {
                        if (connectedWire.IsUnderfloor)
                            continue;

                        normalCount++;
                    }

                    if (2 <= normalCount)
                    {
                        errorSymbols.Add(buleMarking);
                        errorWires.AddRange(connectedWires);
                    }
                }

                if (errorWires.Count == 0 && errorSymbols.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                errorWires.ForEach(a => error.AddInfo(a));
                errorSymbols.ForEach(a => error.AddInfo(a));
                return error;


            };
            validator.Run(messageId);
        }


        /// <summary>
        /// 緑色と黄色マーキングで、床下配線が使用されていたらエラー
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="wires"></param>
        public static void ValidateUnderFloorWireCombine(List<Symbol> markingSymbols, List<Wire> wires)
        {
            string messageId =
@"Please use function to Under Floor Drop when combine Underfloor wire.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                List<Symbol> markings = markingSymbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.MarkingGreen || p.Equipment.Name == Const.EquipmentName.MarkingYellow);
                List<Wire> errorWires = new List<Wire>();
                List<Symbol> errorSymbols = new List<Symbol>();

                foreach (Symbol marking in markings)
                {
                    List<Wire> connectedWires = wires.FindAll(p => p.IsConnected(marking));
                    List<Wire> underFloorWires = new List<Wire>();

                    foreach (Wire connectedWire in connectedWires)
                    {
                        if (connectedWire.IsUnderfloor)
                            underFloorWires.Add(connectedWire);
                    }

                    if (underFloorWires.Count == 0)
                        continue;

                    errorSymbols.Add(marking);
                    errorWires.AddRange(underFloorWires);
                }

                if (errorWires.Count == 0 && errorSymbols.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                errorWires.ForEach(a => error.AddInfo(a));
                errorSymbols.ForEach(a => error.AddInfo(a));
                return error;


            };
            validator.Run(messageId);
        }

        /// <summary>
        /// CombineWireの構成で、3個以上黄色または緑色のマーキングを使用していたらエラー
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="wires"></param>
        public static void ValidateYellowAndGreenOneEach(List<MarkingWire> combineWires)
        {
            string messageId =
@"Ceiling drop point is drawn for wiring by three point or more.
Ceiling drop point(Yellow or Green Marking) is up to two.  
";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                List<Wire> errorWires = new List<Wire>();
                List<Symbol> errorMarkings = new List<Symbol>();

                foreach (MarkingWire combine in combineWires)
                {
                    //マーキングの内訳
                    var yellows = combine.MarkingSymbols.FindAll(p => p.IsYellowMarking);
                    var greens = combine.MarkingSymbols.FindAll(p => p.IsGreenMarking);

                    int totalMarkingCount = yellows.Count + greens.Count;

                    if (3 <= totalMarkingCount)
                    {
                        errorWires.AddRange(combine.Wires);
                        errorMarkings.AddRange(yellows);
                        errorMarkings.AddRange(greens);
                        break;
                    }
                }

                if (errorWires.Count == 0 && errorMarkings.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                errorWires.ForEach(a => error.AddInfo(a));
                errorMarkings.ForEach(a => error.AddInfo(a));
                return error;

            };

            validator.Run(messageId);
        }

        /// <summary>
        /// オレンジマーキングが電気配線に重なっていない場合エラー
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="wires"></param>
        public static void ValidateOrangeMarkingWire(List<Wire> wires, List<Symbol> orangeMarkings)
        {
            string messageId = @"There was marking symbol without the connection.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var errorMarkings = new List<Symbol>();
                foreach (Symbol orangeMarking in orangeMarkings)
                {
                    List<Wire> penetrateWires = wires.FindAll(p => p.IsCrossover(orangeMarking));
                    if (penetrateWires.Count == 0)
                        errorMarkings.Add(orangeMarking);
                }

                if (errorMarkings.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                errorMarkings.ForEach(p => error.AddInfo(p));
                return error;
            };
            validator.Run(messageId);
        }

        /// <summary>
        /// オレンジマーキングと別の色が混在していたらエラー
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="wires"></param>
        public static void ValidateOrangeMarkingOthers(List<MarkingWire> wires)
        {
            string messageId = @"The orange and other marking cannot be used at the same wire.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var errorWires = new List<Wire>();
                foreach (var wire in wires) 
                {
                    if (wire.Wires.Exists(p => p.withOrangeMarking))
                        errorWires.Add(wire);
                }

                if (errorWires.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                errorWires.ForEach(p => error.AddInfo(p));
                return error;
            };
            validator.Run(messageId);
        }
        #endregion マーキングシンボル系


        #endregion 承認不可

        #region 警告

        /// <summary>カップボードがあったら警告メッセージを表示する</summary>
        public static void WarnCupboardSerial(List<TextObject> texts)
        {
            string messageId = @"There's cupboard in floor plan please check height of items installed in it.";

            List<CupboardSerial> serials = UnitWiring.Masters.CupboardSerials;
            var cupboardTexts = texts.FindAll(p => serials.Exists(q => q.Name == p.Text));
            if (cupboardTexts.Count == 0)
                return;

            var error = new ErrorDialog(messageId, 0);
            cupboardTexts.ForEach(p => error.AddInfo(p));
            error.ShowDialog();
        }

        #endregion
    }
}