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
            Console.WriteLine("¡Comienza el juego!");
            Console.WriteLine($"Jugador humano: {human.Name}");
            foreach(Element e in human.AliveElements)
            {
                Console.WriteLine($"- Elemento: {e.Type.Name}, Vida: {e.Health}");
            }
            Console.WriteLine($"Jugador IA: {ai.Name}");
            foreach (Element e in ai.AliveElements)
            {
                Console.WriteLine($"- Elemento: {e.Type.Name}, Vida: {e.Health}");
            }
            Console.WriteLine($"Estrategia: {strategyName}");
        }

        public void ShowElementPresented(Player owner, Element element)
        {
            Console.WriteLine($"{owner.Name} presenta a {element.Type.Name} con {element.Health} de vida.");
        }

        public void ShowAttack(Element attacker, Element defender, int damageDealt)
        {
            Console.WriteLine($"{attacker.Type.Name} ataca a {defender.Type.Name} y le inflige {damageDealt} de daño. {defender.Health} de vida restante.");
        }

        public void ShowElementDefeated(Player owner, Element defeatedElement)
        {
            Console.WriteLine($"{owner.Name} ha perdido a {defeatedElement.Type.Name}.");
        }
        

        public Element RequestElementSelection(IReadOnlyList<Element> availableCards)
        {
            throw new NotImplementedException();
        }

        public void ShowDuelEnd(Player winner)
        {
            Console.WriteLine($"¡El ganador es {winner.Name}!");
        }

        public void ShowBattlefield(Element humanCard, int currentHumanCards, Element aiCard, int currentAiCards)
        {
            throw new NotImplementedException();
        }
    }
}
