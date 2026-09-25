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

        public Element SelectElement()
        {
            if (!HasAliveElements)
            {
                throw new InvalidOperationException("No hay elementos vivos para seleccionar.");
            }
            return _selectionStrategy.SelectElement(_deck.AliveElements.ToList());
        }
    }
}
