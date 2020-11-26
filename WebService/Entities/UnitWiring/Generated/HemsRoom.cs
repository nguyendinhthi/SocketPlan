//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:2.0.50727.3649
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
    [DbTable(Name="HemsRooms", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class HemsRoom : DataEntity<HemsRoom>
    {
        
        private string _ConstructionCode;
        
        private string _Id;
        
        private int _FamilyNo;
        
        private string _Name;
        
        private string _Location;
        
        private System.DateTime _UpdatedDateTime;
        
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
        
        [DbColumn(Name="Id", TypeName="varchar", IsPrimaryKey=true, Length=50)]
        public virtual string Id
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
        
        [DbColumn(Name="FamilyNo", TypeName="int", Length=4)]
        public virtual int FamilyNo
        {
            get
            {
                return this._FamilyNo;
            }
            set
            {
                this._FamilyNo = value;
            }
        }
        
        [DbColumn(Name="Name", TypeName="varchar", Nullable=true, Length=50)]
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
        
        [DbColumn(Name="Location", TypeName="varchar", Nullable=true, Length=50)]
        public virtual string Location
        {
            get
            {
                return this._Location;
            }
            set
            {
                this._Location = value;
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
        
        public static HemsRoom Get(string _ConstructionCode, string _Id)
        {
            HemsRoom entity = new HemsRoom();
            entity.ConstructionCode = _ConstructionCode;
            entity.Id = _Id;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
