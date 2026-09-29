using JuegoElementos.ConsoleApp.Enums;
using JuegoElementos.ConsoleApp.Models;
using JuegoElementos.ConsoleApp.Services;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.ConsoleApp.Renderers;

public class ElementRenderer(BoxStyle? boxStyle = null, int totalWidth = 22, int healthBarUnits = 10)
{
    private readonly BoxStyle _boxStyle = boxStyle ?? BoxStyle.Default;
    private readonly HealthBarRenderer _healthBarRenderer = new(healthBarUnits);
    private readonly int _totalWidth = Math.Max(totalWidth, 16); // min of 16 to make sure the health bar fits

    public IReadOnlyList<string> Render(Element element, bool isInverted = false)
    {
        var baseStyle = ElementVisuals.GetStyle(element.Type);
        var activeStyle = isInverted
            ? baseStyle with { Decoration = baseStyle.Decoration | Decoration.Invert }
            : baseStyle;
        // row 1
        var lines = new List<string>(capacity: 5) {
            BoxHelper.CreateTopBorder(_totalWidth, _boxStyle)
        };
        // row 2
        var typeName = element.Type.Name.ToUpperInvariant();
        var coloredTypeName = $"{Ansi.GetStyleSequence(activeStyle)}{typeName}{Ansi.Reset}";
        lines.Add(BoxHelper.CreateLine(coloredTypeName, _totalWidth, _boxStyle, center: true));

        // row 3
        var percentage = (int)Math.Round((double)element.Health / element.MaxHealth * 100);
        var hpText = $"Vida: {percentage}%";
        lines.Add(BoxHelper.CreateLine(hpText, _totalWidth, _boxStyle, center: true));

        // row 4
        var healthBar = _healthBarRenderer.GetHealthBar(element.Health, element.MaxHealth);
        lines.Add(BoxHelper.CreateLine(healthBar, _totalWidth, _boxStyle, center: true));

        // row 5
        lines.Add(BoxHelper.CreateBottomBorder(_totalWidth, _boxStyle));

        if (!isInverted) return lines;
        var invertSeq = Ansi.GetStyleSequence(new Style(decoration: Decoration.Invert));
        for (var i = 0; i < lines.Count; i++)
        {
            var lineWithInvert = lines[i].Replace(Ansi.Reset, $"{Ansi.Reset}{invertSeq}");
            lines[i] = $"{invertSeq}{lineWithInvert}{Ansi.Reset}";
        }

        return lines;
    }
}