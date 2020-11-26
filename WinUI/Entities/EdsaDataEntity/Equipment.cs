using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

namespace SocketPlan.WinUI.SocketPlanServiceReference
{
    public partial class Equipment
    {
        public Equipment Clone()
        {
            //ApparentPowerをシンボル毎に保持する目的で作ったメソッドです。
            //今のところ、ディープコピーする必要はないので、シャローコピーです。

            return (Equipment)this.MemberwiseClone();
        }

        public bool CustomAction取り出し矢印
        {
            get
            {
                return Array.Exists(this.CustomActions, p => p.CustomActionId == Const.CustomAction.取り出し矢印);
            }
        }

        public bool CustomAction取り出し位置指定
        {
            get
            {
                return Array.Exists(this.CustomActions, p => p.CustomActionId == Const.CustomAction.取り出し位置指定);
            }
        }

        public bool CustomAction照明品番入力
        {
            get
            {
                return Array.Exists(this.CustomActions, p => p.CustomActionId == Const.CustomAction.照明品番入力);
            }
        }

        public string 照明シンボルイメージコード
        {
            get
            {
                var a = Array.Find(this.CustomActions, p => p.CustomActionId == Const.CustomAction.照明品番入力);
                if (a == null)
                    return string.Empty;

                return a.Parameter;
            }
        }

        public bool CustomActionセット品
        {
            get
            {
                return Array.Exists(this.CustomActions, p => p.CustomActionId == Const.CustomAction.セット品);
            }
        }

        public bool IsJBox
        {
            get
            {
                if (this.Name == Const.EquipmentName.JB_D ||
                    this.Name == Const.EquipmentName.JB_DA ||
                    this.Name == Const.EquipmentName.JB_DA_02 ||
                    this.Name == Const.EquipmentName.JB_DA_03 ||
                    this.Name == Const.EquipmentName.JB_DA_04)
                    return true;

                return false;
            }
        }
        public bool IsKansenHikiomiMeter
        {
            get
            {
                if (this.Name == Const.EquipmentName.全量買取メーター ||
                    this.Name == Const.EquipmentName.全量買取用メーター ||
                    this.Name == Const.EquipmentName.余剰電力販売計 ||
                    this.Name == Const.EquipmentName.余剰電力用)
                    return true;

                return false;
            }
        }

        /// <summary>シンボルに対応するEquipmentを返す</summary>
        public static Equipment Find(Symbol symbol)
        {
            var attributes = symbol.OtherAttributes;

            // 基本的にはここでEquipmentを取得できる
            if (symbol.HasEquipmentId)
            {
                var equipment = UnitWiring.Masters.Equipments.Find(p => p.Id == symbol.AttributeEquipmentId);
                if (equipment != null)
                    return equipment;
            }

            // 以下、過去図面用(シンボルがEquipmentIdのAttributeを持つ前)の処理
            List<Equipment> matchedList = new List<Equipment>();
            foreach (var equipment in UnitWiring.Masters.Equipments)
            {
                if (equipment.Block.Name != symbol.BlockName)
                    continue;

                //定義されている属性を図面上シンボルが全て持っていたら一致とする。
                bool isMatch = true;
                foreach (var text in equipment.Texts)
                {
                    //例外的に一般-39の場合は「H=」があってもスルーする
                    if (attributes.Exists(p => p.Value == "電気ﾘﾓｺﾝｷ-用") && text.Value == "H=")
                        continue;

                    if (!attributes.Exists(p => p.Value == text.Value))
                        isMatch = false;
                }

                if (isMatch)
                    matchedList.Add(equipment);
            }

            if (matchedList.Count == 0)
                throw new ApplicationException(Messages.NotFoundEquipmentDefinition(symbol));

            matchedList.Sort((p, q) =>
            {
                //一致した中から、一番多くTextsが一致したものを最優先で返す
                if (p.Texts.Length != q.Texts.Length)
                    return q.Texts.Length.CompareTo(p.Texts.Length);

                //一致した中から、優先度の高いものを返す。(優先度は適当な値なのであまり信用できません・・・。万が一用です。)

                return p.Priority.CompareTo(q.Priority);
            });

            return matchedList[0].Clone();
        }

        //Specifications関係のプロパティがSymbolとEquipmentに分散してしまった・・・。Equipmentにいるべきだよ！
        public bool IsCeilingLight { get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.シーリングライト); } }
        public bool IsDownLight { get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.ダウンライト); } }
        public bool IsWallLight { get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.ウォールライト); } }
        public bool Is設備 { get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.設備); } }
        public bool IsAutoSmartSeries { get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.スマートシリーズ); } }
    }
}
