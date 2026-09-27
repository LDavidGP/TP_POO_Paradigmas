using JuegoElementos.Core.Combat;
using JuegoElementos.Core.ElementTypes;
using Xunit;

namespace JuegoElementos.Tests.Combat;

public class DamageCalculatorTests
{
    private readonly DamageCalculator _calculator = new();

    public static TheoryData<IElementType, IElementType, int> ExpectedDefaultDamageMatrix =>
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
    [MemberData(nameof(ExpectedDefaultDamageMatrix))]
    public void CalculateDamage_DefaultMatrix_ReturnsExpectedDamage(
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

    [Fact]
    public void DamageCalculator_Implements_IDamageCalculator()
    {
        Assert.IsAssignableFrom<IDamageCalculator>(_calculator);
    }

    [Fact]
    public void CalculateDamage_CustomMatrix_OverridesValues()
    {
        // Arrange
        var custom = new Dictionary<(IElementType, IElementType), int>
        {
            { (new WaterType(), new FireType()), 50 },
            { (new FireType(), new WaterType()), 20 }
        };
        var customCalculator = new DamageCalculator(custom);

        // Act
        var waterVsFire = customCalculator.CalculateDamage(new WaterType(), new FireType());
        var fireVsWater = customCalculator.CalculateDamage(new FireType(), new WaterType());

        // Assert
        Assert.Equal(50, waterVsFire);
        Assert.Equal(20, fireVsWater);
    }

    [Fact]
    public void CalculateDamage_CustomMatrix_FallsBackToTenForUnspecifiedPairs()
    {
        // Arrange
        var custom = new Dictionary<(IElementType, IElementType), int>
        {
            { (new WaterType(), new FireType()), 50 }
        };
        var customCalculator = new DamageCalculator(custom);

        // Act & Assert
        Assert.Equal(10, customCalculator.CalculateDamage(new EarthType(), new EarthType()));
    }
}
