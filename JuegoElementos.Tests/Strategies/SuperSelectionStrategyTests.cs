using JuegoElementos.Core.Combat;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementsTypes;
using JuegoElementos.Core.Strategies;
using Xunit;

namespace JuegoElementos.Tests.Strategies;

public class SuperSelectionStrategyTests
{
    private readonly SuperSelectionStrategy _strategy = new();
    private static readonly DamageCalculator _calculator = new();

    private class FixedDamageCalculator(Dictionary<(string, string), int> damageMap) : IDamageCalculator
    {
        public int CalculateDamage(IElementType attacker, IElementType defender)
        {
            return damageMap.GetValueOrDefault((attacker.Name, defender.Name), 10);
        }
    }

    [Fact]
    public void Name_ReturnsSuper()
    {
        Assert.Equal("Super", _strategy.Name);
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
    public void SelectElement_WhenMultipleLethalElementsExist_PicksLethalWithLowestDamage()
    {
        // Arrange: Opponent has 25 HP
        // ElemA deals 15 damage (not lethal)
        // ElemB deals 30 damage (lethal, minimum overkill)
        // ElemC deals 50 damage (lethal, higher overkill)
        var elemA = new Element(new FireType());
        var elemB = new Element(new WaterType());
        var elemC = new Element(new EarthType());

        var calc = new FixedDamageCalculator(new()
        {
            { ("Fire", "Water"), 15 },
            { ("Water", "Water"), 30 },
            { ("Earth", "Water"), 50 }
        });

        var opponent = new Element(new WaterType(), maxHealth: 100);
        opponent.TakeDamage(75); // HP is now 25

        var context = new CombatContext(new List<Element> { elemA, elemB, elemC }, opponent, calc);

        // Act
        var selected = _strategy.SelectElement(context);

        // Assert: ElemB (30 damage) eliminates the opponent without wasting the 50-damage card
        Assert.Same(elemB, selected);
    }

    [Fact]
    public void SelectElement_WhenExactLethalExists_PicksExactLethalOverOverkill()
    {
        // Arrange: Opponent has 20 HP
        // ElemA deals 20 damage (exact lethal)
        // ElemB deals 40 damage (overkill)
        var elemA = new Element(new FireType());
        var elemB = new Element(new WaterType());

        var calc = new FixedDamageCalculator(new()
        {
            { ("Fire", "Earth"), 20 },
            { ("Water", "Earth"), 40 }
        });

        var opponent = new Element(new EarthType(), maxHealth: 100);
        opponent.TakeDamage(80); // HP is now 20

        var context = new CombatContext(new List<Element> { elemA, elemB }, opponent, calc);

        // Act
        var selected = _strategy.SelectElement(context);

        // Assert
        Assert.Same(elemA, selected);
    }

    [Fact]
    public void SelectElement_WhenNoLethalElementExists_PicksElementWithHighestDamage()
    {
        // Arrange: Opponent has 100 HP, none of the elements can kill in 1 hit
        // ElemA deals 10 damage
        // ElemB deals 35 damage (highest)
        // ElemC deals 20 damage
        var elemA = new Element(new FireType());
        var elemB = new Element(new WaterType());
        var elemC = new Element(new EarthType());

        var calc = new FixedDamageCalculator(new()
        {
            { ("Fire", "Fire"), 10 },
            { ("Water", "Fire"), 35 },
            { ("Earth", "Fire"), 20 }
        });

        var opponent = new Element(new FireType(), maxHealth: 100);
        var context = new CombatContext(new List<Element> { elemA, elemB, elemC }, opponent, calc);

        // Act
        var selected = _strategy.SelectElement(context);

        // Assert: ElemB deals 35 damage, highest available
        Assert.Same(elemB, selected);
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
