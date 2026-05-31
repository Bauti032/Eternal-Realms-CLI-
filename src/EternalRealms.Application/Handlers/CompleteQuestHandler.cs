using EternalRealms.Application.Commands;
using EternalRealms.Application.Interfaces;
using EternalRealms.Core.Interfaces;

namespace EternalRealms.Application.Handlers
{
    public sealed class CompleteQuestHandler
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IQuestRepository _questRepository;
        private readonly IQuestService _questService;

        public CompleteQuestHandler(
            ICharacterRepository characterRepository,
            IQuestRepository questRepository,
            IQuestService questService)
        {
            _characterRepository = characterRepository;
            _questRepository = questRepository;
            _questService = questService;
        }

        public void Handle(CompleteQuestCommand command)
        {
            var character = _characterRepository.Load(command.CharacterId) ?? throw new InvalidOperationException("Character not found.");
            var quest = _questRepository.GetById(command.QuestId) ?? throw new InvalidOperationException("Quest not found.");
            _questService.CompleteQuest(character, quest);
            _characterRepository.Save(character);
        }
    }
}

