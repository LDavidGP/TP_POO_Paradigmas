using JuegoElementos.Core.Combat;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementsTypes;
using JuegoElementos.Core.Strategies;
using Xunit;

namespace JuegoElementos.Tests.Domain;

public class PlayerTests
{
    private class FakeSelectionStrategy(Element elementToReturn) : ISelectionStrategy
    {
        public string Name => "Fake";
        public Element SelectElement(CombatContext context) => elementToReturn;
    }

    private static CombatContext CreateDummyContext(IReadOnlyList<Element> elements) =>
        new(elements, null, new DamageCalculator());

    [Fact]
    public void Constructor_InitializesPlayerProperties()
    {
        // Arrange
        var elements = new List<Element> { new(new FireType()) };
        var deck = new Deck(elements);
        var strategy = new FakeSelectionStrategy(elements[0]);

        // Act
        var player = new Player("Jugador 1", strategy, deck);

        // Assert
        Assert.Equal("Jugador 1", player.Name);
        Assert.True(player.HasAliveElements);
        Assert.Single(player.AliveElements);
        Assert.Single(player.Elements);
    }

    [Fact]
    public void SelectElement_WhenHasAliveElements_DelegatesToStrategy()
    {
        // Arrange
        var chosenElement = new Element(new WaterType());
        var elements = new List<Element> { chosenElement };
        var deck = new Deck(elements);
        var strategy = new FakeSelectionStrategy(chosenElement);
        var player = new Player("Test", strategy, deck);
        var context = CreateDummyContext(player.AliveElements);

        // Act
        var result = player.SelectElement(context);

        // Assert
        Assert.Same(chosenElement, result);
    }

    [Fact]
    public void SelectElement_WhenNoAliveElements_ThrowsInvalidOperationException()
    {
        // Arrange
        var element = new Element(new WaterType(), 100);
        element.TakeDamage(100);
        var deck = new Deck(new List<Element> { element });
        var strategy = new FakeSelectionStrategy(element);
        var player = new Player("Test", strategy, deck);
        var context = CreateDummyContext(player.AliveElements);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => player.SelectElement(context));
    }
}
