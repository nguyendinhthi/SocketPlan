namespace Edsa.AutoCadProxy
{
    public class AbstractViewportTable : SymbolTable
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbAbstractViewportTable; } }
    }
}
