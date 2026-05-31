using System.Collections.Generic;
using System.Linq;
using EternalRealms.Application.DTOs;
using EternalRealms.Core.Entities;

namespace EternalRealms.Application.Services
{
    public sealed class GameApplicationService
    {
        public SaveGameDto BuildSaveGame(Character character, IEnumerable<Quest> quests)
        {
            return new SaveGameDto
            {
                Character = new CharacterApplicationService().MapToDto(character),
                Quests = quests.Select(quest => new QuestDto
                {
                    Id = quest.Id,
                    Title = quest.Title,
                    Description = quest.Description,
                    Status = quest.Status,
                    RequiredLevel = quest.RequiredLevel,
                    Reward = quest.Reward
                }).ToList()
            };
        }
    }
}

