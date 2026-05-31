using Spectre.Console;
using EternalRealms.Application.Commands;
using EternalRealms.Core.Enums;

namespace EternalRealms.UI.Console.Prompts
{
    public sealed class CharacterCreationPrompt
    {
        public CreateCharacterCommand Prompt()
        {
            var name = AnsiConsole.Ask<string>("¿Cómo se llamará tu héroe?");
            var characterClass = AnsiConsole.Prompt(
                new SelectionPrompt<CharacterClass>()
                    .Title("Selecciona la clase de tu personaje:")
                    .AddChoices(Enum.GetValues<CharacterClass>()));

            return new CreateCharacterCommand(name, characterClass);
        }
    }
}
