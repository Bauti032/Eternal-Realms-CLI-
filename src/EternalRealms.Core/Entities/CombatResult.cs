namespace EternalRealms.Core.Entities
{
    public sealed record CombatResult(
        int DamageDealt,
        bool IsEnemyDefeated,
        bool IsPlayerDefeated,
        int RemainingPlayerHealth,
        int RemainingEnemyHealth);
}
