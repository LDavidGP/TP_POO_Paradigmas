using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Strategies
{
    public class SuperSelectionStrategy : ISelectionStrategy
    {

        public string Name => "Super";

        public Element SelectElement(CombatContext context)
        {
            var availableElements = context.AvailableElements;
            if (availableElements is null || availableElements.Count == 0)
            {
                throw new InvalidOperationException("La lista de elementos disponibles no puede estar vacía.");
            }

            var targetOpponent = context.OpponentElement;

            if (targetOpponent is null)
                return availableElements[Random.Shared.Next(availableElements.Count)];
            
            var opponentHealth = targetOpponent.Health;

            var lethalElements = availableElements
                .Select(e => new { 
                    Element = e, 
                    Damage = context.DamageCalculator.CalculateDamage(e.Type, targetOpponent.Type) 
                })
                .Where(x => x.Damage >= opponentHealth)
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