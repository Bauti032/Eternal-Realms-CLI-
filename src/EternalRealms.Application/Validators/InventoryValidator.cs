using EternalRealms.Core.Entities;

namespace EternalRealms.Application.Validators
{
    public sealed class InventoryValidator
    {
        public bool CanAddItem(Inventory inventory)
        {
            return !inventory.IsFull;
        }
    }
}

