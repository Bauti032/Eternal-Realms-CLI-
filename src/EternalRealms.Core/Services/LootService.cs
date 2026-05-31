using System;
using System.Collections.Generic;
using System.Linq;
using EternalRealms.Core.Entities;
using EternalRealms.Core.Interfaces;
using EternalRealms.Core.ValueObjects;

namespace EternalRealms.Core.Services
{
    public sealed class LootService : ILootService
    {
        private readonly Random _random = new();

        public IReadOnlyCollection<Item> GenerateLoot(LootTable lootTable)
        {
            if (lootTable.Items.Count == 0)
            {
                return Array.Empty<Item>();
            }

            var dropCount = Math.Max(1, _random.Next(1, Math.Min(lootTable.Items.Count, 3) + 1));
            return lootTable.Items.OrderBy(_ => _random.Next()).Take(dropCount).ToList();
        }

        public Reward GenerateReward(LootTable lootTable) => lootTable.Reward;
    }
}

