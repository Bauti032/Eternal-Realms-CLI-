using EternalRealms.Core.Entities;
using EternalRealms.Core.Events;
using EternalRealms.Core.Exceptions;
using EternalRealms.Core.Interfaces;

namespace EternalRealms.Core.Services
{
    public sealed class CombatService : ICombatService
    {
        private readonly IEventBus _eventBus;

        public CombatService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public CombatResult ExecuteAttack(Character attacker, Enemy defender)
        {
            if (!attacker.IsAlive)
            {
                throw new CharacterDeadException();
            }

            defender.TakeDamage(attacker.AttackPower);
            var enemyDefeated = defender.IsDefeated;
            var damageDealt = attacker.AttackPower;
            var playerDefeated = false;

            if (enemyDefeated)
            {
                _eventBus.Publish(new EnemyKilledEvent(defender.Id, defender.Name, defender.LootTable.Reward));
            }
            else
            {
                try
                {
                    attacker.TakeDamage(defender.Damage.Value);
                }
                catch (CharacterDeadException)
                {
                    playerDefeated = true;
                    _eventBus.Publish(new PlayerDiedEvent(attacker.Id, $"Recibió {defender.Damage.Value} puntos de daño."));
                }
            }

            return new CombatResult(
                damageDealt,
                IsEnemyDefeated: enemyDefeated,
                IsPlayerDefeated: playerDefeated,
                RemainingPlayerHealth: attacker.Health.Current,
                RemainingEnemyHealth: defender.Health.Current);
        }
    }
}

