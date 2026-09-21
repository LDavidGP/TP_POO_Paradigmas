using JuegoElementos.Core.Combat;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Abstractions
{
    public interface IGameView
    {
        void ShowWelcome();
        void DisplayGame(Game game);
        Element PromptElementSelection(IReadOnlyList<Element> availableElements);
        void ShowCardSelected(Player player, Element element);
        void ShowGameOver(Player? winner);
        void ShowMessage(string message);
    }
}
