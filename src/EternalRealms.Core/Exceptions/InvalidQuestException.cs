using System;

namespace EternalRealms.Core.Exceptions
{
    public sealed class InvalidQuestException : Exception
    {
        public InvalidQuestException(string message)
            : base(message)
        {
        }
    }
}

