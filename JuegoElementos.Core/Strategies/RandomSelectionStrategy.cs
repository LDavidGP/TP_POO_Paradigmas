using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Strategies
{
    public class RandomSelectionStrategy : ISelectionStrategy
    {
        private readonly Random random = new();
        public string Name => "Aleatoria";

        public Element SelectElement(CombatContext context)
        {
            if (context.AvailableElements == null || context.AvailableElements.Count == 0)
            {
                throw new ArgumentException("Available elements list cannot be null or empty.");
            }

            var randomIndex = random.Next(context.AvailableElements.Count);
            return context.AvailableElements[randomIndex];
        }
    }
}