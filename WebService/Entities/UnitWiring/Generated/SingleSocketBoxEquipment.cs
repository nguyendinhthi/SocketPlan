//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:2.0.50727.8762
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
    [DbTable(Name="SingleSocketBoxEquipments", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class SingleSocketBoxEquipment : DataEntity<SingleSocketBoxEquipment>
    {
        
        private int _EquipmentId;
        
        [DbColumn(Name="EquipmentId", TypeName="int", IsPrimaryKey=true, Length=4)]
        public virtual int EquipmentId
        {
            get
            {
                return this._EquipmentId;
            }
            set
            {
                this._EquipmentId = value;
            }
        }
        
        public static SingleSocketBoxEquipment Get(int _EquipmentId)
        {
            SingleSocketBoxEquipment entity = new SingleSocketBoxEquipment();
            entity.EquipmentId = _EquipmentId;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
