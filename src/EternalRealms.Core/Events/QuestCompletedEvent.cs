using EternalRealms.Core.ValueObjects;

namespace EternalRealms.Core.Events
{
    public sealed record QuestCompletedEvent(Guid CharacterId, Guid QuestId, Reward Reward);
}

