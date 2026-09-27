using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementTypes;
namespace JuegoElementos.Core.Factories;
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
        ArgumentOutOfRangeException.ThrowIfNegative(elementCount, nameof(elementCount));
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
