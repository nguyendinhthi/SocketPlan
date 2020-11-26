using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public class Filter
    {
        private FilterOption.LayerName layerName = FilterOption.LayerName.All;
        private FilterOption.Color color = FilterOption.Color.All;
        private FilterOption.LineType lineType = FilterOption.LineType.All;
        private FilterOption.ByLayer byLayer = FilterOption.ByLayer.Off;
        private FilterOption.Hilight hilight = FilterOption.Hilight.Off;
        private FilterOption.AndOr andOr = FilterOption.AndOr.And;
        private List<FilterOption.ObjectType> objectTypes = new List<FilterOption.ObjectType>(new FilterOption.ObjectType[] { FilterOption.ObjectType.All });

        public void Add(FilterOption.LayerName value) { this.layerName = value; }
        public void Add(FilterOption.Color value) { this.color = value; }
        public void Add(FilterOption.LineType value) { this.lineType = value; }
        public void Add(FilterOption.ByLayer value) { this.byLayer = value; }
        public void Add(FilterOption.Hilight value) { this.hilight = value; }
        public void Add(FilterOption.AndOr value) { this.andOr = value; }
        public void Add(FilterOption.ObjectType value)
        {
            if (value.IsMatch(FilterOption.ObjectType.All))
                this.objectTypes.Clear();
            else
                this.objectTypes.RemoveAll(p => p.IsMatch(FilterOption.ObjectType.All));

            this.objectTypes.Add(value);
        }

        private object[] ToObjectArray()
        {
            List<object> args = new List<object>();
            args.Add(this.layerName.Value);
            args.Add(this.color.Value);
            args.Add(this.lineType.Value);
            args.Add(this.byLayer.Value);
            args.Add(this.hilight.Value);
            args.Add(this.andOr.Value);
            foreach (var type in this.objectTypes)
            {
                args.Add(type.Value);
            }

            return args.ToArray();
        }

        public List<int> Execute()
        {
            Result<int[]> result = AutoCad.Db.Utility.DoUtil<int[], object[]>("フィルター選択", this.ToObjectArray());
            if (!result.Success)
                return new List<int>();

            return new List<int>(result.Value);
        }
    }
}
