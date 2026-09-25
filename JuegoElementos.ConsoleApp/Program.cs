using JuegoElementos.ConsoleApp;
using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Factories;
using JuegoElementos.Core.Combat;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.Strategies;
using JuegoElementos.Core.ElementTypes;

IGameView view = new ConsoleGameView();
DeckFactory deckFactory = new DeckFactory();
Player Mathy = new Player("Mathy", new HumanSelectionStrategy(view), deckFactory.CreateDeck(5));
Console.WriteLine(Mathy.ToString());
Player David = new Player("David", new HumanSelectionStrategy(view), deckFactory.CreateDeck(5));
Console.WriteLine(David.ToString());

Duel duel = new Duel(view);
Deck deck1 = deckFactory.CreateDeck(new List<Element>{ new Element(new WaterType()) });
Deck deck2 = deckFactory.CreateDeck(new List<Element> { new Element(new WaterType()) });

duel.ResolveDuel(deck1.DrawCard(0), deck2.DrawCard(0));

view.ShowMainMenu();

