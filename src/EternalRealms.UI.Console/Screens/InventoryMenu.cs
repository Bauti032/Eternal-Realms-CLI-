using System;
using System.Linq;
using Spectre.Console;
using EternalRealms.Application.Commands;
using EternalRealms.Application.Handlers;
using EternalRealms.Application.Interfaces;
using EternalRealms.UI.Console.Rendering;

namespace EternalRealms.UI.Console.Screens
{
    public sealed class InventoryMenu
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly InventoryRenderer _inventoryRenderer;
        private readonly EquipItemHandler _equipItemHandler;
        private readonly UseItemHandler _useItemHandler;

        public InventoryMenu(
            ICharacterRepository characterRepository,
            InventoryRenderer inventoryRenderer,
            EquipItemHandler equipItemHandler,
            UseItemHandler useItemHandler)
        {
            _characterRepository = characterRepository;
            _inventoryRenderer = inventoryRenderer;
            _equipItemHandler = equipItemHandler;
            _useItemHandler = useItemHandler;
        }

        public void Show()
        {
            var character = _characterRepository.LoadActive();
            if (character is null)
            {
                AnsiConsole.MarkupLine("[red]No hay personaje activo. Crea uno primero.[/]");
                return;
            }

            var characterDto = MapCharacter(character);
            _inventoryRenderer.Render(characterDto);

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("¿Qué quieres hacer con tu inventario?")
                    .AddChoices("Equipar item", "Usar consumible", "Volver"));

            switch (choice)
            {
                case "Equipar item":
                    EquipItem(character);
                    break;
                case "Usar consumible":
                    UseConsumable(character);
                    break;
            }
        }

        private static Application.DTOs.CharacterDto MapCharacter(Core.Entities.Character character)
        {
            var dto = new Application.DTOs.CharacterDto
            {
                Id = character.Id,
                Name = character.Name,
                CharacterClass = character.CharacterClass,
                Level = character.Level,
                Stats = character.Stats,
                Health = character.Health,
                Mana = character.Mana,
                Gold = character.Gold,
                Experience = character.Experience
            };

            foreach (var item in character.Inventory.Items)
            {
                dto.Inventory.Add(new Application.DTOs.ItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Rarity = item.Rarity,
                    Weight = item.Weight,
                    ItemType = item.GetType().Name,
                    Damage = item is Core.Entities.Weapon weapon ? weapon.Damage : null,
                    Slot = item is Core.Entities.Armor armor ? armor.Slot : null,
                    Defense = item is Core.Entities.Armor armorItem ? armorItem.Defense : null,
                    HealthRestoration = item is Core.Entities.Consumable consumable ? consumable.HealthRestoration : null,
                    ManaRestoration = item is Core.Entities.Consumable consumableItem ? consumableItem.ManaRestoration : null
                });
            }

            return dto;
        }

        private void EquipItem(Core.Entities.Character character)
        {
            var equippableItems = character.Inventory.Items
                .Where(item => item is Core.Entities.Weapon or Core.Entities.Armor)
                .ToList();

            if (!equippableItems.Any())
            {
                AnsiConsole.MarkupLine("[yellow]No tienes ningún arma o armadura para equipar.[/]");
                return;
            }

            var selected = AnsiConsole.Prompt(
                new SelectionPrompt<Core.Entities.Item>()
                    .Title("Selecciona un item para equipar:")
                    .AddChoices(equippableItems));

            try
            {
                _equipItemHandler.Handle(new EquipItemCommand(character.Id, selected.Id));
                AnsiConsole.MarkupLine($"[green]{selected.Name} equipado.[/]");
            }
            catch (Exception exception)
            {
                AnsiConsole.MarkupLine($"[red]No se pudo equipar el item: {exception.Message}[/]");
            }
        }

        private void UseConsumable(Core.Entities.Character character)
        {
            var consumables = character.Inventory.Items
                .OfType<Core.Entities.Consumable>()
                .ToList();

            if (!consumables.Any())
            {
                AnsiConsole.MarkupLine("[yellow]No tienes consumibles disponibles.[/]");
                return;
            }

            var selected = AnsiConsole.Prompt(
                new SelectionPrompt<Core.Entities.Consumable>()
                    .Title("Selecciona un consumible para usar:")
                    .AddChoices(consumables));

            try
            {
                _useItemHandler.Handle(new UseItemCommand(character.Id, selected.Id));
                AnsiConsole.MarkupLine($"[green]{selected.Name} usado.[/]");
            }
            catch (Exception exception)
            {
                AnsiConsole.MarkupLine($"[red]No se pudo usar el consumible: {exception.Message}[/]");
            }
        }
    }
}
