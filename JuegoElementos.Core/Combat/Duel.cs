using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Combat;

public class Duel(Player player1, Player player2, DamageCalculator damageCalculator, IGameView gameView)
{
    private readonly Player _player1 = player1;
    private readonly Player _player2 = player2;
    private readonly DamageCalculator _damageCalculator = damageCalculator;
    private readonly IGameView _gameView = gameView;

    public bool IsFinished => !_player1.HasAliveElements || !_player2.HasAliveElements;

    public Player GetWinner()
    {
        var player1Element = _player1.SelectElement();
        _gameView.ShowElementPresented(_player1, player1Element); // <== Show that player 1 has presented an element
        var player2Element = _player2.SelectElement();
        _gameView.ShowElementPresented(_player2, player2Element); // <== Show that player 2 has presented an element

        while (_player1.HasAliveElements && _player2.HasAliveElements)
        {
            var round = new Round(player1Element, player2Element, _damageCalculator, _gameView);
            var winnerElement = round.GetWinner();

            if (winnerElement != player1Element)
            {
                _gameView.ShowElementDefeated(_player1, player1Element); // <== Show that player 1's element was defeated
                if (_player1.HasAliveElements) player1Element = _player1.SelectElement();
            }
            else if (winnerElement != player2Element)
            {
                _gameView.ShowElementDefeated(_player2, player2Element); // <== Show that player 2's element was defeated
                if (_player2.HasAliveElements) player2Element = _player2.SelectElement();
            }
            else throw new InvalidOperationException("Ronda terminada en un empate, lo cual no debería suceder.");
        }

        if (_player1.HasAliveElements)
        {
            return _player1; // <== Return player 1 as the winner
        }
        else if (_player2.HasAliveElements)
        {
            return _player2; // <== Return player 2 as the winner
        }
        else
        {
            throw new InvalidOperationException("Ambos jugadores no tienen elementos vivos, lo cual no debería suceder.");
        }
    }
}
