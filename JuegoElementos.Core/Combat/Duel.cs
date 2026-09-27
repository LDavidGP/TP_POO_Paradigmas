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
        //Initial selection
        var element1 = _player1.SelectElement();
        var element2 = _player2.SelectElement();
        _gameView.ShowBattlefield(
            element1,
            _player1.AliveElements.Count,
            element2,
            _player1.AliveElements.Count
        );
        while (_player1.HasAliveElements && _player2.HasAliveElements)
        {
            _gameView.ShowBattlefield(element1, player1.AliveElements.Count, element2, player2.AliveElements.Count);
            var round = new Round(element1, element2, _damageCalculator, _gameView);
            var winnerElement = round.GetWinner();

            if (winnerElement != element1)
            {
                _gameView.ShowElementDefeated(_player1, element1); // <== Show that player 1's element was defeated
                if (_player1.HasAliveElements) element1 = _player1.SelectElement();
            }
            else if (winnerElement != element2)
            {
                _gameView.ShowElementDefeated(_player2, element2); // <== Show that player 2's element was defeated
                if (_player2.HasAliveElements) element2 = _player2.SelectElement();
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
