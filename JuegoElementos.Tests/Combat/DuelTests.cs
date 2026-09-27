using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Combat;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementTypes;
using JuegoElementos.Core.Strategies;
using Xunit;

namespace JuegoElementos.Tests.Combat;

public class DuelTests
{
    private class TestCombatListener : ICombatEventsListener
    {
        public List<(Element Attacker, Element Defender, int Damage)> Attacks { get; } = [];
        public List<(Player Owner, Element Defeated)> DefeatedList { get; } = [];
        public List<(Element P1, int P1Alive, Element P2, int P2Alive)> BattlefieldUpdates { get; } = [];
        public List<Player> Winners { get; } = [];

        public void OnAttackOccurred(Element attacker, Element defender, int damageDealt)
        {
            Attacks.Add((attacker, defender, damageDealt));
        }

        public void OnElementDefeated(Player owner, Element defeatedElement)
        {
            DefeatedList.Add((owner, defeatedElement));
        }

        public void OnBattlefieldUpdated(Element player1Element, int humanAlive, Element player2Element, int aiAlive)
        {
            BattlefieldUpdates.Add((player1Element, humanAlive, player2Element, aiAlive));
        }

        public void OnCombatEnded(Player winner)
        {
            Winners.Add(winner);
        }
    }

    private class FirstAvailableStrategy : ISelectionStrategy
    {
        public string Name => "FirstAvailable";
        public Element SelectElement(CombatContext context) => context.AvailableElements[0];
    }

    private class ConstantDamageCalculator(int damage) : IDamageCalculator
    {
        public int CalculateDamage(IElementType attacker, IElementType defender) => damage;
    }

    [Fact]
    public void Duel_SingleElementPerPlayer_Player1WinsWhenPlayer2Falls()
    {
        // Arrange
        var listener = new TestCombatListener();
        var strategy = new FirstAvailableStrategy();
        var calc = new ConstantDamageCalculator(100); // 1-hit KO

        var p1Element = new Element(new FireType(), 100);
        var p2Element = new Element(new WaterType(), 100);

        var player1 = new Player("Jugador 1", strategy, new Deck([p1Element]));
        var player2 = new Player("IA", strategy, new Deck([p2Element]));

        var duel = new Duel(player1, player2, calc, listener);

        // Act
        var winner = duel.GetWinner();

        // Assert
        Assert.Same(player1, winner);
        Assert.True(duel.IsFinished);
        Assert.True(player1.HasAliveElements);
        Assert.False(player2.HasAliveElements);
        Assert.Single(listener.DefeatedList);
        Assert.Same(player2, listener.DefeatedList[0].Owner);
        Assert.Same(p2Element, listener.DefeatedList[0].Defeated);
    }

    [Fact]
    public void Duel_MultiRoundCombat_SelectsReplacementWhenFirstElementFalls()
    {
        // Arrange: Player 1 has 2 elements of 40 HP each.
        // Player 2 has 1 element of 100 HP.
        // Attacker does 50 damage each hit.
        // Round 1: P1 hits P2 (P2: 50 HP). P2 hits P1 (P1-elem1: 0 HP -> dies!).
        // P1 selects elem2 (40 HP).
        // Round 2: P1-elem2 hits P2 (P2: 0 HP -> dies!).
        // Result: Player 1 wins with elem2 remaining alive.
        var listener = new TestCombatListener();
        var strategy = new FirstAvailableStrategy();
        var calc = new ConstantDamageCalculator(50);

        var p1Elem1 = new Element(new FireType(), 40);
        var p1Elem2 = new Element(new WaterType(), 40);
        var p2Elem = new Element(new EarthType(), 100);

        var player1 = new Player("Jugador 1", strategy, new Deck([p1Elem1, p1Elem2]));
        var player2 = new Player("IA", strategy, new Deck([p2Elem]));

        var duel = new Duel(player1, player2, calc, listener);

        // Act
        var winner = duel.GetWinner();

        // Assert
        Assert.Same(player1, winner);
        Assert.True(duel.IsFinished);
        Assert.False(p1Elem1.IsAlive);
        Assert.True(p1Elem2.IsAlive);
        Assert.False(p2Elem.IsAlive);
        Assert.Equal(2, listener.DefeatedList.Count);
        Assert.Same(player1, listener.DefeatedList[0].Owner);
        Assert.Same(p1Elem1, listener.DefeatedList[0].Defeated);
        Assert.Same(player2, listener.DefeatedList[1].Owner);
        Assert.Same(p2Elem, listener.DefeatedList[1].Defeated);
    }
}
