namespace JuegoElementos.Core.Domain
{
    public class Deck
    {
        private readonly List<Element> _elements;
        public Deck(IEnumerable<Element> elements)
        {
            ArgumentNullException.ThrowIfNull(elements);
            _elements = new List<Element>(elements); //Copia
        }
        public IReadOnlyList<Element> Elements => _elements.AsReadOnly();
        
        public IReadOnlyList<Element> AliveElements => _elements.Where(e => e.IsAlive).ToList().AsReadOnly();

        public bool HasAliveElements => _elements.Any(e => e.IsAlive);
        
        public int RemainingAliveCount => _elements.Count(e => e.IsAlive);

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
