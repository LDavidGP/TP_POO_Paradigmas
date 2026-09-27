using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.Strategies;

namespace JuegoElementos.Core.Combat;

public class Duel(Player player1, Player player2, IDamageCalculator damageCalculator, ICombatEventsListener listener)
{
    public bool IsFinished => !player1.HasAliveElements || !player2.HasAliveElements;

    public Player GetWinner()
    {
        //Initial selection
        var contextPlayer1 = new CombatContext(player1.AliveElements,null, damageCalculator);
        var player1Element = player1.SelectElement(contextPlayer1);
        var contextPlayer2 = new CombatContext(player2.AliveElements, player1Element, damageCalculator);
        var player2Element = player2.SelectElement(contextPlayer2);
        listener.OnBattlefieldUpdated(player1Element, player1.RemainingElements, player2Element, player2.RemainingElements);
        while (player1.HasAliveElements && player2.HasAliveElements)
        {
            listener.OnBattlefieldUpdated(player1Element, player1.RemainingElements, player2Element, player2.RemainingElements);
            var round = new Round(player1Element, player2Element, damageCalculator, listener);
            var winnerElement = round.GetWinner();

            if (winnerElement != player1Element)
            {
                listener.OnElementDefeated(player1, player1Element); // <== Show that player 1's element was defeated
                contextPlayer1 = new CombatContext(player1.AliveElements, player2Element, damageCalculator);
                if (player1.HasAliveElements) player1Element = player1.SelectElement(contextPlayer1);
            }
            else if (winnerElement != player2Element)
            {
                listener.OnElementDefeated(player2, player2Element); // <== Show that player 2's element was defeated
                contextPlayer2 = new CombatContext(player2.AliveElements, player1Element, damageCalculator);
                if (player2.HasAliveElements) player2Element = player2.SelectElement(contextPlayer2);
            }
            else throw new InvalidOperationException("Ronda terminada en un empate, lo cual no debería suceder.");
        }

        return player1.HasAliveElements ? player1 : player2;
    }
}
