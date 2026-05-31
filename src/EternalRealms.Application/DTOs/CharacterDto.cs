using System.Collections.Generic;
using EternalRealms.Core.Enums;
using EternalRealms.Core.ValueObjects;

namespace EternalRealms.Application.DTOs
{
    public sealed class CharacterDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public CharacterClass CharacterClass { get; init; }
        public int Level { get; init; }
        public Stats Stats { get; init; } = default!;
        public Health Health { get; init; } = default!;
        public Mana Mana { get; init; } = default!;
        public Gold Gold { get; init; } = default!;
        public Experience Experience { get; init; } = default!;
        public List<ItemDto> Inventory { get; init; } = new();
        public Dictionary<EquipmentSlot, ItemDto?> Equipment { get; init; } = new();
        public QuestDto? ActiveQuest { get; init; }
    }
}

