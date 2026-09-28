using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Combat;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementTypes;
using JuegoElementos.Core.Strategies;
using Xunit;

namespace JuegoElementos.Tests.Strategies;

public class HumanSelectionStrategyTests
{
    private class FakeElementSelector(Element toReturn) : IElementSelector
    {
        public CombatContext? ReceivedContext { get; private set; }

        public Element RequestElement(CombatContext context)
        {
            ReceivedContext = context;
            return toReturn;
        }
    }

    [Fact]
    public void Name_ReturnsHumana()
    {
        var dummyElement = new Element(new FireType());
        var selector = new FakeElementSelector(dummyElement);
        var strategy = new HumanSelectionStrategy(selector);

        Assert.Equal("Humana", strategy.Name);
    }

    [Fact]
    public void SelectElement_DelegatesToElementSelector()
    {
        // Arrange
        var chosenElement = new Element(new WaterType());
        var selector = new FakeElementSelector(chosenElement);
        var strategy = new HumanSelectionStrategy(selector);

        var elements = new List<Element> { chosenElement };
        var context = new CombatContext(elements, null, new DamageCalculator());

        // Act
        var result = strategy.SelectElement(context);

        // Assert
        Assert.Same(chosenElement, result);
        Assert.Same(context, selector.ReceivedContext);
    }
}
