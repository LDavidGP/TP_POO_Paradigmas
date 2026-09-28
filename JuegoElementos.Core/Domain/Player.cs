using JuegoElementos.Core.Strategies;

namespace JuegoElementos.Core.Domain
{
    public class Player(string name, ISelectionStrategy selectionStrategy, Deck? deck = null)
    {
        public ISelectionStrategy SelectionStrategy { get; set; } = selectionStrategy;
        public string Name { get; private set; } = name;
        public Deck Deck { get; private set; } = deck ?? new Deck([]);

        public bool HasAliveElements => Deck.Cards.Any(c => c.IsAlive);

        public Element SelectElement()
        {
            var alive = Deck.Cards.Where(c => c.IsAlive).ToList();
            return SelectionStrategy.SelectElement(alive);
        }
    }
}