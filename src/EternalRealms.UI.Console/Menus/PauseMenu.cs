using System;
using Spectre.Console;

namespace EternalRealms.UI.Console.Menus
{
    public sealed class PauseMenu
    {
        public void Wait()
        {
            AnsiConsole.MarkupLine("[grey]Presione cualquier tecla para continuar...[/]");
            System.Console.ReadKey(true);
        }
    }
}
