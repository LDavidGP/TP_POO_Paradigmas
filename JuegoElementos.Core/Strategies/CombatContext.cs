using JuegoElementos.Core.Domain;
using JuegoElementos.Core.Combat;

namespace JuegoElementos.Core.Strategies;

public record CombatContext
(
    IReadOnlyList<Element> AvailableElements,
    Element? OpponentElement,
    IDamageCalculator DamageCalculator
);