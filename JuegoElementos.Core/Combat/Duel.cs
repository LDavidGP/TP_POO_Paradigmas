using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.Strategies;

namespace JuegoElementos.Core.Combat;

public class Duel(Player player1, Player player2, IDamageCalculator damageCalculator, ICombatEventsListener listener)
{
    public bool IsFinished => !player1.HasAliveElements || !player2.HasAliveElements;

    public Player GetWinner()
    {
        //1- Initial elements selection
        var contextP1 = new CombatContext(player1.AliveElements,null, damageCalculator);
        var p1Element = player1.SelectElement(contextP1);
        
        var contextP2 = new CombatContext(player2.AliveElements, p1Element, damageCalculator);
        var p2Element = player2.SelectElement(contextP2);
        
        //2. Combat by rounds main loop 
        while (player1.HasAliveElements && player2.HasAliveElements)
        {
            listener.OnBattlefieldUpdated(p1Element, player1.RemainingElements, p2Element, player2.RemainingElements);
            
            var round = new Round(p1Element, p2Element, damageCalculator, listener);
            round.Execute();

            if (!p1Element.IsAlive)
            {
                listener.OnElementDefeated(player1, p1Element);
                if (player1.HasAliveElements)
                {
                    contextP1 = new CombatContext(player1.AliveElements, p2Element, damageCalculator);
                    p1Element = player1.SelectElement(contextP1);
                }
            }
            
            if (!p2Element.IsAlive)
            {
                listener.OnElementDefeated(player2, p2Element);
                if (player2.HasAliveElements)
                { 
                 contextP2 = new CombatContext(player2.AliveElements, p1Element, damageCalculator);
                 p2Element = player2.SelectElement(contextP2);
                }
            }
        }

        return player1.HasAliveElements ? player1 : player2;
    }
}
