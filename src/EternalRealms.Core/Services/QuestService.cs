using EternalRealms.Core.Entities;
using EternalRealms.Core.Events;
using EternalRealms.Core.Exceptions;
using EternalRealms.Core.Interfaces;

namespace EternalRealms.Core.Services
{
    public sealed class QuestService : IQuestService
    {
        private readonly IEventBus _eventBus;

        public QuestService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void AcceptQuest(Character character, Quest quest)
        {
            character.AssignQuest(quest);
        }

        public void CompleteQuest(Character character, Quest quest)
        {
            if (character.ActiveQuest is null || character.ActiveQuest.Id != quest.Id)
            {
                throw new InvalidQuestException("El personaje no tiene esta misión activa.");
            }

            character.CompleteQuest();
            _eventBus.Publish(new QuestCompletedEvent(character.Id, quest.Id, quest.Reward));
        }
    }
}

