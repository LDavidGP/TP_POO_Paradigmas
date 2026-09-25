namespace JuegoElementos.Core.Domain
{
    public class Deck(List<Element> elements)
    {
        private readonly List<Element> _elements = elements;
        private List<Element> _aliveElements => _elements.Where(e => e.IsAlive).ToList();

        public IReadOnlyList<Element> Elements => _elements.AsReadOnly();
        public IReadOnlyList<Element> AliveElements => _aliveElements.AsReadOnly();
        public bool HasAliveElements => _aliveElements.Count > 0;

        public Element GetElementAt(int index)
        {
            if (index < 0 || index >= _elements.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Índice fuera de rango.");
            }

            return _elements[index];
        }
    }
}
