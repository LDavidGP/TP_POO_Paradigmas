using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementsTypes;
using JuegoElementos.Core.Factories;
using Xunit;

namespace JuegoElementos.Tests.Factories;

public class DeckFactoryTests
{
    private readonly DeckFactory _factory = new();

    [Fact]
    public void CreateDeck_WithCount_CreatesDeckWithSpecifiedNumberOfElements()
    {
        // Act
        var deck = _factory.CreateDeck(5);

        // Assert
        Assert.NotNull(deck);
        Assert.Equal(5, deck.Elements.Count);
        Assert.All(deck.Elements, element =>
        {
            Assert.Equal(100, element.Health);
            Assert.True(element.IsAlive);
            Assert.True(
                element.Type is FireType or WaterType or EarthType,
                $"Unexpected element type: {element.Type.GetType().Name}"
            );
        });
    }

    [Fact]
    public void CreateDeck_NegativeCount_ThrowsArgumentOutOfRangeException()
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => _factory.CreateDeck(-1));
    }

    [Fact]
    public void CreateDeck_WithProvidedList_CreatesDeckWithThoseElements()
    {
        // Arrange
        var elements = new List<Element>
        {
            new(new FireType()),
            new(new WaterType())
        };

        // Act
        var deck = DeckFactory.CreateDeck(elements);

        // Assert
        Assert.Equal(2, deck.Elements.Count);
    }

    [Fact]
    public void CreateDeck_WithNullList_ReturnsEmptyDeck()
    {
        // Act
        var deck = DeckFactory.CreateDeck((List<Element>)null!);

        // Assert
        Assert.NotNull(deck);
        Assert.Empty(deck.Elements);
    }
}
