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
    [DbTable(Name="CustomActions", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class CustomAction : DataEntity<CustomAction>
    {
        
        private int _Id;
        
        private string _Name;
        
        private System.DateTime _UpdatedDate;
        
        [DbColumn(Name="Id", TypeName="int", IsPrimaryKey=true, Length=4)]
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
        
        [DbColumn(Name="Name", TypeName="varchar", Length=50)]
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
        
        [DbColumn(Name="UpdatedDate", TypeName="datetime", DecimalPlace=3, Length=8)]
        public virtual System.DateTime UpdatedDate
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
        
        public static CustomAction Get(int _Id)
        {
            CustomAction entity = new CustomAction();
            entity.Id = _Id;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}