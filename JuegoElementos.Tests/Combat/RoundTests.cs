using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Combat;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementTypes;
using Xunit;

namespace JuegoElementos.Tests.Combat;

public class RoundTests
{
    private class FakeCombatListener : ICombatEventsListener
    {
        public List<(Element Attacker, Element Defender, int Damage)> Attacks { get; } = [];
        public List<(Player Owner, Element Defeated)> DefeatedElements { get; } = [];
        public List<Player> Winners { get; } = [];

        public void OnAttackOccurred(Element attacker, Element defender, int damageDealt)
        {
            Attacks.Add((attacker, defender, damageDealt));
        }

        public void OnElementDefeated(Player owner, Element defeatedElement)
        {
            DefeatedElements.Add((owner, defeatedElement));
        }

        public void OnBattlefieldUpdated(Element player1Element, int humanAlive, Element player2Element, int aiAlive)
        {
        }

        public void OnCombatEnded(Player winner)
        {
            Winners.Add(winner);
        }
    }

    private class FixedDamageCalculator(int fixedDamage) : IDamageCalculator
    {
        public int CalculateDamage(IElementType attacker, IElementType defender) => fixedDamage;
    }

    [Fact]
    public void GetWinner_WhenAttackerIsNull_ThrowsArgumentNullException()
    {
        var listener = new FakeCombatListener();
        var calculator = new FixedDamageCalculator(10);
        var defender = new Element(new WaterType());
        var round = new Round(null!, defender, calculator, listener);

        Assert.Throws<ArgumentNullException>(() => round.GetWinner());
    }

    [Fact]
    public void GetWinner_WhenDefenderIsNull_ThrowsArgumentNullException()
    {
        var listener = new FakeCombatListener();
        var calculator = new FixedDamageCalculator(10);
        var attacker = new Element(new FireType());
        var round = new Round(attacker, null!, calculator, listener);

        Assert.Throws<ArgumentNullException>(() => round.GetWinner());
    }

    [Fact]
    public void GetWinner_AttackerEliminatesDefenderInOneHit_ReturnsAttackerAndNotifiesAttack()
    {
        // Arrange
        var listener = new FakeCombatListener();
        var calculator = new FixedDamageCalculator(100);
        var attacker = new Element(new FireType(), 100);
        var defender = new Element(new EarthType(), 100);
        var round = new Round(attacker, defender, calculator, listener);

        // Act
        var winner = round.GetWinner();

        // Assert
        Assert.Same(attacker, winner);
        Assert.True(attacker.IsAlive);
        Assert.False(defender.IsAlive);
        Assert.Single(listener.Attacks);
        Assert.Equal(100, listener.Attacks[0].Damage);
        Assert.Same(attacker, listener.Attacks[0].Attacker);
        Assert.Same(defender, listener.Attacks[0].Defender);
    }

    [Fact]
    public void GetWinner_DefenderEliminatesAttackerOnCounterattack_ReturnsDefender()
    {
        // Arrange
        var listener = new FakeCombatListener();
        // Attacker does 30 damage, defender has 100 hp (survives with 70)
        // Defender does 100 damage on counterattack, attacker has 50 hp (dies)
        var customCalc = new DamageCalculator(new Dictionary<(IElementType, IElementType), int>
        {
            { (new FireType(), new WaterType()), 30 },
            { (new WaterType(), new FireType()), 100 }
        });

        var attacker = new Element(new FireType(), 50);
        var defender = new Element(new WaterType(), 100);
        var round = new Round(attacker, defender, customCalc, listener);

        // Act
        var winner = round.GetWinner();

        // Assert
        Assert.Same(defender, winner);
        Assert.False(attacker.IsAlive);
        Assert.True(defender.IsAlive);
        Assert.Equal(70, defender.Health);
        Assert.Equal(2, listener.Attacks.Count);
        Assert.Equal(30, listener.Attacks[0].Damage);
        Assert.Equal(100, listener.Attacks[1].Damage);
    }
}
