//------------------------------------------------------------------------------
// <auto-generated>
//     ���̃R�[�h�̓c�[���ɂ���Đ�������܂����B
//     �����^�C�� �o�[�W����:2.0.50727.8762
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
    [DbTable(Name="SocketBoxDetailSpecifics", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class SocketBoxDetailSpecific : DataEntity<SocketBoxDetailSpecific>
    {
        
        private string _ConstructionCode;
        
        private string _PlanNo;
        
        private string _ZumenNo;
        
        private int _BoxSeq;
        
        private int _Seq;
        
        private int _SpecificId;
        
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
        
        [DbColumn(Name="PlanNo", TypeName="varchar", IsPrimaryKey=true, Length=10)]
        public virtual string PlanNo
        {
            get
            {
                return this._PlanNo;
            }
            set
            {
                this._PlanNo = value;
            }
        }
        
        [DbColumn(Name="ZumenNo", TypeName="varchar", IsPrimaryKey=true, Length=10)]
        public virtual string ZumenNo
        {
            get
            {
                return this._ZumenNo;
            }
            set
            {
                this._ZumenNo = value;
            }
        }
        
        [DbColumn(Name="BoxSeq", TypeName="int", IsPrimaryKey=true, Length=4)]
        public virtual int BoxSeq
        {
            get
            {
                return this._BoxSeq;
            }
            set
            {
                this._BoxSeq = value;
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
        
        [DbColumn(Name="SpecificId", TypeName="int", Length=4)]
        public virtual int SpecificId
        {
            get
            {
                return this._SpecificId;
            }
            set
            {
                this._SpecificId = value;
            }
        }
        
        public static SocketBoxDetailSpecific Get(string _ConstructionCode, string _PlanNo, string _ZumenNo, int _BoxSeq, int _Seq)
        {
            SocketBoxDetailSpecific entity = new SocketBoxDetailSpecific();
            entity.ConstructionCode = _ConstructionCode;
            entity.PlanNo = _PlanNo;
            entity.ZumenNo = _ZumenNo;
            entity.BoxSeq = _BoxSeq;
            entity.Seq = _Seq;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}