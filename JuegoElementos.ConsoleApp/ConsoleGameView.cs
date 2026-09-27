using JuegoElementos.ConsoleApp.Input;
using JuegoElementos.ConsoleApp.Models;
using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;
using JuegoElementos.ConsoleApp.Renderers;
using JuegoElementos.ConsoleApp.Services;

namespace JuegoElementos.ConsoleApp
{
    public class ConsoleGameView(
        ElementRenderer? elementRenderer = null, 
        CombatLogRenderer? logRenderer = null) : IGameView
    {
        private readonly ElementRenderer _elementRenderer = elementRenderer ?? new();
        private readonly CombatLogRenderer _logRenderer = logRenderer ?? new();
        private readonly CombatLog _combatLog = new();

        private Element? _humanElement;
        private Element? _aiElement;
        private int _humanRemaining;
        private int _aiRemaining;
        private static int GetTerminalWidth() =>
            Console.IsOutputRedirected ? 80 : Math.Max(Console.WindowWidth, 80);

        public void ShowAttack(Element attacker, Element defender, int damageDealt)
        {
            _combatLog.AddLogMessage($"{attacker.ToColoredString()} causó {damageDealt} pts de daño a {defender.ToColoredString()}.");
            Thread.Sleep(300);
        }

        public void ShowBattlefield(Element humanElement, int remHumanElements, Element aiElement, int remAiElements)
        {_humanElement = humanElement;
            _humanRemaining = remHumanElements;
            _aiElement = aiElement;
            _aiRemaining = remAiElements;
            RedrawBattlefield();
        }

        public void RedrawBattlefield()
        {
            if (_humanElement == null || _aiElement == null) return;

            Console.Clear();
            var width = GetTerminalWidth();
            DrawHeader(width);

            var sideMargin = 4;
            var middleGap = Math.Max(4, width - (2 * 22) - (2 * sideMargin));
            var marginSpaces = new string(' ', sideMargin);
            var gapSpaces = new string(' ', middleGap);

            // headers
            var playerHeaderFormat = $"{marginSpaces}{{0,-{22}}}{{1}}{{2,-{22}}}";
            Console.WriteLine(string.Format(playerHeaderFormat, "[ JUGADOR HUMANO ]", gapSpaces, "[ IA OPONENTE ]"));

            // Element render
            var humanLines = _elementRenderer.Render(_humanElement);
            var aiLines = _elementRenderer.Render(_aiElement);

            for (var i = 0; i < humanLines.Count; i++)
            {
                var centerText = (i == 2) ? CenterText("VS", middleGap) : gapSpaces;
                Console.WriteLine($"{marginSpaces}{humanLines[i]}{centerText}{aiLines[i]}");
            }
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

        public void ShowElementPresented(Player owner, Element element)
        {
            _combatLog.AddLogMessage($"{owner.Name} envía al combate a {element.ToColoredString()}!");
        }

        public void ShowElementDefeated(Player owner, Element defeatedElement)
        {
            _combatLog.AddLogMessage($"☠️  {defeatedElement.ToColoredString()} de {owner.Name} ha caído.");
            Thread.Sleep(400);
        }
        

        public Element RequestElementSelection(IReadOnlyList<Element> aliveElements)
        {
            ArgumentNullException.ThrowIfNull(aliveElements);

            if (aliveElements.Count == 0)
            {
                throw new InvalidOperationException("No hay elementos disponibles para seleccionar.");
            }

            Console.WriteLine(" Selecciona un elemento:");

            // 1. Dibujar la fila de reserva usando el método de extensión ToBadge()
            for (var i = 0; i < aliveElements.Count; i++)
            {
                Console.Write($" [{i + 1}] {aliveElements[i].ToBadge()}    ");
            }
            Console.WriteLine("\n");

            // 2. Solicitar la entrada al usuario
            Console.Write($" >> Elige un elemento para enviar al combate (1-{aliveElements.Count}): ");
            var selectedIndex = ConsoleInputReader.ReadOption(1, aliveElements.Count);

            // 3. Mapear de base 1 (UI) a base 0 (Lista)
            return aliveElements[selectedIndex - 1];
        }

        public void ShowDuelEnd(Player winner)
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
        private static string CenterText(string text, int width)
        {
            // Si el texto ya es igual o más largo que el ancho disponible, no hay nada que rellenar
            if (text.Length >= width) return text;

            // Calculamos la mitad exacta de espacios para la izquierda
            var left = (width - text.Length) / 2;
            // El resto de espacios van a la derecha (por si la diferencia es impar)
            var right = width - text.Length - left;

            return $"{new string(' ', left)}{text}{new string(' ', right)}";
        }
    }
}
