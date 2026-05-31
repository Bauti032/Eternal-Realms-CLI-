using EternalRealms.Core.Enums;
using EternalRealms.Core.ValueObjects;

namespace EternalRealms.Core.Entities
{
    public sealed class Weapon : Item
    {
        public Weapon(string name, ItemRarity rarity, decimal weight, Damage damage)
            : base(name, rarity, weight)
        {
            Damage = damage;
            Slot = EquipmentSlot.Weapon;
        }

        public Damage Damage { get; init; }
        public EquipmentSlot Slot { get; }
    }
}

