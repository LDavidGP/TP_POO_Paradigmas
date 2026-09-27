using JuegoElementos.ConsoleApp.Models;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.ConsoleApp.Renderers;

public class BattlefieldRenderer(ElementRenderer? elementRenderer=null, CombatLogRenderer? logRenderer = null)
{
    private readonly ElementRenderer _elementRenderer = elementRenderer ?? new ElementRenderer();
    private readonly CombatLogRenderer _combatLogRenderer = logRenderer ?? new CombatLogRenderer();

    public int GetTerminalWidth() => Console.IsOutputRedirected ? 80 : Math.Max(Console.WindowWidth, 80);

    public void DrawHeader(int width)
    {
        Console.WriteLine(new string('=', width));
        const string title = "BATALLA DE ELEMENTOS: AGUA - TIERRA - FUEGO";
        var spaces = Math.Max(0, (width - title.Length) / 2);
        Console.WriteLine($"{new string(' ', spaces)}{title}");
        Console.WriteLine(new string('=', width));
    }

    public void Render(
        Element p1Element,
        int p1Remaining,
        Element p2Element,
        int p2Remaining,
        CombatLog combatLog,
        Element? hitElement = null)
    {
        Console.Clear();
        var width = GetTerminalWidth();
        DrawHeader(width);

        var sideMargin = 4;
        var middleGap = Math.Max(4,width - (2*22) - (2 * sideMargin));
        var marginSpaces = new string(' ', sideMargin);
        var gapSpaces = new string(' ', middleGap);
        
        //Player headers
        var playerHeaderFormat = $"{marginSpaces}{{0,-{22}}}{{1}}{{2,-{22}}}";
        Console.WriteLine(string.Format(playerHeaderFormat, $"[ JUGADOR ({p1Remaining}) ]", gapSpaces, $"[ IA ({p2Remaining}) ]"));
        
        //Render elements with hit effect if it's activated
        var humanLines = _elementRenderer.Render(p1Element, isInverted: p1Element == hitElement);
        var aiLines = _elementRenderer.Render(p2Element, isInverted: p2Element == hitElement);

        for (var i = 0; i < humanLines.Count; i++)
        {
            var centerText = (i == 2) ? CenterText("VS", middleGap) : gapSpaces;
            Console.WriteLine($"{marginSpaces}{humanLines[i]}{centerText}{aiLines[i]}");
        }
        
        //Combat log
        var logLines = _combatLogRenderer.Render(combatLog, width);
        foreach (var line in logLines)
        {
            Console.WriteLine(line);
        }
    }
    
    private static string CenterText(string text, int width)
    {
        if (text.Length >= width) return text;
        var left = (width - text.Length) / 2;
        var right = width - text.Length - left;
        return $"{new string(' ', left)}{text}{new string(' ', right)}";
    }
}