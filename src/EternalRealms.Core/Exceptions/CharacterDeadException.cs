using System;

namespace EternalRealms.Core.Exceptions
{
    public sealed class CharacterDeadException : Exception
    {
        public CharacterDeadException()
            : base("El personaje está muerto y no puede realizar esta acción.")
        {
        }
    }
}

