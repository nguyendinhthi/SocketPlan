using System;
using System.Collections.Generic;
using System.Text;

namespace SocketPlan.WinUI
{
    public struct Const
    {
        public const string APPLICATION_TABLE_NAME = "UnitWiring";
        public const string UNDERFLOOR_WIRING = "UnderstairWiring"; //UnderfloorWiringにしたいけど、変更できるタイミングが無い・・・

        public const double INGORE_LENGTH = 1500;

        public const double TEXT_HEIGHT = 120;   //平面図用
        public const double TEXT_HEIGHT_E = 240; //立面図用

        public const double GRID_INTERVAL = 910;
        public const double CEILING_RECEIVER_MARGIN = 400; //外周側は広いので少し多めに取っています。本来はだいたい1/4グリッド
        public const decimal MARKING_SHOULD_ROLL_LENGTH = 100;

        public const string LINK_CODE_BLANK = " ";

        public struct SelectionCategoryId
        {
            public const int PIPE = 5;
            public const int FIRE_ALARM = 9;
            public const int VENTILATION = 15;
            public const int CUTTING = 19;
        }

        public struct AttributeTag
        {
            public const string WIRE_NO = "WireNo";
            public const string HEIGHT = "Height";
            public const string EQUIPMENT_ID = "EquipmentId";
            public const string OTHER = "Other";
            public const string LINK_CODE = "LinkCode";
            public const string JOINT_BOX_NO = "JointBoxNo";
            public const string SEQ_NO = "SequenceNo";
            public const string SEQ_NO_LE = "SequenceNoLightEle";
            public const string TAKE_OUT = "TakeOutId";
            public const string LIGHT_NO = "LIGHTNO";
            public const string HINBAN = "Hinban";
            public const string KAIRO_NUMBER = "KAIRO_NUMBER";
            public const string KAIRO_SUB_NUMBER = "KAIRO_SUB_NUMBER";
            public const string KAIRO_THIRD_NUMBER = "KAIRO_THIRD_NUMBER";
            public const string KAIRO_TVA = "KAIRO_TVA";
            public const string KAIRO_FLOOR = "KAIRO_FLOOR";
            public const string KAIRO_ROOM = "KAIRO_ROOM";
            public const string KAIRO_BREAER_NAME = "KAIRO_BREAER_NAME";
            public const string KAIRO_IS_200V = "KAIRO_IS_200V";
            public const string KAIRO_EARTH = "KAIRO_EARTH";
            public const string KAIRO_UNITY_ID = "KAIRO_UNITY_ID";
            public const string GLASS_WOOL = "GLASS_WOOL";
            public const string SWITCH_SERIAL = "SwitchSerial";
            public const string CONNECTOR_NUM = "ConnectorNum";
        }

        public struct BlockName
        {
            public const string 一般_01 = "一般-01";
            public const string 配線_01 = "配線-01";

            public const string 照明_01 = "照明-01";
            public const string 照明_02 = "照明-02";
            public const string 照明_03 = "照明-03";
            public const string 照明_04 = "照明-04";
            public const string 照明_05 = "照明-05";
            public const string 照明_06 = "照明-06";
            public const string 照明_07 = "照明-07";
            public const string 照明_08 = "照明-08";
            public const string 照明_41 = "照明-41";

            public const string スイッチ = "normal switch";
            public const string スイッチ3路 = "Switch 3";
            public const string 防水スイッチ3路 = "防水-02";
            public const string スイッチ4路 = "Switch 4";
            public const string センサスイッチ = "センサスイッチ";
            public const string Clip = "Clip";

            public const string 専用E付_27 = "専用E付-27";
            public const string CVALUE = "CVALUE";
            public const string c_value = "c-value";
            public const string PC = "PC";

            public const string forWashing = "一般-01(for washing)";
            public const string forRef = "一般-01(for ref)";
            public const string denkiRimokonKey = "一般-02-denki rimokon ki";
            public const string forSecurityAlarm = "一般-02(for Security alarm)";
            public const string newAshimottou = "一般-01(New Ashimottou)";
            public const string individualVentSwitch = "individual vent switch";

            public const string frame = "frame";
        }

        public struct BlockPath
        {
            public static string Connector { get { return Properties.Settings.Default.SystemBlockDirectory + @"Original\Connector.dwg"; } }
            public static string DuctHole { get { return Properties.Settings.Default.SystemBlockDirectory + @"Original\DuctHole.dwg"; } }
        }

        public struct BreakerName
        {
            public const string 蓄電池 = "蓄電池";
            public const string EVPS = "EVPS";
            public const string エコウィル = "エコウィル";
        }

        public struct CustomAction
        {
            public const int 取り出し矢印 = 1;
            public const int 取り出し位置指定 = 2;
            public const int 照明品番入力 = 3;
            public const int セット品 = 4;
        }

        public struct EquipmentKind
        {
            public const int LIGHT = 1;
            public const int OUTLET = 2;
            public const int SWITCH = 3;
            public const int JOINT_BOX = 4;
            public const int BREAKER = 5;
            public const int FLOOR_ARROW = 6;
            public const int CLIP = 7;
            public const int TAKE_OUT = 8;
            public const int CT_BOX = 9;
            public const int HOME_GATEWAY = 10;
            public const int DISTANCE_PATTERN = 11;
            public const int CONNECTOR_CHECKBOX = 12;
            public const int MARKING = 13;
            public const int SHAWA_TOIRE_YOU = 35;
            public const int TOIRE_YOU = 41;
            public const int OTHER = 99;
        }

        public struct EquipmentName
        {
            public const string WH = "WH";
            public const string op_meter1 = "op-meter1";
            public const string op_meter2 = "op-meter2";
            public const string op_meter3 = "op-meter3";
            public const string op_meter4 = "op-meter4";
            public const string op_meter6 = "op-meter6";
            public const string op_meter7 = "op-meter7";
            public const string 取出し_01 = "取出し-01";
            public const string 取出し_02 = "取出し-02";
            public const string 取出し_03 = "取出し-03";
            public const string 取出し_04 = "取出し-04";
            public const string 取出し_05 = "取出し-05";
            public const string MT51 = "MT51";
            public const string MT71 = "MT71";
            public const string MT81 = "MT81";
            public const string MT82 = "MT82";
            public const string MT83 = "MT83";
            public const string 有り = "有り";
            public const string ｽｲｯﾁ_01 = "ｽｲｯﾁ-01";
            public const string ｽｲｯﾁ_02 = "ｽｲｯﾁ-02";
            public const string ｽｲｯﾁ_03 = "ｽｲｯﾁ-03";
            public const string ｽｲｯﾁ_04 = "ｽｲｯﾁ-04";
            public const string ｽｲｯﾁ_09 = "ｽｲｯﾁ-09";
            public const string ｽｲｯﾁ_11 = "ｽｲｯﾁ-11";
            public const string ｽｲｯﾁ_12 = "ｽｲｯﾁ-12";
            public const string SP_KITEN_Theater = "SP Kiten (Theater)";
            public const string 換気扇_07 = "換気扇-07";
            public const string 換気扇_08 = "換気扇-08";
            public const string ShowerLight = "showerlght";
            public const string 照明 = "照明";
            public const string 照明_01 = "照明-01";
            public const string 照明_02 = "照明-02";
            public const string 照明_04 = "照明-04";
            public const string 照明_05 = "照明-05";
            public const string 照明_08 = "照明-08";
            public const string 照明_09 = "照明-09";
            public const string 照明_10 = "照明-10";
            public const string 照明_30 = "照明-30";
            public const string CT_BOX_maru2 = "CT BOX-4(nisetai)";
            public const string HGW_maru2 = "HGW2(nisetai)";
            public const string XGW_maru2 = "XGW2(nisetai)";
            public const string 分電盤_maru2 = "分電盤-6(nisetai)";
            public const string HCU = "専用E付-27";
            public const string 全量買取メーター = "全量買取";
            public const string 全量買取用メーター = "Zenryou meter-01";
            public const string 分電盤 = "分電盤";
            public const string 分電盤_2 = "分電盤-2";
            public const string 分電盤_3 = "分電盤-3";
            public const string 分電盤_4 = "分電盤-4";
            public const string 分電盤_5 = "分電盤-5(nisetai)";
            public const string Meter = "Meter";
            public const string Meter_01 = "Meter-01";
            public const string ﾘﾓｺﾝ01 = "ﾘﾓｺﾝ01";
            public const string ﾘﾓｺﾝ02 = "ﾘﾓｺﾝ02";
            public const string ﾘﾓｺﾝ03 = "ﾘﾓｺﾝ03";
            public const string ﾘﾓｺﾝ04 = "ﾘﾓｺﾝ04";
            public const string ﾘﾓｺﾝ05 = "ﾘﾓｺﾝ05";
            public const string ﾘﾓｺﾝ06 = "ﾘﾓｺﾝ06";
            public const string ﾘﾓｺﾝ07 = "ﾘﾓｺﾝ07";
            public const string ﾘﾓｺﾝ08 = "ﾘﾓｺﾝ08";
            public const string ﾘﾓｺﾝ09 = "ﾘﾓｺﾝ09";
            public const string ﾘﾓｺﾝ10 = "ﾘﾓｺﾝ10";
            public const string ﾘﾓｺﾝ14 = "ﾘﾓｺﾝ14";
            public const string ﾘﾓｺﾝ15 = "ﾘﾓｺﾝ15";
            public const string ﾘﾓｺﾝ16 = "ﾘﾓｺﾝ16";
            public const string ﾘﾓｺﾝ17 = "ﾘﾓｺﾝ17";
            public const string ﾘﾓｺﾝ18 = "ﾘﾓｺﾝ18";
            public const string ﾘﾓｺﾝ19 = "ﾘﾓｺﾝ19";
            public const string ﾘﾓｺﾝ28 = "ﾘﾓｺﾝ28";
            public const string PVR_LAN = "PVR w/ LAN";
            public const string 防水_01 = "防水-01";
            public const string SLEEVE_CAP_H2669 = "SLEEVE CAP H=2669";
            public const string 幹線引込 = "幹線引込";
            public const string 全量買取幹線引込 = "全量買取幹線引込";
            public const string 電気錠スイッチ = "電気錠スイッチ";
            public const string 配線_01 = "配線-01";
            public const string 配線_02 = "配線-02";
            public const string 配線_03 = "配線-03";
            public const string 配線_07 = "配線-07";
            public const string 配線_12 = "配線-12";
            public const string ｼｬﾜｰ01 = "ｼｬﾜ-01";
            public const string IUB_01 = "IUB-01";
            public const string NR3160_02 = "NR3160-02";
            public const string JC = "JC";
            public const string JCL = "JCL";
            public const string JCT = "JCT";
            public const string JB_D = "JB-D";
            public const string JB_DA = "JB-DA";
            public const string JB_DA_02 = "JB-DA-02";
            public const string JB_DA_03 = "JB-DA-03";
            public const string JB_DA_04 = "JB-DA-04";
            public const string JB_MAIN = "JB-MAIN";
            public const string uw_04 = "uw-04";
            public const string 余剰電力販売計 = "Solar Meter";
            public const string 余剰電力用 = "Solar-01";
            public const string GableWallLine = "line for gable wall";
            public const string PowerBox = "power box";
            public const string TVJ = "TV Joint Box";
            public const string 配管16_16 = "配管16-16";
            public const string 電ｺﾒﾝﾄPrefix = "DEN-";
            public const string InterphonePrefix = "int-";
            public const string int_01 = "int-01";
            public const string int_02 = "int-02";
            public const string int_03 = "int-03";
            public const string int_04 = "int-04";
            public const string int_06 = "int-06";
            public const string int_07 = "int-07";
            public const string 煙式_main2 = "煙式-main2";
            public const string 煙式_main3 = "煙式-main3";
            public const string 煙式_sub2 = "煙式-sub2";
            public const string 煙式_sub3 = "煙式-sub3";
            public const string 熱式_100V2 = "熱式-100V2";
            public const string 熱式_100V3 = "熱式-100V3";
            public const string ﾜｲﾔﾚｽ煙式 = "ﾜｲﾔﾚｽ煙式";
            public const string ﾜｲﾔﾚｽ熱式 = "ﾜｲﾔﾚｽ熱式";
            public const string ｽﾀｰ配管基点 = "ｽﾀｰ配管基点";
            public const string 太陽光_1口 = "一般-29";
            public const string 太陽光_2口 = "一般-30";
            public const string 太陽光_3口 = "一般-37";
            public const string 電気ﾘﾓｺﾝｷｰ = "一般-39";
            public const string Blackmark = "black mark";
            public const string FR = "ﾘﾓｺﾝ11";
            public const string MR = "ﾘﾓｺﾝ12";
            public const string ｴｱｺﾝ_3 = "ｴｱｺﾝ-3";
            public const string 防水E付_06 = "防水E付-06";
            public const string VHV18A = "VHV18A";
            public const string DKI180 = "DKI-180";
            public const string DKI181 = "DKI-181";
            public const string ES1800DC = "ES-1800DC";
            public const string UBsckt_01 = "UBsckt-01";
            public const string UBsckt_03 = "UBsckt-03";
            public const string UBsckt_04 = "UBsckt-04";
            public const string UBsckt_06 = "UBsckt-06";
            public const string 一般_41 = "一般-41";
            public const string 一般_27 = "一般-27";
            public const string 一般_48 = "一般-48";
            public const string 専用E無_04 = "専用E無-04";
            public const string 専用E付_10 = "専用E付-10";
            public const string 専用E付_13 = "専用E付-13";
            public const string 専用E付_16 = "専用E付-16";
            public const string 専用E付_20 = "専用E付-20";
            public const string 専用E付_22 = "専用E付-22";
            public const string 専用E付_24 = "専用E付-24";
            public const string 防水専_01 = "防水専-01";
            public const string 防水専_05 = "防水専-05";
            public const string 防水専_06 = "防水専-06";
            public const string 防水専_07 = "防水専-07";
            public const string 一般_15 = "一般-15";
            public const string 防水E付_01 = "防水E付-01";
            public const string 防水E付_02 = "防水E付-02";
            public const string 防水E付_03 = "防水E付-03";
            public const string 防水E付_04 = "防水E付-04";
            public const string 防水E付_07 = "防水E付-07";
            public const string E付_08 = "E付-08";
            public const string 専用E無_05 = "専用E無-05";
            public const string 専用E無_06 = "専用E無-06";
            public const string 専用E無_07 = "専用E無-07";
            public const string 専用E無_08 = "専用E無-08";
            public const string 換気扇用 = "一般-23";
            public const string ｾｯﾄ部材_22 = "ｾｯﾄ部材-22";
            public const string ｾｯﾄ部材_38 = "ｾｯﾄ部材-38";
            public const string JEM_A = "JEM-A";
            public const string MarkingYellow = "MarkingYellow";
            public const string MarkingGreen = "MarkingGreen";
            public const string MarkingBule = "MarkingBule";
            public const string MarkingOrange = "MarkingOrange";
            public const string InterFace = "ｲﾝﾀ-ﾌｪ-ｽ";
            public const string 全てﾈーﾑｽｲｯﾁ = "全てﾈ-ﾑｽｲｯﾁ";
            public const string 全てﾎﾀﾙ = "全てﾎﾀﾙ";
            public const string 全てﾎﾀﾙｽｲｯﾁ = "全てﾎﾀﾙｽｲｯﾁ";
            public const string 全てﾎﾀﾙﾈｰﾑｽｲｯﾁ = "全てﾎﾀﾙネ-ムｽｲｯﾁ";
            public const string 全てﾎﾀﾙｽｲｯﾁﾈｰﾑｽｲｯﾁ = "Subete hotaru+nemu2";
        }

        public struct NameAtSelection
        {
            public const string LightElecMark = "Light elec. mark";
        }

        public struct NameAtReport
        {
            public const string 食洗機ｺﾝｾﾝﾄ = "食洗機ｺﾝｾﾝﾄ";
            public const string キッズキッチン専用ｺﾝｾﾝﾄ = "キッズキッチン専用ｺﾝｾﾝﾄ";
            public const string カウンタ専用ｺﾝｾﾝﾄ = "カウンタ専用ｺﾝｾﾝﾄ";
            public const string ｵｰﾌﾟﾝｷｯﾁﾝｺﾝｾﾝﾄ = "ｵｰﾌﾟﾝｷｯﾁﾝｺﾝｾﾝﾄ";
            public const string ﾀｯﾁﾚｽ水栓ｺﾝｾﾝﾄ = "ﾀｯﾁﾚｽ水栓ｺﾝｾﾝﾄ";
        }

        public struct MountingType
        {
            public const int OnWall = 1;
            public const int OnCeiling = 2;
        }

        public struct Font
        {
            public const string MSGothic = "ＭＳ ゴシック";
            public const string MSGothicV = "@ＭＳ ゴシック";
            public const string MSPGothic = "ＭＳ Ｐゴシック";
        }

        public struct Kanabakari
        {
            public const string _265 = "265";
            public const string _260 = "260";
            public const string _240 = "240";
        }

        public struct CeilingHeight
        {
            public const decimal _2650 = 2650;
            public const decimal _2600 = 2600;
            public const decimal _2500 = 2500;
            public const decimal _2400 = 2400;
        }

        public struct UnderFloorHeight
        {
            public const decimal _3000 = 3000;
            public const decimal _3200 = 3200;
            public const decimal _3250 = 3250;
        }

        //階またぎ時の天井懐
        public struct CeilingDepth_OverStepFloor
        {
            public const decimal _337 = 337;
            public const decimal _511 = 511;
        }

        public struct Koyaura
        {
            //1Fと小屋裏の間のパネル部分の厚さ
            public const decimal CeilingThickness_240 = 337;
            public const decimal CeilingThickness_265 = 511;

            public const decimal CeilingHeight = 1400;
            public const decimal CeilingDepth = 50;
        }

        public struct Layer
        {
            //どこかのタイミングでレイヤ名を全部英語表記にしたいなあ。
            //英語版AutoCADだと文字化けしてしまうのよ・・・

            public const string Zero = "0";
            public const string 電気_配線 = "電気_配線";
            public const string 電気_配線_非表示用 = "電気_配線_非表示用";
            public const string 電気_設備 = "電気_設備";
            public const string 電気_コメント = "電気_コメント";
            public const string 電気_抜き出し = "電気_抜き出し";
            public const string 電気_外周 = "電気_外周";
            public const string 電気_部屋 = "電気_部屋";
            public const string 電気_部屋_WithJyou = "電気_部屋_WithJyou";
            public const string 電気_部屋_WithoutJyou = "電気_部屋_WithoutJyou";
            public const string 電気_天井パネル = "電気_天井パネル";
            public const string 電気_天井パネル補足 = "電気_天井パネル補足";
            public const string 電気_電気図面配線 = "電気_電気図面配線";
            public const string 電気_ビューポート = "電気_ビューポート";
            public const string 電気_コネクタ = "電気_コネクタ";
            public const string 電気 = "電気";
            public const string 電気_立面図 = "電気_立面図";
            public const string 電気_プレート = "電気_プレート";
            public const string 電気_リモコンニッチプレート = "電気_リモコンニッチプレート";
            public const string 電気_Grid = "電気_Grid";
            public const string 電気_Serial = "電気_Serial";
            public const string 電気_Kairo_Ippan = "電気_KairoIppan";
            public const string 電気_Kairo_Other = "電気_KairoOther";
            public const string 電気_Kairo_Tva = "電気_KairoTva";
            public const string 電気_Kairo_Nisetai = "電気_KairoNisetai";
            public const string 電気_Kairo = "電気_Kairo";
            public const string 電気_Reminder = "電気_Reminder";
            public const string 電気_幹線 = "電気_幹線";
            public const string 電気_JboxWire = "電気_JboxWire";
            public const string Signal_Wire = "Signal_Wire";
            public const string 電気_DistancePattern = "電気_DistancePattern";
            public const string 電気_ConnectorCheckbox = "電気_ConnectorCheckbox";
            //PS5対応
            public const string 電気_設備_仮想 = "電気_設備_仮想";
            public const string 電気_配線_仮想 = "電気_配線_仮想";

            public const string _2_0_通芯 = "_2-0_通芯";
            public const string _2_3_仕上 = "_2-3_仕上";
            public const string _2_6_設備 = "_2-6_設備";
            public const string _2_8_雑線 = "_2-8_雑線";
            public const string _2_9_電気 = "_2-9_電気";
            public const string _2_C_ハッチ = "_2-C_ハッチ";
            public const string _2_D_寸法 = "_2-D_寸法";
            public const string _2_E_文字 = "_2-E_文字";
            public const string _2_E_建具 = "_2-E_建具";
            public const string _2_4_雑仕上 = "_2-4_雑仕上";

            // SocketPlan対応
            public const string 電気_SocketPlan = "電気_SocketPlan";
            public const string 電気_SocketPlan_Specific = "電気_SocketPlan_Specific";
        }

        public struct LightSerial
        {
            public const string _6HL = "6HL";
            public const string _10HL = "10HL";
            public const string _12HL = "12HL";
            public const string HL_L = "HL-L";
            public const string HL_N = "HL-N";
            public const string HLS_L = "HLS-L";
            public const string HLS_N = "HLS-N";
            public const string HL = "HL";
            public const string 商品を選択して下さい = "商品を選択して下さい";

            public const string F6 = "F6";
            public const string F8 = "F8";
            public const string F10 = "F10";
            public const string F12 = "F12";
            public const string D6 = "D6";
            public const string D8 = "D8";
            public const string D10 = "D10";
            public const string D12 = "D12";
            public const string W6 = "W6";
            public const string W8 = "W8";
            public const string W10 = "W10";
            public const string W12 = "W12";

            public const string LL = "LL";
            public const string LN = "LN";
            public const string KLL = "KLL";
            public const string KLN = "KLN";
            public const string ML = "ML";
            public const string MN = "MN";
            public const string KML = "KML";
            public const string KMN = "KMN";
            public const string PL = "PL";
            public const string BL = "BL";
            public const string BN = "BN";

            public const string MLP1 = "ML(P1)";
            public const string MNP1 = "MN(P1)";
            public const string LLP1 = "LL(P1)";
            public const string LNP1 = "LN(P1)";
            public const string KMLP1 = "KML(P1)";
            public const string KMNP1 = "KMN(P1)";
            public const string KLLP1 = "KLL(P1)";
            public const string KLNP1 = "KLN(P1)";
            public const string WLNP1 = "WLN(P1)";
            public const string WMNP1 = "WMN(P1)";
            public const string L_WLNP1 = "L-WLN(P1)";
            public const string L_WMNP1 = "L-WMN(P1)";
            public const string WLLP1 = "WLL(P1)";
            public const string WMLP1 = "WML(P1)";
            public const string L_WLLP1 = "L-WLL(P1)";
            public const string L_WMLP1 = "L-WML(P1)";

            public const string LGB81506LE1 = "LGB81506LE1"; //i-smileの場合、標準設定とするため、オプション計上しない
            public const string LGB81568LE1 = "LGB81568LE1"; //i-smileの場合、オプション対応となるため、オプション計上を行う

            public const string PLS = "PLS";
            public const string MLS = "MLS";
            public const string LLS = "LLS";
        }

        #region ダウンライト対応
        public struct LightSerialCategoryId
        {
            public const int LightUpSelection = 1;
            public const int HL = 2;
            public const int NewLEDLight = 3;
            public const int Others = 99;
        }
        #endregion

        public struct LivingLightControlSerial
        {
            //メインスイッチ
            public const string NQ28732WK = "NQ28732WK";
            public const string NQ28732SK = "NQ28732SK";
            public const string NQ28751WK = "NQ28751WK";
            public const string NQ28751SK = "NQ28751SK";
            public const string NQ28752WK = "NQ28752WK";
            public const string NQ28752SK = "NQ28752SK";

            //ロータリースイッチ
            public const string NQ21585Z = "NQ21585Z";
            public const string NQ21582Z = "NQ21582Z";
            public const string NQ21595Z = "NQ21595Z";
            public const string NQ21592Z = "NQ21592Z";

            //サブスイッチ
            public const string NQ28706W = "NQ28706W";
            public const string NQ28706S = "NQ28706S";
            public const string NK28706W = "NK28706W";

            //かってにスイッチから変更された分
            public const string NQ20355 = "NQ20355";
            public const string NQ20356 = "NQ20356";
        }

        public struct KatteniSwitchSerial
        {
            public const string WTC5820 = "WTC5820";
            public const string WTK37314 = "WTK37314";
            public const string WTK39114 = "WTK39114";
            public const string WTK1411 = "WTK1411";
            public const string WTK1614W = "WTK1614W";
            public const string WTK1911 = "WTK1911";
            public const string WTK4431 = "WTK4431";
            public const string WTK4431W = "WTK4431W";
            public const string WTK24111 = "WTK24111";
            public const string WTK24111K = "WTK24111K";
            public const string WTK29111K = "WTK29111K";
            public const string WTK34314 = "WTK34314";
            public const string WTK2314 = "WTK2314";
            public const string WTC58207 = "WTC58207";
            public const string WTK1274W = "WTK1274W";
        }

        public struct LightType
        {
            public const int Ceiling = 1;
            public const int Down = 2;
            public const int Wall = 3;
        }

        public struct Linetype
        {
            public const string CeilingPanel = "CeilingPanel";
            public const string CeilingReceiver = "CeilingReceiver";
            public const string DuctBrokenLine = "DuctBrokenLine";
            public const string DASHED2 = "DASHED2";
        }

        public struct LineWeight
        {
            public const int Default = -3;
            public const int _0_00 = 0;
            public const int _0_15 = 15;
            public const int _0_20 = 20;
            public const int _0_25 = 25;
            public const int _0_30 = 30;
            public const int _0_50 = 50;
            public const int _0_70 = 70;
            public const int _0_80 = 80;
            public const int _1_00 = 100;
        }

        public struct LayoutId
        {
            public const int 結線図 = 2;
            public const int パネル別配線取付指示図 = 3;
            public const int 生産用結線図 = 4;
            public const int 分電盤系統図 = 5;
            public const int 提案電気図面1F = 6;
            public const int 提案電気図面2F = 7;
            public const int 電気立面図 = 8;
            public const int HEMS回路図面1F = 9;
            public const int HEMS回路図面2F = 10;
            public const int 分電盤仕様図 = 11;
            public const int HEMS回路図面1F_加工依頼後 = 12;
            public const int HEMS回路図面2F_加工依頼後 = 13;
            public const int 分電盤仕様図_加工依頼後 = 14;
            public const int 種類別配線図 = 15;
            public const int SocketPlan1F = 19;
            public const int SocketPlan2F = 20;
        }

        public struct LayoutTextType
        {
            public const int お客様名 = 1;
            public const int 家タイプ = 2;
            public const int 縮尺 = 3;
            public const int 図面名 = 4;
            public const int 電気図面作成コード = 5;
            public const int 営業所 = 6;
            public const int 営業担当 = 7;
            public const int インテリア担当 = 8;
            public const int 設計担当 = 9;
            public const int お客様コード = 10;
            public const int 図面番号 = 11;
            public const int ジョイントボックス番号 = 12;
            public const int 家タイプ_加工依頼前 = 102;
            public const int パネル番号 = 16;
        }

        public struct Room
        {
            public const string 階段 = "階段";
            public const string 階段下 = "階段下";
            public const string 階段下物入 = "階段下物入";
            public const string 階段室 = "階段室";
            public const string 外部 = "外部";
            public const string 共通 = "共通";
            public const string トイレ = "トイレ";
            public const string 玄関 = "玄関";
            public const string ホール = "ホール";
            public const string キッチン = "キッチン";
            public const string DK = "DK";
            public const string システムバス = "システムバス";
            public const string ｼｽﾃﾑﾊﾞｽ = "ｼｽﾃﾑﾊﾞｽ";
            public const string シャワールーム = "シャワールーム";
            public const string ｼｬﾜｰﾙｰﾑ = "ｼｬﾜｰﾙｰﾑ";
            public const string ＩＵＢ = "ＩＵＢ";
            public const string IUB = "IUB";
            public const string ポーチ = "ポーチ";
            public const string 吹抜 = "吹抜";
            public const string iｼﾘｰｽﾞ = "iｼﾘｰｽﾞ";
            public const string 大壁和室 = "大壁和室";
            public const string 真壁和室 = "真壁和室";
            public const string 車庫 = "車庫";
        }

        public struct RoomCode
        {
            public const string ポーチ = "0511";
            public const string 階段室 = "0501";
        }

        public struct ShikakuItemId
        {
            public const int HEMS_ARI = 81; //HEMS分電盤：有
            public const int HEMS_NASHI = 88; //HEMS分電盤：無

            public const int SOLAR_ARI = 43; //太陽光発電：有
            public const int SOLAR_ARI_YOJO = 79; //太陽光発電：有 余剰電力
            public const int SOLAR_ARI_ZENRYO = 80; //太陽光発電：有 全量買取
            public const int SOLAR_NASHI = 46; //太陽光発電：無

            public const int NISETAI_NASI = 56; //特殊工事：2世帯電気工事：無
            public const int NISETAI_ARI = 57; //特殊工事：2世帯電気工事：有

            public const int METER_BOX_ARI = 64; //メーターボックス：有
            public const int METER_BOX_WG = 65; //メーターボックス：Nﾎﾜｲﾄｸﾞﾚｰ
            public const int METER_BOX_LB = 66; //メーターボックス：ﾗｲﾄﾍﾞｰｼﾞｭ
            public const int METER_BOX_DB = 67; //メーターボックス：ﾀﾞｰｸﾌﾞﾗｳﾝ

            public const int BOOSTER_SOCKET_ARI = 19; //ﾌﾞｰｽﾀｰ用ｺﾝｾﾝﾄ：有
            public const int BOOSTER_SOCKET_NASHI = 18; //ﾌﾞｰｽﾀｰ用ｺﾝｾﾝﾄ：無
            public const int HIKIKOMI_ARI = 59; //特殊工事：引込開閉器：有
            public const int HIKIKOMI_KAIDEN_ARI = 90; //引込開閉器：買電用：有
            public const int HIKIKOMI_KAIDEN_NASHI = 89; //引込開閉器：買電用：無
            public const int HIKIKOMI_BAIDEN_ARI = 92; //引込開閉器：売電用：有
            public const int HIKIKOMI_BAIDEN_NASHI = 91; //引込開閉器：売電用：無

            public const int SUKKIRI_POLE_NASI = 60; //ｽｯｷﾘﾎﾟｰﾙ：無
            public const int SUKKIRI_POLE_ARI_PATTERN = 61; //ｽｯｷﾘﾎﾟｰﾙ：有 ﾊﾟﾀｰﾝ
            public const int SUKKIRI_POLE_ARI_PATTERN_GAI = 63; //ｽｯｷﾘﾎﾟｰﾙ：有 ﾊﾟﾀｰﾝ外
            public const int SUKKIRI_POLE_HINBAN_NUMBER = 62; //ｽｯｷﾘﾎﾟｰﾙ：有 ﾊﾟﾀｰﾝ リスト
            public const int SUKKIRI_POLE_HINBAN_COLOR = 76; //ｽｯｷﾘﾎﾟｰﾙ：有 ﾊﾟﾀｰﾝ 色リスト

            public const int BUNPAIKI_ARI_KAZU = 22; //分配器：有 分配数
            public const int BUNPAIKI_TUIKA_ARI_KAZU = 28; //分配器追加：有 分配数
            public const int BUNPAIKI_TUUDEN_ARI_KAZU = 25; //分配器(全端子通電型変更)：有 分配数
            public const int BUNPAIKI_TUUDEN_TUIKA_ARI_KAZU = 31; //分配器追加(全端子通電型変更)：有 分配数

            public const int BUNDENBAN_HIRAIKI_ARI = 96; //分電盤避雷器：有
            public const int BUNDENBAN_KANSHIN_RELAY = 98; //分電盤感震リレー：有

            public const int BUNDENBAN_ARI = 81; //HEMS分電盤：有
            public const int BUNDENBAN_ORIGINAL = 82; //HEMS分電盤：オリジナル
            public const int BUNDENBAN_INABA = 84; //HEMS分電盤：有>因幡電機

            public const int DENKI_YOURYO_1 = 41; //電気容量：1
            public const int DENKI_YOURYO_2 = 42; //電気容量：2
            public const int CONTRACT_PLANCODE_1 = 38; // プランコード:1
            public const int CONTRACT_PLANCODE_2 = 93; // プランコード:2

            public const int CONTRACT_NAME_1 = 39; //電力契約名：1
            public const int CONTRACT_NAME_2 = 94; //電力契約名：2

            public const int CONTRACT_CAMPANY_1 = 99; // 電力会社名：1

            public const int SOCKETPLATE_ROUND = 111;   // スイッチコンセントプレート：ラウンド
            public const int SOCKETPLATE_SQUARE = 112;  // スイッチコンセントプレート：スクエア
        }

        public struct Specification
        {
            public const int E付 = 1;
            public const int 専用 = 2;
            public const int 直結 = 3;
            public const int 防水 = 4;
            public const int 一次送り = 5;
            public const int 住設用部材ICUBE_ISMART = 10;
            public const int 住設用部材IHEAD_IPPAN = 11;
            public const int 住設用部材IHEAD_IPPAN_HOKKAIDO = 12;
            public const int 組み合わせシンボル_親 = 13; //この仕様はユーザーにメンテさせない
            public const int 組み合わせシンボル_子 = 14; //この仕様はユーザーにメンテさせない
            public const int インターホン = 15;
            public const int シーリングライト = 16;
            public const int ダウンライト = 17;
            public const int ウォールライト = 18;
            public const int 設備 = 19;
            public const int スマートシリーズ = 20;
            public const int リモコン = 21;
            public const int ツインライト = 22;
            public const int PVリモコン = 23;

            //以下は、コメントに対して仕様を設定する場合に使っている
            public const int コメント_かってにスイッチ = 1001;
            public const int コメント_かってにスイッチ_壁_親 = 1002;
            public const int コメント_かってにスイッチ_壁_子 = 1003;
            public const int コメント_かってにスイッチ_天井_親 = 1004;
            public const int コメント_かってにスイッチ_天井_子 = 1005;
            public const int コメント_かってにスイッチ_操作ユニット = 1006;
            public const int コメント_かってにスイッチ_トイレ = 1007;
            public const int コメント_スマートシリーズ = 1008;
            public const int コメント_かってにスイッチ不可 = 1090;
            public const int コメント_あけたらスイッチ = 1100;
            public const int コメント_あけたらスイッチ_ノーマル = 1101;
            public const int コメント_あけたらスイッチ_Case1 = 1102;
            public const int コメント_あけたらスイッチ_Case2 = 1103;
            public const int コメント_あけたらスイッチ_1st3Way = 1104;
            public const int コメント_あけたらスイッチ_2nd3Way = 1105;
            public const int コメント_あけたらスイッチ_Sub = 1106;
            public const int コメント_とったらリモコン = 1200;
            public const int コメント_とったらリモコン_ノーマル = 1201;
            public const int コメント_とったらリモコン_Case1 = 1202;
            public const int コメント_とったらリモコン_1st3Way = 1203;
            public const int コメント_とったらリモコン_2nd3Way = 1204;
            public const int コメント_LEDライコン = 1300;
            public const int コメント_LEDライコン_ノーマル = 1301;
            public const int コメント_LEDライコン_Case1 = 1302;
            public const int コメント_LEDライコン_Case2 = 1303;
            public const int コメント_LEDライコン_Case3 = 1304;
            public const int コメント_LEDライコン_1st3way = 1305;
            public const int コメント_LEDライコン_2nd3Way = 1306;
            public const int コメント_OtherSwitch = 1400;
            public const int コメント_OtherSwitchCase1 = 1401;
            public const int コメント_OtherSwitchCase2 = 1402;
            public const int コメント_OtherSwitchCase3 = 1403;
            public const int コメント_OtherSwitchCase4 = 1404;
            public const int コメント_SignalWireConnectable = 1900;
            public const int コメント_サブスイッチ = 1901;
            public const int コメント_SingleForPlate = 1902;

            //以下は、平面図に元々かかれているテキストに仕様を設定する場合に使っている
            public const int テキスト_キッチンシリアル = 2001;
            //システム判断用
            public const int コメント_壁内配線 = 9000;
        }

        public struct Text
        {
            public const string ﾌﾟﾛｼﾞｪｸﾀｰ用 = "ﾌﾟﾛｼﾞｪｸﾀｰ用";
            public const string 煙式火災警報器 = "煙式火災警報器";
            public const string 熱式火災警報器 = "熱式火災警報器";
            public const string 入切表示トイレ = "入切表示(トイレ）";
            public const string かってにｽｲｯﾁ = "かってにｽｲｯﾁ";
            public const string 操作ﾕﾆｯﾄ = "操作ﾕﾆｯﾄ";
            public const string IH = "IH";
            public const string AC = "AC";
            public const string ス = "(ｽ)";
            public const string 換気扇 = "換気扇";
            public const string _200V = "200V";
            public const string EV_Outlet = "充電用EV・PHEV";
            public const string EV_OutletWithout充電用 = "EV・PHEV";
            public const string EV_Switch = "EV・PHEVｽｲｯﾁ";
            public const string EV_Switch充電用 = "EV・PHEV充電用";
            public const string オーニング用 = "オーニング用";
            public const string ﾄﾞﾚｲﾝ用 = "ﾄﾞﾚｲﾝ用";
            public const string 融雪用 = "融雪用";
            public const string S = "S";
            public const string ﾈｰﾑ = "ﾈｰﾑ";
            public const string ﾎﾀﾙ = "ﾎﾀﾙ";
            public const string 光ﾌｧｲﾊﾞｰ = "光ﾌｧｲﾊﾞｰ";
            public const string LEDﾗｲｺﾝ = "LEDﾗｲｺﾝ";
            public const string 基礎先行配管 = "基礎先行配管";
            public const string 電動昇降機用 = "電動昇降機用";
            public const string 上部吹抜 = "上部吹抜";
            public const string 電気温水器 = "電気温水器";
            public const string 電池L = "電池-L";
            public const string BOX内 = "BOX内";
            public const string ｴｺｷｭｰﾄ = "ｴｺｷｭｰﾄ";
            public const string ｴｺｷｭ_ﾄ = "ｴｺｷｭ-ﾄ";
            public const string エコウィル = "エコウィル";
            public const string ｴｺｳｨﾙ = "ｴｺｳｨﾙ";
            public const string 蓄電ﾕﾆｯﾄ = "蓄電ﾕﾆｯﾄ";
            public const string EVPS = "EVPS";
            public const string SB用 = "SB用";
            public const string ロスガード = "ﾛｽｶﾞｰﾄﾞ";
            public const string デシカント = "ﾃﾞｼｶﾝﾄ";
            public const string ｶﾞｽﾎﾞｲﾗｰ = "ｶﾞｽﾎﾞｲﾗｰ";
            public const string 灯油ﾎﾞｲﾗｰ = "灯油ﾎﾞｲﾗｰ";
            public const string RAY1 = "RAY1";
            public const string RAY2 = "RAY2";
            public const string RAY = "RAY";
            public const string 除湿 = "除湿";
            public const string HCU_HU = "HCU/HU";
            public const string VEH = "VEH";
            public const string LIGHT_CONTROL_SERIAL_PREFIX = "NQ";
            public const string ECO_ONE = "ECO ONE";
            public const string 壁付 = "壁付";
            public const string 専用E付 = "専用E付";
            public const string 専用Ｅ付 = "専用Ｅ付";
            public const string 専用E無 = "専用E無";
            public const string 専用Ｅ無 = "専用Ｅ無";
            public const string 専用 = "専用";
            public const string _0030kw = "3.0kw";
            public const string _0055kw = "5.5kw";
            public const string _0080kw = "8.0kw";
            public const string _0099kw = "9.9kw";
            public const string IH専用 = "IH専用";
            public const string 勾配天井 = "勾配天井";
            public const string 光取出 = "光取出";
            public const string NET取出 = "NET取出";
            public const string LAN取出 = "LAN取出";
            public const string TEL取出 = "TEL取出";
            public const string TV取出 = "TV取出";
            public const string BS用取出 = "BS用取出";
            public const string CS用取出 = "CS用取出";
            public const string CATV取出 = "CATV取出";
            public const string アンテナ取出 = "アンテナ取出";
            public const string BSアンテナ取出 = "BSアンテナ取出";
            public const string TVアンテナ取出 = "TVアンテナ取出";
            public const string CSアンテナ取出 = "CSアンテナ取出";
            public const string TV配線取出 = "TV配線取出";
            public const string _3口 = "3口";
            public const string E_断 = "E_断";
            public const string E_遮 = "E_遮";
            public const string E_レ = "E_レ";
            public const string 屋外用 = "屋外用";
            public const string トイレ用 = "トイレ用";
            public const string ﾊﾟﾜｰｺﾝﾃﾞｨｼｮﾅ = "ﾊﾟﾜｰｺﾝﾃﾞｨｼｮﾅ";
            public const string 天井裏 = "天井裏";
            public const string 直結 = "直結";
            public const string ｻｰｷｭﾚｰﾀｰ = "ｻｰｷｭﾚｰﾀｰ";
            public const string サーキュレーター = "サーキュレーター";
            public const string 増設 = "増設";
            public const string 吹抜 = "吹抜";
            public const string Hikikomiban = "引込盤";
            public const string Taiyoukou = "太陽光用";
            public const string 全量買取 = "全量買取";
            public const string 全量買取用 = "全量買取用";
            public const string 現場加工 = "現場加工";
        }

        public struct WireItemId
        {
            public const int 照明 = 1;
            public const int スイッチ = 2;
            public const int コンセント = 3;
            public const int 照明_屋外 = 4;
            public const int スイッチ_3路 = 5;
            public const int コンセント_水周り = 6;
            public const int 専用orパワコンorコンセント_外周 = 7;
            public const int ジョイントボックス = 8;
            public const int 専用orコンセント_外周_E付 = 9;
            public const int ジョイントボックス_E付 = 10;
            public const int IHorEV = 11;
            public const int 弱電 = 12;
            public const int 火災報知機 = 13;
            public const int 火災報知機_壁付 = 14;
        }

        public struct HouseTypeGroupStandardItemName
        {
            public const string GroupHeader = "House_";
            public const string ShawaToireYou = "House_E付コンセント";
            public const string ToireYou = "House_コンセント配線";
            public const string Aircon = "House_エアコン";
        }

        public struct HouseTypeGroupId
        {
            public const int Assre = 2;
            public const int Wakugumi = 3;
            public const int A2_A3_muku = 4;
        }

        public struct CommentCategoryId
        {
            public const int スイッチ = 2;
            public const int 高さ = 10;
            public const int CommonSerials = 12;
            public const int スマートシリーズシリアル = 17;
            /// <summary>
            /// CommentCategoriesテーブルのSerial for Unit Wiring OnlyのID。
            /// </summary>
            public const int SerialForUnitWiringOnly = 18;

        }

        public struct BayWindow
        {
            public const string ﾊｰﾓﾆｰﾌﾟﾚｼｬｽﾌｧﾆﾁｬｰｳｲﾝﾄﾞｳ = "ﾊｰﾓﾆｰﾌﾟﾚｼｬｽﾌｧﾆﾁｬｰｳｲﾝﾄﾞｳ";
            public const string ﾊｰﾓﾆﾍﾞｲｳｲﾝﾄﾞｳ = "ﾊｰﾓﾆｰﾍﾞｲｳｲﾝﾄﾞｳ";
            public const string ﾍﾞｲｳｲﾝﾄﾞｳ = "ﾍﾞｲｳｲﾝﾄﾞｳ";
            public const string ﾊｰﾓﾆｰﾛｲﾔﾙｳｲﾝﾄﾞｳ = "ﾊｰﾓﾆｰﾛｲﾔﾙｳｲﾝﾄﾞｳ";
            public const string ﾌﾟﾚｼｬｽｳｲﾝﾄﾞｳ = "ﾌﾟﾚｼｬｽｳｲﾝﾄﾞｳ";
            public const string ﾌｧﾆﾁｬｰｳｲﾝﾄﾞｳ = "ﾌｧﾆﾁｬｰｳｲﾝﾄﾞｳ";
            public const string ﾊｰﾓﾆｰPFｳｲﾝﾄﾞｳ = "ﾊｰﾓﾆｰPFｳｲﾝﾄﾞｳ";
            public const string 百年出窓 = "百年出窓";
        }

        //出窓(照明不可)
        public struct BayWindowNoLight
        {
            public const string 地袋付和室出窓_90U = "地袋付和室出窓90U";
            public const string ﾘｽﾞﾑｳｲﾝﾄﾞｳ_30U = "ﾘｽﾞﾑｳｲﾝﾄﾞｳ30U";
            public const string ﾊｰﾓﾆｰﾘｽﾞﾑｳｲﾝﾄﾞｳ_60U = "ﾊｰﾓﾆｰﾘｽﾞﾑｳｲﾝﾄﾞｳ60U";
            public const string ｽｸｴｱｳｲﾝﾄﾞｳ = "ｽｸｴｱｳｲﾝﾄﾞｳ";
            public const string ﾊｰﾓﾆｰﾛｲﾔﾙｳｲﾝﾄﾞｳ_90A = "ﾊｰﾓﾆｰﾛｲﾔﾙｳｲﾝﾄﾞｳ90A";
            public const string ﾊｰﾓﾆｰﾍﾞｲｳｲﾝﾄﾞｳ_90A = "ﾊｰﾓﾆｰﾍﾞｲｳｲﾝﾄﾞｳ90A";
            public const string 百年J6060 = "百年J6060";
        }

        public struct FloorUnitKW
        {
            public const decimal Mitsubishi_PICO = 1.54m;
            public const decimal Mitsubishi_LEO = 2.95m;
        }

        public struct MakerName
        {
            public const string Mitsubishi = "三菱電機";
            public const string Sanpot = "サンポット";
        }

        public struct InsulationRegion
        {
            public const string Ⅰ = "0010";
            public const string Ⅰa = "0011";
            public const string Ⅰb = "0012";
            public const string Ⅱ = "0020";
            public const string ⅠWithUrethan = "0060";
            public const string ⅠaWithUrethan = "0065";
            public const string ⅠbWithUrethan = "0067";
            public const string ⅡWithUrethan = "0070";
        }

        /// <summary>
        /// 回路タイプの名前 @sato
        /// </summary>
        public struct BranchType
        {
            /// <summary>
            /// 消費電力
            /// </summary>
            public const string BRANCH = "branch";
            /// <summary>
            /// 買電電力
            /// </summary>
            public const string BUY_POWER = "buy_power";
            /// <summary>
            /// 売電電力
            /// </summary>
            public const string SELL_POWER = "sell_power";
            /// <summary>
            /// 太陽光発電
            /// </summary>
            public const string SOLAR_POWER = "solar_power";
            /// <summary>
            /// 充電量
            /// </summary>
            public const string BATTERY_CHARGE = "battery_charge";
            /// <summary>
            /// 放電量
            /// </summary>
            public const string BATTERY_DISCHARGE = "battery_discharge";
            /// <summary>
            /// 非常用電源
            /// </summary>
            public const string RESERVE_POWER = "reserve_power";
            /// <summary>
            /// 外部発電消費電力
            /// </summary>
            public const string EXTERNAL_CONSUMPTION = "external_consumption";
            /// <summary>
            /// 外部発電量
            /// </summary>
            public const string EXTERNAL_GENERATE = "external_generate";

        }

        public struct ConstructionTypeId
        {
            public const string I_PALETTE = "320"; //スマイル分譲
        }

        public struct ElectricCompanyCode
        {
            public const string 東北電力 = "01";
            public const string 東京電力 = "02";
            public const string 中部電力 = "03";
            public const string 北陸電力 = "04";
            public const string 関西電力 = "05";
            public const string 中国電力 = "06";
            public const string 四国電力 = "07";
            public const string 九州電力 = "08";
            public const string 北海道電力 = "09";
        }

        /// <summary>
        /// 仕様書に記載されているHEMSのProductCode
        /// </summary>
        public struct HemsProductCode
        {
            public const string ZEHあり = "4000001";
            public const string ZEHなし = "4000002";
            public const string 展示場仕様1 = "4000003";
            public const string 展示場仕様2 = "4000004";
            public const string 展示場仕様3 = "4000005";
        }
        public struct SpecificationType
        {
            public const int 設備 = 2;
        }
        public struct HEMS
        {
            public const int 回路最大数 = 44;

            public struct ItemID
            {
                public const int シーリングライト = 1;
                public const int その他照明スイッチ = 2;
                public const int 防犯アラーム = 3;
                public const int 防犯アラーム_子機 = 4;
                public const int 火災報知機 = 5;
                public const int エコキュート = 6;
                public const int 床暖房 = 7;
                public const int デシカント = 8;
                public const int エアコン = 9;
                public const int ロスガード９０ = 10;
                public const int 電力測定ボックス = 11;
                public const int ホームゲートウェイ = 12;
            }
            public struct Column
            {
                public const string ConstructionCode = "ConstructionCode";
                public const string FamilyNumber = "FamilyNumber";
                public const string Floor = "Floor";
                public const string RoomID = "RoomID";
                public const string ItemID = "ItemID";
                public const string Seq = "Seq";
            }
        }

        public struct PartsColor
        {
            public const int White = 1;
            public const int Beigu = 2;
        }
    }
}
