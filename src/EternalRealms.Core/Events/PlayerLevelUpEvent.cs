namespace EternalRealms.Core.Events
{
    public sealed record PlayerLevelUpEvent(Guid CharacterId, int NewLevel);
}

