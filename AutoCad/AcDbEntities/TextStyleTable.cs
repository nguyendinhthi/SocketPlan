namespace Edsa.AutoCadProxy
{
    public class TextStyleTable : SymbolTable
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbTextStyleTable; } }

        public bool Exist(string styleName)
        {
            Result<short> result = this.DoTable<short, string>("所有？", styleName);
            if (!result.Success)
                throw new AutoCadException("Failed to get text style.");

            return result.Value == 1;
        }

        public int Add(string styleName)
        {
            Result<int> result = this.DoTable<int, string>("追加", styleName);
            if (!result.Success)
                throw new AutoCadException("Failed to add text style.");

            return result.Value;
        }

        public int GetId(string styleName)
        {
            Result<int> result = this.DoTable<int, string>("名前から取得", styleName);
            if (!result.Success)
                throw new AutoCadException("Failed to get text style.");

            return result.Value;
        }
    }
}
