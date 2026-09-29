using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Abstractions;
/// <summary>
/// Listener desacoplado (variante de observer) para notificar eventos del combate a la interfaz del usuario.
/// </summary>
public interface ICombatEventsListener
{
    void OnAttackOccurred(Element attacker, Element defender, int damage);
    void OnElementDefeated(Player owner, Element defeatedElement);
    void OnBattlefieldUpdated(Element p1Element, int p1Remaining, Element p2Element, int p2Remaining);
    void OnCombatEnded(Player winner);
}