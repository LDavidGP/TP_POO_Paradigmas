using JuegoElementos.Core.ElementTypes;

namespace JuegoElementos.Core.Domain
{
    public class Deck
    {
        public List<Element> Cards { get; private set; }
        public List<Element> AliveCards => Cards.Where(e => e.IsAlive).ToList();
        public bool HasAliveCards => AliveCards.Count > 0;

        public Deck(List<Element> cards)
        {
            Cards = cards ?? new List<Element>();
        }

        public Deck()
        {
            Cards = new List<Element>();
        }

        public Deck(int capacity)
        {
            Cards = new List<Element>();
            BuildDeck(capacity);
        }

        private void BuildDeck(int capacity)
        {
            for (int i = 0; i < capacity; i++)
            {
                var elementType = GetRandomElementType();
                var card = new Element(elementType);
                Cards.Add(card);
            }
        }

        private IElementType GetRandomElementType()
        {
            var elementTypes = new List<IElementType>
            {
                new EarthType(),
                new WaterType(),
                new FireType(),
            };
            var random = new Random();
            int index = random.Next(elementTypes.Count);
            return elementTypes[index];
        }

        public void Shuffle()
        {
            var rng = new Random();
            int n = Cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = Cards[k];
                Cards[k] = Cards[n];
                Cards[n] = value;
            }
        }

        public void AddCard(Element card, int index = -1)
        {
            if (index == -1)
            {
                Cards.Add(card);
            }
            else
            {
                if (index < 0 || index > Cards.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "Índice fuera de rango.");
                }
                Cards.Insert(index, card);
            }
        }

        public void RemoveCard(int index)
        {
            if (index < 0 || index >= Cards.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Índice fuera de rango.");
            }
            Cards.RemoveAt(index);
        }

        public Element DrawCard(int index)
        {
            if (index < 0 || index >= Cards.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Índice fuera de rango.");
            }
            var card = Cards[index];
            Cards.RemoveAt(index);
            return card;
        }
    }
}