using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementTypes;
using Xunit;

namespace JuegoElementos.Tests.Domain;

public class DeckTests
{
    [Fact]
    public void Constructor_WithElements_InitializesCorrectly()
    {
        // Arrange
        var elements = new List<Element>
        {
            new(new FireType()),
            new(new WaterType())
        };

        // Act
        var deck = new Deck(elements);

        // Assert
        Assert.Equal(2, deck.Elements.Count);
        Assert.Equal(2, deck.AliveElements.Count);
        Assert.True(deck.HasAliveElements);
    }

    [Fact]
    public void AliveElements_FiltersOutDeadElements()
    {
        // Arrange
        var e1 = new Element(new FireType(), 100);
        var e2 = new Element(new WaterType(), 100);
        var deck = new Deck(new List<Element> { e1, e2 });

        // Act
        e1.TakeDamage(100);

        // Assert
        Assert.Single(deck.AliveElements);
        Assert.Same(e2, deck.AliveElements[0]);
        Assert.True(deck.HasAliveElements);
    }

    [Fact]
    public void HasAliveElements_AllElementsDead_ReturnsFalse()
    {
        // Arrange
        var e1 = new Element(new FireType(), 50);
        var deck = new Deck(new List<Element> { e1 });

        // Act
        e1.TakeDamage(50);

        // Assert
        Assert.Empty(deck.AliveElements);
        Assert.False(deck.HasAliveElements);
    }

    [Fact]
    public void GetElementAt_ValidIndex_ReturnsElement()
    {
        // Arrange
        var e1 = new Element(new FireType());
        var e2 = new Element(new EarthType());
        var deck = new Deck(new List<Element> { e1, e2 });

        // Act & Assert
        Assert.Same(e1, deck.GetElementAt(0));
        Assert.Same(e2, deck.GetElementAt(1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(2)]
    public void GetElementAt_InvalidIndex_ThrowsArgumentOutOfRangeException(int invalidIndex)
    {
        // Arrange
        var deck = new Deck(new List<Element>
        {
            new(new FireType()),
            new(new EarthType())
        });

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => deck.GetElementAt(invalidIndex));
    }
}
