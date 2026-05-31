using Spectre.Console;
using EternalRealms.Application.DTOs;

namespace EternalRealms.UI.Console.Rendering
{
    public sealed class QuestRenderer
    {
        public void Render(QuestDto quest)
        {
            if (quest is null)
            {
                AnsiConsole.MarkupLine("[yellow]No hay misión activa.[/]");
                return;
            }

            var table = new Table().AddColumn("Campo").AddColumn("Valor");
            table.AddRow("Título", quest.Title);
            table.AddRow("Descripción", quest.Description);
            table.AddRow("Estado", quest.Status.ToString());
            table.AddRow("Requisito de nivel", quest.RequiredLevel.ToString());
            table.AddRow("Experiencia", quest.Reward.Experience.Amount.ToString());
            table.AddRow("Oro", quest.Reward.Gold.Amount.ToString());

            AnsiConsole.Write(new Panel(table).Header("Misión Activa", Justify.Center));
        }
    }
}
