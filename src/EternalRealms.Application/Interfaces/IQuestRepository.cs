using System.Collections.Generic;
using EternalRealms.Core.Entities;

namespace EternalRealms.Application.Interfaces
{
    public interface IQuestRepository
    {
        Quest? GetById(Guid questId);
        IEnumerable<Quest> GetAvailable();
    }
}

