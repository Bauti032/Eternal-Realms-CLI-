using System.Collections.Generic;
using EternalRealms.Core.ValueObjects;

namespace EternalRealms.Core.Entities
{
    public sealed class LootTable
    {
        public LootTable(IEnumerable<Item> items, Reward reward)
        {
            Items = new List<Item>(items);
            Reward = reward;
        }

        public IReadOnlyCollection<Item> Items { get; }
        public Reward Reward { get; }
    }
}
