using JuegoElementos.ConsoleApp.Input;
using JuegoElementos.ConsoleApp.Models;
using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;
using JuegoElementos.ConsoleApp.Renderers;
using JuegoElementos.ConsoleApp.Screens;
using JuegoElementos.ConsoleApp.Services;
using JuegoElementos.Core.Strategies;

namespace JuegoElementos.ConsoleApp
{
    public class ConsoleGameView(
        BattlefieldRenderer? battlefieldRenderer = null): ICombatEventsListener, IElementSelector
    {
        private readonly BattlefieldRenderer _battlefieldRenderer = battlefieldRenderer ?? new();
        private readonly CombatLog _combatLog = new();

        private Element? _p1Element;
        private Element? _p2Element;
        private int _humanRemaining;
        private int _aiRemaining;

        public string WelcomePlayer() => ConsoleScreens.ShowWelcome();

        public void ShowGameStart(Player p1, Player p2, string strategyName) => 
            ConsoleScreens.ShowGameStart(p1,p2, strategyName,
                _battlefieldRenderer.GetTerminalWidth());

        public Element RequestElement(CombatContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            return RequestElementSelection(context.AvailableElements);
        }

        private static Element RequestElementSelection(IReadOnlyList<Element> aliveElements)
        {
            ArgumentNullException.ThrowIfNull(aliveElements);

            if (aliveElements.Count == 0)
            {
                throw new InvalidOperationException("No hay elementos disponibles para seleccionar.");
            }

            Console.WriteLine(" Selecciona un elemento:");

            // 1. Draw remaining elements using ToBadge()
            for (var i = 0; i < aliveElements.Count; i++)
            {
                Console.Write($" [{i + 1}] {aliveElements[i].ToBadge()}    ");
            }
            Console.WriteLine("\n");

            // 2. Request user entry
            Console.Write($" >> Elige un elemento para enviar al combate (1-{aliveElements.Count}): ");
            var selectedIndex = ConsoleInputReader.ReadOption(1, aliveElements.Count);

            // 3. map from base 1 (UI) to base 0 (List)
            return aliveElements[selectedIndex -1];
        }

        public void OnBattlefieldUpdated(Element p1Element, int p1Remaining, Element p2Element, int p2Remaining)
        {
            _p1Element = p1Element;
            _humanRemaining = p1Remaining;
            _p2Element = p2Element;
            _aiRemaining = p2Remaining;
            Redraw();
        }

        public void OnAttackOccurred(Element attacker, Element defender, int damage)
        {
            _combatLog.AddLogMessage($"{attacker.ToColoredString()} causó {damage} pts de daño a {defender.ToColoredString()}.");
            
            if (_p1Element != null && _p2Element != null)
            {
                _battlefieldRenderer.Render(_p1Element, _humanRemaining, _p2Element, _aiRemaining, _combatLog, hitElement: defender);
                Thread.Sleep(180);
            }

            Redraw();
            Thread.Sleep(300);
        }

        public void OnElementDefeated(Player owner, Element defeatedElement)
        {
            _combatLog.AddLogMessage($"☠️  {defeatedElement.ToColoredString()} de {owner.Name} ha caído.");
            Redraw();
            Thread.Sleep(400);
        }

        public void OnCombatEnded(Player winner)
        {
            ConsoleScreens.ShowDuelEnd(winner, _battlefieldRenderer.GetTerminalWidth());
        }
        
        private void Redraw()
        {
            if (_p1Element == null || _p2Element == null) return;
            _battlefieldRenderer.Render(_p1Element, _humanRemaining, _p2Element, _aiRemaining, _combatLog);
        }
    }
}
