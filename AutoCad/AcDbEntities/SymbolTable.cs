namespace Edsa.AutoCadProxy
{
    public class SymbolTable : Object
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbSymbolTable; } }

        protected Result<G> DoTable<G>(string method)
        {
            object setData = null;
            object getData = null;

            bool isSuccess = AutoCad.vbcom.Table(this.ObjectType.ToString(), method, ref setData, ref getData);

            if (getData == null)
                return new Result<G>(isSuccess, default(G));

            return new Result<G>(isSuccess, (G)getData);
        }

        protected Result<G> DoTable<G, S>(string method, S setData)
        {
            object thisSetData = (object)setData;
            object getData = null;

            bool isSuccess = AutoCad.vbcom.Table(this.ObjectType.ToString(), method, ref thisSetData, ref getData);

            if (getData == null)
                return new Result<G>(isSuccess, default(G));

            return new Result<G>(isSuccess, (G)getData);
        }
    }
}