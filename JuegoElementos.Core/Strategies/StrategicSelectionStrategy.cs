using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementTypes;

namespace JuegoElementos.Core.Strategies
{
    public class StrategicSelectionStrategy : ISelectionStrategy
    {
        public string Name => "Estratégica";

        public Element SelectElement(CombatContext context)
        {
            return context.AvailableElements[0]; //Esteban implementa, tener en cuenta el caso de que el elemento oponente sea null.
        }
    }
}