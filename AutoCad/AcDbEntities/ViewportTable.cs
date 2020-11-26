using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public class ViewportTable : AbstractViewportTable
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbViewportTable; } }

        public int GetId(string name)
        {
            var setData = new List<object>();
            setData.Add(name);

            var result = this.DoTable<int, object[]>("名前から取得", setData.ToArray());
            if (!result.Success)
                throw new AutoCadException("Failed to get viewport table record.");

            return result.Value;
        }

        public List<int> GetAll()
        {

            var result = this.DoTable<int[]>("全レコード取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get viewport table record.");

            return new List<int>(result.Value);
        }
    }
}