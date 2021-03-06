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
    [DbTable(Name="HouseTypeGroupStandardItems_Old", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class HouseTypeGroupStandardItems_Old : DataEntity<HouseTypeGroupStandardItems_Old>
    {
        
        private int _HouseTypeGroupId;
        
        private string _ItemName;
        
        // -1は図面からカウントする。
        private int _Quantity;
        
        private System.DateTime _UpdatedDateTime;
        
        [DbColumn(Name="HouseTypeGroupId", TypeName="int", IsPrimaryKey=true, Length=4)]
        public virtual int HouseTypeGroupId
        {
            get
            {
                return this._HouseTypeGroupId;
            }
            set
            {
                this._HouseTypeGroupId = value;
            }
        }
        
        [DbColumn(Name="ItemName", TypeName="varchar", IsPrimaryKey=true, Length=100)]
        public virtual string ItemName
        {
            get
            {
                return this._ItemName;
            }
            set
            {
                this._ItemName = value;
            }
        }
        
        [DbColumn(Name="Quantity", TypeName="int", Length=4, Remarks="-1は図面からカウントする。")]
        public virtual int Quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                this._Quantity = value;
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
        
        public static HouseTypeGroupStandardItems_Old Get(int _HouseTypeGroupId, string _ItemName)
        {
            HouseTypeGroupStandardItems_Old entity = new HouseTypeGroupStandardItems_Old();
            entity.HouseTypeGroupId = _HouseTypeGroupId;
            entity.ItemName = _ItemName;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
