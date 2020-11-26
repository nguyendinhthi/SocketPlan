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
    [DbTable(Name="Settings", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class Setting : DataEntity<Setting>
    {
        
        // システム変数ID
        private int _Id;
        
        // システム変数種類
        private int _SettingKindId;
        
        // 家タイプ
        private System.Nullable<int> _HouseTypeId;
        
        // 補足
        private string _Remarks;
        
        // 値
        private string _Value;
        
        // 更新日時
        private System.DateTime _UpdatedDateTime;
        
        [DbColumn(Name="Id", TypeName="int", IsPrimaryKey=true, Length=4, Remarks="システム変数ID")]
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
        
        [DbColumn(Name="SettingKindId", TypeName="int", Length=4, Remarks="システム変数種類")]
        public virtual int SettingKindId
        {
            get
            {
                return this._SettingKindId;
            }
            set
            {
                this._SettingKindId = value;
            }
        }
        
        [DbColumn(Name="HouseTypeId", TypeName="int", Nullable=true, Length=4, Remarks="家タイプ")]
        public virtual System.Nullable<int> HouseTypeId
        {
            get
            {
                return this._HouseTypeId;
            }
            set
            {
                this._HouseTypeId = value;
            }
        }
        
        [DbColumn(Name="Remarks", TypeName="varchar", Length=50, Remarks="補足")]
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
        
        [DbColumn(Name="Value", TypeName="varchar", Length=200, Remarks="値")]
        public virtual string Value
        {
            get
            {
                return this._Value;
            }
            set
            {
                this._Value = value;
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
        
        public static Setting Get(int _Id)
        {
            Setting entity = new Setting();
            entity.Id = _Id;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
