namespace Edsa.AutoCadProxy
{
    public class RegAppTable : SymbolTable
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbRegAppTable; } }

        public new void Make(string applicationTableName)
        {
            if (!this.Exist(applicationTableName))
                this.Add(applicationTableName);
        }

        private bool Exist(string appName)
        {
            var result = this.DoTable<short, string>("所有？", appName);
            if (!result.Success)
                throw new AutoCadException("Failed to check that application table exists.");

            return result.Value == 1;
        }

        private void Add(string appName)
        {
            var result = this.DoTable<int, string>("追加", appName);
            if (!result.Success)
                throw new AutoCadException("Failed to add application table.");
        }
    }
}
