using System;

using System.Collections.Generic;
using SocketPlan.WinUI.SocketPlanServiceReference;
using Edsa.AutoCadProxy;
using System.Text.RegularExpressions;

namespace SocketPlan.WinUI
{
    public partial class Validation
    {
        #region 弱電承認可

        //警報機シンボルがないときエラー
        public static void ValidateSecuritySymbol(List<Symbol> symbols)
        {
            string messageId = @"Please check security alarm and remote in floor plan.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var errors = new List<string>();
                var alarms = symbols.FindAll(p => p.Floor == 1);

                if (!alarms.Exists(p => p.Equipment.Name == Const.EquipmentName.ﾘﾓｺﾝ18) &&
                   !alarms.Exists(p => p.Equipment.Name == Const.EquipmentName.ﾘﾓｺﾝ16) &&
                   !alarms.Exists(p => p.Equipment.Name == Const.EquipmentName.ﾘﾓｺﾝ19) &&
                   !alarms.Exists(p => p.Equipment.Name == Const.EquipmentName.ﾘﾓｺﾝ17))
                    errors.Add("1F");

                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                errors.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        #endregion

        #region 弱電承認不可
        public static void ValidateConnectedLightElectricalPair(string category, string equipmentName, List<Symbol> symbols)
        {
            string messageId = equipmentName + " has no wire found";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var errors = new List<Symbol>();

                var electricals = UnitWiring.Masters.LightElectricals.FindAll(p => p.Category == category);
                var tops = new List<Symbol>();
                var subs = new List<Symbol>();

                foreach (var electrical in electricals)
                {
                    if (electrical.IsTop)
                        tops.AddRange(symbols.FindAll(p => p.Equipment.Id == electrical.EquipmentId));
                    else
                        subs.AddRange(symbols.FindAll(p => p.Equipment.Id == electrical.EquipmentId));
                }

                //親と配線されていない子供がいたらエラー
                foreach (var top in tops)
                {
                    if (top.Children.Count == 0)
                    {
                        errors.Add(top);
                        continue;
                    }

                    //Categoryに属していないものが配線されていてもエラーです。
                    foreach (var child in top.Children)
                    {
                        if (electricals.Exists(p => p.EquipmentId == child.Equipment.Id))
                            subs.Remove(child);
                        else
                            errors.Add(child);
                    }
                }

                //子供しか配線されていなかったらエラー
                if (tops.Count == 0 && subs.Count != 0)
                    errors.AddRange(subs);

                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                errors.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        public static void ValidateConnectedToTVJ(string equipmentName, List<Symbol> symbols)
        {
            var validation = new Validation();

            string messageId = equipmentName + " has no wire found.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var errors = new List<Symbol>();

                var tvSymbols = symbols.FindAll(p => p.Equipment.Name == equipmentName);
                var tvjSymbols = symbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.TVJ);
                var tvSubGroup = UnitWiring.Masters.LightElectricals.FindAll(p => p.Category == "TV" && !p.IsTop);

                foreach (var tvj in tvjSymbols)
                {
                    var children = tvj.Children.FindAll(p=>p.Equipment.Name == equipmentName);

                    foreach (var child in children)
                    {
                        if (tvSubGroup.Exists(p => p.EquipmentId == child.Equipment.Id))
                            tvSymbols.Remove(child);
                        else
                            errors.Add(child);
                    }
                }

                if (tvSymbols.Count != 0)
                    errors.AddRange(tvSymbols);

                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                errors.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        public static void ValidateConnectedToStarHaikan(string equipmentName, List<Symbol> symbols)
        {
            var validation = new Validation();

            string messageId = equipmentName + " has no wire found.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var errors = new List<Symbol>();

                var targetSymbols = symbols.FindAll(p => p.Equipment.Name == equipmentName);
                var starSymbols = symbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.ｽﾀｰ配管基点);
                var starSubGroup = UnitWiring.Masters.LightElectricals.FindAll(p => p.Category == "StarHaikan" && !p.IsTop);

                foreach (var star in starSymbols)
                {
                    var children = star.Children.FindAll(p => p.Equipment.Name == equipmentName);

                    foreach (var child in children)
                    {
                        if (starSubGroup.Exists(p => p.EquipmentId == child.Equipment.Id))
                            targetSymbols.Remove(child);
                        else
                            errors.Add(child);
                    }
                }

                if (targetSymbols.Count != 0)
                    errors.AddRange(targetSymbols);

                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                errors.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);

        }

        public static void ValidateConnectedFireAlarm(List<Symbol> symbols)
        {
            var validation = new Validation();

            string messageId = "FireAlarm has no wire found.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var errors = new List<Symbol>();
                var lightFireAlarms = UnitWiring.Masters.LightElectricals.FindAll(p => p.Category == "FireAlarm");
                var parentFireAlarms = new List<Symbol>();
                var childFireAlarms = new List<Symbol>();

                foreach (var alarm in lightFireAlarms)
                {
                    if (alarm.IsTop)
                        parentFireAlarms.AddRange(symbols.FindAll(p => p.Equipment.Id == alarm.EquipmentId));
                    else
                        childFireAlarms.AddRange(symbols.FindAll(p => p.Equipment.Id == alarm.EquipmentId));
                }

                //親のFireAlarmが、子供が0か、2つ以上持っていた場合はエラー
                foreach (var parentItem in parentFireAlarms)
                {
                    Symbol child;
                    if (parentItem.Children.Count == 0 || 1 < parentItem.Children.Count)
                    {
                        errors.Add(parentItem);
                        continue;
                    }
                    else
                    {
                        child = parentItem.Children[0];
                        childFireAlarms.Remove(child);
                    }

                    FindUnConnectionFireAlarm(ref childFireAlarms, child, ref errors);
                }

                if (childFireAlarms.Count == 0)
                    return null;
                else
                    errors.AddRange(childFireAlarms);

                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId, false);
                errors.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        private static void FindUnConnectionFireAlarm(ref List<Symbol> children, Symbol parent,ref List<Symbol> errors)
        {
            var wire = parent.Wire.ChildSymbol;
            if (wire == null)
                return;

            if (wire.Children.Count == 0 || 1 < wire.Children.Count)
            {
                errors.Add(parent);
                return;
            }

            var child = wire.Children[0];
            children.Remove(child);
            FindUnConnectionFireAlarm(ref children, child, ref errors);
        }

        public static void ValidateConnectedToBlackmark(List<Symbol> symbols)
        {
            string messageId = @"BlackMark has comment is wrong.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var toridashimarks = symbols.FindAll(p => p.IsToridashiForLightElectrical);
                var starSymbols = symbols.FindAll(p => p.Equipment.Name == Const.EquipmentName.ｽﾀｰ配管基点);

                //NET取出、LAN取出、TEL取出、光ﾌｧｲﾊﾞｰのどれかコメントにあれば、それはｽﾀｰ配管基点と接続される。
                if(starSymbols.Count == 0 && toridashimarks.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>シンボル「Light elec. mark」が見つからなければエラー</summary>
        public static void ValidateLightElecMark(List<Symbol> symbols)
        {
            string messageId =
@"There is no light electric mark in floor plan.
Please check!";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                //コンテナからのシンボル情報を取得
                if (symbols.Exists(p => p.Equipment.NameAtSelection == Const.NameAtSelection.LightElecMark))
                    return null;

                var error = new ErrorDialog(messageId, false);
                return error;
            };

            validator.Run(messageId);
        }

        #endregion
    }
}
