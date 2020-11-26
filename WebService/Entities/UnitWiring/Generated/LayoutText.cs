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
    [DbTable(Name="LayoutTexts", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class LayoutText : DataEntity<LayoutText>
    {
        
        // �}������}��
        private int _LayoutId;
        
        // �����Ӗ��̗�ɂ͓���TypeId��U���Ă���Ă�������
        private int _TypeId;
        
        // ���O
        private string _Name;
        
        // �}��������WX
        private decimal _PointX;
        
        // �}��������WY
        private decimal _PointY;
        
        // �}�ʂ̘g���B�͂ݏo���΍�B
        private decimal _MaxWidth;
        
        // �e�����Ƃ̃t�H���g�T�C�Y
        private decimal _FontSize;
        
        // AutoCAD�̈ʒu���킹����
        //0	���񂹁i�K��l�j
        //1	���S�iC�j
        //2	�E�񂹁iR�j
        //3	���[�����iA�j
        //4	�����iM�j
        //5	�t�B�b�g�iF�j
        //6	����iTL�j
        //7	�㒆�S�iTC�j
        //8	�E��iTR�j
        //9	�������iML�j
        //10	�����iMC�j
        //11	�E�����iMR�j
        //12	�����iBL�j
        //13	�����S�iBC�j
        //14	�E���iBR�j
        private int _Align;
        
        // ���̃N�G�����ʂ�}�ʂɏ�������
        private int _QueryId;
        
        // �X�V����
        private System.DateTime _UpdatedDateTime;
        
        [DbColumn(Name="LayoutId", TypeName="int", IsPrimaryKey=true, Length=4, Remarks="�}������}��")]
        public virtual int LayoutId
        {
            get
            {
                return this._LayoutId;
            }
            set
            {
                this._LayoutId = value;
            }
        }
        
        [DbColumn(Name="TypeId", TypeName="int", IsPrimaryKey=true, Length=4, Remarks="�����Ӗ��̗�ɂ͓���TypeId��U���Ă���Ă�������")]
        public virtual int TypeId
        {
            get
            {
                return this._TypeId;
            }
            set
            {
                this._TypeId = value;
            }
        }
        
        [DbColumn(Name="Name", TypeName="varchar", Length=50, Remarks="���O")]
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
        
        [DbColumn(Name="PointX", TypeName="decimal", DecimalPlace=2, Length=9, Remarks="�}��������WX")]
        public virtual decimal PointX
        {
            get
            {
                return this._PointX;
            }
            set
            {
                this._PointX = value;
            }
        }
        
        [DbColumn(Name="PointY", TypeName="decimal", DecimalPlace=2, Length=9, Remarks="�}��������WY")]
        public virtual decimal PointY
        {
            get
            {
                return this._PointY;
            }
            set
            {
                this._PointY = value;
            }
        }
        
        [DbColumn(Name="MaxWidth", TypeName="decimal", Length=9, Remarks="�}�ʂ̘g���B�͂ݏo���΍�B")]
        public virtual decimal MaxWidth
        {
            get
            {
                return this._MaxWidth;
            }
            set
            {
                this._MaxWidth = value;
            }
        }
        
        [DbColumn(Name="FontSize", TypeName="decimal", DecimalPlace=2, Length=9, Remarks="�e�����Ƃ̃t�H���g�T�C�Y")]
        public virtual decimal FontSize
        {
            get
            {
                return this._FontSize;
            }
            set
            {
                this._FontSize = value;
            }
        }
        
        [DbColumn(Name="Align", TypeName="int", Length=4, Remarks="AutoCAD�̈ʒu���킹����\r\n0\t���񂹁i�K��l�j\r\n1\t���S�iC�j\r\n2\t�E�񂹁iR�j\r\n3\t���[�����iA�j\r\n4\t�����iM�j\r\n5\t�t�B�b�g�iF�j\r\n6\t" +
            "����iTL�j\r\n7\t�㒆�S�iTC�j\r\n8\t�E��iTR�j\r\n9\t�������iML�j\r\n10\t�����iMC�j\r\n11\t�E�����iMR�j\r\n12\t�����iBL�j\r\n13\t�����S" +
            "�iBC�j\r\n14\t�E���iBR�j")]
        public virtual int Align
        {
            get
            {
                return this._Align;
            }
            set
            {
                this._Align = value;
            }
        }
        
        [DbColumn(Name="QueryId", TypeName="int", Length=4, Remarks="���̃N�G�����ʂ�}�ʂɏ�������")]
        public virtual int QueryId
        {
            get
            {
                return this._QueryId;
            }
            set
            {
                this._QueryId = value;
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
        
        public static LayoutText Get(int _LayoutId, int _TypeId)
        {
            LayoutText entity = new LayoutText();
            entity.LayoutId = _LayoutId;
            entity.TypeId = _TypeId;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}