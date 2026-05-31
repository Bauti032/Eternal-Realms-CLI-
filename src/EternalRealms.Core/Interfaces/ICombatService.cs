using EternalRealms.Core.Entities;

namespace EternalRealms.Core.Interfaces
{
    public interface ICombatService
    {
        CombatResult ExecuteAttack(Character attacker, Enemy defender);
    }
}

