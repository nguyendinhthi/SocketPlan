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
    [DbTable(Name="HouseTypeGroupStandardItems_New", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class HouseTypeGroupStandardItems_New : DataEntity<HouseTypeGroupStandardItems_New>
    {
        
        private int _HouseTypeGroupId;
        
        private string _ItemName;
        
        private int _Quantity;
        
        private System.DateTime _UpdatedDateTime;
        
        [DbColumn(Name="HouseTypeGroupId", TypeName="int", IsPrimaryKey=true, Length=4)]
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
        
        [DbColumn(Name="ItemName", TypeName="varchar", IsPrimaryKey=true, Length=100)]
        public virtual string ItemName
        {
            get
            {
                return this._ItemName;
            }
            set
            {
                this._ItemName = value;
            }
        }
        
        [DbColumn(Name="Quantity", TypeName="int", Length=4)]
        public virtual int Quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                this._Quantity = value;
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
        
        public static HouseTypeGroupStandardItems_New Get(int _HouseTypeGroupId, string _ItemName)
        {
            HouseTypeGroupStandardItems_New entity = new HouseTypeGroupStandardItems_New();
            entity.HouseTypeGroupId = _HouseTypeGroupId;
            entity.ItemName = _ItemName;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}