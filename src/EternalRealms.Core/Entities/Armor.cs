using EternalRealms.Core.Enums;

namespace EternalRealms.Core.Entities
{
    public sealed class Armor : Item
    {
        public Armor(string name, ItemRarity rarity, decimal weight, EquipmentSlot slot, int defense)
            : base(name, rarity, weight)
        {
            Slot = slot;
            Defense = defense;
        }

        public EquipmentSlot Slot { get; init; }
        public int Defense { get; init; }
    }
}

