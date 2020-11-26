using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SocketPlan.WinUI.SocketPlanServiceReference;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public static class LightSerialConverter
    {
        //切替条件とか
        private static DateTime SwitchingDate { get { return new DateTime(2017, 2, 18); } }
        private static string checkedConstructionCode = string.Empty;
        private static List<SwitchLightSerial> switchLights = new List<SwitchLightSerial>();
        private static List<ConvertionLightSerial> convertionSerials = new List<ConvertionLightSerial>();
        private static List<UsableRohmOfLightSerial> usableSerials = new List<UsableRohmOfLightSerial>();
        private static List<UsableBracketLightSerial> usableBracketSerials = new List<UsableBracketLightSerial>();
        private static bool isUsingIrisLight;
        private static DateTime? releaseDate;

        private const string Bracket_PL = "PL";
        private const string Bracket_BL = "BL";
        private const string Bracket_BN = "BN";
        private const string Bracket_PL_Kibiroi = "L-PA-C";
        private const string Bracket_BL_Kibiroi = "L-BL-C";
        private const string Bracket_BN_Kibiroi = "L-BN-C";

        public static void Initialize(string constructionCode)
        {
            using (var service = new SocketPlanServiceNoTimeout())
            {
                convertionSerials = new List<ConvertionLightSerial>(service.GetConvertionLightSerials(constructionCode));
                switchLights = new List<SwitchLightSerial>(service.GetSwitchLightSerials());
                usableSerials = new List<UsableRohmOfLightSerial>(service.GetUsableRohmOfLightSerial(constructionCode));
                isUsingIrisLight = service.IsUsingIrisohyamaLightConstructions(constructionCode);
                usableBracketSerials = new List<UsableBracketLightSerial>(service.GetUsableBracketLightSerials(Static.ConstructionCode));
            }

            if (Static.Schedule != null)
                releaseDate = Static.Schedule.ExpectedReleaseDate;
            else
                releaseDate = null;

            checkedConstructionCode = constructionCode;
        }

        public static string ConvertIrisohyamaLightSerial(string constructionCode, string baseSerial)
        {
            //プレート付きの場合があるのでバラしてチェック
            var serial = TextObject.GetCountedText(baseSerial)[0];

            //新品番が必要か
            if (NeedNewLightSerial(constructionCode, serial)) 
            {
                var switchLight = LightSerialConverter.switchLights.Find(p => p.OldHinban == serial);
                if (switchLight == null)
                    throw new ApplicationException("データ壊れてます");

                return baseSerial.Replace(switchLight.OldHinban, switchLight.Newhinban);
            }

            return string.Empty;
        }

        public static string ConvertBracketSerial(string constructionCode, string baseSerial)
        {
            //プレート付きの場合があるのでバラしてチェック
            var serial = TextObject.GetCountedText(baseSerial)[0];

            //初回のみDB取得
            if (LightSerialConverter.checkedConstructionCode != constructionCode)
                LightSerialConverter.Initialize(constructionCode);

            //在庫テーブルにある場合はそのまま
            if (usableBracketSerials.Exists(p => p.LightSerial == serial))
                return string.Empty;

            switch (serial) 
            {
                case (Bracket_PL_Kibiroi):
                    return baseSerial.Replace(serial, "L-PA(D1)-C");
                case (Bracket_BL_Kibiroi):
                    return baseSerial.Replace(serial, "L-BL(P1)-C");
                case (Bracket_BN_Kibiroi):
                    return baseSerial.Replace(serial, "L-BN(P1)-C");
                case (Bracket_PL):
                    return baseSerial.Replace(serial, serial + "(D1)");
                case (Bracket_BL):
                    return baseSerial.Replace(serial, serial + "(P1)");
                case (Bracket_BN):
                    return baseSerial.Replace(serial, serial + "(P1)");
                default:
                    return string.Empty;
            }
        }

        //新商品が必要か
        public static bool NeedNewLightSerial(string constructionCode, string baseSerial)
        {
            //初回のみDB取得
            if (LightSerialConverter.checkedConstructionCode != constructionCode)
                LightSerialConverter.Initialize(constructionCode);

            //プレート付きの場合があるのでバラしてチェック
            var serial = TextObject.GetCountedText(baseSerial)[0];

            //変換テーブルにあったら終了
            foreach (var convertSerial in convertionSerials)
            {
                if (serial == convertSerial.TargetLightSerial)
                    return false;
                else if (serial == "L-" + convertSerial.TargetLightSerial)
                    return false;
                else if (serial == "L-" + convertSerial.TargetLightSerial + "-C")
                    return false;
            }

            //数量チェックが行われているものに関しては、現行品となる。
            //三段階のチェック
            foreach (var usableSerial in usableSerials)
            {
                if (serial == usableSerial.LightSerial)
                    return false;
                else if (serial == "L-" + usableSerial.LightSerial)
                    return false;
                else if (serial == "L-" + usableSerial.LightSerial + "-C")
                    return false;
            }

            //対象品番か
            if (!LightSerialConverter.switchLights.Exists(p => p.OldHinban == serial))
                return false;

            //加工ストップしている現場だったら新商品
            if (isUsingIrisLight)
                return true;

            //加工依頼前は全てやる
            if (Static.IsBeforeKakouIrai)
                return true;

            //引渡し日が空ならやる。
            //引渡し日で切り替え日以降はやる
            if (releaseDate == null || releaseDate >= LightSerialConverter.SwitchingDate)
                return true;

            return false;
        }
    }
}
