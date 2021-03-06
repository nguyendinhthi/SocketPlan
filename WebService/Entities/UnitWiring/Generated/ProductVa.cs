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
    [DbTable(Name="ProductVas", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class ProductVa : DataEntity<ProductVa>
    {
        
        private string _Class1Code;
        
        private string _Class1Name;
        
        private string _Class2Code;
        
        private string _Class2Name;
        
        private string _ProductCode;
        
        private string _ProductName;
        
        private int _SeqNo;
        
        private string _Comment;
        
        private string _BreakerName;
        
        private int _Va;
        
        private bool _Is200V;
        
        private System.DateTime _UpdatedDateTime;
        
        [DbColumn(Name="Class1Code", TypeName="varchar", IsPrimaryKey=true, Length=4)]
        public virtual string Class1Code
        {
            get
            {
                return this._Class1Code;
            }
            set
            {
                this._Class1Code = value;
            }
        }
        
        [DbColumn(Name="Class1Name", TypeName="varchar", Nullable=true, Length=50)]
        public virtual string Class1Name
        {
            get
            {
                return this._Class1Name;
            }
            set
            {
                this._Class1Name = value;
            }
        }
        
        [DbColumn(Name="Class2Code", TypeName="varchar", IsPrimaryKey=true, Length=4)]
        public virtual string Class2Code
        {
            get
            {
                return this._Class2Code;
            }
            set
            {
                this._Class2Code = value;
            }
        }
        
        [DbColumn(Name="Class2Name", TypeName="varchar", Nullable=true, Length=50)]
        public virtual string Class2Name
        {
            get
            {
                return this._Class2Name;
            }
            set
            {
                this._Class2Name = value;
            }
        }
        
        [DbColumn(Name="ProductCode", TypeName="varchar", IsPrimaryKey=true, Length=7)]
        public virtual string ProductCode
        {
            get
            {
                return this._ProductCode;
            }
            set
            {
                this._ProductCode = value;
            }
        }
        
        [DbColumn(Name="ProductName", TypeName="varchar", Nullable=true, Length=200)]
        public virtual string ProductName
        {
            get
            {
                return this._ProductName;
            }
            set
            {
                this._ProductName = value;
            }
        }
        
        [DbColumn(Name="SeqNo", TypeName="int", IsPrimaryKey=true, Length=4)]
        public virtual int SeqNo
        {
            get
            {
                return this._SeqNo;
            }
            set
            {
                this._SeqNo = value;
            }
        }
        
        [DbColumn(Name="Comment", TypeName="varchar", Nullable=true, Length=200)]
        public virtual string Comment
        {
            get
            {
                return this._Comment;
            }
            set
            {
                this._Comment = value;
            }
        }
        
        [DbColumn(Name="BreakerName", TypeName="varchar", Nullable=true, Length=100)]
        public virtual string BreakerName
        {
            get
            {
                return this._BreakerName;
            }
            set
            {
                this._BreakerName = value;
            }
        }
        
        [DbColumn(Name="Va", TypeName="int", Length=4)]
        public virtual int Va
        {
            get
            {
                return this._Va;
            }
            set
            {
                this._Va = value;
            }
        }
        
        [DbColumn(Name="Is200V", TypeName="bit", Length=1)]
        public virtual bool Is200V
        {
            get
            {
                return this._Is200V;
            }
            set
            {
                this._Is200V = value;
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
        
        public static ProductVa Get(string _Class1Code, string _Class2Code, string _ProductCode, int _SeqNo)
        {
            ProductVa entity = new ProductVa();
            entity.Class1Code = _Class1Code;
            entity.Class2Code = _Class2Code;
            entity.ProductCode = _ProductCode;
            entity.SeqNo = _SeqNo;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
