using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementTypes;

namespace JuegoElementos.Core.Factories;


public class DeckFactory
{
    private static readonly Random _random = new();

    public Deck CreateDeck(int elementCount)
    {
        if (elementCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(elementCount), "La cantidad de elementos no puede ser negativa.");
        }

        var elements = new List<Element>(elementCount);

        for (int i = 0; i < elementCount; i++)
        {
            IElementType randomElementType = CreateRandomElementType();
            elements.Add(new Element(randomElementType));
        }

        return new Deck(elements);
    }

    public Deck CreateDeck(List<Element> elements)
    {
        if (elements == null)
        {
            return new Deck(new List<Element>());
        }
        return new Deck(elements);
    } 

    private IElementType CreateRandomElementType()
    {
        var availableTypes = new Func<IElementType>[]
        {
            () => new EarthType(),
            () => new WaterType(),
            () => new FireType()
        };

        int index = _random.Next(availableTypes.Length);
        return availableTypes[index]();
    }
}
