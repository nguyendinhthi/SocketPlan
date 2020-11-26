using System;
using System.Collections.Generic;
using System.Text;

namespace SocketPlan.WinUI
{
    public class HouseSpecs
    {
        public int MaxFloor { get; set; }
        
        /// <summary>矩計</summary>
        public string Kanabakari { get; set; }

        /// <summary>天井の高さ</summary>
        public decimal CeilingHeight_1F { get; set; }
        public decimal CeilingHeight_2F { get; set; }
        public decimal GetCeilingHeight(int floor)
        {
            if (floor == 1)
                return Static.HouseSpecs.CeilingHeight_1F;
            else
                return Static.HouseSpecs.CeilingHeight_2F;
        }

        /// <summary>高気密・高断熱（BasicSpecificatinDetails(tbl_siyo_kihon)の0111）</summary>
        public string HouseTypeCode { get; set; }
        public bool IsNotIHead { get { return Static.HouseSpecs.HouseTypeCode == "0010"; } }
        public bool IsIHead { get { return Static.HouseSpecs.IsIHead2 || Static.HouseSpecs.IsIHead3 || Static.HouseSpecs.IsIHead4; } }
        private bool IsIHead2 { get { return Static.HouseSpecs.HouseTypeCode == "0020"; } }
        private bool IsIHead3 { get { return Static.HouseSpecs.HouseTypeCode == "0030"; } }
        private bool IsIHead4 { get { return Static.HouseSpecs.HouseTypeCode == "0040"; } }
        public bool IsICube { get { return Static.HouseSpecs.HouseTypeCode == "0050"; } }
        public bool IsICube2 { get { return Static.HouseSpecs.HouseTypeCode == "0510"; } }
        public bool IsISmart { get { return Static.HouseSpecs.HouseTypeCode == "0060"; } }
        public bool IsISmart2 { get { return Static.HouseSpecs.HouseTypeCode == "0520"; } }
        public bool IsISmile { get { return Static.HouseSpecs.HouseTypeCode == "9010"; } }
        public bool IsIPalette { get { return Static.HouseSpecs.HouseTypeCode == "9020"; } }

        public bool IsJikugumi { get { return IsNotIHead || IsIHead2 || IsIHead3 || IsIHead4; } }
        public int HouseTypeGroupId { get; set; }

        //UI側での家タイプ判定は全部これでやっていきたいなあ。いちいちWebサービスのIsISmartとか呼ぶのめんどくさくなってきた。

        /// <summary>一律の余長</summary>
        public decimal ExtraLength { get; set; }

        /// <summary>JB側・SJ側の余長</summary>
        public decimal JBExtraLength { get; set; }

        /// <summary>分電盤側の余長</summary>
        public decimal BreakerExtraLength { get; set; }

        /// <summary>すべてのスイッチ、コンセント、照明側の余長</summary>
        public decimal TerminalExtraLength { get; set; }

        /// <summary>太陽電池_自立運転用余長</summary>
        public decimal SolarSocketExtraLength { get; set; }

        /// <summary>パワコン余長</summary>
        public decimal PowerConExtraLength { get; set; }

        /// <summary> ダウンライト側の余長 </summary>
        public decimal DownLightExtraLength { get; set; }

        /// <summary>天井受け余長</summary>
        public decimal CeilingReceiverExtraLength { get; set; }

        /// <summary>1F天井懐</summary>
        public decimal CeilingDepth1F { get; set; }

        /// <summary>2F天井懐</summary>
        public decimal CeilingDepth2F { get; set; }

        /// <summary>2F天井板の厚さ</summary>
        public decimal CeilingThickness1F { get; set; }

        /// <summary>1F天井板の厚さ</summary>
        public decimal CeilingThickness2F { get; set; }

        /// <summary>床の厚さ</summary>
        public decimal FloorThickness { get; set; }

        /// <summary>コネクタ余長</summary>
        public decimal ConnectorExtraLength { get { return this.ConnectorMaleExtraLength + this.ConnectorFemaleExtraLength; } }
        public decimal ConnectorMaleExtraLength { get; set; }
        public decimal ConnectorFemaleExtraLength { get; set; }
        public decimal ConnectorHaikanExtraLength { get { return this.ConnectorHaikanMaleExtraLength + this.ConnectorHaikanFemaleExtraLength; } }
        public decimal ConnectorHaikanMaleExtraLength { get; set; }
        public decimal ConnectorHaikanFemaleExtraLength { get; set; }

        /// <summary>太陽光の有無</summary>
        public bool ExistSolar { get; set; }

        /// <summary>パワコンの台数</summary>
        public int PowerConditionerCount { get; set; }

        /// <summary>配管取付余長</summary>
        public decimal HaikanTakeOutExtraLength { get; set; }
       
        public decimal GetUnderFloorExtraLength(int floor)
        {
            if (Static.HouseSpecs.Kanabakari == Const.Kanabakari._260)
            {
                if (floor == 1)
                    return Const.UnderFloorHeight._3200;
                else
                    return Const.UnderFloorHeight._3000;
            }

            return Const.UnderFloorHeight._3000;
        }

        public decimal GetUnderFloorExtraLengthJbox(int floor)
        {
            if (Static.HouseSpecs.Kanabakari == Const.Kanabakari._265)
                return Const.UnderFloorHeight._3250;
            else if (Static.HouseSpecs.Kanabakari == Const.Kanabakari._260)
            {
                if (floor == 1)
                    return Const.UnderFloorHeight._3200;
                else
                    return Const.UnderFloorHeight._3000;
            }
            else
            {
                return Const.UnderFloorHeight._3000;
            }
        }
    }
}
