using System.Collections.Generic;
using EternalRealms.Core.Entities;

namespace EternalRealms.Application.Interfaces
{
    public interface ICharacterRepository
    {
        void Save(Character character);
        Character? Load(Guid characterId);
        Character? LoadActive();
        IEnumerable<Character> LoadAll();
    }
}

