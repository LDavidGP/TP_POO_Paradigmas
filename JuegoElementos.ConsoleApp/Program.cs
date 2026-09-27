using JuegoElementos.ConsoleApp.Renderers;
using JuegoElementos.Core.Combat;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.Strategies;
using JuegoElementos.Core.Factories;

namespace JuegoElementos.ConsoleApp;
public static class Program
{
    public static void Main()
    {
        var elementRenderer = new ElementRenderer();
        var logRenderer = new CombatLogRenderer();
        var damageCalculator = new DamageCalculator();
        var view = new ConsoleGameView(elementRenderer, logRenderer);
        var playerName = view.WelcomePlayer();
        var deckFactory = new DeckFactory();
        var player = new Player(playerName, new HumanSelectionStrategy(view), deckFactory.CreateDeck(5));
        var opponentStrategy = ChooseAiStrategy() ?? throw new ArgumentNullException($"{nameof(ChooseAiStrategy)}()");
        var opponent = new Player("IA",opponentStrategy, deckFactory.CreateDeck(5));
        view.ShowGameStart(player, opponent, opponentStrategy.Name);
        var game = new Game(player, opponent, damageCalculator, view);
        game.Start();
    }

    private static ISelectionStrategy ChooseAiStrategy()
    {
        return new RandomSelectionStrategy();
        // var num =  _random.Next(1, 4);
        // switch (num)
        // {
        //     case 1: return new RandomSelectionStrategy();
        //     case 2: return new StrategicSelectionStrategy();;
        //     case 3: return new SuperSelectionStrategy();
        //     default: return new StrategicSelectionStrategy();
        // }
    }
}