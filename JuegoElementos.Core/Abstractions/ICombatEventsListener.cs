using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Abstractions;

public interface ICombatEventsListener
{
    void OnAttackOccurred(Element attacker, Element defender, int damage);
    void OnElementDefeated(Player owner, Element defeatedElement);
    void OnBattlefieldUpdated(Element player1Element, int humanAlive, Element player2Element, int aiAlive);
    void OnCombatEnded(Player winner);
}