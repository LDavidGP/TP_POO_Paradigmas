using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Combat;

public class Duel(Player player1, Player player2, DamageCalculator damageCalculator, ICombatEventsListener listener)
{
    public bool IsFinished => !player1.HasAliveElements || !player2.HasAliveElements;

    public Player GetWinner()
    {
        //Initial selection
        var player1Element = player1.SelectElement();
        var player2Element = player2.SelectElement();
        listener.OnBattlefieldUpdated(player1Element, player1.RemainingElements, player2Element, player2.RemainingElements);
        while (player1.HasAliveElements && player2.HasAliveElements)
        {
            listener.OnBattlefieldUpdated(player1Element, player1.RemainingElements, player2Element, player2.RemainingElements);
            var round = new Round(player1Element, player2Element, damageCalculator, listener);
            var winnerElement = round.GetWinner();

            if (winnerElement != player1Element)
            {
                listener.OnElementDefeated(player1, player1Element); // <== Show that player 1's element was defeated
                if (player1.HasAliveElements) player1Element = player1.SelectElement();
            }
            else if (winnerElement != player2Element)
            {
                listener.OnElementDefeated(player2, player2Element); // <== Show that player 2's element was defeated
                if (player2.HasAliveElements) player2Element = player2.SelectElement();
            }
            else throw new InvalidOperationException("Ronda terminada en un empate, lo cual no debería suceder.");
        }

        if (player1.HasAliveElements)
        {
            return player1; // <== Return player 1 as the winner
        }
        else if (player2.HasAliveElements)
        {
            return player2; // <== Return player 2 as the winner
        }
        else
        {
            throw new InvalidOperationException("Ambos jugadores no tienen elementos vivos, lo cual no debería suceder.");
        }
    }
}
