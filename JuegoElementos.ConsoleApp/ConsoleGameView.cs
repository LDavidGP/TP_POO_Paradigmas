using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.ConsoleApp
{
    public class ConsoleGameView : IGameView
    {
        public void ShowMainMenu()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("        JUEGO DE ELEMENTOS (TP POO)               ");
            Console.WriteLine("==================================================");
        }

        public void ShowGameStart(Player human, Player ai, string strategyName)
        {
            throw new NotImplementedException();
        }

        public void ShowCardPresented(Player owner, Element card)
        {
            throw new NotImplementedException();
        }

        public void ShowAttack(Element attacker, Element defender, int damageDealt)
        {
            throw new NotImplementedException();
        }

        public void ShowCardDefeated(Player owner, Element defeatedCard)
        {
            throw new NotImplementedException();
        }

        public Element RequestCardSelection(IReadOnlyList<Element> availableCards)
        {
            throw new NotImplementedException();
        }

        public void ShowMatchEnd(Player winner)
        {
            throw new NotImplementedException();
        }

        public void ShowBattlefield(Element humanCard, int currentHumanCards, Element aiCard, int currentAiCards)
        {
            throw new NotImplementedException();
        }
    }
}
