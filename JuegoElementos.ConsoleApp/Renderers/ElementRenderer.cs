using System.Drawing;
using JuegoElementos.ConsoleApp.Enums;
using JuegoElementos.ConsoleApp.Models;
using JuegoElementos.ConsoleApp.Services;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementTypes;

namespace JuegoElementos.ConsoleApp.Renderers;

public class ElementRenderer(BoxStyle? boxStyle = null, int totalWidth = 22, int healthBarUnits = 10)
{
    private readonly BoxStyle _boxStyle = boxStyle ?? BoxStyle.Default;
    private readonly HealthBarRenderer _healthBarRenderer = new(healthBarUnits);
    private readonly int _totalWidth = Math.Max(totalWidth, 16); // min of 16 to make sure the health bar fits

    public IReadOnlyList<string> Render(Element element)
    {
        var lines = new List<string>(capacity: 5);

        // row 1
        lines.Add(BoxHelper.CreateTopBorder(_totalWidth, _boxStyle));

        // row 2
        var elementStyle = GetElementStyle(element.Type);
        var typeName = element.Type.Name.ToUpperInvariant();
        var coloredTypeName = $"{Ansi.GetStyleSequence(elementStyle)}{typeName}{Ansi.Reset}";
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

        return lines;
    }

    private static Style GetElementStyle(IElementType type) => type switch
    {
        WaterType => new Style(Color.CornflowerBlue, decoration: Decoration.Bold),
        FireType => new Style(Color.Crimson, decoration: Decoration.Bold),
        EarthType => new Style(Color.MediumSeaGreen, decoration: Decoration.Bold),
        _ => new Style(Color.White, decoration: Decoration.Bold)
    };
}