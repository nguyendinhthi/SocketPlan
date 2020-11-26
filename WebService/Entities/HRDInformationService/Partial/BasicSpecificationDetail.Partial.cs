using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class BasicSpecificationDetail
    {
        #region 定数

        private struct HouseType
        {
            public const string IHEAD20 = "0020";
            public const string IHEAD30 = "0030";
            public const string IHEAD40 = "0040";
            public const string ICUBE = "0050";
            public const string ICUBE2 = "0510";
            public const string ISMART = "0060";
            public const string ISMART2 = "0520";
            public const string ISMILE = "9010";
            public const string IPALETTE = "9020";
        }

        private struct Kanabakari
        {
            public const string _265 = "0010";
            public const string _240 = "0020";
            public const string _240_PLUS = "0040";
            public const string _240_PLUS_T = "0041";
            public const string _240_PLUS_KUCHI = "0050";
            public const string _240_PLUS_T_SHIKAKU = "0051";
            public const string _240_KUCHI = "0060";
            public const string _260 = "0030";
            public const string _265_KUCHI = "0070";
            public const string _260_PLUS = "0065";
            public const string _260_PLUS_KUCHI = "0067";
        }

        //HemsとZEHのSpecificationDetailCodeはこれで合ってる。
        private struct Hems
        {
            public const string NASI = "0010";
            public const string ARI = "0020";
            public const string MISETTEI = "9999";
        }

        private struct ZEH
        {
            public const string NASI = "0020";
            public const string ARI = "0010";
            public const string MISETTEI = "9999";
        }

        private struct Mizumawari
        {
            public const string JunB2 = "0050";
            public const string JunC = "0060";
            public const string JunE2 = "0090";
            public const string A = "0100";
            public const string B = "0110";
        }

        private const string HOUSE_TYPE_ERROR =
             "\n\n" +
             "==================== CAUSE ====================\n" +
             "Cannot get house type.\n" +
             "家タイプを取得できませんでした。\n" +
             "================================================\n";

        private const string KANABAKARI_ERROR =
            "\n\n" +
            "==================== CAUSE ====================\n" +
            "Cannot get Kanabakari.\n" +
            "矩計を取得できませんでした。\n" +
            "================================================\n";

        private const string KANABAKARI_UNSUPPORT_ERROR =
            "\n\n" +
            "==================== CAUSE ====================\n" +
            "Unsupported Kanabakari.\n" +
            "未対応の矩計です。\n" +
            "================================================\n";

        #endregion

        //SpecificationDetailCodeまで見て何かを判定したいメソッド
        //このへんやりすぎかも・・・

        public static bool IsICubeOrISmileOrIPalette(string constructionCode, int siyoCode)
        {
            //i-smileはi-cubeと仕様がほぼ一緒なので、まとめて判断しちゃおう

            var code = BasicSpecificationDetail.GetHouseTypeCode(constructionCode, siyoCode);
            if (code == null)
                throw new ApplicationException(HOUSE_TYPE_ERROR);

            return code == HouseType.ICUBE ||
                   code == HouseType.ICUBE2 ||
                   code == HouseType.ISMILE ||
                   code == HouseType.IPALETTE;
        }

        public static bool IsISmart(string constructionCode, int siyoCode)
        {
            var code = BasicSpecificationDetail.GetHouseTypeCode(constructionCode, siyoCode);
            if (code == null)
                throw new ApplicationException(HOUSE_TYPE_ERROR);

            return code == HouseType.ISMART || code == HouseType.ISMART2;
        }

        public static bool IsIHead(string constructionCode, int siyoCode)
        {
            var code = BasicSpecificationDetail.GetHouseTypeCode(constructionCode, siyoCode);
            if (code == null)
                throw new ApplicationException(HOUSE_TYPE_ERROR);

            return
                code == HouseType.IHEAD20 ||
                code == HouseType.IHEAD30 ||
                code == HouseType.IHEAD40;
        }

        public static bool IsISmartICubeISmileIPaletteIHead4(string constructionCode, int siyoCode)
        {
            var code = BasicSpecificationDetail.GetHouseTypeCode(constructionCode, siyoCode);
            if (code == null)
                throw new ApplicationException(HOUSE_TYPE_ERROR);
            return
                code == HouseType.IHEAD40 ||
                code == HouseType.ICUBE ||
                code == HouseType.ICUBE2 ||
                code == HouseType.ISMILE ||
                code == HouseType.ISMART ||
                code == HouseType.ISMART2 ||
                code == HouseType.IPALETTE;
        }

        public static bool IsIPalette(string constructionCode, int siyoCode)
        {
            var code = BasicSpecificationDetail.GetHouseTypeCode(constructionCode, siyoCode);
            if (code == null)
                throw new ApplicationException(HOUSE_TYPE_ERROR);

            return code == HouseType.IPALETTE;
        }

        public static string GetKanabakari(string constructionCode, int siyoCode)
        {
            string code = BasicSpecificationDetail.GetKanabakariCode(constructionCode, siyoCode);

            if (code == null)
                throw new ApplicationException(KANABAKARI_ERROR);

            if (code == Kanabakari._265 || code == Kanabakari._265_KUCHI)
                return "265";
            else if (code == Kanabakari._240 || code == Kanabakari._240_PLUS || code == Kanabakari._240_PLUS_KUCHI || code == Kanabakari._240_KUCHI ||
                code == Kanabakari._240_PLUS_T || code == Kanabakari._240_PLUS_T_SHIKAKU)
                return "240";
            else if (code == Kanabakari._260 || code == Kanabakari._260_PLUS || code == Kanabakari._260_PLUS_KUCHI)
                return "260";
            else
                throw new ApplicationException(KANABAKARI_UNSUPPORT_ERROR);
        }

        public static bool IsKanabakari240(string constructionCode, int siyoCode)
        {
            string code = BasicSpecificationDetail.GetKanabakariCode(constructionCode, siyoCode);
            if (code == null)
                throw new ApplicationException(KANABAKARI_ERROR);

            if (code == Kanabakari._240 || code == Kanabakari._240_KUCHI)
                return true;
            else
                return false;
        }

        public static bool IsKanabakari240Plus(string constructionCode, int siyoCode)
        {
            string code = BasicSpecificationDetail.GetKanabakariCode(constructionCode, siyoCode);
            if (code == null)
                throw new ApplicationException(KANABAKARI_ERROR);

            if (code == Kanabakari._240_PLUS || code == Kanabakari._240_PLUS_KUCHI || code == Kanabakari._240_PLUS_T || code == Kanabakari._240_PLUS_T_SHIKAKU)
                return true;
            else
                return false;
        }

        public static bool IsHemsPlan(string constructionCode, int siyoCode)
        {
            var hemsCode = GetHemsPlanCode(constructionCode, siyoCode);
            return hemsCode == Hems.ARI;
        }

        public static bool IsZehPlan(string constructionCode, int siyoCode)
        {
            var zehCode = GetZehPlanCode(constructionCode, siyoCode);
            return zehCode == ZEH.ARI;
        }

        public static bool IsKanreiArea(string constructionCode, int siyoCode)
        {
            var code = GetMizumawariSiyoCode(constructionCode, siyoCode);
            if (code == null)
                throw new ApplicationException(HOUSE_TYPE_ERROR);

            if (code == Mizumawari.JunB2 ||
                code == Mizumawari.JunC ||
                code == Mizumawari.JunE2 ||
                code == Mizumawari.A ||
                code == Mizumawari.B)
                return true;
            else
                return false;
        }

        //GlassWoolコメントを記述するフラグ
        public static bool CanDrawGlassWool(string constructionCode, int siyoCode)
        {
            var code = BasicSpecificationDetail.GetHouseTypeCode(constructionCode, siyoCode);
            if (code == null)
                throw new ApplicationException(HOUSE_TYPE_ERROR);

            if (code == HouseType.ISMART ||
                code == HouseType.ISMART2 ||
                code == HouseType.ICUBE ||
                code == HouseType.ICUBE2 ||
                code == HouseType.ISMILE ||
                code == HouseType.IPALETTE)
                return true;

            var shoureiCode = BasicSpecificationDetail.Get(constructionCode, siyoCode, "0123");
            if (shoureiCode == null)
                return false;

            if (shoureiCode == "0010")
                return true;

            var boukaCode = BasicSpecificationDetail.Get(constructionCode, siyoCode, "0102");
            if (boukaCode == null)
                return false;

            //省令準耐火がなしか未定で、準耐火仕様が準耐火だったらTrue
            if ((shoureiCode == "0020" || shoureiCode == "9999") && boukaCode == "0030")
                return true;

            return false;
        }

        //BasicSpecificationCode毎にメソッドにしよう

        public static string GetHouseStoryCode(string constructionCode, int siyoCode)
        {
            return BasicSpecificationDetail.Get(constructionCode, siyoCode, "0001");
        }

        public static string GetKanabakariCode(string constructionCode, int siyoCode)
        {
            return BasicSpecificationDetail.Get(constructionCode, siyoCode, "0070");
        }

        public static string GetInsulationRegionCode(string constructionCode, int siyoCode)
        {
            return BasicSpecificationDetail.Get(constructionCode, siyoCode, "0108");
        }

        public static string GetHouseTypeCode(string constructionCode, int siyoCode)
        {
            return BasicSpecificationDetail.Get(constructionCode, siyoCode, "0111");
        }

        public static string GetHemsPlanCode(string constructionCode, int siyoCode)
        {
            return BasicSpecificationDetail.Get(constructionCode, siyoCode, "0128");
        }

        public static string GetZehPlanCode(string constructionCode, int siyoCode)
        {
            return BasicSpecificationDetail.Get(constructionCode, siyoCode, "0133");
        }

        public static string GetMizumawariSiyoCode(string constructionCode, int siyoCode)
        {
            return BasicSpecificationDetail.Get(constructionCode, siyoCode, "0112");
        }
        //とりあえずこれ使え

        /// <summary>
        /// <para>加工依頼前はSiyoDataBrokerを参照する</para>
        /// <para>加工依頼後はBasicSpecificationDetailsを参照する</para>
        /// </summary>
        private static string Get(string constructionCode, int siyoCode, string specCode)
        {
            if (ConstructionSchedule.IsBeforeProcessRequest(constructionCode))
            {
                var kihon = tbl_siyo_kihon.Get(constructionCode, siyoCode, specCode);
                if (kihon == null)
                    return null;

                return kihon.siyoDetailCd;
            }
            else
            {
                var detail = BasicSpecificationDetail.Get(constructionCode, specCode);
                if (detail == null)
                    return null;

                return detail.SpecificationDetailCode;
            }
        }
    }
}
