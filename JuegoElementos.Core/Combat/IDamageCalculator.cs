using JuegoElementos.Core.ElementTypes;

namespace JuegoElementos.Core.Combat;

public interface IDamageCalculator
{
    int CalculateDamage(IElementType attacker, IElementType defender);
}