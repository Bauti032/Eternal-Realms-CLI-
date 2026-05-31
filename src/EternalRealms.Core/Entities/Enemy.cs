using EternalRealms.Core.Enums;
using EternalRealms.Core.ValueObjects;

namespace EternalRealms.Core.Entities
{
    public sealed class Enemy : Lootable
    {
        public Enemy(Guid id, string name, EnemyType type, int level, Health health, Damage damage, LootTable lootTable)
            : base(lootTable)
        {
            Id = id;
            Name = name;
            Type = type;
            Level = level;
            Health = health;
            Damage = damage;
        }

        public Guid Id { get; init; }
        public string Name { get; init; }
        public EnemyType Type { get; init; }
        public int Level { get; init; }
        public Health Health { get; private set; }
        public Damage Damage { get; init; }
        public bool IsDefeated => Health.IsDead;

        public void TakeDamage(int amount)
        {
            Health = Health.TakeDamage(amount);
        }
    }
}

