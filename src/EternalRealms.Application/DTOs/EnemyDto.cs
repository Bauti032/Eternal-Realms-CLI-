using System.Collections.Generic;
using EternalRealms.Core.Enums;
using EternalRealms.Core.ValueObjects;

namespace EternalRealms.Application.DTOs
{
    public sealed class EnemyDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public EnemyType Type { get; init; }
        public int Level { get; init; }
        public Health Health { get; init; } = default!;
        public Damage Damage { get; init; } = default!;
        public Reward Reward { get; init; } = default!;
        public List<ItemDto> LootItems { get; init; } = new();
    }
}

