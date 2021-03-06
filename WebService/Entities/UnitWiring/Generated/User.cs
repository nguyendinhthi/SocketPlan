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
    [DbTable(Name="Users", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class User : DataEntity<User>
    {
        
        private string _Id;
        
        private string _Password;
        
        private System.DateTime _UpdatedDateTime;
        
        private bool _CanMasterMaintenance;
        
        [DbColumn(Name="Id", TypeName="varchar", IsPrimaryKey=true, Length=50)]
        public virtual string Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }
        
        [DbColumn(Name="Password", TypeName="varchar", Length=50)]
        public virtual string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                this._Password = value;
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
        
        [DbColumn(Name="CanMasterMaintenance", TypeName="bit", Length=1)]
        public virtual bool CanMasterMaintenance
        {
            get
            {
                return this._CanMasterMaintenance;
            }
            set
            {
                this._CanMasterMaintenance = value;
            }
        }
        
        public static User Get(string _Id)
        {
            User entity = new User();
            entity.Id = _Id;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
