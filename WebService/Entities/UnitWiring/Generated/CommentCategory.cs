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
    [DbTable(Name="CommentCategories", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class CommentCategory : DataEntity<CommentCategory>
    {
        
        private int _Id;
        
        private string _Name;
        
        private int _SortNo;
        
        private System.DateTime _UpdatedDateTime;
        
        [DbColumn(Name="Id", TypeName="int", IsPrimaryKey=true, Length=4)]
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
        
        [DbColumn(Name="Name", TypeName="varchar", Length=100)]
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
        
        [DbColumn(Name="SortNo", TypeName="int", Length=4)]
        public virtual int SortNo
        {
            get
            {
                return this._SortNo;
            }
            set
            {
                this._SortNo = value;
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
        
        public static CommentCategory Get(int _Id)
        {
            CommentCategory entity = new CommentCategory();
            entity.Id = _Id;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
