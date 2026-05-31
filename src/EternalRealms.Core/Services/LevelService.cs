using EternalRealms.Core.Entities;
using EternalRealms.Core.Events;
using EternalRealms.Core.Interfaces;

namespace EternalRealms.Core.Services
{
    public sealed class LevelService : ILevelService
    {
        private readonly IEventBus _eventBus;

        public LevelService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public bool ShouldLevelUp(Character character)
        {
            return character.Experience.Amount >= GetExperienceThreshold(character.Level + 1);
        }

        public void ApplyLevelUp(Character character)
        {
            if (!ShouldLevelUp(character))
            {
                return;
            }

            character.LevelUp();
            _eventBus.Publish(new PlayerLevelUpEvent(character.Id, character.Level));
        }

        private static int GetExperienceThreshold(int level)
        {
            return level switch
            {
                2 => 100,
                3 => 300,
                4 => 600,
                5 => 1000,
                6 => 1500,
                _ => level * 400
            };
        }
    }
}

