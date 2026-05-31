using System;
using System.Collections.Generic;
using System.Linq;
using EternalRealms.Application.Interfaces;
using EternalRealms.Core.Entities;

namespace EternalRealms.Infrastructure.Repositories
{
    public sealed class CharacterRepository : ICharacterRepository
    {
        private readonly Dictionary<Guid, Character> _store = new();
        private Guid? _activeCharacterId;

        public void Save(Character character)
        {
            _store[character.Id] = character;
            _activeCharacterId = character.Id;
        }

        public Character? Load(Guid characterId)
        {
            _store.TryGetValue(characterId, out var character);
            return character;
        }

        public Character? LoadActive()
        {
            return _activeCharacterId is null ? null : Load(_activeCharacterId.Value);
        }

        public IEnumerable<Character> LoadAll() => _store.Values.ToList();
    }
}
