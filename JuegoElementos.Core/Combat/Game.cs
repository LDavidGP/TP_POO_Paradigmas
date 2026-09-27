using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Combat
{
    public class Game(Player humanPlayer, Player aiPlayer, DamageCalculator damageCalculator, ICombatEventsListener listener)
    {
        public bool IsFinished { get; private set; } = false;
        public void Start()
        {
            Duel duel = new Duel(humanPlayer, aiPlayer, damageCalculator, listener);
            listener.OnCombatEnded(duel.GetWinner());
            IsFinished = duel.IsFinished;
        }
    }
}
