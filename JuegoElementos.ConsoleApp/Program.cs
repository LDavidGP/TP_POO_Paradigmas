using JuegoElementos.ConsoleApp;
using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Factories;
using JuegoElementos.Core.Combat;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.Strategies;

IGameView view = new ConsoleGameView();
DeckFactory deckFactory = new DeckFactory();
DamageCalculator damageCalculator = new DamageCalculator();

Game game = new Game(
    new Player("Mathy", new HumanSelectionStrategy(view), deckFactory.CreateDeck(2)),
    new Player("David", new HumanSelectionStrategy(view), deckFactory.CreateDeck(2)),
    damageCalculator,
    view
);

game.Start();

