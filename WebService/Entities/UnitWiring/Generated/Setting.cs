//------------------------------------------------------------------------------
// <auto-generated>
//     ���̃R�[�h�̓c�[���ɂ���Đ�������܂����B
//     �����^�C�� �o�[�W����:2.0.50727.3643
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
    [DbTable(Name="Settings", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class Setting : DataEntity<Setting>
    {
        
        // �V�X�e���ϐ�ID
        private int _Id;
        
        // �V�X�e���ϐ����
        private int _SettingKindId;
        
        // �ƃ^�C�v
        private System.Nullable<int> _HouseTypeId;
        
        // �⑫
        private string _Remarks;
        
        // �l
        private string _Value;
        
        // �X�V����
        private System.DateTime _UpdatedDateTime;
        
        [DbColumn(Name="Id", TypeName="int", IsPrimaryKey=true, Length=4, Remarks="�V�X�e���ϐ�ID")]
        public virtual int Id
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
        
        [DbColumn(Name="SettingKindId", TypeName="int", Length=4, Remarks="�V�X�e���ϐ����")]
        public virtual int SettingKindId
        {
            get
            {
                return this._SettingKindId;
            }
            set
            {
                this._SettingKindId = value;
            }
        }
        
        [DbColumn(Name="HouseTypeId", TypeName="int", Nullable=true, Length=4, Remarks="�ƃ^�C�v")]
        public virtual System.Nullable<int> HouseTypeId
        {
            get
            {
                return this._HouseTypeId;
            }
            set
            {
                this._HouseTypeId = value;
            }
        }
        
        [DbColumn(Name="Remarks", TypeName="varchar", Length=50, Remarks="�⑫")]
        public virtual string Remarks
        {
            get
            {
                return this._Remarks;
            }
            set
            {
                this._Remarks = value;
            }
        }
        
        [DbColumn(Name="Value", TypeName="varchar", Length=200, Remarks="�l")]
        public virtual string Value
        {
            get
            {
                return this._Value;
            }
            set
            {
                this._Value = value;
            }
        }
        
        [DbColumn(Name="UpdatedDateTime", TypeName="datetime", DecimalPlace=3, Length=8, Remarks="�X�V����")]
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
        
        public static Setting Get(int _Id)
        {
            Setting entity = new Setting();
            entity.Id = _Id;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}