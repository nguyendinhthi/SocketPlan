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
    [DbTable(Name="FloorTexts", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class FloorText : DataEntity<FloorText>
    {
        
        private string _Text;
        
        private int _SpecificationId;
        
        private string _Parameter;
        
        private System.DateTime _UpdatedDateTime;
        
        [DbColumn(Name="Text", TypeName="varchar", IsPrimaryKey=true, Length=100)]
        public virtual string Text
        {
            get
            {
                return this._Text;
            }
            set
            {
                this._Text = value;
            }
        }
        
        [DbColumn(Name="SpecificationId", TypeName="int", IsPrimaryKey=true, Length=4)]
        public virtual int SpecificationId
        {
            get
            {
                return this._SpecificationId;
            }
            set
            {
                this._SpecificationId = value;
            }
        }
        
        [DbColumn(Name="Parameter", TypeName="varchar", Nullable=true, Length=200)]
        public virtual string Parameter
        {
            get
            {
                return this._Parameter;
            }
            set
            {
                this._Parameter = value;
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
        
        public static FloorText Get(string _Text, int _SpecificationId)
        {
            FloorText entity = new FloorText();
            entity.Text = _Text;
            entity.SpecificationId = _SpecificationId;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}