using JuegoElementos.ConsoleApp.Models;
using JuegoElementos.ConsoleApp.Renderers;
using JuegoElementos.ConsoleApp.Screens;
using JuegoElementos.ConsoleApp.Services;
using JuegoElementos.Core.Combat;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.Factories;
using JuegoElementos.Core.Strategies;

namespace JuegoElementos.ConsoleApp;

public static class Program
{
    public static void Main()
    {
        try
        {
            var initialPlayerName = ConsoleScreens.ShowWelcome();
            var settings = new GameSettings(initialPlayerName);

            var isRunning = true;
            while (isRunning)
            {
                var option = MenuScreens.ShowMainMenu(settings);

                switch (option)
                {
                    case MainMenuOption.PlayGame:
                        PlayGame(settings);
                        break;
                    case MainMenuOption.SelectAi:
                        MenuScreens.ConfigureAiStrategy(settings);
                        break;
                    case MainMenuOption.ConfigureMatrix:
                        MenuScreens.ConfigureDamageMatrix(settings);
                        break;
                    case MainMenuOption.ViewRules:
                        MenuScreens.ShowRulesAndMatrix(settings);
                        break;
                    case MainMenuOption.ChangeName:
                        settings.PlayerName = ConsoleScreens.ShowWelcome(isRename: true);
                        break;
                    case MainMenuOption.Exit:
                        isRunning = false;
                        Console.WriteLine("\n   ¡Gracias por jugar a Batalla de Elementos! Hasta pronto.\n");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(Ansi.Reset);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[ERROR INESPERADO DEL SISTEMA]: {ex.Message}");
            Console.ResetColor();
            Console.WriteLine("\nLa partida finalizó para proteger la integridad del sistema.");
        }
    }

    private static void PlayGame(GameSettings settings)
    {
        var elementRenderer = new ElementRenderer();
        var logRenderer = new CombatLogRenderer();
        var battlefieldRenderer = new BattlefieldRenderer(elementRenderer, logRenderer);
        var view = new ConsoleGameView(battlefieldRenderer);

        var deckFactory = new DeckFactory();
        var damageCalculator = new DamageCalculator(settings.DamageMatrix);

        var player = new Player(settings.PlayerName, new HumanSelectionStrategy(view), deckFactory.CreateDeck(5));
        var opponentStrategy = settings.CreateOpponentStrategy();
        var opponent = new Player("IA", opponentStrategy, deckFactory.CreateDeck(5));

        view.ShowGameStart(player, opponent, opponentStrategy.Name);

        var game = new Game(player, opponent, damageCalculator, view);
        game.Play();
    }
}