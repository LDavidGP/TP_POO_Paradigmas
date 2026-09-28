using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.Strategies;

namespace JuegoElementos.Core.Combat
{
    public class Game(Player humanPlayer, Player aiPlayer, IGameView view)
    {
        private Player HumanPlayer { get; set; } = humanPlayer;
        private Player AiPlayer { get; set; } = aiPlayer;
        public Duel Duel { get; private set; } = new Duel(view);
        public bool IsFinished => !HumanPlayer.HasAliveElements || !AiPlayer.HasAliveElements;
        public IGameView View { get; private set; } = view;
        public void Start()
        {
            var initialSelection = view.RequestCardSelection(humanPlayer.Deck.Cards);
            if (AiPlayer.SelectionStrategy is StrategicSelectionStrategy strategic)
            {
                strategic.OpponentElement = initialSelection;
            }
            else if (AiPlayer.SelectionStrategy is SuperSelectionStrategy superStrategy)
            {
                superStrategy.OpponentElement = initialSelection;
            }

            var aiSelection = AiPlayer.SelectElement();

            view.ShowBattlefield(initialSelection, 1, aiSelection, AiPlayer.Deck.Cards.Count(a => a.IsAlive));
            view.ShowMatchEnd(humanPlayer);
            Console.WriteLine("Falta implementar...");
            Console.ReadKey();
        }
    }
}