//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:2.0.50727.5485
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
    [DbTable(Name = "ConvertionLightSerials", DatabaseType = DatabaseType.SqlServer, ConnectionSettingKeyName = "SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class ConvertionLightSerial : DataEntity<ConvertionLightSerial>
    {
        
        private string _ConstructionCode;
        
        private string _PlanNo;
        
        private string _TargetLightSerial;
        
        private string _UpdateLightSerial;
        
        private bool _ConfirmedFlag;
        
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
        
        [DbColumn(Name="PlanNo", TypeName="varchar", Nullable=true, Length=50)]
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
        
        [DbColumn(Name="TargetLightSerial", TypeName="varchar", IsPrimaryKey=true, Length=50)]
        public virtual string TargetLightSerial
        {
            get
            {
                return this._TargetLightSerial;
            }
            set
            {
                this._TargetLightSerial = value;
            }
        }
        
        [DbColumn(Name="UpdateLightSerial", TypeName="varchar", Length=50)]
        public virtual string UpdateLightSerial
        {
            get
            {
                return this._UpdateLightSerial;
            }
            set
            {
                this._UpdateLightSerial = value;
            }
        }
        
        [DbColumn(Name="ConfirmedFlag", TypeName="datetime", DecimalPlace=3, Length=8)]
        public virtual bool ConfirmedFlag
        {
            get
            {
                return this._ConfirmedFlag;
            }
            set
            {
                this._ConfirmedFlag = value;
            }
        }
        
        public static ConvertionLightSerial Get(string _ConstructionCode, string _TargetLightSerial)
        {
            ConvertionLightSerial entity = new ConvertionLightSerial();
            entity.ConstructionCode = _ConstructionCode;
            entity.TargetLightSerial = _TargetLightSerial;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
