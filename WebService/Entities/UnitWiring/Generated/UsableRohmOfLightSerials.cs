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
    [DbTable(Name = "UsableRohmOfLightSerials", DatabaseType = DatabaseType.SqlServer, ConnectionSettingKeyName = "SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class UsableRohmOfLightSerial : DataEntity<UsableRohmOfLightSerial>
    {
        
        private string _ConstructionCode;
        
        private string _LightSerial;
        
        private int _UsableCount;
        
        private System.DateTime _UpdatedDateTime;
        
        [DbColumn(Name="ConstructionCode", TypeName="varchar", Length=12)]
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
        
        [DbColumn(Name="LightSerial", TypeName="varchar", Length=12)]
        public virtual string LightSerial
        {
            get
            {
                return this._LightSerial;
            }
            set
            {
                this._LightSerial = value;
            }
        }
        
        [DbColumn(Name="UsableCount", TypeName="int", Length=4)]
        public virtual int UsableCount
        {
            get
            {
                return this._UsableCount;
            }
            set
            {
                this._UsableCount = value;
            }
        }
        
        [DbColumn(Name="UpdatedDateTime", TypeName="datetime", DecimalPlace=3, Length=8)]
        public virtual System.DateTime UpdatedDateTime
        {
            get
            {
                return this._UpdatedDateTime;
            }
            set
            {
                this._UpdatedDateTime = value;
            }
        }
    }
}
