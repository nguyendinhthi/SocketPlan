//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:2.0.50727.8800
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
    [DbTable(Name = "SocketPlanMaintenance_CommentLogs", DatabaseType = DatabaseType.SqlServer, ConnectionSettingKeyName = "SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class SocketPlanMaintenance_CommentLog : DataEntity<SocketPlanMaintenance_CommentLog>
    {
        
        private int _LogId;
        
        private int _EquipmentSeq;
        
        private int _Seq;
        
        private string _Comment;
        
        [DbColumn(Name="LogId", TypeName="int", IsPrimaryKey=true, Length=4)]
        public virtual int LogId
        {
            get
            {
                return this._LogId;
            }
            set
            {
                this._LogId = value;
            }
        }
        
        [DbColumn(Name="EquipmentSeq", TypeName="int", IsPrimaryKey=true, Length=4)]
        public virtual int EquipmentSeq
        {
            get
            {
                return this._EquipmentSeq;
            }
            set
            {
                this._EquipmentSeq = value;
            }
        }
        
        [DbColumn(Name="Seq", TypeName="int", IsPrimaryKey=true, Length=4)]
        public virtual int Seq
        {
            get
            {
                return this._Seq;
            }
            set
            {
                this._Seq = value;
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
        
        public static SocketPlanMaintenance_CommentLog Get(int _LogId, int _EquipmentSeq, int _Seq)
        {
            SocketPlanMaintenance_CommentLog entity = new SocketPlanMaintenance_CommentLog();
            entity.LogId = _LogId;
            entity.EquipmentSeq = _EquipmentSeq;
            entity.Seq = _Seq;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
