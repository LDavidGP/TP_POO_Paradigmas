using JuegoElementos.Core.Strategies;

namespace JuegoElementos.Core.Domain
{
    public class Player(string name, ISelectionStrategy selectionStrategy, Deck deck)
    {
        public string Name { get; private set; } = name;
        private readonly ISelectionStrategy _selectionStrategy = selectionStrategy;
        private readonly Deck _deck = deck;

        public bool HasAliveElements => _deck.HasAliveElements;
        public IReadOnlyList<Element> AliveElements => _deck.AliveElements;
        public IReadOnlyList<Element> Elements => _deck.Elements;
        public Element SelectElement()
        {
            if (!HasAliveElements)
            {
                throw new InvalidOperationException("No hay elementos vivos para seleccionar.");
            }
            return _selectionStrategy.SelectElement(AliveElements);
        }
    }
}
