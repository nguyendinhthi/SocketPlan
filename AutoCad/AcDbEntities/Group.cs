using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public class Group : Object
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbGroup; } }

        public new int Make(string groupName)
        {
            var result = base.Make(groupName);
            if (!result.Success)
                throw new AutoCadException("Failed to make group.");

            return result.Value;
        }

        public void Append(int groupId, List<int> objectIds)
        {
            if (!this.Set<int[]>(groupId, "追加", objectIds.ToArray()))
                throw new AutoCadException("Failed to append entity to group.");
        }

        public int Make(string groupName, List<int> objectIds)
        {
            //既に同名のグループがあったら、グループ名の後にAを足してエラーを防ぐ
            var groupDictionaryId = AutoCad.Db.Database.GetGroupDictionary();
            while (AutoCad.Db.Dictionary.Has(groupDictionaryId, groupName))
            {
                groupName += "A";
            }

            var groupId = this.Make(groupName);
            this.Append(groupId, objectIds);

            return groupId;
        }

        public string GetName(int groupId)
        {
            var result = this.Get<string>(groupId, "名前取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get group name.");

            return result.Value;
        }

        public int Count(int groupId)
        {
            var result = this.Get<int>(groupId, "図形の数取得");
            if (!result.Success)
                throw new AutoCadException("Failed to count group.");

            return result.Value;
        }

        public List<int> GetEntities(int groupId)
        {
            var result = this.Get<int[]>(groupId, "全グループ図形取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get entity of group.");

            return new List<int>(result.Value);
        }

        public void Erase(string groupName)
        {
            //地味に用途がありそうだからメソッドにしておこう
            var groupIds = AutoCad.Db.Dictionary.GetGroupIds();
            foreach (var groupId in groupIds)
            {
                var name = AutoCad.Db.Group.GetName(groupId);
                if (name == groupName)
                    AutoCad.Db.Object.Erase(groupId);
            }
        }

        /// <summary>からっぽのグループを削除する</summary>
        public void EraseEmpty()
        {
            //地味に用途がありそうだからメソッドにしておこう
            var groupIds = AutoCad.Db.Dictionary.GetGroupIds();
            foreach (var groupId in groupIds)
            {
                var count = AutoCad.Db.Group.Count(groupId);
                if (count == 0)
                    AutoCad.Db.Object.Erase(groupId);
            }
        }
    }
}