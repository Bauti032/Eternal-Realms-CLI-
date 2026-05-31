using Spectre.Console;
using EternalRealms.Application.DTOs;

namespace EternalRealms.UI.Console.Rendering
{
    public sealed class InventoryRenderer
    {
        public void Render(CharacterDto character)
        {
            var inventory = character.Inventory;
            if (inventory.Count == 0)
            {
                AnsiConsole.MarkupLine("[grey]El inventario está vacío.[/]");
                return;
            }

            var table = new Table().Border(TableBorder.Minimal).AddColumn("Item").AddColumn("Tipo");

            foreach (var item in inventory)
            {
                table.AddRow(item.Name, item.ItemType);
            }

            AnsiConsole.Write(new Panel(table).Header("Inventario").Expand());
        }
    }
}
