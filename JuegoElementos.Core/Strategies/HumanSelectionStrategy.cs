using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Strategies
{
    public class HumanSelectionStrategy(IGameView gameView) : ISelectionStrategy
    {
        readonly IGameView _gameView = gameView;
        public Element SelectElement(IReadOnlyList<Element> availableElements)
        {
            var rand = new Random();
            var index = rand.Next(availableElements.Count);
            return availableElements[index];
        }
    }
}