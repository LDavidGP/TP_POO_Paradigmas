using JuegoElementos.Core.Combat;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementsTypes;
using JuegoElementos.Core.Strategies;
using Xunit;

namespace JuegoElementos.Tests.Strategies;

public class StrategicSelectionStrategyTests
{
    private readonly StrategicSelectionStrategy _strategy = new();
    private static readonly DamageCalculator _calculator = new();

    private class FixedDamageCalculator(Dictionary<(string, string), int> damageMap) : IDamageCalculator
    {
        public int CalculateDamage(IElementType attacker, IElementType defender)
        {
            return damageMap.GetValueOrDefault((attacker.Name, defender.Name), 10);
        }
    }

    [Fact]
    public void Name_ReturnsEstrategica()
    {
        Assert.Equal("Estratégica", _strategy.Name);
    }

    [Fact]
    public void SelectElement_WhenOpponentIsNull_ReturnsAnElementFromList()
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
    public void SelectElement_WhenOpponentPresent_SelectsElementWithHighestDamage()
    {
        // Arrange
        // In DefaultMatrix:
        // Water vs Fire = 20
        // Fire vs Fire = 10
        // Earth vs Fire = 15
        var fireElement = new Element(new FireType());
        var waterElement = new Element(new WaterType());
        var earthElement = new Element(new EarthType());

        var available = new List<Element> { fireElement, waterElement, earthElement };
        var opponent = new Element(new FireType());
        var context = new CombatContext(available, opponent, _calculator);

        // Act
        var selected = _strategy.SelectElement(context);

        // Assert: Water deals 20, which is maximum against Fire
        Assert.Same(waterElement, selected);
    }

    [Fact]
    public void SelectElement_WithCustomDamageCalculator_ChoosesHighestDamageElement()
    {
        // Arrange
        var elemA = new Element(new FireType());
        var elemB = new Element(new WaterType());
        var elemC = new Element(new EarthType());

        var calc = new FixedDamageCalculator(new()
        {
            { ("Fire", "Water"), 5 },
            { ("Water", "Water"), 15 },
            { ("Earth", "Water"), 45 }
        });

        var opponent = new Element(new WaterType());
        var context = new CombatContext(new List<Element> { elemA, elemB, elemC }, opponent, calc);

        // Act
        var selected = _strategy.SelectElement(context);

        // Assert: Earth deals 45 damage, highest against Water
        Assert.Same(elemC, selected);
    }

    [Fact]
    public void SelectElement_EmptyList_ThrowsInvalidOperationException()
    {
        // Arrange
        var emptyList = new List<Element>();
        var opponent = new Element(new FireType());
        var context = new CombatContext(emptyList, opponent, _calculator);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _strategy.SelectElement(context));
    }

    [Fact]
    public void SelectElement_NullList_ThrowsInvalidOperationException()
    {
        // Arrange
        var context = new CombatContext(null!, null, _calculator);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _strategy.SelectElement(context));
    }
}
