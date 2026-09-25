using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Strategies
{
    public class HumanSelectionStrategy(IGameView gameView) : ISelectionStrategy
    {
        readonly IGameView _gameView = gameView;
        public string Name => "Humana";
        public Element SelectElement(List<Element> availableElements)
        {
            throw new NotImplementedException("Human selection strategy is not implemented yet.");
        }
    }
}