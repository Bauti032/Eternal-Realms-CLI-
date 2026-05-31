using EternalRealms.Core.ValueObjects;

namespace EternalRealms.Core.Events
{
    public sealed record EnemyKilledEvent(Guid EnemyId, string EnemyName, Reward Reward);
}

