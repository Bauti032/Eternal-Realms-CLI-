using System;

namespace EternalRealms.Core.Exceptions
{
    public sealed class InvalidEquipmentException : Exception
    {
        public InvalidEquipmentException(string message)
            : base(message)
        {
        }
    }
}

