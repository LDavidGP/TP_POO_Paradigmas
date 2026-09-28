using JuegoElementos.Core.Combat;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementTypes;
using JuegoElementos.Core.Strategies;
using Xunit;

namespace JuegoElementos.Tests.Strategies;

public class RandomSelectionStrategyTests
{
    private readonly RandomSelectionStrategy _strategy = new();
    private static readonly DamageCalculator _calculator = new();

    [Fact]
    public void Name_ReturnsAleatoria()
    {
        Assert.Equal("Aleatoria", _strategy.Name);
    }

    [Fact]
    public void SelectElement_WithElements_ReturnsAnElementFromList()
    {
        // Arrange
        var elements = new List<Element>
        {
            new(new FireType()),
            new(new WaterType()),
            new(new EarthType())
        };
        var context = new CombatContext(elements, null, _calculator);

        // Act
        var selected = _strategy.SelectElement(context);

        // Assert
        Assert.NotNull(selected);
        Assert.Contains(selected, elements);
    }

    [Fact]
    public void SelectElement_EmptyList_ThrowsArgumentException()
    {
        // Arrange
        var emptyList = new List<Element>();
        var context = new CombatContext(emptyList, null, _calculator);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _strategy.SelectElement(context));
    }
}
