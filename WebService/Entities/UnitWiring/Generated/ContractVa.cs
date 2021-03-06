//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:2.0.50727.3643
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
    [DbTable(Name="ContractVas", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class ContractVa : DataEntity<ContractVa>
    {
        
        private int _TotalVaLower;
        
        private int _TotalVaUpper;
        
        private string _ContractBaseVa;
        
        private System.DateTime _UpdatedDateTime;
        
        [DbColumn(Name="TotalVaLower", TypeName="int", IsPrimaryKey=true, Length=4)]
        public virtual int TotalVaLower
        {
            get
            {
                return this._TotalVaLower;
            }
            set
            {
                this._TotalVaLower = value;
            }
        }
        
        [DbColumn(Name="TotalVaUpper", TypeName="int", Length=4)]
        public virtual int TotalVaUpper
        {
            get
            {
                return this._TotalVaUpper;
            }
            set
            {
                this._TotalVaUpper = value;
            }
        }
        
        [DbColumn(Name="ContractBaseVa", TypeName="varchar", Length=50)]
        public virtual string ContractBaseVa
        {
            get
            {
                return this._ContractBaseVa;
            }
            set
            {
                this._ContractBaseVa = value;
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
        
        public static ContractVa Get(int _TotalVaLower)
        {
            ContractVa entity = new ContractVa();
            entity.TotalVaLower = _TotalVaLower;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
