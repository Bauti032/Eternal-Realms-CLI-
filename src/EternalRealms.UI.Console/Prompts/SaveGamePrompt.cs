using Spectre.Console;
using EternalRealms.Application.Commands;

namespace EternalRealms.UI.Console.Prompts
{
    public sealed class SaveGamePrompt
    {
        public SaveGameCommand Prompt()
        {
            var directory = AnsiConsole.Ask<string>("Ruta completa para guardar/cargar la partida:");
            return new SaveGameCommand(directory);
        }
    }
}
