using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Strategies
{
    public interface ISelectionStrategy
    {
        string Name { get; }
        Element SelectElement(CombatContext context);
    }
}