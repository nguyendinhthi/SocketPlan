namespace Edsa.AutoCadProxy
{
    public class AbstractViewTableRecord : SymbolTableRecord
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbAbstractViewTableRecord; } }
    }
}