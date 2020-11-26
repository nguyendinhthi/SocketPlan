using System;
using System.Collections.Generic;
using System.Text;

namespace Edsa.AutoCadProxy
{
    public class Hatch : Entity
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbHatch; } }
    }
}
