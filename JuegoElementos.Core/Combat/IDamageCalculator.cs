using JuegoElementos.Core.ElementsTypes;

namespace JuegoElementos.Core.Combat;
/// <summary>
/// Abstracción para el cálculo de daño de los elementos. Permite modificar y extender
/// las reglas de multiplicadores de efectividad sin alterar las entidades del dominio.
/// </summary>
public interface IDamageCalculator
{
    int CalculateDamage(IElementType attacker, IElementType defender);
}