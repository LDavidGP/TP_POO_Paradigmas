using JuegoElementos.ConsoleApp.Renderers;
using JuegoElementos.Core.Combat;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.Strategies;
using JuegoElementos.Core.Factories;

namespace JuegoElementos.ConsoleApp;
public static class Program
{
    private static Random _random = new Random();
    public static void Main()
    {
        var elementRenderer = new ElementRenderer();
        var logRenderer = new CombatLogRenderer();
        var damageCalculator = new DamageCalculator();
        var view = new ConsoleGameView(elementRenderer, logRenderer);
        var playerName = view.WelcomePlayer();
        var deckFactory = new DeckFactory();
        var player = new Player(playerName, new HumanSelectionStrategy(view), deckFactory.CreateDeck(5));
        var oponentStrategy = ChooseAiStrategy();
        var oponent = new Player("IA",oponentStrategy, deckFactory.CreateDeck(5));
        view.ShowGameStart(player, oponent, oponentStrategy.Name);
        var game = new Game(player, oponent, damageCalculator, view);
        game.Start();
        Console.WriteLine("Espero te haya gustado, adiós!");
        
    }

    private static ISelectionStrategy ChooseAiStrategy()
    {
        return new RandomSelectionStrategy();
        var num =  _random.Next(1, 4);
        switch (num)
        {
            case 1: return new RandomSelectionStrategy();
            case 2: return new StrategicSelectionStrategy();;
            case 3: return new SuperSelectionStrategy();
            default: return new StrategicSelectionStrategy();
        }
    }
}