using System;
using System.Collections.Generic;
using System.Text;

namespace SocketPlan.WinUI.SocketPlanServiceReference
{
    public partial class LightSerial
    {
        public static DateTime JotoSwitchingDate
        {
            get { return new DateTime(2014, 9, 21); }
        }

        public static DateTime KakoSwitchingDate
        {
            get { return new DateTime(2014, 9, 1); }
        }

        public static DateTime KakoSwitchingDate2
        {
            get { return new DateTime(2014, 10, 20); }
        }

        public bool IsHRDLED
        {
            get
            {
                return this.CategoryId == 2 || this.CategoryId == 3;
            }
        }

        public bool IsCeilingLED
        {
            get
            {
                if (this.LightTypeId.HasValue && this.LightTypeId == Const.LightType.Ceiling)
                    return true;

                return false;
            }
        }

        public bool IsDownLED
        {
            get
            {
                if (this.LightTypeId.HasValue && this.LightTypeId == Const.LightType.Down)
                    return true;

                return false;
            }
        }

        public bool IsWallLED
        {
            get
            {
                if (this.LightTypeId.HasValue && this.LightTypeId == Const.LightType.Wall)
                    return true;

                return false;
            }
        }

        public bool IsDeleted
        {
            get
            {
                return this.DeletedDate.HasValue && this.DeletedDate.Value <= DateTime.Now;
            }
        }

        public static LightSerial GetLightSerial(string hinban)
        {
            foreach (var category in UnitWiring.Masters.LightSerialCategories)
            {
                var serial = Array.Find(category.LightSerials, p => p.Name == hinban);
                if (serial != null)
                    return serial;
            }

            return null;
        }

        public static LightSerial FindLightSerial(string hinban)
        {
            foreach (var category in UnitWiring.Masters.LightSerialCategories)
            {
                foreach (var serial in category.LightSerials)
                {
                    if (serial.Name == hinban)
                        return serial;
                }
            }

            return null;
        }

        public static LightSerial FindLightSerialWithSetteigai(string hinban)
        {
            return UnitWiring.Masters.LightSerialsWithSetteigai.Find(p => p.Name == hinban);
        }

        public static string GetHinban(Symbol symbol, string hinban)
        {
            if (!symbol.IsLight)
                return string.Empty;

            var light = new Light(symbol);
            var lightSerial = FindLightSerial(hinban);
            if (lightSerial == null)
                return string.Empty;

            //新商品対象の場合置換
            var serial = LightSerialConverter.ConvertIrisohyamaLightSerial(Static.ConstructionCode, hinban);
            if (!string.IsNullOrEmpty(serial))
                return serial;

            //ブラケット新商品
            serial = LightSerialConverter.ConvertBracketSerial(Static.ConstructionCode, hinban);
            if (!string.IsNullOrEmpty(serial))
                return serial;

            if (NeedToAddVersionForHinban(lightSerial, symbol))
            {
                if (hinban.StartsWith("K") || hinban == Const.LightSerial.PLS)
                {
                    return hinban.Replace(hinban, hinban + "(P1)");
                }
                else
                {
                    var result = hinban;

                    if (symbol.IsOutdoor)
                        result = "W" + result;

                    if (hinban == Const.LightSerial.MLS || hinban == Const.LightSerial.LLS)
                        result = result + "(K1)";
                    else
                        result = result + "(P1)";

                    if (result == Const.LightSerial.WLNP1)
                        return Const.LightSerial.WLLP1;

                    if (result == Const.LightSerial.WMNP1)
                        return Const.LightSerial.WMLP1;

                    if (result == Const.LightSerial.L_WLNP1)
                        return Const.LightSerial.L_WLLP1;

                    if (result == Const.LightSerial.L_WMNP1)
                        return Const.LightSerial.L_WMLP1;

                    return result;
                }
            }

            return hinban;
        }

        public static string GetHinbanForKibiroi(Symbol symbol, List<LightSerialItem> lightSerialItems, string hinban)
        {
            if (!symbol.IsLight)
                return string.Empty;

            var light = new Light(symbol);
            var lightSerial = FindLightSerialWithSetteigai(hinban);
            if (lightSerial == null)
                return string.Empty;

            //PLS,MLS,LLSはLightSerialItemテーブルに対応させていない
            if (hinban.Contains(Const.LightSerial.PLS) ||
                hinban.Contains(Const.LightSerial.MLS) ||
                hinban.Contains(Const.LightSerial.LLS))
            {
                return GetHinban(symbol, hinban);
            }

            var item = lightSerialItems.Find(p => hinban == p.Serial && light.RoomName.Contains(p.Room));
            if (item == null)
                return string.Empty;

            //新商品対象の場合置換
            var serial = LightSerialConverter.ConvertIrisohyamaLightSerial(Static.ConstructionCode, item.ItemName);
            if (!string.IsNullOrEmpty(serial))
                return serial;

            //ブラケット新商品
            serial = LightSerialConverter.ConvertBracketSerial(Static.ConstructionCode, item.ItemName);
            if (!string.IsNullOrEmpty(serial))
                return serial;

            if (NeedToAddVersionForHinban(lightSerial, symbol))
            {
                if (item.ItemName.StartsWith("L-"))
                {
                    var s = item.ItemName.Split('-');
                    var result = s[1];

                    if (symbol.IsOutdoor)
                        result = "W" + result;

                    result = s[0] + "-" + result + "(P1)";

                    if (result == Const.LightSerial.WLNP1)
                        return Const.LightSerial.WLLP1;

                    if (result == Const.LightSerial.WMNP1)
                        return Const.LightSerial.WMLP1;

                    if (result == Const.LightSerial.L_WLNP1)
                        return Const.LightSerial.L_WLLP1;

                    if (result == Const.LightSerial.L_WMNP1)
                        return Const.LightSerial.L_WMLP1;

                    return result;
                }
                else // K品番
                {
                    return item.ItemName.Replace(item.Serial, item.Serial + "(P1)");
                }
            }

            return item.ItemName;
        }

        public static bool NeedToAddVersion(LightSerial lightSerial, Symbol symbol)
        {
            if (lightSerial.CategoryId != 3)
                return false;

            if (lightSerial.Name.StartsWith("K"))
            {
                if (!Static.Schedule.ExpectedHouseRaisingDate.HasValue ||
                    Static.Schedule.ExpectedHouseRaisingDate.Value >= JotoSwitchingDate)
                    return true;

                return false;
            }

            if (!Static.Schedule.SentProcessRequestDate.HasValue ||
                Static.Schedule.SentProcessRequestDate >= KakoSwitchingDate2)
                return true;

            if (symbol.IsOutdoor)
            {
                if (lightSerial.Name == Const.LightSerial.LL ||
                    lightSerial.Name == Const.LightSerial.ML ||
                    lightSerial.Name == Const.LightSerial.LLS ||
                    lightSerial.Name == Const.LightSerial.MLS ||
                    lightSerial.Name == Const.LightSerial.PLS)
                {
                    if (!Static.Schedule.SentProcessRequestDate.HasValue ||
                        Static.Schedule.SentProcessRequestDate.Value >= KakoSwitchingDate)
                        return true;

                    if (!Static.Schedule.ExpectedHouseRaisingDate.HasValue ||
                        Static.Schedule.ExpectedHouseRaisingDate.Value >= JotoSwitchingDate)
                        return true;

                    return false;
                }

                if (lightSerial.Name == Const.LightSerial.LN ||
                    lightSerial.Name == Const.LightSerial.MN)
                {
                    if (!Static.Schedule.SentProcessRequestDate.HasValue ||
                        Static.Schedule.SentProcessRequestDate.Value >= KakoSwitchingDate)
                        return true;

                    return false;
                }

                return false;
            }
            else
            {
                if (!Static.Schedule.ExpectedHouseRaisingDate.HasValue)
                    return true;

                if (Static.Schedule.ExpectedHouseRaisingDate.Value >= JotoSwitchingDate)
                    return true;

                return false;
            }
        }

        public static bool NeedToAddVersionForHinban(LightSerial lightSerial, Symbol symbol)
        {
            if (!lightSerial.IsDownLED) 
            {
                if (lightSerial.Name != Const.LightSerial.PLS)
                    return false;
            }

            if (lightSerial.CategoryId != 3)
                return false;

            if (lightSerial.Name.StartsWith("K"))
            {
                if (!Static.Schedule.ExpectedHouseRaisingDate.HasValue ||
                    Static.Schedule.ExpectedHouseRaisingDate.Value >= JotoSwitchingDate)
                    return true;

                return false;
            }

            if (symbol.IsOutdoor)
            {
                if (lightSerial.Name == Const.LightSerial.LL ||
                    lightSerial.Name == Const.LightSerial.ML ||
                    lightSerial.Name == Const.LightSerial.LLS ||
                    lightSerial.Name == Const.LightSerial.MLS ||
                    lightSerial.Name == Const.LightSerial.PLS)
                {
                    if (!Static.Schedule.SentProcessRequestDate.HasValue ||
                        Static.Schedule.SentProcessRequestDate.Value >= KakoSwitchingDate)
                        return true;

                    if (!Static.Schedule.ExpectedHouseRaisingDate.HasValue ||
                        Static.Schedule.ExpectedHouseRaisingDate.Value >= JotoSwitchingDate)
                        return true;

                    return false;
                }

                if (lightSerial.Name == Const.LightSerial.LN ||
                    lightSerial.Name == Const.LightSerial.MN)
                {
                    if (!Static.Schedule.SentProcessRequestDate.HasValue ||
                        Static.Schedule.SentProcessRequestDate.Value >= KakoSwitchingDate)
                        return true;

                    return false;
                }

                return false;
            }
            else
            {
                if (!Static.Schedule.ExpectedHouseRaisingDate.HasValue)
                    return true;

                if (Static.Schedule.ExpectedHouseRaisingDate.Value >= JotoSwitchingDate)
                    return true;

                return false;
            }
        }
    }
}
