//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:2.0.50727.3643
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
    [DbTable(Name="WireItems", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class WireItem : DataEntity<WireItem>
    {
        
        // 配線ID
        private int _Id;
        
        // 配線径
        private decimal _Diameter;
        
        // 芯数
        private int _CoreNumber;
        
        // 配線色。AutoCADの色番号と対応させる
        private int _Color;
        
        // ケーブルの種類。VVFしかないかも。
        private string _Type;
        
        // アース付かどうか
        private bool _WithEarth;
        
        // 備考。配線の用途などを書く欄
        private string _Remarks;
        
        // 更新日時
        private System.DateTime _UpdatedDateTime;
        
        [DbColumn(Name="Id", TypeName="int", IsPrimaryKey=true, Length=4, Remarks="配線ID")]
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
        
        [DbColumn(Name="Diameter", TypeName="decimal", DecimalPlace=1, Length=9, Remarks="配線径")]
        public virtual decimal Diameter
        {
            get
            {
                return this._Diameter;
            }
            set
            {
                this._Diameter = value;
            }
        }
        
        [DbColumn(Name="CoreNumber", TypeName="int", Length=4, Remarks="芯数")]
        public virtual int CoreNumber
        {
            get
            {
                return this._CoreNumber;
            }
            set
            {
                this._CoreNumber = value;
            }
        }
        
        [DbColumn(Name="Color", TypeName="int", Length=4, Remarks="配線色。AutoCADの色番号と対応させる")]
        public virtual int Color
        {
            get
            {
                return this._Color;
            }
            set
            {
                this._Color = value;
            }
        }
        
        [DbColumn(Name="Type", TypeName="varchar", Length=50, Remarks="ケーブルの種類。VVFしかないかも。")]
        public virtual string Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
        
        [DbColumn(Name="WithEarth", TypeName="bit", Length=1, Remarks="アース付かどうか")]
        public virtual bool WithEarth
        {
            get
            {
                return this._WithEarth;
            }
            set
            {
                this._WithEarth = value;
            }
        }
        
        [DbColumn(Name="Remarks", TypeName="varchar", Length=100, Remarks="備考。配線の用途などを書く欄")]
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
        
        [DbColumn(Name="UpdatedDateTime", TypeName="datetime", DecimalPlace=3, Length=8, Remarks="更新日時")]
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
        
        public static WireItem Get(int _Id)
        {
            WireItem entity = new WireItem();
            entity.Id = _Id;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
