using System.Text.RegularExpressions;
using JuegoElementos.ConsoleApp.Models;

namespace JuegoElementos.ConsoleApp.Services;

public static class BoxHelper
{
    //To ignore the Ansi and calculate correctly
    private static readonly Regex AnsiRegex = new(@"\e\[[0-9;]*m", RegexOptions.Compiled);

    private static int GetVisibleLength(string text) =>
        string.IsNullOrEmpty(text) ? 0 : AnsiRegex.Replace(text, "").Length;

    public static string CreateTopBorder(int width, BoxStyle box) =>
        $"{box.TopLeftCorner}{new string(box.HorizontalLineTop, Math.Max(0, width - 2))}{box.TopRightCorner}";

    public static string CreateBottomBorder(int width, BoxStyle box) =>
        $"{box.BottomLeftCorner}{new string(box.HorizontalLineBottom, Math.Max(0, width - 2))}{box.BottomRightCorner}";

    public static string CreateLine(string content, int totalWidth, BoxStyle box, bool center = false)
    {
        var innerWidth = Math.Max(0, totalWidth - 2);
        var visibleLen = GetVisibleLength(content);

        if (visibleLen > innerWidth)
        {
            return $"{box.VerticalLineLeft}{content[..innerWidth]}{box.VerticalLineRight}";
        }

        var totalSpaces = innerWidth - visibleLen;

        if (center)
        {
            var left = totalSpaces / 2;
            var right = totalSpaces - left;
            return $"{box.VerticalLineLeft}{new string(' ', left)}{content}{new string(' ', right)}{box.VerticalLineRight}";
        }
        
        return $"{box.VerticalLineLeft}{content}{new string(' ', totalSpaces)}{box.VerticalLineRight}";
    }
}