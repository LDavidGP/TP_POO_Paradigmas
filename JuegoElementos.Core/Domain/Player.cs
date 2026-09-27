using JuegoElementos.Core.Strategies;

namespace JuegoElementos.Core.Domain
{
    public class Player(string name, ISelectionStrategy selectionStrategy, Deck deck)
    {
        public string Name { get; private set; } = name;

        public bool HasAliveElements => deck.HasAliveElements;
        public IReadOnlyList<Element> Elements => deck.Elements;
        public IReadOnlyList<Element> AliveElements => deck.AliveElements;
        public int RemainingElements => deck.RemainingAliveCount;

        public Element SelectElement(CombatContext context)
        {
            return !HasAliveElements ? throw new InvalidOperationException("No hay elementos vivos para seleccionar.") 
                : selectionStrategy.SelectElement(context);
        }
    }
}
