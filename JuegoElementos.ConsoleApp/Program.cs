using JuegoElementos.ConsoleApp.Renderers;
using JuegoElementos.ConsoleApp.Services;
using JuegoElementos.Core.Combat;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.Strategies;
using JuegoElementos.Core.Factories;

namespace JuegoElementos.ConsoleApp;
public static class Program
{
    public static void Main()
    {
        
        try
        {
            var elementRenderer = new ElementRenderer();
            var logRenderer = new CombatLogRenderer();
            var battleFieldRenderer = new BattlefieldRenderer(elementRenderer, logRenderer);
            var view = new ConsoleGameView(battleFieldRenderer);
        
            var playerName = view.WelcomePlayer();
            var deckFactory = new DeckFactory();
            var damageCalculator = new DamageCalculator();

            var player = new Player(playerName, new HumanSelectionStrategy(view), deckFactory.CreateDeck(5));
            var opponentStrategy = ChooseAiStrategy();
            var opponent = new Player("IA",opponentStrategy, deckFactory.CreateDeck(5));
        
            view.ShowGameStart(player, opponent, opponentStrategy.Name);
        
            var game = new Game(player, opponent, damageCalculator, view);
            game.Play();    
        }
        catch (Exception e)
        {
            Console.WriteLine(Ansi.Reset);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[ERROR INESPERADO]: {e.Message}");
            Console.ResetColor();
            Console.WriteLine("\nLa partida finalizó para proteger la integridad del sistema.");
        }
    }

    private static ISelectionStrategy ChooseAiStrategy()
    {
        var num =  Random.Shared.Next(1,4);
        return num switch
        {
            1 => new RandomSelectionStrategy(),
            2 => new StrategicSelectionStrategy(),
            3 => new SuperSelectionStrategy(),
            _ => new StrategicSelectionStrategy()
        };
    }
}