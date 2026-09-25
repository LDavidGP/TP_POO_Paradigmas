using System.Drawing;
using JuegoElementos.ConsoleApp.Enums;
using JuegoElementos.ConsoleApp.Models;
using JuegoElementos.ConsoleApp.Services;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementTypes;

namespace JuegoElementos.ConsoleApp.Renderers;

public class CardRenderer
{
    private readonly BoxStyle _boxStyle;
    private readonly HealthBarRenderer _healthBarRenderer;
    private readonly int _totalWidth;

    public CardRenderer(BoxStyle? boxStyle = null, int totalWidth = 22, int healthBarUnits = 10)
    {
        _boxStyle = boxStyle ?? BoxStyle.Default;
        _totalWidth = Math.Max(totalWidth, 16); // Asegura espacio suficiente para la barra
        _healthBarRenderer = new HealthBarRenderer(healthBarUnits);
    }

    public IReadOnlyList<string> Render(Element element)
    {
        var lines = new List<string>(capacity: 5);
        var innerWidth = _totalWidth - 2; // Descuenta los dos bordes laterales

        // 1. Fila 0: Borde Superior
        lines.Add(BuildHorizontalBorder(_boxStyle.TopLeftCorner, _boxStyle.TopRightCorner, _boxStyle.HorizontalLineTop));

        // 2. Fila 1: Nombre Elemental centrado con estilo
        var elementStyle = GetElementStyle(element.Type);
        var typeName = element.Type.Name.ToUpperInvariant();
        lines.Add(BuildContentLine(typeName, innerWidth, elementStyle));

        // 3. Fila 2: Vida Numérica
        var hpText = $"Vida: {element.Health}%";
        lines.Add(BuildContentLine(hpText, innerWidth));

        // 4. Fila 3: Barra de Energía
        var healthBar = _healthBarRenderer.GetHealthBar(element.Health, 100);
        // La barra visual mide healthBarUnits + 2 caracteres visuales
        lines.Add(BuildCenteredRawLine(healthBar, _healthBarRenderer.TotalUnits + 2, innerWidth));

        // 5. Fila 4: Borde Inferior
        lines.Add(BuildHorizontalBorder(_boxStyle.BottomLeftCorner, _boxStyle.BottomRightCorner, _boxStyle.HorizontalLineBottom));

        return lines;
    }

    private string BuildHorizontalBorder(char leftCorner, char rightCorner, char horizontalLine)
    {
        var border = new string(horizontalLine, _totalWidth - 2);
        return $"{leftCorner}{border}{rightCorner}";
    }

    private string BuildContentLine(string text, int innerWidth, Style? style = null)
    {
        var totalSpaces = innerWidth - text.Length;
        var leftSpaces = totalSpaces / 2;
        var rightSpaces = totalSpaces - leftSpaces;

        var content = $"{new string(' ', leftSpaces)}{text}{new string(' ', rightSpaces)}";

        if (style.HasValue)
        {
            content = $"{Ansi.GetStyleSequence(style.Value)}{content}{Ansi.Reset}";
        }

        return $"{_boxStyle.VerticalLineLeft}{content}{_boxStyle.VerticalLineRight}";
    }

    private string BuildCenteredRawLine(string ansiContent, int visibleLength, int innerWidth)
    {
        var totalSpaces = innerWidth - visibleLength;
        var leftSpaces = Math.Max(0, totalSpaces / 2);
        var rightSpaces = Math.Max(0, totalSpaces - leftSpaces);

        return $"{_boxStyle.VerticalLineLeft}{new string(' ', leftSpaces)}{ansiContent}{new string(' ', rightSpaces)}{_boxStyle.VerticalLineRight}";
    }

    private static Style GetElementStyle(IElementType type) => type switch
    {
        WaterType => new Style(Color.CornflowerBlue, decoration: Decoration.Bold),
        FireType => new Style(Color.Crimson, decoration: Decoration.Bold),
        EarthType => new Style(Color.MediumSeaGreen, decoration: Decoration.Bold),
        _ => new Style(Color.White, decoration: Decoration.Bold)
    };
}