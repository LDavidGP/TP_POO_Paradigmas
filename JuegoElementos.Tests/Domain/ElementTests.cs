using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementsTypes;
using Xunit;

namespace JuegoElementos.Tests.Domain;

public class ElementTests
{
    [Fact]
    public void Constructor_DefaultMaxHealth_InitializesWith100Health()
    {
        // Arrange & Act
        var element = new Element(new FireType());

        // Assert
        Assert.Equal(100, element.MaxHealth);
        Assert.Equal(100, element.Health);
        Assert.True(element.IsAlive);
        Assert.Equal("Fire", element.Type.Name);
    }

    [Fact]
    public void Constructor_CustomHealth_SetsMaxHealthAndHealth()
    {
        // Arrange & Act
        var element = new Element(new WaterType(), maxHealth: 80);

        // Assert
        Assert.Equal(80, element.MaxHealth);
        Assert.Equal(80, element.Health);
        Assert.True(element.IsAlive);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-50)]
    public void Constructor_InvalidMaxHealth_ClampsToAtLeastOne(int invalidHealth)
    {
        // Arrange & Act
        var element = new Element(new EarthType(), maxHealth: invalidHealth);

        // Assert
        Assert.Equal(1, element.MaxHealth);
        Assert.Equal(1, element.Health);
        Assert.True(element.IsAlive);
    }

    [Fact]
    public void TakeDamage_ValidDamage_ReducesHealth()
    {
        // Arrange
        var element = new Element(new FireType(), maxHealth: 100);

        // Act
        element.TakeDamage(30);

        // Assert
        Assert.Equal(70, element.Health);
        Assert.True(element.IsAlive);
    }

    [Fact]
    public void TakeDamage_ExcessiveDamage_ClampsHealthToZeroAndDies()
    {
        // Arrange
        var element = new Element(new WaterType(), maxHealth: 50);

        // Act
        element.TakeDamage(80);

        // Assert
        Assert.Equal(0, element.Health);
        Assert.False(element.IsAlive);
    }

    [Fact]
    public void TakeDamage_WhenAlreadyDead_DoesNothing()
    {
        // Arrange
        var element = new Element(new EarthType(), maxHealth: 50);
        element.TakeDamage(50);
        Assert.False(element.IsAlive);

        // Act
        element.TakeDamage(20);

        // Assert
        Assert.Equal(0, element.Health);
        Assert.False(element.IsAlive);
    }

    [Fact]
    public void TakeDamage_NegativeDamage_ThrowsArgumentException()
    {
        // Arrange
        var element = new Element(new FireType(), maxHealth: 100);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => element.TakeDamage(-10));
    }
}
