using JuegoElementos.Core.Combat;
using JuegoElementos.Core.ElementTypes;
using Xunit;

namespace JuegoElementos.Tests.Combat;

public class DamageCalculatorTests
{
    private readonly DamageCalculator _calculator = new();

    public static TheoryData<IElementType, IElementType, int> ExpectedDamageMatrix =>
        new()
        {
            { new FireType(), new WaterType(), 5 },
            { new FireType(), new FireType(), 10 },
            { new FireType(), new EarthType(), 15 },
            { new WaterType(), new FireType(), 20 },
            { new WaterType(), new WaterType(), 10 },
            { new WaterType(), new EarthType(), 5 },
            { new EarthType(), new FireType(), 15 },
            { new EarthType(), new WaterType(), 10 },
            { new EarthType(), new EarthType(), 10 }
        };

    [Theory]
    [MemberData(nameof(ExpectedDamageMatrix))]
    public void CalculateDamage_DefinedPairs_ReturnsExpectedDamage(
        IElementType attacker,
        IElementType defender,
        int expectedDamage)
    {
        // Act
        var damage = _calculator.CalculateDamage(attacker, defender);

        // Assert
        Assert.Equal(expectedDamage, damage);
    }

    private record UnknownType : IElementType
    {
        public string Name => "Unknown";
    }

    [Fact]
    public void CalculateDamage_UndefinedType_ReturnsDefaultDamageTen()
    {
        // Arrange
        var unknown = new UnknownType();
        var fire = new FireType();

        // Act
        var damage = _calculator.CalculateDamage(unknown, fire);

        // Assert
        Assert.Equal(10, damage);
    }
}
