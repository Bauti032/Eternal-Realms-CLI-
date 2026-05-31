using EternalRealms.Core.Enums;

namespace EternalRealms.Core.Entities
{
    public sealed class Consumable : Item
    {
        public Consumable(string name, ItemRarity rarity, decimal weight, int healthRestoration, int manaRestoration)
            : base(name, rarity, weight)
        {
            HealthRestoration = healthRestoration;
            ManaRestoration = manaRestoration;
        }

        public int HealthRestoration { get; init; }
        public int ManaRestoration { get; init; }
    }
}

