using JuegoElementos.Core.Abstractions;

namespace JuegoElementos.Core.Combat
{
    public class Duel(IGameView gameView)
    {
        readonly IGameView _gameView = gameView;

        public void StartDuel()
        {
            // Logic to start the duel
        }
    }
}