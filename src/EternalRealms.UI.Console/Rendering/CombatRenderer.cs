using Spectre.Console;
using EternalRealms.Application.DTOs;

namespace EternalRealms.UI.Console.Rendering
{
    public sealed class CombatRenderer
    {
        public void Render(CombatResultDto result)
        {
            var table = new Table().Border(TableBorder.Rounded).AddColumn("Resultado").AddColumn("Valor");
            table.AddRow("Daño infligido", result.DamageDealt.ToString());
            table.AddRow("Enemigo derrotado", result.IsEnemyDefeated ? "Sí" : "No");
            table.AddRow("Jugador derrotado", result.IsPlayerDefeated ? "Sí" : "No");
            table.AddRow("Vida restante", result.RemainingPlayerHealth.ToString());
            table.AddRow("Vida enemiga restante", result.RemainingEnemyHealth.ToString());

            AnsiConsole.Write(new Panel(table).Header("Resultado de combate", Justify.Center));
        }
    }
}
