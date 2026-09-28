using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.Strategies;

namespace JuegoElementos.Core.Combat;

public class Game(Player player1, Player player2, IDamageCalculator damageCalculator, ICombatEventsListener listener)
{
    public bool IsFinished => !player1.HasAliveElements || !player2.HasAliveElements;

   public Player Play()
    {
        // 1. Inicializamos los primeros elementos usando la lista de vivos (AliveElements)
        Element p1Element = player1.SelectElement(new CombatContext(player1.AliveElements, null, damageCalculator));
        Element p2Element = player2.SelectElement(new CombatContext(player2.AliveElements, p1Element, damageCalculator));

        while (player1.HasAliveElements && player2.HasAliveElements)
        {
            // 2. Si la IA tiene Super IA, le actualizamos la carta rival actual
            if (player2.SelectionStrategy is SuperSelectionStrategy superStrategy)
            {
                superStrategy.OpponentElement = p1Element;
            }

            // 3. Notificamos a la interfaz mandando la lista de vivos
            listener.OnBattlefieldUpdated(p1Element, player1.RemainingElements, p2Element, player2.RemainingElements);

            // 4. Se ejecuta la ronda
            var round = new Round(p1Element, p2Element, damageCalculator, listener);
            round.Execute();

            // 5. Reemplazo para Jugador 1 si cae su elemento
            if (!p1Element.IsAlive)
            {
                listener.OnElementDefeated(player1, p1Element);
                if (player1.HasAliveElements)
                {
                    var contextP1 = new CombatContext(player1.AliveElements, p2Element, damageCalculator);
                    p1Element = player1.SelectElement(contextP1);
                }
            }

            // 6. Reemplazo para Jugador 2 (IA) reaccionando a la carta del humano
            if (!p2Element.IsAlive)
            {
                listener.OnElementDefeated(player2, p2Element);
                if (player2.HasAliveElements)
                {
                    if (player2.SelectionStrategy is SuperSelectionStrategy superIA)
                    {
                        superIA.OpponentElement = p1Element;
                    }

                    var contextP2 = new CombatContext(player2.AliveElements, p1Element, damageCalculator);
                    p2Element = player2.SelectElement(contextP2);
                }
            }
        }

        var winner = player1.HasAliveElements ? player1 : player2;
        return winner;
    }

    public Player Start() => Play();
    public Player GetWinner() => Play();
}
