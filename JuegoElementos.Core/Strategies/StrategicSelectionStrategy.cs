using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementTypes;

namespace JuegoElementos.Core.Strategies
{
    public class StrategicSelectionStrategy : ISelectionStrategy
    {
        private readonly Random _random = new();

        public string Name => "Estratégica";

        // Propiedad donde se guarda el elemento del oponente
        public Element? OpponentElement { get; set; }

        public StrategicSelectionStrategy() { }

        public StrategicSelectionStrategy(Element? opponentElement)
        {
            OpponentElement = opponentElement;
        }

        public Element SelectElement(CombatContext context)
{
    var availableElements = context.AvailableElements;
    if (availableElements == null || availableElements.Count == 0)
    {
        throw new InvalidOperationException("No hay elementos disponibles para seleccionar.");
    }
    Element? targetOpponent = context.OpponentElement ?? OpponentElement;
    if (targetOpponent == null)
    {
        return availableElements[_random.Next(availableElements.Count)];
    }
    Element bestElement = availableElements[0];
    double maxDamage = -1;

    foreach (var element in availableElements)
    {
        double damage = context.DamageCalculator.CalculateDamage(element.Type, targetOpponent.Type);
        if (damage > maxDamage)
        {
            maxDamage = damage;
            bestElement = element;
        }
    }

    return bestElement;
}
    }
}