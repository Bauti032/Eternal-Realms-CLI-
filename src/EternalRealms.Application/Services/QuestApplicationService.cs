using System.Collections.Generic;
using System.Linq;
using EternalRealms.Application.DTOs;
using EternalRealms.Core.Entities;

namespace EternalRealms.Application.Services
{
    public sealed class QuestApplicationService
    {
        public IEnumerable<QuestDto> MapToDto(IEnumerable<Quest> quests)
        {
            return quests.Select(MapQuest);
        }

        private static QuestDto MapQuest(Quest quest)
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

