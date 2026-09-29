using System.Drawing;
using JuegoElementos.ConsoleApp.Enums;
using JuegoElementos.ConsoleApp.Models;
using JuegoElementos.ConsoleApp.Services;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.ConsoleApp.Screens;

public static class ConsoleScreens
{
    private static int GetTerminalWidth() =>
        Console.IsOutputRedirected ? 80 : Math.Max(Console.WindowWidth, 80);

    public static string ShowWelcome(bool isRename = false)
    {
        var box = BoxStyle.Default;
        var width = Math.Min(GetTerminalWidth(), 72);
        string? name;

        do
        {
            Console.Clear();
            Console.WriteLine();

            // Main Banner
            var titleStyle = new Style(Color.Goldenrod, decoration: Decoration.Bold);
            var subStyle = new Style(Color.LightGray, decoration: Decoration.Faint);

            var title = $"{Ansi.GetStyleSequence(titleStyle)}⚔️  BATALLA DE ELEMENTOS: AGUA - TIERRA - FUEGO  ⚔️{Ansi.Reset}";
            var subtitle = $"{Ansi.GetStyleSequence(subStyle)}Trabajo Práctico Integrador — Programación Orientada a Objetos{Ansi.Reset}";

            Console.WriteLine(BoxHelper.CreateTopBorder(width, box));
            Console.WriteLine(BoxHelper.CreateLine(string.Empty, width, box));
            Console.WriteLine(BoxHelper.CreateLine(title, width, box, center: true));
            Console.WriteLine(BoxHelper.CreateLine(subtitle, width, box, center: true));
            Console.WriteLine(BoxHelper.CreateLine(string.Empty, width, box));
            Console.WriteLine(BoxHelper.CreateBottomBorder(width, box));

            Console.WriteLine();

            // Player card
            var promptBox = BoxStyle.Default;
            var promptTitle = isRename
                ? "  ⚙️  ACTUALIZACIÓN DE PERFIL  "
                : "  👤  REGISTRO DE DUELISTA  ";

            Console.WriteLine(BoxHelper.CreateTopBorder(width, promptBox));
            Console.WriteLine(BoxHelper.CreateLine(promptTitle, width, promptBox));
            Console.WriteLine(BoxHelper.CreateLine(string.Empty, width, promptBox));

            var instruction = isRename
                ? "  Ingresa tu nuevo nombre de jugador:"
                : "  ¡Bienvenido a la arena! ¿Cómo te gustaría que te llamemos?:";
            Console.WriteLine(BoxHelper.CreateLine(instruction, width, promptBox));
            Console.WriteLine(BoxHelper.CreateLine(string.Empty, width, promptBox));
            Console.WriteLine(BoxHelper.CreateBottomBorder(width, promptBox));

            Console.WriteLine();
            Console.Write("  ➤ Tu Nombre: ");
            name = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(name)) continue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  ⚠️  El nombre no puede estar vacío. Por favor, intenta de nuevo.");
            Console.ResetColor();
            Thread.Sleep(900);
        } while (string.IsNullOrWhiteSpace(name));

        var cleanName = name.Trim();

        Console.WriteLine();
        var confirmStyle = new Style(Color.MediumSeaGreen, decoration: Decoration.Bold);
        Console.WriteLine($"  {Ansi.GetStyleSequence(confirmStyle)}✨ ¡Excelente! Todo listo para el combate, {cleanName}. ✨{Ansi.Reset}");
        Thread.Sleep(500);

        return cleanName;
    }

    public static void ShowGameStart(Player human, Player ai, string strategyName, int width)
    {
        Console.Clear();
        Console.WriteLine(new string('=', width));
        Console.WriteLine("\n");
        Console.WriteLine($"   ¡Bienvenido, {human.Name}!");
        Thread.Sleep(300);
        Console.WriteLine($"   Te enfrentarás a: {ai.Name} (Dificultad/Estrategia: {strategyName})");
        Console.WriteLine("\n");
        Console.Write("   Presiona cualquier tecla para comenzar...");
        if (!Console.IsInputRedirected)
        {
            Console.ReadKey(intercept: true);
        }
        Console.Clear();
    }

    public static void ShowDuelEnd(Player winner, int width)
    {
        Console.Clear();
        Console.WriteLine(new string('=', width));
        Console.WriteLine("\n\n");

        var box = BoxStyle.Default;
        var resultText = $"  🏆 ¡EL GANADOR ES {winner.Name.ToUpperInvariant()}! 🏆  ";

        Console.WriteLine(BoxHelper.CreateTopBorder(width, box));
        Console.WriteLine(BoxHelper.CreateLine(string.Empty, width, box));
        Console.WriteLine(BoxHelper.CreateLine(resultText, width, box, center: true));
        Console.WriteLine(BoxHelper.CreateLine(string.Empty, width, box));
        Console.WriteLine(BoxHelper.CreateBottomBorder(width, box));

        Console.WriteLine("\n   Fin de la partida.");
        Console.WriteLine("   Presiona cualquier tecla para continuar...");
        if (!Console.IsInputRedirected)
        {
            Console.ReadKey(intercept: true);
        }
    }
}