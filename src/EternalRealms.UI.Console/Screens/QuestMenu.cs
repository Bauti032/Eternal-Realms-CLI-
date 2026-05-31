using System;
using System.Linq;
using Spectre.Console;
using EternalRealms.Application.Commands;
using EternalRealms.Application.Handlers;
using EternalRealms.Application.Interfaces;
using EternalRealms.Application.DTOs;
using EternalRealms.UI.Console.Rendering;

namespace EternalRealms.UI.Console.Screens
{
    public sealed class QuestMenu
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IQuestRepository _questRepository;
        private readonly QuestRenderer _questRenderer;
        private readonly AcceptQuestHandler _acceptQuestHandler;
        private readonly CompleteQuestHandler _completeQuestHandler;

        public QuestMenu(
            ICharacterRepository characterRepository,
            IQuestRepository questRepository,
            QuestRenderer questRenderer,
            AcceptQuestHandler acceptQuestHandler,
            CompleteQuestHandler completeQuestHandler)
        {
            _characterRepository = characterRepository;
            _questRepository = questRepository;
            _questRenderer = questRenderer;
            _acceptQuestHandler = acceptQuestHandler;
            _completeQuestHandler = completeQuestHandler;
        }

        public void Show()
        {
            var character = _characterRepository.LoadActive();
            if (character is null)
            {
                AnsiConsole.MarkupLine("[red]No hay personaje activo. Crea uno primero.[/]");
                return;
            }

            if (character.ActiveQuest is not null)
            {
                _questRenderer.Render(MapQuest(character.ActiveQuest));
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]No hay misión activa actualmente.[/]");
            }

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Qué deseas hacer con las misiones?")
                    .AddChoices("Ver misiones disponibles", "Aceptar misión", "Completar misión", "Volver"));

            switch (choice)
            {
                case "Ver misiones disponibles":
                    ShowAvailableQuests();
                    break;
                case "Aceptar misión":
                    AcceptQuest(character);
                    break;
                case "Completar misión":
                    CompleteQuest(character);
                    break;
            }
        }

        private void ShowAvailableQuests()
        {
            var availableQuests = _questRepository.GetAvailable().ToList();
            if (!availableQuests.Any())
            {
                AnsiConsole.MarkupLine("[grey]No hay misiones disponibles en este momento.[/]");
                return;
            }

            var table = new Table().AddColumn("Título").AddColumn("Requisito de nivel").AddColumn("Recompensa");
            foreach (var quest in availableQuests)
            {
                table.AddRow(quest.Title, quest.RequiredLevel.ToString(), $"{quest.Reward.Experience.Amount} XP, {quest.Reward.Gold.Amount} oro");
            }

            AnsiConsole.Write(new Panel(table).Header("Misiones disponibles", Justify.Center));
        }

        private void AcceptQuest(Core.Entities.Character character)
        {
            var availableQuests = _questRepository.GetAvailable().ToList();
            if (!availableQuests.Any())
            {
                AnsiConsole.MarkupLine("[yellow]No hay misiones disponibles para aceptar.[/]");
                return;
            }

            var quest = AnsiConsole.Prompt(
                new SelectionPrompt<Core.Entities.Quest>()
                    .Title("Selecciona la misión que deseas aceptar:")
                    .AddChoices(availableQuests));

            try
            {
                _acceptQuestHandler.Handle(new AcceptQuestCommand(character.Id, quest.Id));
                AnsiConsole.MarkupLine($"[green]Misión '{quest.Title}' aceptada.[/]");
            }
            catch (Exception exception)
            {
                AnsiConsole.MarkupLine($"[red]No se pudo aceptar la misión: {exception.Message}[/]");
            }
        }

        private void CompleteQuest(Core.Entities.Character character)
        {
            if (character.ActiveQuest is null)
            {
                AnsiConsole.MarkupLine("[yellow]No hay ninguna misión activa para completar.[/]");
                return;
            }

            try
            {
                _completeQuestHandler.Handle(new CompleteQuestCommand(character.Id, character.ActiveQuest.Id));
                AnsiConsole.MarkupLine($"[green]Misión '{character.ActiveQuest.Title}' completada.[/]");
            }
            catch (Exception exception)
            {
                AnsiConsole.MarkupLine($"[red]No se pudo completar la misión: {exception.Message}[/]");
            }
        }

        private static QuestDto MapQuest(Core.Entities.Quest quest)
        {
            return new QuestDto
            {
                Id = quest.Id,
                Title = quest.Title,
                Description = quest.Description,
                Status = quest.Status,
                RequiredLevel = quest.RequiredLevel,
                Reward = quest.Reward
            };
        }
    }
}
