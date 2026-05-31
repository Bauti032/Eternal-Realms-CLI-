using System;
using System.Collections.Generic;
using System.Linq;
using EternalRealms.Application.Interfaces;
using EternalRealms.Core.Entities;
using EternalRealms.Core.Enums;
using EternalRealms.Core.ValueObjects;

namespace EternalRealms.Infrastructure.Repositories
{
    public sealed class QuestRepository : IQuestRepository
    {
        private readonly List<Quest> _quests;

        public QuestRepository()
        {
            _quests = CreateDefaultQuests();
        }

        public Quest? GetById(Guid questId)
        {
            return _quests.FirstOrDefault(quest => quest.Id == questId);
        }

        public IEnumerable<Quest> GetAvailable()
        {
            return _quests.Where(quest => quest.Status == QuestStatus.Available || quest.Status == QuestStatus.Active).ToList();
        }

        private static List<Quest> CreateDefaultQuests()
        {
            return new List<Quest>
            {
                new(
                    Guid.NewGuid(),
                    "Caza del cuervo sombrío",
                    "Elimina al cuervo sombrío que merodea el Bosque de Bruma.",
                    requiredLevel: 1,
                    new Reward(new Experience(50), new Gold(20))),
                new(
                    Guid.NewGuid(),
                    "Escudo del anciano",
                    "Recupera el escudo perdido del anciano en las ruinas del templo.",
                    requiredLevel: 2,
                    new Reward(new Experience(100), new Gold(50))),
                new(
                    Guid.NewGuid(),
                    "Aliento del dragón",
                    "Derrota al dragón que amenaza la frontera oriental.",
                    requiredLevel: 4,
                    new Reward(new Experience(300), new Gold(150)))
            };
        }
    }
}
