using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementsTypes;

namespace JuegoElementos.Core.Factories;
/// <summary>
/// Fábrica encargada de la instanciación y distribución aleatoria o personalizada de elementos en un mazo.
/// </summary>
/// <param name="customAvailableTypes"></param>
public class DeckFactory(IReadOnlyList<Func<IElementType>>? customAvailableTypes = null)
{
    private readonly IReadOnlyList<Func<IElementType>> _availableTypes = customAvailableTypes ??
    [
        () => new EarthType(), 
        () => new WaterType(), 
        () => new FireType()
    ];
    
    public Deck CreateDeck(int elementCount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(elementCount);
        var elements = new List<Element>(elementCount);
        
        for (var i = 0; i < elementCount; i++)
        {
            var factoryMethod = _availableTypes[Random.Shared.Next(_availableTypes.Count)];
            elements.Add(new Element(factoryMethod()));
        }
        return new Deck(elements);
    }

    public static Deck CreateDeck(List<Element>? elements = null)
    {
        return elements == null ? new Deck([]) : new Deck(elements);
    }
}
