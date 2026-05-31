using EternalRealms.Core.Enums;

namespace EternalRealms.Core.Events
{
    public sealed record PlayerCreatedEvent(Guid CharacterId, string Name, CharacterClass CharacterClass);
}

