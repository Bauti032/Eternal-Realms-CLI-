using System;
using System.Collections.Generic;
using Spectre.Console;
using EternalRealms.Application.Commands;
using EternalRealms.Application.Handlers;
using EternalRealms.Application.Interfaces;
using EternalRealms.Application.DTOs;
using EternalRealms.Core.Entities;
using EternalRealms.Core.Enums;
using EternalRealms.Core.ValueObjects;
using EternalRealms.UI.Console.Rendering;

namespace EternalRealms.UI.Console.Screens
{
    public sealed class CombatScreen
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly AttackHandler _attackHandler;
        private readonly CombatRenderer _combatRenderer;

        public CombatScreen(
            ICharacterRepository characterRepository,
            AttackHandler attackHandler,
            CombatRenderer combatRenderer)
        {
            _characterRepository = characterRepository;
            _attackHandler = attackHandler;
            _combatRenderer = combatRenderer;
        }

        public void StartCombat()
        {
            var character = _characterRepository.LoadActive();
            if (character is null)
            {
                AnsiConsole.MarkupLine("[red]No hay personaje activo. Crea uno primero.[/]");
                return;
            }

            var enemy = BuildEnemy(character.Level);
            var result = _attackHandler.Handle(new AttackCommand(character.Id, enemy));
            _combatRenderer.Render(result);
        }

        private static EnemyDto BuildEnemy(int playerLevel)
        {
            var level = Math.Max(1, playerLevel);
            return new EnemyDto
            {
                Id = Guid.NewGuid(),
                Name = "Goblin de la mazmorra",
                Type = EnemyType.Beast,
                Level = level,
                Health = Health.Create(20 + level * 5),
                Damage = new Damage(4 + level, DamageType.Physical),
                Reward = new Reward(new Experience(10 + level * 2), new Gold(15 + level * 3)),
                LootItems = new List<ItemDto>
                {
                    new ItemDto
                    {
                        Id = Guid.NewGuid(),
                        Name = "Poción de curación",
                        ItemType = nameof(Consumable),
                        Rarity = Core.Enums.ItemRarity.Common,
                        Weight = 0.5m,
                        HealthRestoration = 15,
                        ManaRestoration = 0
                    }
                }
            };
        }
    }
}
