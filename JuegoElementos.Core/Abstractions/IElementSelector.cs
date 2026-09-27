using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Abstractions;

public interface IElementSelector
{
    Element RequestElement(IReadOnlyList<Element> availableElements);
}