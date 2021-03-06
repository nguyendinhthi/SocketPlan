//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:2.0.50727.3634
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SocketPlan.WebService
{
    using System;
    using System.Collections.Generic;
    using Edsa.Data;
    using Edsa.Data.Attributes;
    
    
    [Serializable()]
    [DbTable(Name = "EmployeeLisences", DatabaseType = DatabaseType.SqlServer, ConnectionSettingKeyName = "SocketPlan.WebService.Properties.Settings.ConnectionStringOfHRDInformationService")]
    public partial class EmployeeLisence : DataEntity<EmployeeLisence>
    {
        
        private string _EmployeeCode;
        
        private string _LisenceCode;
        
        private System.Nullable<System.DateTime> _AcquiredDate;
        
        private System.Nullable<System.DateTime> _TermOfValidity;
        
        private System.Nullable<System.DateTime> _InputtedDate;
        
        private System.Nullable<System.DateTime> _PassedDate;
        
        private string _PrefectureCode;
        
        private string _RegistrationNumber;
        
        private System.Nullable<System.DateTime> _RegisteredDate;
        
        private string _Remarks;
        
        private System.Nullable<System.DateTime> _UpdatedDate;
        
        [DbColumn(Name="EmployeeCode", TypeName="varchar", IsPrimaryKey=true, Length=5)]
        public virtual string EmployeeCode
        {
            get
            {
                return this._EmployeeCode;
            }
            set
            {
                this._EmployeeCode = value;
            }
        }
        
        [DbColumn(Name="LisenceCode", TypeName="varchar", IsPrimaryKey=true, Length=3)]
        public virtual string LisenceCode
        {
            get
            {
                return this._LisenceCode;
            }
            set
            {
                this._LisenceCode = value;
            }
        }
        
        [DbColumn(Name="AcquiredDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> AcquiredDate
        {
            get
            {
                return this._AcquiredDate;
            }
            set
            {
                this._AcquiredDate = value;
            }
        }
        
        [DbColumn(Name="TermOfValidity", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> TermOfValidity
        {
            get
            {
                return this._TermOfValidity;
            }
            set
            {
                this._TermOfValidity = value;
            }
        }
        
        [DbColumn(Name="InputtedDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> InputtedDate
        {
            get
            {
                return this._InputtedDate;
            }
            set
            {
                this._InputtedDate = value;
            }
        }
        
        [DbColumn(Name="PassedDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> PassedDate
        {
            get
            {
                return this._PassedDate;
            }
            set
            {
                this._PassedDate = value;
            }
        }
        
        [DbColumn(Name="PrefectureCode", TypeName="varchar", Nullable=true, Length=2)]
        public virtual string PrefectureCode
        {
            get
            {
                return this._PrefectureCode;
            }
            set
            {
                this._PrefectureCode = value;
            }
        }
        
        [DbColumn(Name="RegistrationNumber", TypeName="varchar", Nullable=true, Length=20)]
        public virtual string RegistrationNumber
        {
            get
            {
                return this._RegistrationNumber;
            }
            set
            {
                this._RegistrationNumber = value;
            }
        }
        
        [DbColumn(Name="RegisteredDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> RegisteredDate
        {
            get
            {
                return this._RegisteredDate;
            }
            set
            {
                this._RegisteredDate = value;
            }
        }
        
        [DbColumn(Name="Remarks", TypeName="varchar", Nullable=true, Length=100)]
        public virtual string Remarks
        {
            get
            {
                return this._Remarks;
            }
            set
            {
                this._Remarks = value;
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
        
        public static EmployeeLisence Get(string _EmployeeCode, string _LisenceCode)
        {
            EmployeeLisence entity = new EmployeeLisence();
            entity.EmployeeCode = _EmployeeCode;
            entity.LisenceCode = _LisenceCode;
            if (!entity.Exists())
            {
                return null;
            }
            entity.Fill();
            return entity;
        }
    }
}
