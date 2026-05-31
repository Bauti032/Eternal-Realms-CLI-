using System.Linq;
using Spectre.Console;
using EternalRealms.Application.DTOs;
using EternalRealms.Application.Interfaces;
using EternalRealms.Core.Entities;
using EternalRealms.UI.Console.Rendering;

namespace EternalRealms.UI.Console.Screens
{
    public sealed class CharacterScreen
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly CharacterRenderer _characterRenderer;

        public CharacterScreen(ICharacterRepository characterRepository, CharacterRenderer characterRenderer)
        {
            _characterRepository = characterRepository;
            _characterRenderer = characterRenderer;
        }

        public void Show()
        {
            var character = _characterRepository.LoadActive();
            if (character is null)
            {
                AnsiConsole.MarkupLine("[red]No hay personaje activo. Crea uno primero.[/]");
                return;
            }

            var dto = new CharacterDto
            {
                Id = character.Id,
                Name = character.Name,
                CharacterClass = character.CharacterClass,
                Level = character.Level,
                Stats = character.Stats,
                Health = character.Health,
                Mana = character.Mana,
                Gold = character.Gold,
                Experience = character.Experience,
                ActiveQuest = character.ActiveQuest is null ? null : new QuestDto
                {
                    Id = character.ActiveQuest.Id,
                    Title = character.ActiveQuest.Title,
                    Description = character.ActiveQuest.Description,
                    Status = character.ActiveQuest.Status,
                    RequiredLevel = character.ActiveQuest.RequiredLevel,
                    Reward = character.ActiveQuest.Reward
                }
            };

            foreach (var item in character.Inventory.Items)
            {
                dto.Inventory.Add(new ItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Rarity = item.Rarity,
                    Weight = item.Weight,
                    ItemType = item.GetType().Name,
                    Damage = item is Weapon weapon ? weapon.Damage : null,
                    Slot = item is Armor armor ? armor.Slot : null,
                    Defense = item is Armor armorItem ? armorItem.Defense : null,
                    HealthRestoration = item is Consumable consumable ? consumable.HealthRestoration : null,
                    ManaRestoration = item is Consumable consumableItem ? consumableItem.ManaRestoration : null
                });
            }

            foreach (var equipment in character.Equipment.Slots)
            {
                dto.Equipment[equipment.Key] = equipment.Value is null ? null : new ItemDto
                {
                    Id = equipment.Value.Id,
                    Name = equipment.Value.Name,
                    Rarity = equipment.Value.Rarity,
                    Weight = equipment.Value.Weight,
                    ItemType = equipment.Value.GetType().Name,
                    Damage = equipment.Value is Weapon weapon ? weapon.Damage : null,
                    Slot = equipment.Value is Armor armor ? armor.Slot : null,
                    Defense = equipment.Value is Armor armorItem ? armorItem.Defense : null,
                    HealthRestoration = equipment.Value is Consumable consumable ? consumable.HealthRestoration : null,
                    ManaRestoration = equipment.Value is Consumable consumableItem ? consumableItem.ManaRestoration : null
                };
            }

            _characterRenderer.Render(dto);
        }
    }
}
