using JuegoElementos.ConsoleApp;
using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.Strategies;

IGameView view = new ConsoleGameView();
//Player Mathy = new Player("Mathy", new HumanSelectionStrategy(view), new Deck(5));
//Console.WriteLine(Mathy.ToString());
view.ShowMainMenu();

