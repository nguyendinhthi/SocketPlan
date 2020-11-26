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
    [DbTable(Name="tbl_siyo_boss", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionStringOfSiyoDataBroker")]
    public partial class tbl_siyo_boss : DataEntity<tbl_siyo_boss>
    {
        
        private string _customerCode;
        
        private int _siyoCode;
        
        private string _siyoNo;
        
        private string _zumenCode;
        
        private string _fpFlg;
        
        private string _setubiChangeOk;
        
        private string _naibusiyoChangeOk;
        
        private string _equipChangeOk;
        
        private string _ritumen2Exist;
        
        private System.DateTime _kousinDay;
        
        private string _interiorFlg;
        
        private string _appExeFlg;
        
        private string _siyoCheckFlg;
        
        private System.Nullable<System.DateTime> _houkokuDate;
        
        private string _bikou;
        
        private string _checkTantou;
        
        private bool _sentFlg;
        
        [DbColumn(Name="customerCode", TypeName="char", IsPrimaryKey=true, Length=12)]
        public virtual string customerCode
        {
            get
            {
                return this._customerCode;
            }
            set
            {
                this._customerCode = value;
            }
        }
        
        [DbColumn(Name="siyoCode", TypeName="int", IsPrimaryKey=true, Length=4)]
        public virtual int siyoCode
        {
            get
            {
                return this._siyoCode;
            }
            set
            {
                this._siyoCode = value;
            }
        }
        
        [DbColumn(Name="siyoNo", TypeName="varchar", Nullable=true, Length=10)]
        public virtual string siyoNo
        {
            get
            {
                return this._siyoNo;
            }
            set
            {
                this._siyoNo = value;
            }
        }
        
        [DbColumn(Name="zumenCode", TypeName="varchar", Length=10)]
        public virtual string zumenCode
        {
            get
            {
                return this._zumenCode;
            }
            set
            {
                this._zumenCode = value;
            }
        }
        
        [DbColumn(Name="fpFlg", TypeName="char", Nullable=true, Length=1)]
        public virtual string fpFlg
        {
            get
            {
                return this._fpFlg;
            }
            set
            {
                this._fpFlg = value;
            }
        }
        
        [DbColumn(Name="setubiChangeOk", TypeName="char", Nullable=true, Length=1)]
        public virtual string setubiChangeOk
        {
            get
            {
                return this._setubiChangeOk;
            }
            set
            {
                this._setubiChangeOk = value;
            }
        }
        
        [DbColumn(Name="naibusiyoChangeOk", TypeName="char", Nullable=true, Length=1)]
        public virtual string naibusiyoChangeOk
        {
            get
            {
                return this._naibusiyoChangeOk;
            }
            set
            {
                this._naibusiyoChangeOk = value;
            }
        }
        
        [DbColumn(Name="equipChangeOk", TypeName="char", Nullable=true, Length=1)]
        public virtual string equipChangeOk
        {
            get
            {
                return this._equipChangeOk;
            }
            set
            {
                this._equipChangeOk = value;
            }
        }
        
        [DbColumn(Name="ritumen2Exist", TypeName="char", Nullable=true, Length=1)]
        public virtual string ritumen2Exist
        {
            get
            {
                return this._ritumen2Exist;
            }
            set
            {
                this._ritumen2Exist = value;
            }
        }
        
        [DbColumn(Name="kousinDay", TypeName="datetime", DecimalPlace=3, Length=8)]
        public virtual System.DateTime kousinDay
        {
            get
            {
                return this._kousinDay;
            }
            set
            {
                this._kousinDay = value;
            }
        }
        
        [DbColumn(Name="interiorFlg", TypeName="char", Nullable=true, Length=1)]
        public virtual string interiorFlg
        {
            get
            {
                return this._interiorFlg;
            }
            set
            {
                this._interiorFlg = value;
            }
        }
        
        [DbColumn(Name="appExeFlg", TypeName="char", Nullable=true, Length=1)]
        public virtual string appExeFlg
        {
            get
            {
                return this._appExeFlg;
            }
            set
            {
                this._appExeFlg = value;
            }
        }
        
        [DbColumn(Name="siyoCheckFlg", TypeName="char", Nullable=true, Length=1)]
        public virtual string siyoCheckFlg
        {
            get
            {
                return this._siyoCheckFlg;
            }
            set
            {
                this._siyoCheckFlg = value;
            }
        }
        
        [DbColumn(Name="houkokuDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> houkokuDate
        {
            get
            {
                return this._houkokuDate;
            }
            set
            {
                this._houkokuDate = value;
            }
        }
        
        [DbColumn(Name="bikou", TypeName="varchar", Nullable=true, Length=200)]
        public virtual string bikou
        {
            get
            {
                return this._bikou;
            }
            set
            {
                this._bikou = value;
            }
        }
        
        [DbColumn(Name="checkTantou", TypeName="varchar", Nullable=true, Length=20)]
        public virtual string checkTantou
        {
            get
            {
                return this._checkTantou;
            }
            set
            {
                this._checkTantou = value;
            }
        }
        
        [DbColumn(Name="sentFlg", TypeName="bit", Length=1)]
        public virtual bool sentFlg
        {
            get
            {
                return this._sentFlg;
            }
            set
            {
                this._sentFlg = value;
            }
        }
        
        public static tbl_siyo_boss Get(string _customerCode, int _siyoCode)
        {
            tbl_siyo_boss entity = new tbl_siyo_boss();
            entity.customerCode = _customerCode;
            entity.siyoCode = _siyoCode;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}