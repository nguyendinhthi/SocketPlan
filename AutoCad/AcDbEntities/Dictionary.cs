using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public class Dictionary : Object
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbDictionary; } }

        public List<int> GetLayoutIds()
        {
            var layoutDictionary = AutoCad.Db.Database.GetLayoutDictionary();

            var result = this.Get<int[]>(layoutDictionary, "全レコード取得");

            if (!result.Success)
                return new List<int>();

            return new List<int>(result.Value);
        }

        public List<int> GetGroupIds()
        {
            var groupDictionary = AutoCad.Db.Database.GetGroupDictionary();

            var result = this.Get<int[]>(groupDictionary, "全レコード取得");

            if (!result.Success)
                return new List<int>();

            return new List<int>(result.Value);
        }

        public bool Has(int dictionaryId, string name)
        {
            var result = this.Get<short, string>(dictionaryId, "所有？", name);
            if (!result.Success)
                throw new AutoCadException("Failed to check to have group.");

            return result.Value == 1;
        }
    }
}