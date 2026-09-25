using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Combat;

public class Round(Element attacker, Element defender, DamageCalculator damageCalculator, IGameView gameView)
{

    // This class is used to calculate the winner of a single round which ends with one element dead
    // The class recieves two elements and the damage calculator and the game view to show the attacks

    private readonly Element _attacker = attacker;
    private readonly Element _defender = defender;

    public Element GetWinner()
    {
        if (_attacker == null || _defender == null)
        {
            throw new ArgumentNullException("Atacante o Defensor no pueden ser nulos");
        }

        while (_attacker.IsAlive && _defender.IsAlive) // <== goes untill one of the elements is dead
        {
            int damageToDefender = damageCalculator.CalculateDamage(_attacker.Type, _defender.Type);
            _defender.TakeDamage(damageToDefender);
            gameView.ShowAttack(_attacker, _defender, damageToDefender); // <== shows the attack in the game view
            if (!_defender.IsAlive)
            {
                return _attacker;
            }

            int damageToAttacker = damageCalculator.CalculateDamage(_defender.Type, _attacker.Type);
            _attacker.TakeDamage(damageToAttacker);
            gameView.ShowAttack(_defender, _attacker, damageToAttacker); // <== shows the attack in the game view
            if (!_attacker.IsAlive)
            {
                return _defender;
            }
        }

        throw new InvalidOperationException("Ronda no produjo un ganador, lo cual no debería suceder.");
    }
}
