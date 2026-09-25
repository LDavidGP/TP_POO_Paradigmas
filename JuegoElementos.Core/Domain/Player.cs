using JuegoElementos.Core.Strategies;

namespace JuegoElementos.Core.Domain
{
    public class Player
    {
        private readonly ISelectionStrategy _selectionStrategy;
        public string Name { get; private set; }
        public Deck Deck { get; set; }
        public bool HasAliveElements => Deck.Cards.Any(c => c.IsAlive);

        public Player(string name, ISelectionStrategy selectionStrategy, Deck? deck)
        {
            _selectionStrategy = selectionStrategy;
            Name = name;
            Deck = deck ?? new Deck();
        }

        public Element SelectElement()
        {
            var alive = Deck.Cards.Where(c => c.IsAlive).ToList();
            return _selectionStrategy.SelectElement(alive);
        }

        //public override string ToString()
        //{
        //    string deck = "";
        //    foreach (Element e in Deck.Cards)
        //    {
        //        deck += "- " + e.Type.Name + "\n";
        //    }
        //    return Name + " (" + Deck.Cards.Count + " cartas)" + "\nDeck:\n" + deck;
        //}
    }
}