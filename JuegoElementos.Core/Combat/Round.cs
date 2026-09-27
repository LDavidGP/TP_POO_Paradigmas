using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Combat;

public class Round(Element attacker, Element defender, IDamageCalculator damageCalculator, ICombatEventsListener listener)
{

    // This class is used to calculate the winner of a single round which ends with one element dead
    // The class recieves two elements and the damage calculator and the game view to show the attacks
    public Element GetWinner()
    {
        if (attacker == null || defender == null)
        {
            throw new ArgumentNullException("Atacante o Defensor no pueden ser nulos");
        }

        while (attacker.IsAlive && defender.IsAlive) // <== goes untill one of the elements is dead
        {
            
            int damageToDefender = damageCalculator.CalculateDamage(attacker.Type, defender.Type);
            defender.TakeDamage(damageToDefender);
            listener.OnAttackOccurred(attacker, defender, damageToDefender);
            
            //gameView.ShowBattlefield();
            if (!defender.IsAlive)
            {
                return attacker;
            }

            int damageToAttacker = damageCalculator.CalculateDamage(defender.Type, attacker.Type);
            attacker.TakeDamage(damageToAttacker);
            listener.OnAttackOccurred(defender, attacker, damageToAttacker);
            if (!attacker.IsAlive)
            {
                return defender;
            }
        }

        throw new InvalidOperationException("Ronda no produjo un ganador, lo cual no debería suceder.");
    }
}
