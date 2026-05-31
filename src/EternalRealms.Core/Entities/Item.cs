using EternalRealms.Core.Enums;

namespace EternalRealms.Core.Entities
{
    public abstract class Item
    {
        protected Item(string name, ItemRarity rarity, decimal weight)
        {
            Id = Guid.NewGuid();
            Name = name;
            Rarity = rarity;
            Weight = weight;
        }

        public Guid Id { get; init; }
        public string Name { get; init; }
        public ItemRarity Rarity { get; init; }
        public decimal Weight { get; init; }
    }
}

