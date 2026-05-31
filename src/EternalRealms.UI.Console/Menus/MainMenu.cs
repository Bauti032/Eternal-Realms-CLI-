using System;
using Spectre.Console;
using EternalRealms.Application.Handlers;
using EternalRealms.Application.Commands;
using EternalRealms.Application.DTOs;
using EternalRealms.Application.Interfaces;
using EternalRealms.Core.Entities;
using EternalRealms.UI.Console.Prompts;
using EternalRealms.UI.Console.Rendering;
using EternalRealms.UI.Console.Screens;

namespace EternalRealms.UI.Console.Menus
{
    public sealed class MainMenu
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly CreateCharacterHandler _createCharacterHandler;
        private readonly SaveGameHandler _saveGameHandler;
        private readonly LoadGameHandler _loadGameHandler;
        private readonly CharacterCreationPrompt _creationPrompt;
        private readonly SaveGamePrompt _saveGamePrompt;
        private readonly CharacterScreen _characterScreen;
        private readonly CombatScreen _combatScreen;
        private readonly InventoryMenu _inventoryMenu;
        private readonly QuestMenu _questMenu;
        private readonly PauseMenu _pauseMenu;
        private readonly Application.Interfaces.ILogger _logger;

        public MainMenu(
            ICharacterRepository characterRepository,
            CreateCharacterHandler createCharacterHandler,
            SaveGameHandler saveGameHandler,
            LoadGameHandler loadGameHandler,
            CharacterCreationPrompt creationPrompt,
            SaveGamePrompt saveGamePrompt,
            CharacterScreen characterScreen,
            CombatScreen combatScreen,
            InventoryMenu inventoryMenu,
            QuestMenu questMenu,
            PauseMenu pauseMenu,
            Application.Interfaces.ILogger logger)
        {
            _characterRepository = characterRepository;
            _createCharacterHandler = createCharacterHandler;
            _saveGameHandler = saveGameHandler;
            _loadGameHandler = loadGameHandler;
            _creationPrompt = creationPrompt;
            _saveGamePrompt = saveGamePrompt;
            _characterScreen = characterScreen;
            _combatScreen = combatScreen;
            _inventoryMenu = inventoryMenu;
            _questMenu = questMenu;
            _pauseMenu = pauseMenu;
            _logger = logger;
        }

        public void Run()
        {
            while (true)
            {
                var activeCharacter = _characterRepository.LoadActive();
                var items = new SelectionPrompt<string>()
                    .Title("[bold]Menú principal de Eternal Realms[/]")
                    .PageSize(10)
                    .AddChoices(
                        "Crear personaje",
                        "Ver personaje",
                        "Combate",
                        "Inventario",
                        "Misiones",
                        "Guardar partida",
                        "Cargar partida",
                        "Salir");

                var choice = AnsiConsole.Prompt(items);
                switch (choice)
                {
                    case "Crear personaje":
                        CreateCharacter();
                        break;
                    case "Ver personaje":
                        _characterScreen.Show();
                        break;
                    case "Combate":
                        if (activeCharacter is null)
                        {
                            AnsiConsole.MarkupLine("[red]Primero debes crear un personaje.[/]");
                        }
                        else
                        {
                            _combatScreen.StartCombat();
                        }

                        _pauseMenu.Wait();
                        break;
                    case "Inventario":
                        _inventoryMenu.Show();
                        break;
                    case "Misiones":
                        _questMenu.Show();
                        break;
                    case "Guardar partida":
                        if (activeCharacter is null)
                        {
                            AnsiConsole.MarkupLine("[red]No hay ningún personaje activo para guardar.[/]");
                            _pauseMenu.Wait();
                        }
                        else
                        {
                            SaveGame();
                        }

                        break;
                    case "Cargar partida":
                        LoadGame();
                        break;
                    case "Salir":
                        AnsiConsole.MarkupLine("[green]Gracias por jugar Eternal Realms CLI.[/]");
                        return;
                }
            }
        }

        private void CreateCharacter()
        {
            var command = _creationPrompt.Prompt();
            try
            {
                var character = _createCharacterHandler.Handle(command);
                AnsiConsole.MarkupLine($"[green]Personaje '{character.Name}' creado. Nivel {character.Level}.[/]");
                _logger.Info($"Personaje creado: {character.Name} ({character.CharacterClass})");
            }
            catch (Exception exception)
            {
                AnsiConsole.MarkupLine($"[red]No se pudo crear el personaje: {exception.Message}[/]");
                _logger.Error("Error al crear personaje.", exception);
            }

            _pauseMenu.Wait();
        }

        private void SaveGame()
        {
            var command = _saveGamePrompt.Prompt();
            try
            {
                _saveGameHandler.Handle(command);
                AnsiConsole.MarkupLine("[green]Partida guardada con éxito.[/]");
                _logger.Info($"Partida guardada en '{command.Path}'.");
            }
            catch (Exception exception)
            {
                AnsiConsole.MarkupLine($"[red]Error guardando la partida: {exception.Message}[/]");
                _logger.Error("Error guardando partida.", exception);
            }

            _pauseMenu.Wait();
        }

        private void LoadGame()
        {
            var saveCommand = _saveGamePrompt.Prompt();
            var command = new LoadGameCommand(saveCommand.Path);
            try
            {
                var save = _loadGameHandler.Handle(command);
                AnsiConsole.MarkupLine($"[green]Partida cargada: {save.Character.Name}[/]");
                _logger.Info($"Partida cargada desde '{command.Path}'.");
            }
            catch (Exception exception)
            {
                AnsiConsole.MarkupLine($"[red]No se pudo cargar la partida: {exception.Message}[/]");
                _logger.Error("Error cargando partida.", exception);
            }

            _pauseMenu.Wait();
        }
    }
}
