namespace EternalRealms.Application.DTOs
{
    public sealed class CombatResultDto
    {
        public int DamageDealt { get; init; }
        public bool IsEnemyDefeated { get; init; }
        public bool IsPlayerDefeated { get; init; }
        public int RemainingPlayerHealth { get; init; }
        public int RemainingEnemyHealth { get; init; }
    }
}

