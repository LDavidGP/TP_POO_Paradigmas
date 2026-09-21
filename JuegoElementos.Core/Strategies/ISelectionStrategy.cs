using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Strategies
{
    public interface ISelectionStrategy
    {
        Element SelectElement(List<Element> availableElements);
    }
}