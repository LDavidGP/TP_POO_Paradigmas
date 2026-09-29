using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Combat;

public class Round(Element attacker, Element defender, IDamageCalculator damageCalculator, ICombatEventsListener listener)
{

    /// <summary>
    /// Esta clase es un intercambio de golpes atómico, donde uno ataca y el otro contraataca si sobrevive, solo eso.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    public void Execute()
    {
        ArgumentNullException.ThrowIfNull(attacker);
        ArgumentNullException.ThrowIfNull(defender);

        if (!attacker.IsAlive || !defender.IsAlive) return;
        
        var damageToDefender = damageCalculator.CalculateDamage(attacker.Type, defender.Type);
        defender.TakeDamage(damageToDefender);
        listener.OnAttackOccurred(attacker, defender, damageToDefender);
        if (!defender.IsAlive) return;
        
        var damageToAttacker = damageCalculator.CalculateDamage(defender.Type, attacker.Type);
        attacker.TakeDamage(damageToAttacker);
        listener.OnAttackOccurred(defender, attacker, damageToAttacker);
    }
}
