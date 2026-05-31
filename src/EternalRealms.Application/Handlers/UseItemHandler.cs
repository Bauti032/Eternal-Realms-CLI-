using EternalRealms.Application.Commands;
using EternalRealms.Application.Interfaces;
using EternalRealms.Core.Entities;

namespace EternalRealms.Application.Handlers
{
    public sealed class UseItemHandler
    {
        private readonly ICharacterRepository _characterRepository;

        public UseItemHandler(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public void Handle(UseItemCommand command)
        {
            var character = _characterRepository.Load(command.CharacterId) ?? throw new InvalidOperationException("Character not found.");
            var consumable = character.Inventory.GetItem<Consumable>(command.ItemId);
            character.UseConsumable(consumable);
            _characterRepository.Save(character);
        }
    }
}

