////------------------------------------------------------------------------------
//// <auto-generated>
////     このコードはツールによって生成されました。
////     ランタイム バージョン:2.0.50727.3643
////
////     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
////     コードが再生成されるときに損失したりします。
//// </auto-generated>
////------------------------------------------------------------------------------

//namespace SocketPlan.WebService
//{
//    using System;
//    using System.Collections.Generic;
//    using Edsa.Data;
//    using Edsa.Data.Attributes;
    
    
//    [Serializable()]
//    [DbTable(Name="SekisanMaterials", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionStringOfSekisanDataBroker")]
//    public partial class SekisanMaterial : DataEntity<SekisanMaterial>
//    {
        
//        private string _ConstructionCode;
        
//        private string _PlanNo;
        
//        private string _ShiyoushoNo;
        
//        private string _BasicGradeClassificationCode;
        
//        private string _BasicGradeClassificationName;
        
//        private string _FirstGradeClassificationCode;
        
//        private string _FirstGradeClassificationName;
        
//        private string _SecondGradeClassificationCode;
        
//        private string _SecondGradeClassificationName;
        
//        private int _ConstructionItemType;
        
//        private string _ConstructionItemCode;
        
//        private int _ConstructionItemNo;
        
//        private string _ConstructionItemName;
        
//        private string _ConstructionItemName2;
        
//        private decimal _Quantity;
        
//        private string _UnitCode;
        
//        private string _UnitName;
        
//        private string _ItemKindName;
        
//        private System.Nullable<int> _ServiceType;
        
//        private string _Remarks;
        
//        private string _RenewId;
        
//        private bool _SentFlag;
        
//        private int _SystemNo;
        
//        private System.Nullable<System.DateTime> _UpdatedDate;
        
//        [DbColumn(Name="ConstructionCode", TypeName="varchar", IsPrimaryKey=true, Length=12)]
//        public virtual string ConstructionCode
//        {
//            get
//            {
//                return this._ConstructionCode;
//            }
//            set
//            {
//                this._ConstructionCode = value;
//            }
//        }
        
//        [DbColumn(Name="PlanNo", TypeName="varchar", IsPrimaryKey=true, Length=10)]
//        public virtual string PlanNo
//        {
//            get
//            {
//                return this._PlanNo;
//            }
//            set
//            {
//                this._PlanNo = value;
//            }
//        }
        
//        [DbColumn(Name="ShiyoushoNo", TypeName="varchar", IsPrimaryKey=true, Length=10)]
//        public virtual string ShiyoushoNo
//        {
//            get
//            {
//                return this._ShiyoushoNo;
//            }
//            set
//            {
//                this._ShiyoushoNo = value;
//            }
//        }
        
//        [DbColumn(Name="BasicGradeClassificationCode", TypeName="varchar", Nullable=true, Length=3)]
//        public virtual string BasicGradeClassificationCode
//        {
//            get
//            {
//                return this._BasicGradeClassificationCode;
//            }
//            set
//            {
//                this._BasicGradeClassificationCode = value;
//            }
//        }
        
//        [DbColumn(Name="BasicGradeClassificationName", TypeName="varchar", Nullable=true, Length=80)]
//        public virtual string BasicGradeClassificationName
//        {
//            get
//            {
//                return this._BasicGradeClassificationName;
//            }
//            set
//            {
//                this._BasicGradeClassificationName = value;
//            }
//        }
        
//        [DbColumn(Name="FirstGradeClassificationCode", TypeName="varchar", Nullable=true, Length=3)]
//        public virtual string FirstGradeClassificationCode
//        {
//            get
//            {
//                return this._FirstGradeClassificationCode;
//            }
//            set
//            {
//                this._FirstGradeClassificationCode = value;
//            }
//        }
        
//        [DbColumn(Name="FirstGradeClassificationName", TypeName="varchar", Nullable=true, Length=80)]
//        public virtual string FirstGradeClassificationName
//        {
//            get
//            {
//                return this._FirstGradeClassificationName;
//            }
//            set
//            {
//                this._FirstGradeClassificationName = value;
//            }
//        }
        
//        [DbColumn(Name="SecondGradeClassificationCode", TypeName="varchar", Nullable=true, Length=3)]
//        public virtual string SecondGradeClassificationCode
//        {
//            get
//            {
//                return this._SecondGradeClassificationCode;
//            }
//            set
//            {
//                this._SecondGradeClassificationCode = value;
//            }
//        }
        
//        [DbColumn(Name="SecondGradeClassificationName", TypeName="varchar", Nullable=true, Length=80)]
//        public virtual string SecondGradeClassificationName
//        {
//            get
//            {
//                return this._SecondGradeClassificationName;
//            }
//            set
//            {
//                this._SecondGradeClassificationName = value;
//            }
//        }
        
//        [DbColumn(Name="ConstructionItemType", TypeName="int", IsPrimaryKey=true, Length=4)]
//        public virtual int ConstructionItemType
//        {
//            get
//            {
//                return this._ConstructionItemType;
//            }
//            set
//            {
//                this._ConstructionItemType = value;
//            }
//        }
        
//        [DbColumn(Name="ConstructionItemCode", TypeName="varchar", IsPrimaryKey=true, Length=7)]
//        public virtual string ConstructionItemCode
//        {
//            get
//            {
//                return this._ConstructionItemCode;
//            }
//            set
//            {
//                this._ConstructionItemCode = value;
//            }
//        }
        
//        [DbColumn(Name="ConstructionItemNo", TypeName="int", IsPrimaryKey=true, Length=4)]
//        public virtual int ConstructionItemNo
//        {
//            get
//            {
//                return this._ConstructionItemNo;
//            }
//            set
//            {
//                this._ConstructionItemNo = value;
//            }
//        }
        
//        [DbColumn(Name="ConstructionItemName", TypeName="varchar", Nullable=true, Length=80)]
//        public virtual string ConstructionItemName
//        {
//            get
//            {
//                return this._ConstructionItemName;
//            }
//            set
//            {
//                this._ConstructionItemName = value;
//            }
//        }
        
//        [DbColumn(Name="ConstructionItemName2", TypeName="varchar", Nullable=true, Length=50)]
//        public virtual string ConstructionItemName2
//        {
//            get
//            {
//                return this._ConstructionItemName2;
//            }
//            set
//            {
//                this._ConstructionItemName2 = value;
//            }
//        }
        
//        [DbColumn(Name="Quantity", TypeName="decimal", DecimalPlace=2, Length=5)]
//        public virtual decimal Quantity
//        {
//            get
//            {
//                return this._Quantity;
//            }
//            set
//            {
//                this._Quantity = value;
//            }
//        }
        
//        [DbColumn(Name="UnitCode", TypeName="varchar", Nullable=true, Length=2)]
//        public virtual string UnitCode
//        {
//            get
//            {
//                return this._UnitCode;
//            }
//            set
//            {
//                this._UnitCode = value;
//            }
//        }
        
//        [DbColumn(Name="UnitName", TypeName="varchar", Nullable=true, Length=6)]
//        public virtual string UnitName
//        {
//            get
//            {
//                return this._UnitName;
//            }
//            set
//            {
//                this._UnitName = value;
//            }
//        }
        
//        [DbColumn(Name="ItemKindName", TypeName="varchar", Nullable=true, Length=30)]
//        public virtual string ItemKindName
//        {
//            get
//            {
//                return this._ItemKindName;
//            }
//            set
//            {
//                this._ItemKindName = value;
//            }
//        }
        
//        [DbColumn(Name="ServiceType", TypeName="int", Nullable=true, Length=4)]
//        public virtual System.Nullable<int> ServiceType
//        {
//            get
//            {
//                return this._ServiceType;
//            }
//            set
//            {
//                this._ServiceType = value;
//            }
//        }
        
//        [DbColumn(Name="Remarks", TypeName="varchar", Nullable=true, Length=40)]
//        public virtual string Remarks
//        {
//            get
//            {
//                return this._Remarks;
//            }
//            set
//            {
//                this._Remarks = value;
//            }
//        }
        
//        [DbColumn(Name="RenewId", TypeName="varchar", Nullable=true, Length=5)]
//        public virtual string RenewId
//        {
//            get
//            {
//                return this._RenewId;
//            }
//            set
//            {
//                this._RenewId = value;
//            }
//        }
        
//        [DbColumn(Name="SentFlag", TypeName="bit", Length=1)]
//        public virtual bool SentFlag
//        {
//            get
//            {
//                return this._SentFlag;
//            }
//            set
//            {
//                this._SentFlag = value;
//            }
//        }
        
//        [DbColumn(Name="SystemNo", TypeName="int", Length=4)]
//        public virtual int SystemNo
//        {
//            get
//            {
//                return this._SystemNo;
//            }
//            set
//            {
//                this._SystemNo = value;
//            }
//        }
        
//        [DbColumn(Name="UpdatedDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
//        public virtual System.Nullable<System.DateTime> UpdatedDate
//        {
//            get
//            {
//                return this._UpdatedDate;
//            }
//            set
//            {
//                this._UpdatedDate = value;
//            }
//        }
        
//        public static SekisanMaterial Get(string _ConstructionCode, string _PlanNo, string _ShiyoushoNo, int _ConstructionItemType, string _ConstructionItemCode, int _ConstructionItemNo)
//        {
//            SekisanMaterial entity = new SekisanMaterial();
//            entity.ConstructionCode = _ConstructionCode;
//            entity.PlanNo = _PlanNo;
//            entity.ShiyoushoNo = _ShiyoushoNo;
//            entity.ConstructionItemType = _ConstructionItemType;
//            entity.ConstructionItemCode = _ConstructionItemCode;
//            entity.ConstructionItemNo = _ConstructionItemNo;
//            if (!entity.Fill())
//            {
//                return null;
//            }
//            return entity;
//        }
//    }
//}
