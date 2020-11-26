//------------------------------------------------------------------------------
// <auto-generated>
//     ���̃R�[�h�̓c�[���ɂ���Đ�������܂����B
//     �����^�C�� �o�[�W����:2.0.50727.8669
//
//     ���̃t�@�C���ւ̕ύX�́A�ȉ��̏󋵉��ŕs���ȓ���̌����ɂȂ�����A
//     �R�[�h���Đ��������Ƃ��ɑ��������肵�܂��B
// </auto-generated>
//------------------------------------------------------------------------------

namespace SocketPlan.WebService
{
    using System;
    using System.Collections.Generic;
    using Edsa.Data;
    using Edsa.Data.Attributes;
    
    
    [Serializable()]
    [DbTable(Name = "SpcadPlanInfos_Extract", DatabaseType = DatabaseType.SqlServer, ConnectionSettingKeyName = "SocketPlan.WebService.Properties.Settings.ConnectionStringOfPhotovoltaic")]
    public partial class SpcadPlanInfos_Extract : DataEntity<SpcadPlanInfos_Extract>
    {
        
        private string _ConstructionCode;
        
        private string _PlanNo;
        
        private string _ContractKind;
        
        private int _HouseType;
        
        private int _PanelVersion;
        
        private int _PanelConvertVersion;
        
        private int _Carport1;
        
        private int _Carport2;
        
        private bool _CarportSnowstop;
        
        private int _GroundbasedQty;
        
        private string _SnowAreaType;
        
        private int _SnowAccumulation;
        
        private int _ReferenceWindVelocity;
        
        private int _AreaCode;
        
        private int _Gousetsu;
        
        private int _BankinSpace;
        
        private int _RoofExtend;
        
        private int _TekkotsuUdekiQty;
        
        private int _RoofPanelQty;
        
        private decimal _RoofKw;
        
        private decimal _CarportKw;
        
        private decimal _GroundbasedKw;
        
        private decimal _ShinseiKw;
        
        private int _ExcelMode;
        
        private string _SpecialNote;
        
        private string _FileName;
        
        private int _Inverter30Qty;
        
        private int _Inverter55Qty;
        
        private int _Inverter80Qty;
        
        private int _Inverter99Qty;
        
        private bool _IsHemsInverter;
        
        private bool _ManualRegist;
        
        private string _SpcadVersion;
        
        private string _SpcadMode;
        
        private System.Nullable<System.DateTime> _UpdatedDate;
        
        private string _ElectricCompany;
        
        [DbColumn(Name="ConstructionCode", TypeName="varchar", IsPrimaryKey=true, Length=12)]
        public virtual string ConstructionCode
        {
            get
            {
                return this._ConstructionCode;
            }
            set
            {
                this._ConstructionCode = value;
            }
        }
        
        [DbColumn(Name="PlanNo", TypeName="varchar", IsPrimaryKey=true, Length=10)]
        public virtual string PlanNo
        {
            get
            {
                return this._PlanNo;
            }
            set
            {
                this._PlanNo = value;
            }
        }
        
        [DbColumn(Name="ContractKind", TypeName="varchar", Length=1)]
        public virtual string ContractKind
        {
            get
            {
                return this._ContractKind;
            }
            set
            {
                this._ContractKind = value;
            }
        }
        
        [DbColumn(Name="HouseType", TypeName="int", Length=4)]
        public virtual int HouseType
        {
            get
            {
                return this._HouseType;
            }
            set
            {
                this._HouseType = value;
            }
        }
        
        [DbColumn(Name="PanelVersion", TypeName="int", Length=4)]
        public virtual int PanelVersion
        {
            get
            {
                return this._PanelVersion;
            }
            set
            {
                this._PanelVersion = value;
            }
        }
        
        [DbColumn(Name="PanelConvertVersion", TypeName="int", Length=4)]
        public virtual int PanelConvertVersion
        {
            get
            {
                return this._PanelConvertVersion;
            }
            set
            {
                this._PanelConvertVersion = value;
            }
        }
        
        [DbColumn(Name="Carport1", TypeName="int", Length=4)]
        public virtual int Carport1
        {
            get
            {
                return this._Carport1;
            }
            set
            {
                this._Carport1 = value;
            }
        }
        
        [DbColumn(Name="Carport2", TypeName="int", Length=4)]
        public virtual int Carport2
        {
            get
            {
                return this._Carport2;
            }
            set
            {
                this._Carport2 = value;
            }
        }
        
        [DbColumn(Name="CarportSnowstop", TypeName="bit", Length=1)]
        public virtual bool CarportSnowstop
        {
            get
            {
                return this._CarportSnowstop;
            }
            set
            {
                this._CarportSnowstop = value;
            }
        }
        
        [DbColumn(Name="GroundbasedQty", TypeName="int", Length=4)]
        public virtual int GroundbasedQty
        {
            get
            {
                return this._GroundbasedQty;
            }
            set
            {
                this._GroundbasedQty = value;
            }
        }
        
        [DbColumn(Name="SnowAreaType", TypeName="varchar", Length=1)]
        public virtual string SnowAreaType
        {
            get
            {
                return this._SnowAreaType;
            }
            set
            {
                this._SnowAreaType = value;
            }
        }
        
        [DbColumn(Name="SnowAccumulation", TypeName="int", Length=4)]
        public virtual int SnowAccumulation
        {
            get
            {
                return this._SnowAccumulation;
            }
            set
            {
                this._SnowAccumulation = value;
            }
        }
        
        [DbColumn(Name="ReferenceWindVelocity", TypeName="int", Length=4)]
        public virtual int ReferenceWindVelocity
        {
            get
            {
                return this._ReferenceWindVelocity;
            }
            set
            {
                this._ReferenceWindVelocity = value;
            }
        }
        
        [DbColumn(Name="AreaCode", TypeName="int", Length=4)]
        public virtual int AreaCode
        {
            get
            {
                return this._AreaCode;
            }
            set
            {
                this._AreaCode = value;
            }
        }
        
        [DbColumn(Name="Gousetsu", TypeName="int", Length=4)]
        public virtual int Gousetsu
        {
            get
            {
                return this._Gousetsu;
            }
            set
            {
                this._Gousetsu = value;
            }
        }
        
        [DbColumn(Name="BankinSpace", TypeName="int", Length=4)]
        public virtual int BankinSpace
        {
            get
            {
                return this._BankinSpace;
            }
            set
            {
                this._BankinSpace = value;
            }
        }
        
        [DbColumn(Name="RoofExtend", TypeName="int", Length=4)]
        public virtual int RoofExtend
        {
            get
            {
                return this._RoofExtend;
            }
            set
            {
                this._RoofExtend = value;
            }
        }
        
        [DbColumn(Name="TekkotsuUdekiQty", TypeName="int", Length=4)]
        public virtual int TekkotsuUdekiQty
        {
            get
            {
                return this._TekkotsuUdekiQty;
            }
            set
            {
                this._TekkotsuUdekiQty = value;
            }
        }
        
        [DbColumn(Name="RoofPanelQty", TypeName="int", Length=4)]
        public virtual int RoofPanelQty
        {
            get
            {
                return this._RoofPanelQty;
            }
            set
            {
                this._RoofPanelQty = value;
            }
        }
        
        [DbColumn(Name="RoofKw", TypeName="decimal", DecimalPlace=3, Length=5)]
        public virtual decimal RoofKw
        {
            get
            {
                return this._RoofKw;
            }
            set
            {
                this._RoofKw = value;
            }
        }
        
        [DbColumn(Name="CarportKw", TypeName="decimal", DecimalPlace=3, Length=5)]
        public virtual decimal CarportKw
        {
            get
            {
                return this._CarportKw;
            }
            set
            {
                this._CarportKw = value;
            }
        }
        
        [DbColumn(Name="GroundbasedKw", TypeName="decimal", DecimalPlace=3, Length=5)]
        public virtual decimal GroundbasedKw
        {
            get
            {
                return this._GroundbasedKw;
            }
            set
            {
                this._GroundbasedKw = value;
            }
        }
        
        [DbColumn(Name="ShinseiKw", TypeName="decimal", DecimalPlace=3, Length=5)]
        public virtual decimal ShinseiKw
        {
            get
            {
                return this._ShinseiKw;
            }
            set
            {
                this._ShinseiKw = value;
            }
        }
        
        [DbColumn(Name="ExcelMode", TypeName="int", Length=4)]
        public virtual int ExcelMode
        {
            get
            {
                return this._ExcelMode;
            }
            set
            {
                this._ExcelMode = value;
            }
        }
        
        [DbColumn(Name="SpecialNote", TypeName="varchar", Nullable=true, Length=8000)]
        public virtual string SpecialNote
        {
            get
            {
                return this._SpecialNote;
            }
            set
            {
                this._SpecialNote = value;
            }
        }
        
        [DbColumn(Name="FileName", TypeName="varchar", Nullable=true, Length=300)]
        public virtual string FileName
        {
            get
            {
                return this._FileName;
            }
            set
            {
                this._FileName = value;
            }
        }
        
        [DbColumn(Name="Inverter30Qty", TypeName="int", Length=4)]
        public virtual int Inverter30Qty
        {
            get
            {
                return this._Inverter30Qty;
            }
            set
            {
                this._Inverter30Qty = value;
            }
        }
        
        [DbColumn(Name="Inverter55Qty", TypeName="int", Length=4)]
        public virtual int Inverter55Qty
        {
            get
            {
                return this._Inverter55Qty;
            }
            set
            {
                this._Inverter55Qty = value;
            }
        }
        
        [DbColumn(Name="Inverter80Qty", TypeName="int", Length=4)]
        public virtual int Inverter80Qty
        {
            get
            {
                return this._Inverter80Qty;
            }
            set
            {
                this._Inverter80Qty = value;
            }
        }
        
        [DbColumn(Name="Inverter99Qty", TypeName="int", Length=4)]
        public virtual int Inverter99Qty
        {
            get
            {
                return this._Inverter99Qty;
            }
            set
            {
                this._Inverter99Qty = value;
            }
        }
        
        [DbColumn(Name="IsHemsInverter", TypeName="bit", Length=1)]
        public virtual bool IsHemsInverter
        {
            get
            {
                return this._IsHemsInverter;
            }
            set
            {
                this._IsHemsInverter = value;
            }
        }
        
        [DbColumn(Name="ManualRegist", TypeName="bit", Length=1)]
        public virtual bool ManualRegist
        {
            get
            {
                return this._ManualRegist;
            }
            set
            {
                this._ManualRegist = value;
            }
        }
        
        [DbColumn(Name="SpcadVersion", TypeName="varchar", Length=20)]
        public virtual string SpcadVersion
        {
            get
            {
                return this._SpcadVersion;
            }
            set
            {
                this._SpcadVersion = value;
            }
        }
        
        [DbColumn(Name="SpcadMode", TypeName="varchar", Length=20)]
        public virtual string SpcadMode
        {
            get
            {
                return this._SpcadMode;
            }
            set
            {
                this._SpcadMode = value;
            }
        }
        
        [DbColumn(Name="UpdatedDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> UpdatedDate
        {
            get
            {
                return this._UpdatedDate;
            }
            set
            {
                this._UpdatedDate = value;
            }
        }
        
        [DbColumn(Name="ElectricCompany", TypeName="nvarchar", Nullable=true, Length=40)]
        public virtual string ElectricCompany
        {
            get
            {
                return this._ElectricCompany;
            }
            set
            {
                this._ElectricCompany = value;
            }
        }
        
        public static SpcadPlanInfos_Extract Get(string _ConstructionCode, string _PlanNo)
        {
            SpcadPlanInfos_Extract entity = new SpcadPlanInfos_Extract();
            entity.ConstructionCode = _ConstructionCode;
            entity.PlanNo = _PlanNo;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}