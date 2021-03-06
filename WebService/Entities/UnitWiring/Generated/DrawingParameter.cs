//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:2.0.50727.5477
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
    [DbTable(Name="DrawingParameter", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class DrawingParameter : DataEntity<DrawingParameter>
    {
        
        private string _ConstructionCode;
        
        private string _PlanNo;
        
        private string _RevNo;
        
        private string _KeyString;
        
        private int _KeySeq;
        
        private string _Value;
        
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
        
        [DbColumn(Name="PlanNo", TypeName="varchar", IsPrimaryKey=true, Length=10)]
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
        
        [DbColumn(Name="RevNo", TypeName="varchar", IsPrimaryKey=true, Length=10)]
        public virtual string RevNo
        {
            get
            {
                return this._RevNo;
            }
            set
            {
                this._RevNo = value;
            }
        }
        
        [DbColumn(Name="KeyString", TypeName="varchar", IsPrimaryKey=true, Length=50)]
        public virtual string KeyString
        {
            get
            {
                return this._KeyString;
            }
            set
            {
                this._KeyString = value;
            }
        }
        
        [DbColumn(Name="KeySeq", TypeName="int", IsPrimaryKey=true, Length=4)]
        public virtual int KeySeq
        {
            get
            {
                return this._KeySeq;
            }
            set
            {
                this._KeySeq = value;
            }
        }
        
        [DbColumn(Name="Value", TypeName="varchar", Nullable=true, Length=200)]
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
        
        public static DrawingParameter Get(string _ConstructionCode, string _PlanNo, string _RevNo, string _KeyString, int _KeySeq)
        {
            DrawingParameter entity = new DrawingParameter();
            entity.ConstructionCode = _ConstructionCode;
            entity.PlanNo = _PlanNo;
            entity.RevNo = _RevNo;
            entity.KeyString = _KeyString;
            entity.KeySeq = _KeySeq;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
