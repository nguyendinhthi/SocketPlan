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
    [DbTable(Name = "ExportLightSerialConstructions", DatabaseType = DatabaseType.SqlServer, ConnectionSettingKeyName = "SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class ExportLightSerialConstruction : DataEntity<ExportLightSerialConstruction>
    {
        
        private string _ConstructionCiode;
        
        [DbColumn(Name="ConstructionCode", TypeName="varchar", IsPrimaryKey=true, Length=12)]
        public virtual string ConstructionCiode
        {
            get
            {
                return this._ConstructionCiode;
            }
            set
            {
                this._ConstructionCiode = value;
            }
        }

        public static ExportLightSerialConstruction Get(string _ConstructionCiode)
        {
            ExportLightSerialConstruction entity = new ExportLightSerialConstruction();
            entity.ConstructionCiode = _ConstructionCiode;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
