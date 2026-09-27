using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Strategies
{
    public class HumanSelectionStrategy(IGameView gameView) : ISelectionStrategy
    {
        readonly IGameView _gameView = gameView;
        public string Name => "Humana";

        public Element SelectElement(IReadOnlyList<Element> availableElements)
        {
            return _gameView.RequestElementSelection(availableElements);
        }
    }
}