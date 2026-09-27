using JuegoElementos.Core.Domain;
using JuegoElementos.Core.Strategies;

namespace JuegoElementos.Core.Abstractions;

public interface IElementSelector
{
    Element RequestElement(CombatContext context);
}