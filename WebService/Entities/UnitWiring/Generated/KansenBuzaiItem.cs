//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:2.0.50727.5477
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
    [DbTable(Name = "KansenBuzaiItems", DatabaseType = DatabaseType.SqlServer, ConnectionSettingKeyName = "SocketPlan.WebService.Properties.Settings.ConnectionString")]
    public partial class KansenBuzaiItem : DataEntity<KansenBuzaiItem>
    {

        // 部材コード
        private string _BuzaiCode;

        // 区分
        private string _BuzaiKubun;

        private string _BuzaiName;

        private string _BuzaiNameYojo;

        [DbColumn(Name = "BuzaiCode", TypeName = "varchar", IsPrimaryKey = true, Length = 5, Remarks = "部材コード")]
        public virtual string BuzaiCode
        {
            get
            {
                return this._BuzaiCode;
            }
            set
            {
                this._BuzaiCode = value;
            }
        }

        [DbColumn(Name = "BuzaiKubun", TypeName = "varchar", Length = 1, Remarks = "区分")]
        public virtual string BuzaiKubun
        {
            get
            {
                return this._BuzaiKubun;
            }
            set
            {
                this._BuzaiKubun = value;
            }
        }

        [DbColumn(Name = "BuzaiName", TypeName = "varchar", Nullable = true, Length = 50)]
        public virtual string BuzaiName
        {
            get
            {
                return this._BuzaiName;
            }
            set
            {
                this._BuzaiName = value;
            }
        }

        [DbColumn(Name = "BuzaiNameYojo", TypeName = "varchar", Nullable = true, Length = 50)]
        public virtual string BuzaiNameYojo
        {
            get
            {
                return this._BuzaiNameYojo;
            }
            set
            {
                this._BuzaiNameYojo = value;
            }
        }

        public static KansenBuzaiItem Get(string _BuzaiCode)
        {
            KansenBuzaiItem entity = new KansenBuzaiItem();
            entity.BuzaiCode = _BuzaiCode;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
