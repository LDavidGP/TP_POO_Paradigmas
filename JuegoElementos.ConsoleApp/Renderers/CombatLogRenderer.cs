using JuegoElementos.ConsoleApp.Enums;
using JuegoElementos.ConsoleApp.Models;
using JuegoElementos.ConsoleApp.Services;

namespace JuegoElementos.ConsoleApp.Renderers;

public class CombatLogRenderer(BoxStyle? boxStyle = null)
{
    private readonly BoxStyle _boxStyle = boxStyle ?? BoxStyle.Default;
    
    public IReadOnlyList<string> Render(CombatLog combatLog, int width)
    {
        var lines = new List<string>(capacity: 6);

        // top border
        lines.Add(BoxHelper.CreateTopBorder(width, _boxStyle));

        // header
        var titleStyle = new Style(decoration: Decoration.Bold);
        var formattedTitle = $" {Ansi.GetStyleSequence(titleStyle)}REGISTRO DE COMBATE{Ansi.Reset}";
        lines.Add(BoxHelper.CreateLine(formattedTitle, width, _boxStyle));

        // content
        var logEntries = combatLog.GetVisibleLines();
        foreach (var entry in logEntries)
        {
            var lineText = string.IsNullOrWhiteSpace(entry) 
                ? "  >" 
                : $"  > {entry}";

            lines.Add(BoxHelper.CreateLine(lineText, width, _boxStyle));
        }
        // bottom border
        lines.Add(BoxHelper.CreateBottomBorder(width, _boxStyle));

        return lines;
    }
}