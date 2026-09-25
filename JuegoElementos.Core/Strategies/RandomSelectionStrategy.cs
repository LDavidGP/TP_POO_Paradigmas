using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Strategies
{
    public class RandomSelectionStrategy : ISelectionStrategy
    {
        private readonly Random random = new();

        public Element SelectElement(IReadOnlyList<Element> availableElements)
        {
            if (availableElements == null || availableElements.Count == 0)
            {
                throw new ArgumentException("Available elements list cannot be null or empty.");
            }

            int randomIndex = random.Next(availableElements.Count);
            return availableElements[randomIndex];
        }
    }
}