using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Strategies
{
    public class HumanSelectionStrategy(IElementSelector elementSelector) : ISelectionStrategy
    {
        public string Name => "Humana";

        public Element SelectElement(IReadOnlyList<Element> availableElements)
        {
            return elementSelector.RequestElement(availableElements);
        }
    }
}