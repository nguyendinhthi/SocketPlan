using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public class Region : Entity
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbRegion; } }

        public int Make(int objectId)
        {
            Result<int> result = base.Make<int>(objectId);
            if (!result.Success)
                throw new AutoCadException("Failed to make region.");

            return result.Value;
        }

        /// <summary>和</summary>
        public void Union(int objectId, int targetId)
        {
            this.BooleanOper(objectId, 0, targetId);
        }

        /// <summary>交差</summary>
        public void Intersect(int objectId, int targetId)
        {
            this.BooleanOper(objectId, 1, targetId);
        }

        /// <summary>差</summary>
        public void Subtract(int objectId, int targetId)
        {
            this.BooleanOper(objectId, 2, targetId);
        }

        /// <summary>リージョンの高さが違うとこの処理失敗するぽ</summary>
        private void BooleanOper(int objectId, short operationId, int targetId)
        {
            List<object> args = new List<object>();
            args.Add(operationId);
            args.Add(targetId);

            if (!base.Set<object[]>(objectId, "ブール演算", args.ToArray()))
                throw new AutoCadException("Failed to set boolean operation.");
        }

        public bool IsEmpty(int objectId)
        {
            Result<short> result = this.Get<short>(objectId, "空？");
            if (!result.Success)
                throw new AutoCadException("Failed to get empty region.");

            return result.Value == 1;
        }

        public double GetArea(int objectId)
        {
            var areaProperty = this.GetAreaProperty(objectId);
            return (double)areaProperty[1];
        }

        private object[] GetAreaProperty(int objectId)
        {
            var setData = new List<object>();
            setData.Add(new double[] { 0, 0, 0 });
            setData.Add(new double[] { 1, 0, 0 });
            setData.Add(new double[] { 0, 1, 0 });

            var result = this.Get<object[], object[]>(objectId, "エリアプロパティ取得", setData.ToArray());
            if (!result.Success)
                throw new AutoCadException("Failed to get area property of region.");

            return result.Value;
        }
    }
}