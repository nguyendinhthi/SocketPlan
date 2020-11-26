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
    [DbTable(Name="HouseTypeGroupDetails", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class HouseTypeGroupDetail : DataEntity<HouseTypeGroupDetail>
    {
        
        private string _ConstructionTypeCode;
        
        private int _HouseTypeGroupId;
        
        private System.DateTime _UpdatedDateTime;
        
        [DbColumn(Name="ConstructionTypeCode", TypeName="varchar", IsPrimaryKey=true, Length=3)]
        public virtual string ConstructionTypeCode
        {
            get
            {
                return this._ConstructionTypeCode;
            }
            set
            {
                this._ConstructionTypeCode = value;
            }
        }
        
        [DbColumn(Name="HouseTypeGroupId", TypeName="int", Length=4)]
        public virtual int HouseTypeGroupId
        {
            get
            {
                return this._HouseTypeGroupId;
            }
            set
            {
                this._HouseTypeGroupId = value;
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
        
        public static HouseTypeGroupDetail Get(string _ConstructionTypeCode)
        {
            HouseTypeGroupDetail entity = new HouseTypeGroupDetail();
            entity.ConstructionTypeCode = _ConstructionTypeCode;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}