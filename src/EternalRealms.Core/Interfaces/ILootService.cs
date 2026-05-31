using System.Collections.Generic;
using EternalRealms.Core.Entities;
using EternalRealms.Core.ValueObjects;

namespace EternalRealms.Core.Interfaces
{
    public interface ILootService
    {
        IReadOnlyCollection<Item> GenerateLoot(LootTable lootTable);
        Reward GenerateReward(LootTable lootTable);
    }
}

