using JuegoElementos.Core.Strategies;

namespace JuegoElementos.Core.Domain
{
    public class Player(string name, ISelectionStrategy selectionStrategy, Deck? deck = null)
    {
        private readonly ISelectionStrategy _selectionStrategy = selectionStrategy;
        public string Name { get; private set; } = name;
        public Deck Deck { get; set; } = deck ?? new Deck([]);

        public bool HasAliveElements => Deck.Cards.Any(c => c.IsAlive);

        public Element SelectElement()
        {
            var alive = Deck.Cards.Where(c => c.IsAlive).ToList();
            return _selectionStrategy.SelectElement(alive);
        }
    }
}