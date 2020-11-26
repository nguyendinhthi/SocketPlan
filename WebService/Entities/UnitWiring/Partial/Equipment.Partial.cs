using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class Equipment
    {
        public Block Block { get; set; }

        private List<Text> texts = new List<Text>();
        public List<Text> Texts
        {
            get { return this.texts; }
            set { this.texts = value; }
        }

        private List<Specification> specifications = new List<Specification>();
        public List<Specification> Specifications
        {
            get { return this.specifications; }
            set { this.specifications = value; }
        }

        //XMLSerializeで循環参照してしまうから、Equipmentのリストを持つのは止めておこう。
        private List<RelatedEquipment> relatedEquipments = new List<RelatedEquipment>();
        public List<RelatedEquipment> RelatedEquipments
        {
            get { return this.relatedEquipments; }
            set { this.relatedEquipments = value; }
        }

        private List<EquipmentCustomAction> customActions = new List<EquipmentCustomAction>();
        public List<EquipmentCustomAction> CustomActions
        {
            get
            {
                return this.customActions;
            }

            set
            {
                this.customActions = value;
            }
        }

        public EquipmentKind Kind { get; set; }

        public bool IsSwitch
        {
            get
            {
                if (this.EquipmentKindId == 3)
                    return true;
                else
                    return false;
            }
        }

        public static void Delete(int equipmentId)
        {
            var db = Equipment.GetDatabase();
            db.ExecuteNonQuery("DELETE FROM Equipments WHERE Id =" + equipmentId);
        }

        public static List<Equipment> FindAll(int blockId)
        {
            var db = Equipment.GetDatabase();

            return db.ExecuteQuery<Equipment>("SELECT * FROM Equipments WHERE BlockId =" + blockId);
        }

        public  void Store(int categoryId)
        {
            var now = DateTime.Now;
            var oldBlockId = this.BlockId;

            //更新
            //Blocks (未登録だったら追加、登録済みだったら取得)
            if (Block.Exists(this.Block.Name))
            {
                this.Block = Block.Find(this.Block.Name);
            }
            else
            {
                this.Block.UpdatedDateTime = now;
                this.Block.Store();
            }

            //Equipments（追加 or 更新）
            this.BlockId = this.Block.Id;
            this.UpdatedDateTime = now;
            this.Store();

            //Blocks （変更によって以前のBlockが誰からも参照されなくなったら削除）
            if (oldBlockId != 0 && oldBlockId != this.BlockId)
            {
                var equipments = Equipment.FindAll(oldBlockId);
                if (equipments.Count == 0)
                    Block.Delete(oldBlockId);
            }

            //Texts（削除・追加）
            Text.Delete(this.Id);
            foreach (var text in this.Texts)
            {
                text.EquipmentId = this.Id;
                text.UpdatedDateTime = now;
                text.Store();
            }

            //EquipmentSpecifications（削除・追加）
            EquipmentSpecification.Delete(this.Id);
            foreach (var spec in this.Specifications)
            {
                var equipSpec = new EquipmentSpecification();
                equipSpec.EquipmentId = this.Id;
                equipSpec.SpecificationId = spec.Id;
                equipSpec.UpdatedDateTime = now;
                equipSpec.Store();
            }

            //SelectionCategoryDetails（新規の時 or カテゴリ替えた時は削除・追加）
            var currentDetails = SelectionCategoryDetail.Get(this.Id);
            if (!currentDetails.Exists(p => p.SelectionCategoryId == categoryId))
            {
                SelectionCategoryDetail.DeleteByEquipmentId(this.Id);

                var categoryDetail = new SelectionCategoryDetail();
                categoryDetail.EquipmentId = this.Id;
                categoryDetail.SelectionCategoryId = categoryId;
                categoryDetail.SortNo = SelectionCategoryDetail.GetMaxSortNo(categoryId) + 1;
                categoryDetail.UpdatedDateTime = now;
                categoryDetail.Store();
            }

            //RelatedEquipments（他のEquipに関連付けられているこのEquipは変更なし）
            //RelatedEquipments（このEquipに関連付けたEqiupは削除・追加）
            RelatedEquipment.Delete(this.Id);
            foreach (var relatedEquipment in this.RelatedEquipments)
            {
                relatedEquipment.EquipmentId = this.Id;
                relatedEquipment.UpdatedDateTime = now;
                relatedEquipment.Store();
            }

            //EquipmentCustomActions
            EquipmentCustomAction.Delete(this.Id);
            foreach (var action in this.CustomActions)
            {
                var equipAction = new EquipmentCustomAction();
                equipAction.EquipmentId = this.Id;
                equipAction.CustomActionId = action.CustomActionId;
                equipAction.Parameter = action.Parameter;
                equipAction.UpdatedDateTime = now;
                equipAction.Store();
            }
        }

        public override void Drop()
        {
            EquipmentCustomAction.Delete(this.Id);
            SelectionCategoryDetail.DeleteByEquipmentId(this.Id);
            RelatedEquipment.DeleteRelatedEquipment(this.Id);
            RelatedEquipment.Delete(this.Id);
            Text.Delete(this.Id);
            EquipmentSpecification.Delete(this.Id);
            Equipment.Delete(this.Id);

            var equipments = Equipment.FindAll(this.BlockId);
            equipments.RemoveAll(p => p.Id == this.Id);
            if (equipments.Count == 0) //このEquipment以外で参照してなかったら削除
                Block.Delete(this.BlockId);
        }

        public static List<Equipment> Get(List<int> Ids)
        {
            if(Ids.Count == 0)
                return new List<Equipment>();

            var s = string.Empty;
            Ids.ForEach(p => s += p + ",");
            s = s.Substring(0, s.Length - 1);

            var sql = @"SELECT * FROM Equipments WHERE Id IN (" + s + ")";
            var db = Equipment.GetDatabase();
            return db.ExecuteQuery<Equipment>(sql);
        }
    }
}
