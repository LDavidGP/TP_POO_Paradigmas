using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementTypes;

namespace JuegoElementos.Core.Factories
{
    public class DeckFactory
    {
        public Deck? CreateDeck()
        {
            List<Element> cards = [
                new Element(new EarthType()),
                new Element(new EarthType()),
                new Element(new EarthType()),
                new Element(new EarthType()),
                new Element(new EarthType()),
            ];

            return new Deck(cards);
        }
    }
}