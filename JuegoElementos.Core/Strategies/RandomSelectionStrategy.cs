using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Strategies
{
    public class RandomSelectionStrategy : ISelectionStrategy
    {
        public string Name => "Aleatoria";

        public Element SelectElement(CombatContext context)
        {
            if (context.AvailableElements == null || context.AvailableElements.Count == 0)
            {
                throw new ArgumentException("Available elements list cannot be null or empty.");
            }
            var randomIndex = Random.Shared.Next(context.AvailableElements.Count);
            return context.AvailableElements[randomIndex];
        }
    }
}