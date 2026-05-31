using Spectre.Console;
using EternalRealms.Application.Handlers;
using EternalRealms.Application.Validators;
using EternalRealms.Core.Interfaces;
using EternalRealms.Core.Services;
using EternalRealms.Infrastructure.Events;
using EternalRealms.Infrastructure.Logging;
using EternalRealms.Infrastructure.Persistence;
using EternalRealms.Infrastructure.Repositories;
using EternalRealms.Infrastructure.Serialization;
using EternalRealms.UI.Console.Menus;
using EternalRealms.UI.Console.Prompts;
using EternalRealms.UI.Console.Rendering;
using EternalRealms.UI.Console.Screens;

var fileStorage = new FileStorageService();
var jsonSerializer = new JsonSerializerService();
var eventBus = new DomainEventBus();

var characterRepository = new CharacterRepository();
var questRepository = new QuestRepository();
var saveGameRepository = new SaveGameRepository(fileStorage, jsonSerializer);
var logger = new FileLogger(fileStorage, "logs/game.log");

var combatService = new CombatService(eventBus);
var lootService = new LootService();
var questService = new QuestService(eventBus);
var levelService = new LevelService(eventBus);

var characterValidator = new CharacterValidator();
var inventoryValidator = new InventoryValidator();
var questValidator = new QuestValidator();
var saveGameValidator = new SaveGameValidator();

var createCharacterHandler = new CreateCharacterHandler(characterRepository, characterValidator);
var attackHandler = new AttackHandler(characterRepository, combatService, lootService, levelService);
var equipItemHandler = new EquipItemHandler(characterRepository);
var useItemHandler = new UseItemHandler(characterRepository);
var acceptQuestHandler = new AcceptQuestHandler(characterRepository, questRepository, questService);
var completeQuestHandler = new CompleteQuestHandler(characterRepository, questRepository, questService);
var saveGameHandler = new SaveGameHandler(characterRepository, questRepository, saveGameRepository);
var loadGameHandler = new LoadGameHandler(saveGameRepository, characterRepository);

var characterCreationPrompt = new CharacterCreationPrompt();
var saveGamePrompt = new SaveGamePrompt();

var characterRenderer = new CharacterRenderer();
var inventoryRenderer = new InventoryRenderer();
var combatRenderer = new CombatRenderer();
var questRenderer = new QuestRenderer();

var characterScreen = new CharacterScreen(characterRepository, characterRenderer);
var combatScreen = new CombatScreen(characterRepository, attackHandler, combatRenderer);
var inventoryMenu = new InventoryMenu(characterRepository, inventoryRenderer, equipItemHandler, useItemHandler);
var questMenu = new QuestMenu(characterRepository, questRepository, questRenderer, acceptQuestHandler, completeQuestHandler);
var pauseMenu = new PauseMenu();

var mainMenu = new MainMenu(
    characterRepository,
    createCharacterHandler,
    saveGameHandler,
    loadGameHandler,
    characterCreationPrompt,
    saveGamePrompt,
    characterScreen,
    combatScreen,
    inventoryMenu,
    questMenu,
    pauseMenu,
    logger);

AnsiConsole.MarkupLine("[green]Bienvenido a Eternal Realms CLI.[/]");
mainMenu.Run();

