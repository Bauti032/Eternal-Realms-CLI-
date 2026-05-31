using System;
using System.Linq;
using EternalRealms.Application.Commands;
using EternalRealms.Application.DTOs;
using EternalRealms.Application.Interfaces;
using EternalRealms.Core.Entities;
using EternalRealms.Core.Interfaces;
using EternalRealms.Core.ValueObjects;

namespace EternalRealms.Application.Handlers
{
    public sealed class AttackHandler
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly ICombatService _combatService;
        private readonly ILootService _lootService;
        private readonly ILevelService _levelService;

        public AttackHandler(
            ICharacterRepository characterRepository,
            ICombatService combatService,
            ILootService lootService,
            ILevelService levelService)
        {
            _characterRepository = characterRepository;
            _combatService = combatService;
            _lootService = lootService;
            _levelService = levelService;
        }

        public CombatResultDto Handle(AttackCommand command)
        {
            var character = _characterRepository.Load(command.CharacterId) ?? throw new InvalidOperationException("Character not found.");
            var enemy = MapEnemy(command.Enemy);
            var result = _combatService.ExecuteAttack(character, enemy);

            if (result.IsEnemyDefeated)
            {
                foreach (var lootItem in _lootService.GenerateLoot(enemy.LootTable))
                {
                    try
                    {
                        character.Inventory.AddItem(lootItem);
                    }
                    catch
                    {
                        break;
                    }
                }

                character.AddGold(enemy.LootTable.Reward.Gold);
                character.AddExperience(enemy.LootTable.Reward.Experience);

                if (_levelService.ShouldLevelUp(character))
                {
                    _levelService.ApplyLevelUp(character);
                }
            }

            _characterRepository.Save(character);
            return new CombatResultDto
            {
                DamageDealt = result.DamageDealt,
                IsEnemyDefeated = result.IsEnemyDefeated,
                IsPlayerDefeated = result.IsPlayerDefeated,
                RemainingPlayerHealth = result.RemainingPlayerHealth,
                RemainingEnemyHealth = result.RemainingEnemyHealth
            };
        }

        private static Enemy MapEnemy(EnemyDto dto)
        {
            var items = dto.LootItems.Select(MapItem).ToList();
            var lootTable = new LootTable(items, dto.Reward);
            return new Enemy(dto.Id, dto.Name, dto.Type, dto.Level, dto.Health, dto.Damage, lootTable);
        }

        private static Item MapItem(ItemDto dto)
        {
            return dto.ItemType switch
            {
                nameof(Weapon) => new Weapon(dto.Name, dto.Rarity, dto.Weight, dto.Damage ?? new Damage(0, Core.Enums.DamageType.Physical)),
                nameof(Armor) => new Armor(dto.Name, dto.Rarity, dto.Weight, dto.Slot ?? default, dto.Defense ?? 0),
                nameof(Consumable) => new Consumable(dto.Name, dto.Rarity, dto.Weight, dto.HealthRestoration ?? 0, dto.ManaRestoration ?? 0),
                _ => new Weapon(dto.Name, dto.Rarity, dto.Weight, dto.Damage ?? new Damage(0, Core.Enums.DamageType.Physical)),
            };
        }
    }
}

