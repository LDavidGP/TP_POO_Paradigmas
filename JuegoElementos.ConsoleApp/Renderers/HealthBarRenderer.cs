using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;
using JuegoElementos.ConsoleApp.Enums;
using JuegoElementos.ConsoleApp.Models;
using JuegoElementos.ConsoleApp.Services;

namespace JuegoElementos.ConsoleApp.Renderers;

public class HealthBarRenderer(int totalUnits)
{
    public int TotalUnits { get; } = totalUnits;
    public string GetHealthBar(int currentHealth, int maxHealth)
    {
        var coloredUnits = Math.Clamp((currentHealth * TotalUnits)/ maxHealth, 0, TotalUnits);
        if (currentHealth > 0 && coloredUnits == 0)
            coloredUnits = 1;
        var barBuilder = new StringBuilder();
        barBuilder.Append('[');
        barBuilder.Append(Ansi.GetStyleSequence(GetHealthStyle((double)currentHealth / maxHealth)));
        for (var i = 0; i < coloredUnits; i++)
            barBuilder.Append('█');
        if (coloredUnits < TotalUnits)
        {
            barBuilder.Append(Ansi.GetStyleSequence(new Style(decoration: Decoration.Faint)));
            for (var i = coloredUnits; i < TotalUnits; i++)
                barBuilder.Append('░');
        }
        barBuilder.Append(Ansi.Reset);
        barBuilder.Append(']');
        return barBuilder.ToString();
    }
    
    private static Style GetHealthStyle(double percentage) => percentage switch
    {
        > 0.50 => new Style(Color.MediumSeaGreen),
        > 0.20 => new Style(Color.Goldenrod),
        _      => new Style(Color.Crimson, decoration: Decoration.Bold)
    };
}