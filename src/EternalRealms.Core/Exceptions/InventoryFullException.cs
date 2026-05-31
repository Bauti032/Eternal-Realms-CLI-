using System;

namespace EternalRealms.Core.Exceptions
{
    public sealed class InventoryFullException : Exception
    {
        public InventoryFullException()
            : base("El inventario está lleno.")
        {
        }
    }
}

