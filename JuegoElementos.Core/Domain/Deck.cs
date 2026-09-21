namespace JuegoElementos.Core.Domain
{
    public class Deck(List<Element> cards)
    {
        public List<Element> Cards { get; private set; } = cards;
    }
}