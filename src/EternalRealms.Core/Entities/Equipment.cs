using System.Collections.Generic;
using System.Linq;
using EternalRealms.Core.Enums;
using EternalRealms.Core.Exceptions;

namespace EternalRealms.Core.Entities
{
    public sealed class Equipment
    {
        private readonly Dictionary<EquipmentSlot, Item?> _slots;

        public Equipment()
        {
            _slots = System.Enum.GetValues<EquipmentSlot>()
                .Cast<EquipmentSlot>()
                .ToDictionary(slot => slot, slot => (Item?)null);
        }

        public IReadOnlyDictionary<EquipmentSlot, Item?> Slots => _slots;

        public Item? GetEquippedItem(EquipmentSlot slot) => _slots[slot];

        public void Equip(Item item)
        {
            switch (item)
            {
                case Weapon weapon:
                    _slots[weapon.Slot] = weapon;
                    break;
                case Armor armor:
                    _slots[armor.Slot] = armor;
                    break;
                default:
                    throw new InvalidEquipmentException("Solo se pueden equipar armas o armaduras.");
            }
        }

        public Item? Unequip(EquipmentSlot slot)
        {
            if (!_slots.ContainsKey(slot))
            {
                return null;
            }

            var item = _slots[slot];
            _slots[slot] = null;
            return item;
        }
    }
}
