using Spectre.Console;
using EternalRealms.Application.DTOs;
using EternalRealms.Core.Enums;

namespace EternalRealms.UI.Console.Rendering
{
    public sealed class CharacterRenderer
    {
        public void Render(CharacterDto character)
        {
            var table = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn("Estadística")
                .AddColumn("Valor");

            table.AddRow("Nombre", character.Name);
            table.AddRow("Clase", character.CharacterClass.ToString());
            table.AddRow("Nivel", character.Level.ToString());
            table.AddRow("Vida", $"{character.Health.Current}/{character.Health.Maximum}");
            table.AddRow("Mana", $"{character.Mana.Current}/{character.Mana.Maximum}");
            table.AddRow("Oro", character.Gold.Amount.ToString());
            table.AddRow("Experiencia", character.Experience.Amount.ToString());

            var stats = new Table().AddColumn("Fuerza").AddColumn("Agilidad").AddColumn("Inteligencia").AddColumn("Vitalidad").AddColumn("Espíritu");
            stats.AddRow(
                character.Stats.Strength.ToString(),
                character.Stats.Agility.ToString(),
                character.Stats.Intelligence.ToString(),
                character.Stats.Vitality.ToString(),
                character.Stats.Spirit.ToString());

            AnsiConsole.Write(new Panel(table).Header("Personaje", Justify.Center));
            AnsiConsole.Write(new Panel(stats).Header("Atributos", Justify.Center));

            RenderEquipment(character);
            RenderQuest(character); 
        }

        private static void RenderEquipment(CharacterDto character)
        {
            var equipment = new Table().AddColumn("Ranura").AddColumn("Equipo");

            foreach (var slot in Enum.GetValues<EquipmentSlot>())
            {
                character.Equipment.TryGetValue(slot, out var item);
                var value = item is null ? "Vacío" : item.Name;
                equipment.AddRow(slot.ToString(), value);
            }

            AnsiConsole.Write(new Panel(equipment).Header("Equipamiento", Justify.Center));
        }

        private static void RenderQuest(CharacterDto character)
        {
            if (character.ActiveQuest is null)
            {
                AnsiConsole.MarkupLine("[yellow]No hay misiones activas.[/]");
                return;
            }

            var panel = new Panel($"[green]{character.ActiveQuest.Title}[/]\n{character.ActiveQuest.Description}\nEstado: {character.ActiveQuest.Status}")
                .Header("Misión activa");

            AnsiConsole.Write(panel);
        }
    }
}
