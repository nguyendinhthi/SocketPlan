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
    [DbTable(Name="LightSerialUpdateList", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class LightSerialUpdateList : DataEntity<LightSerialUpdateList>
    {
        
        private string _ConstructionCode;
        
        private string _OldLightSerial;
        
        private string _NewLightSerial;
        
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
        
        [DbColumn(Name="OldLightSerial", TypeName="varchar", IsPrimaryKey=true, Length=50)]
        public virtual string OldLightSerial
        {
            get
            {
                return this._OldLightSerial;
            }
            set
            {
                this._OldLightSerial = value;
            }
        }
        
        [DbColumn(Name="NewLightSerial", TypeName="varchar", Length=50)]
        public virtual string NewLightSerial
        {
            get
            {
                return this._NewLightSerial;
            }
            set
            {
                this._NewLightSerial = value;
            }
        }
        
        public static LightSerialUpdateList Get(string _ConstructionCode, string _OldLightSerial)
        {
            LightSerialUpdateList entity = new LightSerialUpdateList();
            entity.ConstructionCode = _ConstructionCode;
            entity.OldLightSerial = _OldLightSerial;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}