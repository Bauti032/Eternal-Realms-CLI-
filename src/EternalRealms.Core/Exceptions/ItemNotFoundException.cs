using System;

namespace EternalRealms.Core.Exceptions
{
    public sealed class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string itemName)
            : base($"El ítem '{itemName}' no se encontró en el inventario.")
        {
        }
    }
}

