//------------------------------------------------------------------------------
// <auto-generated>
//     ���̃R�[�h�̓c�[���ɂ���Đ�������܂����B
//     �����^�C�� �o�[�W����:2.0.50727.5485
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
    [DbTable(Name="JboxColorEntries", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class JboxColorEntry : DataEntity<JboxColorEntry>
    {
        
        private string _ConstructionCode;
        
        private int _Seq;
        
        private int _Floor;
        
        private string _JboxEquipmentName;
        
        private string _RoomName;
        
        private decimal _WireLength;
        
        private decimal _HacchuLength;
        
        private decimal _Height;
        
        private int _JboxColorId;
        
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
        
        [DbColumn(Name="Floor", TypeName="int", Length=4)]
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
        
        [DbColumn(Name="JboxEquipmentName", TypeName="varchar", Length=50)]
        public virtual string JboxEquipmentName
        {
            get
            {
                return this._JboxEquipmentName;
            }
            set
            {
                this._JboxEquipmentName = value;
            }
        }
        
        [DbColumn(Name="RoomName", TypeName="varchar", Length=50)]
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
        
        [DbColumn(Name="WireLength", TypeName="decimal", DecimalPlace=2, Length=9)]
        public virtual decimal WireLength
        {
            get
            {
                return this._WireLength;
            }
            set
            {
                this._WireLength = value;
            }
        }
        
        [DbColumn(Name="HacchuLength", TypeName="decimal", DecimalPlace=2, Length=9)]
        public virtual decimal HacchuLength
        {
            get
            {
                return this._HacchuLength;
            }
            set
            {
                this._HacchuLength = value;
            }
        }
        
        [DbColumn(Name="Height", TypeName="decimal", Length=9)]
        public virtual decimal Height
        {
            get
            {
                return this._Height;
            }
            set
            {
                this._Height = value;
            }
        }
        
        [DbColumn(Name="JboxColorId", TypeName="int", Length=4)]
        public virtual int JboxColorId
        {
            get
            {
                return this._JboxColorId;
            }
            set
            {
                this._JboxColorId = value;
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
        
        public static JboxColorEntry Get(string _ConstructionCode, int _Seq)
        {
            JboxColorEntry entity = new JboxColorEntry();
            entity.ConstructionCode = _ConstructionCode;
            entity.Seq = _Seq;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}