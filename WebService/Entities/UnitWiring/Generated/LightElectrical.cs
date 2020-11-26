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
    [DbTable(Name="LightElectricals", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class LightElectrical : DataEntity<LightElectrical>
    {
        
        private int _EquipmentId;
        
        private bool _IsTop;

        private string _NumberingFormat;

        private string _Category;
        
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
        
        [DbColumn(Name="IsTop", TypeName="bit", Length=1)]
        public virtual bool IsTop
        {
            get
            {
                return this._IsTop;
            }
            set
            {
                this._IsTop = value;
            }
        }

        [DbColumn(Name = "NumberingFormat", TypeName = "varchar", Length = 5)]
        public virtual string NumberingFormat
        {
            get
            {
                return this._NumberingFormat;
            }
            set
            {
                this._NumberingFormat = value;
            }
        }

        [DbColumn(Name = "Category", TypeName = "varchar", Length = 50)]
        public virtual string Category
        {
            get
            {
                return this._Category;
            }
            set
            {
                this._Category = value;
            }
        }
        
        public static LightElectrical Get(int _EquipmentId)
        {
            LightElectrical entity = new LightElectrical();
            entity.EquipmentId = _EquipmentId;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
