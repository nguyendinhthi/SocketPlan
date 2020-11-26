using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public class LayerTable : SymbolTable
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbLayerTable; } }

        public List<int> GetLayerIds()
        {
            var result = this.DoTable<int[]>("全レコード取得");

            if (!result.Success)
                return new List<int>();

            return new List<int>(result.Value);
        }

        public int Add(string name)
        {
            Result<int> result = this.DoTable<int, string>("追加", name);
            if (!result.Success)
                throw new AutoCadException("Failed to add layer.");

            return result.Value;
        }

        public bool Exist(string name)
        {
            Result<short> result = this.DoTable<short, string>("所有？", name);
            if (!result.Success)
                throw new AutoCadException("Failed to check to have layer.");

            return result.Value == 1;
        }

        public int GetLayerId(string name)
        {
            Result<int> result = this.DoTable<int, string>("名前から取得", name);
            if (!result.Success)
                throw new AutoCadException("Failed to get layer id.");

            return result.Value;
        }
    }
}