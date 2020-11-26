//------------------------------------------------------------------------------
// <auto-generated>
//     ���̃R�[�h�̓c�[���ɂ���Đ�������܂����B
//     �����^�C�� �o�[�W����:2.0.50727.3649
//
//     ���̃t�@�C���ւ̕ύX�́A�ȉ��̏󋵉��ŕs���ȓ���̌����ɂȂ�����A
//     �R�[�h���Đ��������Ƃ��ɑ��������肵�܂��B
// </auto-generated>
//------------------------------------------------------------------------------

namespace SocketPlan.WebService
{
    using System;
    using System.Collections.Generic;
    using Edsa.Data;
    using Edsa.Data.Attributes;
    
    
    [Serializable()]
    [DbTable(Name="HemsRoomBlocks", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class HemsRoomBlock : DataEntity<HemsRoomBlock>
    {
        
        private string _ConstructionCode;
        
        private int _DeviceId;
        
        private int _BlockId;
        
        private string _RoomId;
        
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
        
        [DbColumn(Name="DeviceId", TypeName="int", IsPrimaryKey=true, Length=4)]
        public virtual int DeviceId
        {
            get
            {
                return this._DeviceId;
            }
            set
            {
                this._DeviceId = value;
            }
        }
        
        [DbColumn(Name="BlockId", TypeName="int", IsPrimaryKey=true, Length=4)]
        public virtual int BlockId
        {
            get
            {
                return this._BlockId;
            }
            set
            {
                this._BlockId = value;
            }
        }
        
        [DbColumn(Name="RoomId", TypeName="varchar", IsPrimaryKey=true, Length=50)]
        public virtual string RoomId
        {
            get
            {
                return this._RoomId;
            }
            set
            {
                this._RoomId = value;
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
        
        public static HemsRoomBlock Get(string _ConstructionCode, int _DeviceId, int _BlockId, string _RoomId)
        {
            HemsRoomBlock entity = new HemsRoomBlock();
            entity.ConstructionCode = _ConstructionCode;
            entity.DeviceId = _DeviceId;
            entity.BlockId = _BlockId;
            entity.RoomId = _RoomId;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}