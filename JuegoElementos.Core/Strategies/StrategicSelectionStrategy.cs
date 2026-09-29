using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Strategies;
    public class StrategicSelectionStrategy : ISelectionStrategy
    {
        public string Name => "Estratégica";

        public Element SelectElement(CombatContext context)
        {
            var availableElements = context.AvailableElements;
            if (availableElements is null || availableElements.Count == 0)
            {
                throw new InvalidOperationException("No hay elementos disponibles para seleccionar.");
            }

            if(context.OpponentElement is null)
                return availableElements[Random.Shared.Next(availableElements.Count)];

            var bestElement = availableElements[0];
            var maxDamage = -1;

            foreach (var element in availableElements)
            {
                var damage = context.DamageCalculator.CalculateDamage(element.Type, context.OpponentElement.Type);
                if (!(damage > maxDamage)) continue;
                maxDamage = damage;
                bestElement = element;
            }

            return bestElement;
        }
    }
