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
    [DbTable(Name="ShikakuTableEntries", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class ShikakuTableEntry : DataEntity<ShikakuTableEntry>
    {
        
        // お客様コード
        private string _ConstructionCode;
        
        // 項目Id
        private int _ItemId;
        
        // 値
        private string _Value;
        
        // 更新日時
        private System.DateTime _UpdatedDateTime;
        
        [DbColumn(Name="ConstructionCode", TypeName="varchar", IsPrimaryKey=true, Length=12, Remarks="お客様コード")]
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
        
        [DbColumn(Name="ItemId", TypeName="int", IsPrimaryKey=true, Length=4, Remarks="項目Id")]
        public virtual int ItemId
        {
            get
            {
                return this._ItemId;
            }
            set
            {
                this._ItemId = value;
            }
        }
        
        [DbColumn(Name="Value", TypeName="varchar", Length=50, Remarks="値")]
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
        
        public static ShikakuTableEntry Get(string _ConstructionCode, int _ItemId)
        {
            ShikakuTableEntry entity = new ShikakuTableEntry();
            entity.ConstructionCode = _ConstructionCode;
            entity.ItemId = _ItemId;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
