using EternalRealms.Application.Commands;
using EternalRealms.Application.Interfaces;
using EternalRealms.Core.Entities;

namespace EternalRealms.Application.Handlers
{
    public sealed class EquipItemHandler
    {
        private readonly ICharacterRepository _characterRepository;

        public EquipItemHandler(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public void Handle(EquipItemCommand command)
        {
            var character = _characterRepository.Load(command.CharacterId) ?? throw new InvalidOperationException("Character not found.");
            var item = character.Inventory.GetItem<Item>(command.ItemId);
            character.EquipItem(item);
            _characterRepository.Save(character);
        }
    }
}

