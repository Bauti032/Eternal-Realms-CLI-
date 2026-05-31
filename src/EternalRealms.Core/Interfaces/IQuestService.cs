using EternalRealms.Core.Entities;

namespace EternalRealms.Core.Interfaces
{
    public interface IQuestService
    {
        void AcceptQuest(Character character, Quest quest);
        void CompleteQuest(Character character, Quest quest);
    }
}

