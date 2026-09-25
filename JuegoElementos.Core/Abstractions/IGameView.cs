using JuegoElementos.Core.Combat;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Abstractions
{
    public interface IGameView
    {
        string WelcomePlayer();
        void ShowGameStart(Player human, Player ai, string strategyName);
        void ShowBattlefield(Element humanCard, int remHumanCards, Element aiCard, int remAiCards);
        void ShowCardPresented(Player owner, Element card);
        void ShowAttack(Element attacker, Element defender, int damageDealt); //To show feedback in each hit.
        void ShowCardDefeated(Player owner, Element defeatedCard);
        Element RequestCardSelection(IReadOnlyList<Element> availableCards); //To ask the user to pick a new card
        void ShowMatchEnd(Player winner);
    }
}
