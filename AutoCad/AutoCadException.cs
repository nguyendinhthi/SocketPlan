using System;

namespace Edsa.AutoCadProxy
{
    public class AutoCadException : Exception
    {
        public AutoCadException(string message)
            : base(message)
        {
        }
    }
}
