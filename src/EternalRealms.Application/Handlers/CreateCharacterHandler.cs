using EternalRealms.Application.Commands;
using EternalRealms.Application.Interfaces;
using EternalRealms.Application.Validators;
using EternalRealms.Core.Entities;

namespace EternalRealms.Application.Handlers
{
    public sealed class CreateCharacterHandler
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly CharacterValidator _validator;

        public CreateCharacterHandler(ICharacterRepository characterRepository, CharacterValidator validator)
        {
            _characterRepository = characterRepository;
            _validator = validator;
        }

        public Character Handle(CreateCharacterCommand command)
        {
            _validator.Validate(command.Name);

            var character = Character.Create(command.Name, command.CharacterClass);
            _characterRepository.Save(character);
            return character;
        }
    }
}

