using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Strategies;
    public class HumanSelectionStrategy(IElementSelector elementSelector) : ISelectionStrategy
    {
        public string Name => "Humana";

        public Element SelectElement(CombatContext context)
        {
            return elementSelector.RequestElement(context);
        }
    }
