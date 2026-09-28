using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Strategies
{
    public class SuperSelectionStrategy : ISelectionStrategy
    {
        private readonly Random _random = new();

        public string Name => "Super";

        public Element? OpponentElement { get; set; }

        public Element SelectElement(CombatContext context)
        {
            var availableElements = context.AvailableElements;
            if (availableElements == null || availableElements.Count == 0)
            {
                throw new InvalidOperationException("La lista de elementos disponibles no puede estar vacía.");
            }
            Element? targetOpponent = context.OpponentElement ?? OpponentElement;

            if (targetOpponent == null)
            {
                return availableElements[_random.Next(availableElements.Count)];
            }
            double targetHealth = targetOpponent.Health;

            var lethalElements = availableElements
                .Select(e => new { 
                    Element = e, 
                    Damage = context.DamageCalculator.CalculateDamage(e.Type, targetOpponent.Type) 
                })
                .Where(x => x.Damage >= targetHealth)
                .OrderBy(x => x.Damage)
                .ToList();

            if (lethalElements.Any())
            {
                return lethalElements.First().Element;
            }

            return availableElements
                .OrderByDescending(e => context.DamageCalculator.CalculateDamage(e.Type, targetOpponent.Type))
                .First();
        }
    }
}