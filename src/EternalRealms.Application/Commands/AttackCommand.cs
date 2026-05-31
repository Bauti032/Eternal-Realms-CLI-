using EternalRealms.Application.DTOs;

namespace EternalRealms.Application.Commands
{
    public sealed record AttackCommand(Guid CharacterId, EnemyDto Enemy);
}

