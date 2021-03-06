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
    [DbTable(Name = "RoomLayoutsForKibiroi", DatabaseType = DatabaseType.SqlServer, ConnectionSettingKeyName = "SocketPlan.WebService.Properties.Settings.ConnectionStringOfHRDInformationService")]
    public partial class RoomLayoutsForKibiroi : DataEntity<RoomLayoutsForKibiroi>
    {
        
        private string _ConstructionCode;
        
        // 1：１階 2：２階 3：３階 9：その他
        private int _Floor;
        
        private string _RoomCode;
        
        private string _RoomNameJapanese;
        
        private string _RoomNameEnglish;
        
        private string _ShortNameJapanese;
        
        private string _ShortNameEnglish;
        
        private System.Nullable<System.DateTime> _UpdatedDate;
        
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
        
        [DbColumn(Name="Floor", TypeName="int", IsPrimaryKey=true, Length=4, Remarks="1：１階 2：２階 3：３階 9：その他")]
        public virtual int Floor
        {
            get
            {
                return this._Floor;
            }
            set
            {
                this._Floor = value;
            }
        }
        
        [DbColumn(Name="RoomCode", TypeName="varchar", IsPrimaryKey=true, Length=4)]
        public virtual string RoomCode
        {
            get
            {
                return this._RoomCode;
            }
            set
            {
                this._RoomCode = value;
            }
        }
        
        [DbColumn(Name="RoomNameJapanese", TypeName="varchar", Nullable=true, Length=30)]
        public virtual string RoomNameJapanese
        {
            get
            {
                return this._RoomNameJapanese;
            }
            set
            {
                this._RoomNameJapanese = value;
            }
        }
        
        [DbColumn(Name="RoomNameEnglish", TypeName="varchar", Nullable=true, Length=30)]
        public virtual string RoomNameEnglish
        {
            get
            {
                return this._RoomNameEnglish;
            }
            set
            {
                this._RoomNameEnglish = value;
            }
        }
        
        [DbColumn(Name="ShortNameJapanese", TypeName="varchar", Nullable=true, Length=16)]
        public virtual string ShortNameJapanese
        {
            get
            {
                return this._ShortNameJapanese;
            }
            set
            {
                this._ShortNameJapanese = value;
            }
        }
        
        [DbColumn(Name="ShortNameEnglish", TypeName="varchar", Nullable=true, Length=16)]
        public virtual string ShortNameEnglish
        {
            get
            {
                return this._ShortNameEnglish;
            }
            set
            {
                this._ShortNameEnglish = value;
            }
        }
        
        [DbColumn(Name="UpdatedDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> UpdatedDate
        {
            get
            {
                return this._UpdatedDate;
            }
            set
            {
                this._UpdatedDate = value;
            }
        }
        
        public static RoomLayoutsForKibiroi Get(string _ConstructionCode, int _Floor, string _RoomCode)
        {
            RoomLayoutsForKibiroi entity = new RoomLayoutsForKibiroi();
            entity.ConstructionCode = _ConstructionCode;
            entity.Floor = _Floor;
            entity.RoomCode = _RoomCode;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
