using EternalRealms.Core.Enums;
using EternalRealms.Core.Exceptions;
using EternalRealms.Core.ValueObjects;

namespace EternalRealms.Core.Entities
{
    public sealed class Quest
    {
        public Quest(Guid id, string title, string description, int requiredLevel, Reward reward)
        {
            Id = id;
            Title = title;
            Description = description;
            RequiredLevel = requiredLevel;
            Reward = reward;
            Status = QuestStatus.Available;
        }

        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public QuestStatus Status { get; private set; }
        public int RequiredLevel { get; init; }
        public Reward Reward { get; init; }

        public void Accept(Character character)
        {
            if (Status != QuestStatus.Available)
            {
                throw new InvalidQuestException("La misión no está disponible para aceptar.");
            }

            if (character.Level < RequiredLevel)
            {
                throw new InvalidQuestException("El personaje no cumple los requisitos de nivel para esta misión.");
            }

            Status = QuestStatus.Active;
        }

        public void Complete()
        {
            if (Status != QuestStatus.Active)
            {
                throw new InvalidQuestException("Solo se puede completar una misión activa.");
            }

            Status = QuestStatus.Completed;
        }

        public static Quest Restore(Guid id, string title, string description, int requiredLevel, Reward reward, QuestStatus status)
        {
            var quest = new Quest(id, title, description, requiredLevel, reward);
            quest.Status = status;
            return quest;
        }
    }
}

