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
    [DbTable(Name="InteriorEstimateRooms", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class InteriorEstimateRoom : DataEntity<InteriorEstimateRoom>
    {
        
        // 部屋コード（このテーブルは、インテリア見積もりシステムが持っている部屋マスタと同じものです）
        private string _RoomCode;
        
        // 部屋名
        private string _RoomName;
        
        // 部屋別名（図面上の部屋名との比較はこっちで行う）セミコロンで区切って複数入力可能
        private string _AliasName;
        
        // 更新日時
        private System.DateTime _UpdatedDateTime;
        
        [DbColumn(Name="RoomCode", TypeName="varchar", IsPrimaryKey=true, Length=4, Remarks="部屋コード（このテーブルは、インテリア見積もりシステムが持っている部屋マスタと同じものです）")]
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
        
        [DbColumn(Name="RoomName", TypeName="varchar", Length=50, Remarks="部屋名")]
        public virtual string RoomName
        {
            get
            {
                return this._RoomName;
            }
            set
            {
                this._RoomName = value;
            }
        }
        
        [DbColumn(Name="AliasName", TypeName="varchar", Nullable=true, Length=200, Remarks="部屋別名（図面上の部屋名との比較はこっちで行う）セミコロンで区切って複数入力可能")]
        public virtual string AliasName
        {
            get
            {
                return this._AliasName;
            }
            set
            {
                this._AliasName = value;
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
        
        public static InteriorEstimateRoom Get(string _RoomCode)
        {
            InteriorEstimateRoom entity = new InteriorEstimateRoom();
            entity.RoomCode = _RoomCode;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
