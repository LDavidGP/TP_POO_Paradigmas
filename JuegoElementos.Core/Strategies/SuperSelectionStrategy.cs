using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementTypes;

namespace JuegoElementos.Core.Strategies
{
    public class SuperSelectionStrategy : ISelectionStrategy
    {
        public string Name => "Super";

        public Element SelectElement(CombatContext context)
        {
            return context.AvailableElements[0];//Esteban Implementa, tener en cuenta el caso de que el elemento oponente sea null.
        }
    }
}