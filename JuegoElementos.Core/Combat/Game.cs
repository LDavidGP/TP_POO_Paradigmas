using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Combat
{
    public class Game(Player humanPlayer, Player aiPlayer, IDamageCalculator damageCalculator, ICombatEventsListener listener)
    {
        public void Start()
        {
            var duel = new Duel(humanPlayer, aiPlayer, damageCalculator, listener);
            listener.OnCombatEnded(duel.GetWinner());
        }
    }
}
