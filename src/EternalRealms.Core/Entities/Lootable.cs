namespace EternalRealms.Core.Entities
{
    public abstract class Lootable
    {
        protected Lootable(LootTable lootTable)
        {
            LootTable = lootTable;
        }

        public LootTable LootTable { get; init; }
    }
}

