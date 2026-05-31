using EternalRealms.Core.Enums;

namespace EternalRealms.Application.Commands
{
    public sealed record CreateCharacterCommand(string Name, CharacterClass CharacterClass);
}

