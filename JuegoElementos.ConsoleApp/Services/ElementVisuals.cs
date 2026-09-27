using System.Drawing;
using JuegoElementos.ConsoleApp.Enums;
using JuegoElementos.ConsoleApp.Models;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementTypes;

namespace JuegoElementos.ConsoleApp.Services;

public static class ElementVisuals
{
    private static (string Icon, Style Style) GetVisuals(IElementType type) => type switch
    {
        WaterType => ("💧", new Style(Color.CornflowerBlue, decoration: Decoration.Bold)),
        FireType  => ("🔥", new Style(Color.Crimson, decoration: Decoration.Bold)),
        EarthType => ("🌱", new Style(Color.MediumSeaGreen, decoration: Decoration.Bold)),
        _         => ("⚔️", new Style(Color.White, decoration: Decoration.Bold))
    };

    public static Style GetStyle(IElementType type) => GetVisuals(type).Style;
    public static string GetIcon(IElementType type) => GetVisuals(type).Icon;

    extension(Element element)
    {
        /// <summary>
        /// returns the name with color and icon
        /// </summary>
        public string ToColoredString()
        {
            var (icon, style) = GetVisuals(element.Type);
            return $"{Ansi.GetStyleSequence(style)}{icon} {element.Type.Name}{Ansi.Reset}";
        }

        /// <summary>
        /// Returns a  badge compacted with the health
        /// </summary>
        public string ToBadge()
        {
            var percentage = (int)Math.Round((double)element.Health / element.MaxHealth * 100);
            return $"{element.ToColoredString()} ({percentage}%)";
        }
    }
}