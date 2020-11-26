//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:2.0.50727.3655
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
    [DbTable(Name="PartsColors", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class PartsColor : DataEntity<PartsColor>
    {
        
        // ID
        private int _Id;
        
        // 色名
        private string _Name;
        
        // プレフィックス
        private string _Prefix;
        
        // 更新日時
        private System.DateTime _UpdatedDateTime;
        
        [DbColumn(Name="Id", TypeName="int", IsPrimaryKey=true, Length=4, Remarks="ID")]
        public virtual int Id
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
        
        [DbColumn(Name="Name", TypeName="varchar", Length=50, Remarks="色名")]
        public virtual string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }
        
        [DbColumn(Name="Prefix", TypeName="varchar", Nullable=true, Length=2, Remarks="プレフィックス")]
        public virtual string Prefix
        {
            get
            {
                return this._Prefix;
            }
            set
            {
                this._Prefix = value;
            }
        }
        
        [DbColumn(Name="UpdatedDateTime", TypeName="datetime", DecimalPlace=3, Length=8, Remarks="更新日時")]
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
        
        public static PartsColor Get(int _Id)
        {
            PartsColor entity = new PartsColor();
            entity.Id = _Id;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
