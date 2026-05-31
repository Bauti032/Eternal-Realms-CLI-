using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using EternalRealms.Application.Commands;
using EternalRealms.Application.DTOs;
using EternalRealms.Application.Handlers;
using EternalRealms.Application.Interfaces;
using EternalRealms.Application.Validators;
using EternalRealms.Core.Entities;
using EternalRealms.Core.Enums;
using EternalRealms.Core.ValueObjects;

namespace EternalRealms.Application.Tests
{
    [TestFixture]
    public class ApplicationHandlerTests
    {
        [Test]
        public void CreateCharacterHandler_ShouldSaveCharacterAndReturnIt()
        {
            var repository = new InMemoryCharacterRepository();
            var handler = new CreateCharacterHandler(repository, new CharacterValidator());
            var command = new CreateCharacterCommand("Maya", CharacterClass.Cleric);

            var character = handler.Handle(command);

            character.Should().NotBeNull();
            character.Name.Should().Be("Maya");
            character.CharacterClass.Should().Be(CharacterClass.Cleric);
            repository.SavedCharacter.Should().BeSameAs(character);
        }

        [Test]
        public void CreateCharacterHandler_ShouldThrow_WhenNameInvalid()
        {
            var repository = new InMemoryCharacterRepository();
            var handler = new CreateCharacterHandler(repository, new CharacterValidator());
            var command = new CreateCharacterCommand("   ", CharacterClass.Rogue);

            Action action = () => handler.Handle(command);

            action.Should().Throw<ArgumentException>().WithMessage("El nombre del personaje no puede estar vacío.*");
            repository.SavedCharacter.Should().BeNull();
        }

        [Test]
        public void SaveGameHandler_ShouldPersistActiveCharacterAndAvailableQuests()
        {
            var character = Character.Create("Thorn", CharacterClass.Warrior);
            var quest = new Quest(Guid.NewGuid(), "Proteger la aldea", "Detén a los saqueadores.", 1, new Reward(new Experience(25), new Gold(50)));
            var characterRepository = new ActiveCharacterRepository(character);
            var questRepository = new InMemoryQuestRepository(new[] { quest });
            var saveGameRepository = new CaptureSaveGameRepository();
            var handler = new SaveGameHandler(characterRepository, questRepository, saveGameRepository);
            var command = new SaveGameCommand("save.json");

            handler.Handle(command);

            saveGameRepository.SavedPath.Should().Be("save.json");
            saveGameRepository.SavedGame.Should().NotBeNull();
            saveGameRepository.SavedGame.Character.Name.Should().Be("Thorn");
            saveGameRepository.SavedGame.Quests.Should().ContainSingle().Which.Title.Should().Be("Proteger la aldea");
        }

        private sealed class InMemoryCharacterRepository : ICharacterRepository
        {
            public Character? SavedCharacter { get; private set; }

            public void Save(Character character)
            {
                SavedCharacter = character;
            }

            public Character? Load(Guid characterId) => SavedCharacter?.Id == characterId ? SavedCharacter : null;
            public Character? LoadActive() => SavedCharacter;
            public IEnumerable<Character> LoadAll() => SavedCharacter is null ? Array.Empty<Character>() : new[] { SavedCharacter };
        }

        private sealed class ActiveCharacterRepository : ICharacterRepository
        {
            private readonly Character _activeCharacter;

            public ActiveCharacterRepository(Character activeCharacter)
            {
                _activeCharacter = activeCharacter;
            }

            public void Save(Character character) => throw new NotImplementedException();
            public Character? Load(Guid characterId) => _activeCharacter.Id == characterId ? _activeCharacter : null;
            public Character? LoadActive() => _activeCharacter;
            public IEnumerable<Character> LoadAll() => new[] { _activeCharacter };
        }

        private sealed class InMemoryQuestRepository : IQuestRepository
        {
            private readonly IEnumerable<Quest> _quests;

            public InMemoryQuestRepository(IEnumerable<Quest> quests)
            {
                _quests = quests;
            }

            public Quest? GetById(Guid questId) => default;
            public IEnumerable<Quest> GetAvailable() => _quests;
        }

        private sealed class CaptureSaveGameRepository : ISaveGameRepository
        {
            public SaveGameDto? SavedGame { get; private set; }
            public string? SavedPath { get; private set; }

            public void Save(SaveGameDto saveGameDto, string path)
            {
                SavedGame = saveGameDto;
                SavedPath = path;
            }

            public SaveGameDto Load(string path) => throw new NotImplementedException();
        }
    }
}
