namespace EternalRealms.Core.Events
{
    public sealed record PlayerDiedEvent(Guid CharacterId, string Reason);
}

