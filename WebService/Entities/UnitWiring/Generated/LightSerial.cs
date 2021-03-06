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
    [DbTable(Name = "LightSerials", DatabaseType = DatabaseType.SqlServer, ConnectionSettingKeyName = "SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class LightSerial : DataEntity<LightSerial>
    {

        private int _Id;

        private string _Name;

        private string _ItemName;

        // 照明タイプ（1:天井,2:ダウン,3:壁）
        private System.Nullable<int> _LightTypeId;

        private string _UnitCode;

        private string _UnitName;

        private int _CategoryId;

        private int _PartsQuantity;

        private int _LightSource;

        private int _LightControl;

        private int _Sensor;

        private decimal _PowerConsumption;

        private bool _RequireApproval;

        private System.Nullable<System.DateTime> _DeletedDate;

        private bool _Deletable;

        private System.DateTime _UpdatedDateTime;

        private string _ImplantBore;

        private string _ImplantHeight;

        // 在庫が空であるか否かを示すフラグ。
        private bool _IsStockEmpty;

        [DbColumn(Name = "Id", TypeName = "int", IsPrimaryKey = true, Length = 4)]
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

        [DbColumn(Name = "Name", TypeName = "varchar", Length = 50)]
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

        [DbColumn(Name = "ItemName", TypeName = "varchar", Nullable = true, Length = 80)]
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

        [DbColumn(Name = "LightTypeId", TypeName = "int", Nullable = true, Length = 4, Remarks = "照明タイプ（1:天井,2:ダウン,3:壁）")]
        public virtual System.Nullable<int> LightTypeId
        {
            get
            {
                return this._LightTypeId;
            }
            set
            {
                this._LightTypeId = value;
            }
        }

        [DbColumn(Name = "UnitCode", TypeName = "varchar", Nullable = true, Length = 2)]
        public virtual string UnitCode
        {
            get
            {
                return this._UnitCode;
            }
            set
            {
                this._UnitCode = value;
            }
        }

        [DbColumn(Name = "UnitName", TypeName = "varchar", Nullable = true, Length = 6)]
        public virtual string UnitName
        {
            get
            {
                return this._UnitName;
            }
            set
            {
                this._UnitName = value;
            }
        }

        [DbColumn(Name = "CategoryId", TypeName = "int", Length = 4)]
        public virtual int CategoryId
        {
            get
            {
                return this._CategoryId;
            }
            set
            {
                this._CategoryId = value;
            }
        }

        [DbColumn(Name = "PartsQuantity", TypeName = "int", Length = 4)]
        public virtual int PartsQuantity
        {
            get
            {
                return this._PartsQuantity;
            }
            set
            {
                this._PartsQuantity = value;
            }
        }

        [DbColumn(Name = "LightSource", TypeName = "int", Length = 4)]
        public virtual int LightSource
        {
            get
            {
                return this._LightSource;
            }
            set
            {
                this._LightSource = value;
            }
        }

        [DbColumn(Name = "LightControl", TypeName = "int", Length = 4)]
        public virtual int LightControl
        {
            get
            {
                return this._LightControl;
            }
            set
            {
                this._LightControl = value;
            }
        }

        [DbColumn(Name = "Sensor", TypeName = "int", Length = 4)]
        public virtual int Sensor
        {
            get
            {
                return this._Sensor;
            }
            set
            {
                this._Sensor = value;
            }
        }

        [DbColumn(Name = "PowerConsumption", TypeName = "decimal", DecimalPlace = 2, Length = 5)]
        public virtual decimal PowerConsumption
        {
            get
            {
                return this._PowerConsumption;
            }
            set
            {
                this._PowerConsumption = value;
            }
        }

        [DbColumn(Name = "RequireApproval", TypeName = "bit", Length = 1)]
        public virtual bool RequireApproval
        {
            get
            {
                return this._RequireApproval;
            }
            set
            {
                this._RequireApproval = value;
            }
        }

        [DbColumn(Name = "DeletedDate", TypeName = "datetime", Nullable = true, DecimalPlace = 3, Length = 8)]
        public virtual System.Nullable<System.DateTime> DeletedDate
        {
            get
            {
                return this._DeletedDate;
            }
            set
            {
                this._DeletedDate = value;
            }
        }

        [DbColumn(Name = "Deletable", TypeName = "bit", Length = 1)]
        public virtual bool Deletable
        {
            get
            {
                return this._Deletable;
            }
            set
            {
                this._Deletable = value;
            }
        }

        [DbColumn(Name = "UpdatedDateTime", TypeName = "datetime", DecimalPlace = 3, Length = 8)]
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

        [DbColumn(Name = "ImplantBore", TypeName = "varchar", Nullable = true, Length = 10)]
        public virtual string ImplantBore
        {
            get
            {
                return this._ImplantBore;
            }
            set
            {
                this._ImplantBore = value;
            }
        }

        [DbColumn(Name = "ImplantHeight", TypeName = "varchar", Nullable = true, Length = 10)]
        public virtual string ImplantHeight
        {
            get
            {
                return this._ImplantHeight;
            }
            set
            {
                this._ImplantHeight = value;
            }
        }

        [DbColumn(Name = "IsStockEmpty", TypeName = "bit", Length = 1, Remarks = "在庫が空であるか否かを示すフラグ。")]
        public virtual bool IsStockEmpty
        {
            get
            {
                return this._IsStockEmpty;
            }
            set
            {
                this._IsStockEmpty = value;
            }
        }

        public static LightSerial Get(int _Id)
        {
            LightSerial entity = new LightSerial();
            entity.Id = _Id;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
