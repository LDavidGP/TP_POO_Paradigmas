using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Strategies
{
    public class StrategicSelectionStrategy : ISelectionStrategy
    {
        public string Name => "Estratégica";
        public Element SelectElement(List<Element> availableElements)
        {
            throw new NotImplementedException("Strategic selection strategy is not implemented yet.");
        }
    }
}