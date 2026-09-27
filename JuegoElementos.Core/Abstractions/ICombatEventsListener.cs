using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Abstractions;

public interface ICombatEventsListener
{
    void OnAttackOccurred(Element attacker, Element defender, int damage);
    void OnElementDefeated(Player owner, Element defeatedElement);
    void OnBattlefieldUpdated(Element p1Element, int p1Remaining, Element p2Element, int p2Remaining);
    void OnCombatEnded(Player winner);
}