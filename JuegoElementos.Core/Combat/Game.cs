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
            _gameview.ShowGameStart(_humanPlayer, _aiPlayer, "Estrategia por defecto");
            Duel duel = new Duel(_humanPlayer, _aiPlayer, _damageCalculator, _gameview);
            _gameview.ShowDuelEnd(duel.GetWinner());
            IsFinished = true;
        }
    }
}
