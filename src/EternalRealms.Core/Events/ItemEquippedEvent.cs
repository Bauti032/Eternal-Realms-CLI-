using EternalRealms.Core.Entities;
using EternalRealms.Core.Enums;

namespace EternalRealms.Core.Events
{
    public sealed record ItemEquippedEvent(Guid CharacterId, Item Item, EquipmentSlot Slot);
}

