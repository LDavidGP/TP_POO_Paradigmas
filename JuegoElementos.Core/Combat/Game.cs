using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Combat
{
    public class Game(Player humanPlayer, Player aiPlayer, DamageCalculator damageCalculator, IGameView view)
    {
        private readonly Player _humanPlayer = humanPlayer;
        private readonly Player _aiPlayer = aiPlayer;
        private readonly DamageCalculator _damageCalculator = damageCalculator;
        private readonly IGameView _gameview = view;

        public bool IsFinished { get; private set; } = false;
        public void Start()
        {
            var initialSelection = _gameview.RequestElementSelection(_humanPlayer.Elements);
            _gameview.ShowBattlefield(
                initialSelection, 
                _humanPlayer.AliveElements.Count, //TODO: Change something idk
                _aiPlayer.SelectElement(),
                _aiPlayer.AliveElements.Count
            );

            // Duel 
            Duel duel = new Duel(_humanPlayer, _aiPlayer, _damageCalculator, _gameview);
            _gameview.ShowDuelEnd(duel.GetWinner());
            IsFinished = duel.IsFinished;

            // IDK
            Console.WriteLine("Falta implementar...");
            Console.ReadKey();
        }
    }
}
