//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:2.0.50727.5485
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SocketPlan.WebService
{
    using System;
    using System.Collections.Generic;
    using Edsa.Data;
    using Edsa.Data.Attributes;
    
    
    [Serializable()]
    [DbTable(Name="SpcadCLInfos", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionStringOfPhotovoltaic")]
    public partial class SpcadCLInfo : DataEntity<SpcadCLInfo>
    {
        
        private string _ConstructionCode;
        
        private string _PC1ModelNo;
        
        private decimal _PC1RatedPower;
        
        private decimal _PC1Kw;
        
        private int _PC1Qty;
        
        private decimal _PC1KwSmall;
        
        private string _PC2ModelNo;
        
        private System.Nullable<decimal> _PC2RatedPower;
        
        private decimal _PC2Kw;
        
        private int _PC2Qty;
        
        private decimal _PC2KwSmall;
        
        private string _PC3ModelNo;
        
        private System.Nullable<decimal> _PC3RatedPower;
        
        private decimal _PC3Kw;
        
        private int _PC3Qty;
        
        private System.Nullable<decimal> _PC3KwSmall;
        
        private string _PC4ModelNo;
        
        private System.Nullable<decimal> _PC4RatedPower;
        
        private decimal _PC4Kw;
        
        private int _PC4Qty;
        
        private decimal _PC4KwSmall;
        
        private string _PC5ModelNo;
        
        private System.Nullable<decimal> _PC5RatedPower;
        
        private decimal _PC5Kw;
        
        private int _PC5Qty;
        
        private decimal _PC5KwSmall;
        
        private decimal _AllKwSmall;
        
        private string _PanelModelNo1;
        
        private int _PanelCnt1;
        
        private string _PanelModelNo2;
        
        private int _PanelCnt2;
        
        private string _PanelModelNo3;
        
        private int _PanelCnt3;
        
        private string _PanelModelNo4;
        
        private int _PanelCnt4;
        
        private System.Nullable<System.DateTime> _UpdatedDate;
        
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
        
        [DbColumn(Name="PC1ModelNo", TypeName="varchar", Length=15)]
        public virtual string PC1ModelNo
        {
            get
            {
                return this._PC1ModelNo;
            }
            set
            {
                this._PC1ModelNo = value;
            }
        }
        
        [DbColumn(Name="PC1RatedPower", TypeName="decimal", DecimalPlace=3, Length=5)]
        public virtual decimal PC1RatedPower
        {
            get
            {
                return this._PC1RatedPower;
            }
            set
            {
                this._PC1RatedPower = value;
            }
        }
        
        [DbColumn(Name="PC1Kw", TypeName="decimal", DecimalPlace=3, Length=5)]
        public virtual decimal PC1Kw
        {
            get
            {
                return this._PC1Kw;
            }
            set
            {
                this._PC1Kw = value;
            }
        }
        
        [DbColumn(Name="PC1Qty", TypeName="int", Length=4)]
        public virtual int PC1Qty
        {
            get
            {
                return this._PC1Qty;
            }
            set
            {
                this._PC1Qty = value;
            }
        }
        
        [DbColumn(Name="PC1KwSmall", TypeName="decimal", DecimalPlace=3, Length=5)]
        public virtual decimal PC1KwSmall
        {
            get
            {
                return this._PC1KwSmall;
            }
            set
            {
                this._PC1KwSmall = value;
            }
        }
        
        [DbColumn(Name="PC2ModelNo", TypeName="varchar", Nullable=true, Length=15)]
        public virtual string PC2ModelNo
        {
            get
            {
                return this._PC2ModelNo;
            }
            set
            {
                this._PC2ModelNo = value;
            }
        }
        
        [DbColumn(Name="PC2RatedPower", TypeName="decimal", Nullable=true, DecimalPlace=3, Length=5)]
        public virtual System.Nullable<decimal> PC2RatedPower
        {
            get
            {
                return this._PC2RatedPower;
            }
            set
            {
                this._PC2RatedPower = value;
            }
        }
        
        [DbColumn(Name="PC2Kw", TypeName="decimal", DecimalPlace=3, Length=5)]
        public virtual decimal PC2Kw
        {
            get
            {
                return this._PC2Kw;
            }
            set
            {
                this._PC2Kw = value;
            }
        }
        
        [DbColumn(Name="PC2Qty", TypeName="int", Length=4)]
        public virtual int PC2Qty
        {
            get
            {
                return this._PC2Qty;
            }
            set
            {
                this._PC2Qty = value;
            }
        }
        
        [DbColumn(Name="PC2KwSmall", TypeName="decimal", DecimalPlace=3, Length=5)]
        public virtual decimal PC2KwSmall
        {
            get
            {
                return this._PC2KwSmall;
            }
            set
            {
                this._PC2KwSmall = value;
            }
        }
        
        [DbColumn(Name="PC3ModelNo", TypeName="varchar", Nullable=true, Length=15)]
        public virtual string PC3ModelNo
        {
            get
            {
                return this._PC3ModelNo;
            }
            set
            {
                this._PC3ModelNo = value;
            }
        }
        
        [DbColumn(Name="PC3RatedPower", TypeName="decimal", Nullable=true, DecimalPlace=3, Length=5)]
        public virtual System.Nullable<decimal> PC3RatedPower
        {
            get
            {
                return this._PC3RatedPower;
            }
            set
            {
                this._PC3RatedPower = value;
            }
        }
        
        [DbColumn(Name="PC3Kw", TypeName="decimal", DecimalPlace=3, Length=5)]
        public virtual decimal PC3Kw
        {
            get
            {
                return this._PC3Kw;
            }
            set
            {
                this._PC3Kw = value;
            }
        }
        
        [DbColumn(Name="PC3Qty", TypeName="int", Length=4)]
        public virtual int PC3Qty
        {
            get
            {
                return this._PC3Qty;
            }
            set
            {
                this._PC3Qty = value;
            }
        }
        
        [DbColumn(Name="PC3KwSmall", TypeName="decimal", Nullable=true, DecimalPlace=3, Length=5)]
        public virtual System.Nullable<decimal> PC3KwSmall
        {
            get
            {
                return this._PC3KwSmall;
            }
            set
            {
                this._PC3KwSmall = value;
            }
        }
        
        [DbColumn(Name="PC4ModelNo", TypeName="varchar", Nullable=true, Length=15)]
        public virtual string PC4ModelNo
        {
            get
            {
                return this._PC4ModelNo;
            }
            set
            {
                this._PC4ModelNo = value;
            }
        }
        
        [DbColumn(Name="PC4RatedPower", TypeName="decimal", Nullable=true, DecimalPlace=3, Length=5)]
        public virtual System.Nullable<decimal> PC4RatedPower
        {
            get
            {
                return this._PC4RatedPower;
            }
            set
            {
                this._PC4RatedPower = value;
            }
        }
        
        [DbColumn(Name="PC4Kw", TypeName="decimal", DecimalPlace=3, Length=5)]
        public virtual decimal PC4Kw
        {
            get
            {
                return this._PC4Kw;
            }
            set
            {
                this._PC4Kw = value;
            }
        }
        
        [DbColumn(Name="PC4Qty", TypeName="int", Length=4)]
        public virtual int PC4Qty
        {
            get
            {
                return this._PC4Qty;
            }
            set
            {
                this._PC4Qty = value;
            }
        }
        
        [DbColumn(Name="PC4KwSmall", TypeName="decimal", DecimalPlace=3, Length=5)]
        public virtual decimal PC4KwSmall
        {
            get
            {
                return this._PC4KwSmall;
            }
            set
            {
                this._PC4KwSmall = value;
            }
        }
        
        [DbColumn(Name="PC5ModelNo", TypeName="varchar", Nullable=true, Length=15)]
        public virtual string PC5ModelNo
        {
            get
            {
                return this._PC5ModelNo;
            }
            set
            {
                this._PC5ModelNo = value;
            }
        }
        
        [DbColumn(Name="PC5RatedPower", TypeName="decimal", Nullable=true, DecimalPlace=3, Length=5)]
        public virtual System.Nullable<decimal> PC5RatedPower
        {
            get
            {
                return this._PC5RatedPower;
            }
            set
            {
                this._PC5RatedPower = value;
            }
        }
        
        [DbColumn(Name="PC5Kw", TypeName="decimal", DecimalPlace=3, Length=5)]
        public virtual decimal PC5Kw
        {
            get
            {
                return this._PC5Kw;
            }
            set
            {
                this._PC5Kw = value;
            }
        }
        
        [DbColumn(Name="PC5Qty", TypeName="int", Length=4)]
        public virtual int PC5Qty
        {
            get
            {
                return this._PC5Qty;
            }
            set
            {
                this._PC5Qty = value;
            }
        }
        
        [DbColumn(Name="PC5KwSmall", TypeName="decimal", DecimalPlace=3, Length=5)]
        public virtual decimal PC5KwSmall
        {
            get
            {
                return this._PC5KwSmall;
            }
            set
            {
                this._PC5KwSmall = value;
            }
        }
        
        [DbColumn(Name="AllKwSmall", TypeName="decimal", DecimalPlace=3, Length=5)]
        public virtual decimal AllKwSmall
        {
            get
            {
                return this._AllKwSmall;
            }
            set
            {
                this._AllKwSmall = value;
            }
        }
        
        [DbColumn(Name="PanelModelNo1", TypeName="varchar", Length=10)]
        public virtual string PanelModelNo1
        {
            get
            {
                return this._PanelModelNo1;
            }
            set
            {
                this._PanelModelNo1 = value;
            }
        }
        
        [DbColumn(Name="PanelCnt1", TypeName="int", Length=4)]
        public virtual int PanelCnt1
        {
            get
            {
                return this._PanelCnt1;
            }
            set
            {
                this._PanelCnt1 = value;
            }
        }
        
        [DbColumn(Name="PanelModelNo2", TypeName="varchar", Nullable=true, Length=10)]
        public virtual string PanelModelNo2
        {
            get
            {
                return this._PanelModelNo2;
            }
            set
            {
                this._PanelModelNo2 = value;
            }
        }
        
        [DbColumn(Name="PanelCnt2", TypeName="int", Length=4)]
        public virtual int PanelCnt2
        {
            get
            {
                return this._PanelCnt2;
            }
            set
            {
                this._PanelCnt2 = value;
            }
        }
        
        [DbColumn(Name="PanelModelNo3", TypeName="varchar", Nullable=true, Length=10)]
        public virtual string PanelModelNo3
        {
            get
            {
                return this._PanelModelNo3;
            }
            set
            {
                this._PanelModelNo3 = value;
            }
        }
        
        [DbColumn(Name="PanelCnt3", TypeName="int", Length=4)]
        public virtual int PanelCnt3
        {
            get
            {
                return this._PanelCnt3;
            }
            set
            {
                this._PanelCnt3 = value;
            }
        }
        
        [DbColumn(Name="PanelModelNo4", TypeName="varchar", Nullable=true, Length=10)]
        public virtual string PanelModelNo4
        {
            get
            {
                return this._PanelModelNo4;
            }
            set
            {
                this._PanelModelNo4 = value;
            }
        }
        
        [DbColumn(Name="PanelCnt4", TypeName="int", Length=4)]
        public virtual int PanelCnt4
        {
            get
            {
                return this._PanelCnt4;
            }
            set
            {
                this._PanelCnt4 = value;
            }
        }
        
        [DbColumn(Name="UpdatedDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> UpdatedDate
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
        
        public static SpcadCLInfo Get(string _ConstructionCode)
        {
            SpcadCLInfo entity = new SpcadCLInfo();
            entity.ConstructionCode = _ConstructionCode;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
