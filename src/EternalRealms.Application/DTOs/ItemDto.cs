using EternalRealms.Core.Enums;
using EternalRealms.Core.ValueObjects;

namespace EternalRealms.Application.DTOs
{
    public sealed class ItemDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public ItemRarity Rarity { get; init; }
        public decimal Weight { get; init; }
        public string ItemType { get; init; } = string.Empty;
        public Damage? Damage { get; init; }
        public EquipmentSlot? Slot { get; init; }
        public int? Defense { get; init; }
        public int? HealthRestoration { get; init; }
        public int? ManaRestoration { get; init; }
    }
}

