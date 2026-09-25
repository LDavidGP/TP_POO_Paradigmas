using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Strategies
{
    public class SuperSelectionStrategy : ISelectionStrategy
    {
        public string Name => "Super";

        public Element SelectElement(IReadOnlyList<Element> availableElements)
        {
            throw new NotImplementedException("Super selection strategy is not implemented yet.");
        }
    }
}