using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Combat;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.ConsoleApp
{
    public class ConsoleGameView : IGameView
    {
        public void DisplayGame(Game game)
        {
            throw new NotImplementedException();
        }

        public Element PromptElementSelection(IReadOnlyList<Element> availableElements)
        {
            throw new NotImplementedException();
        }

        public void ShowCardSelected(Player player, Element element)
        {
            throw new NotImplementedException();
        }

        public void ShowGameOver(Player? winner)
        {
            throw new NotImplementedException();
        }

        public void ShowMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void ShowWelcome()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("        JUEGO DE ELEMENTOS (TP POO)               ");
            Console.WriteLine("==================================================");
        }

    }
}
