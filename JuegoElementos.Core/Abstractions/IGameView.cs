using JuegoElementos.Core.Combat;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Abstractions
{
    public interface IGameView
    {
        string WelcomePlayer();
        void ShowGameStart(Player human, Player ai, string strategyName);
        void ShowBattlefield(Element humanElement, int remHumanElements, Element aiElement, int remAiElements);
        void ShowElementPresented(Player owner, Element element);
        void ShowAttack(Element attacker, Element defender, int damageDealt); //To show feedback in each hit.
        void ShowElementDefeated(Player owner, Element defeatedElement);
        Element RequestElementSelection(IReadOnlyList<Element> aliveElements); //To ask the user to pick a new element to play.
        void ShowDuelEnd(Player winner);
    }
}
