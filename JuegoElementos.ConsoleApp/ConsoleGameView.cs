using JuegoElementos.ConsoleApp.Input;
using JuegoElementos.ConsoleApp.Models;
using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;
using JuegoElementos.ConsoleApp.Renderers;
using JuegoElementos.ConsoleApp.Services;

namespace JuegoElementos.ConsoleApp
{
    public class ConsoleGameView(
        CardRenderer? cardRenderer = null, 
        CombatLogRenderer? logRenderer = null) : IGameView
    {
        private readonly CardRenderer _cardRenderer = cardRenderer ?? new();
        private readonly CombatLogRenderer _logRenderer = logRenderer ?? new();
        private readonly CombatLog _combatLog = new();

        private static int GetTerminalWidth() =>
            Console.IsOutputRedirected ? 80 : Math.Max(Console.WindowWidth, 80);

        public void ShowAttack(Element attacker, Element defender, int damageDealt)
        {
            _combatLog.AddLogMessage($"{attacker.ToColoredString()} causó {damageDealt} pts de daño a {defender.ToColoredString()}.");
            Thread.Sleep(300);
        }

        public void ShowBattlefield(Element humanCard, int remHumanCards, Element aiCard, int remAiCards)
        {
            Console.Clear();
            var width = GetTerminalWidth();

            //headers
            DrawHeader(width);

            // cards
            var humanLines = _cardRenderer.Render(humanCard);
            var aiLines = _cardRenderer.Render(aiCard);

            for (var i = 0; i < humanLines.Count; i++)
            {
                var separator = (i == 2) ? "             VS              " : "                             ";
                Console.WriteLine($"   {humanLines[i]}{separator}{aiLines[i]}");
            }

            Console.WriteLine($"   Mazo vivo: {remHumanCards}/5                                     Mazo vivo: {remAiCards}/5\n");

            // logs
            var logLines = _logRenderer.Render(_combatLog, width);
            foreach (var line in logLines)
            {
                Console.WriteLine(line);
            }
        }

        private void DrawHeader(int width)
        {
            Console.WriteLine(new string('=', width));
            const string title = "BATALLA DE ELEMENTOS: AGUA - TIERRA - FUEGO";
            var spaces = Math.Max(0, (width - title.Length) / 2);
            Console.WriteLine($"{new string(' ', spaces)}{title}");
    
            Console.WriteLine(new string('=', width));
        }

        public string WelcomePlayer()
        {
            string? name;
            do
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("        JUEGO DE ELEMENTOS (TP POO)               ");
                Console.WriteLine("==================================================");
                Console.WriteLine("BIENVENIDO!");
                Console.Write("Cómo quieres que te llamemos? : ");
                name = Console.ReadLine();
            } while (string.IsNullOrEmpty(name));
            Console.WriteLine($"Perfecto! {name}");
            Thread.Sleep(300);
            return name;
        }

        public void ShowGameStart(Player human, Player ai, string strategyName)
        {
            Console.Clear();
            var width = GetTerminalWidth();
            DrawHeader(width);
            Console.WriteLine("\n");
            Console.WriteLine($"   ¡Bienvenido, {human.Name}!");
            Thread.Sleep(300);
            Console.WriteLine($"   Te enfrentarás a: {ai.Name} (Dificultad/Estrategia: {strategyName})");
            Console.WriteLine("\n");
            Console.Write("   Presiona cualquier tecla para comenzar...");
            Console.ReadKey(intercept: true);
            Console.Clear();
        }

        public void ShowCardPresented(Player owner, Element card)
        {
            _combatLog.AddLogMessage($"{owner.Name} envía al combate a {card.ToColoredString()}!");
        }

        public void ShowCardDefeated(Player owner, Element defeatedCard)
        {
            _combatLog.AddLogMessage($"☠️  {defeatedCard.ToColoredString()} de {owner.Name} ha caído.");
            Thread.Sleep(400);
        }

        public Element RequestCardSelection(IReadOnlyList<Element> availableCards)
        {
            ArgumentNullException.ThrowIfNull(availableCards);

            if (availableCards.Count == 0)
            {
                throw new InvalidOperationException("No hay elementos disponibles para seleccionar.");
            }

            Console.WriteLine(" Selecciona una carta:");
    
            // 1. Dibujar la fila de reserva usando el método de extensión ToBadge()
            for (var i = 0; i < availableCards.Count; i++)
            {
                Console.Write($" [{i + 1}] {availableCards[i].ToBadge()}    ");
            }
            Console.WriteLine("\n");

            // 2. Solicitar la entrada al usuario
            Console.Write($" >> Elige un elemento para enviar al combate (1-{availableCards.Count}): ");
            var selectedIndex = ConsoleInputReader.ReadOption(1, availableCards.Count);

            // 3. Mapear de base 1 (UI) a base 0 (Lista)
            return availableCards[selectedIndex - 1];
        }

        public void ShowMatchEnd(Player winner)
        {
            Console.Clear();
            var width = GetTerminalWidth();
            DrawHeader(width);

            Console.WriteLine("\n\n");
    
            var box = BoxStyle.Default;
            var resultText = $"  🏆 ¡EL GANADOR ES {winner.Name.ToUpperInvariant()}! 🏆  ";

            Console.WriteLine(BoxHelper.CreateTopBorder(width, box));
            Console.WriteLine(BoxHelper.CreateLine(string.Empty, width, box));
            Console.WriteLine(BoxHelper.CreateLine(resultText, width, box, center: true));
            Console.WriteLine(BoxHelper.CreateLine(string.Empty, width, box));
            Console.WriteLine(BoxHelper.CreateBottomBorder(width, box));

            Console.WriteLine("\n   Fin de la partida");
            Console.WriteLine("   Presiona cualquier tecla para salir...");
            Console.ReadKey(intercept: true);
        }
    }
}
