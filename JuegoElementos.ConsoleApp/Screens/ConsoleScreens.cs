using JuegoElementos.ConsoleApp.Models;
using JuegoElementos.ConsoleApp.Services;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.ConsoleApp.Screens;

public static class ConsoleScreens
{
    public static string ShowWelcome()
    {
        string? name;
        do
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("        JUEGO DE ELEMENTOS (TP POO)               ");
            Console.WriteLine("==================================================");
            Console.WriteLine("BIENVENIDO!");
            Console.Write("¿Cómo quieres que te llamemos? : ");
            name = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(name));

        Console.WriteLine($"¡Perfecto, {name}!");
        Thread.Sleep(300);
        return name.Trim();
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
        Console.WriteLine("   Presiona cualquier tecla para salir...");
        if (!Console.IsInputRedirected)
        {
            Console.ReadKey(intercept: true);
        }
    }
}