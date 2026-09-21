using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Combat
{
    public class Game(Player humanPlayer, Player aiPlayer, IGameView view)
    {
        public Player HumanPlayer { get; private set; } = humanPlayer;
        public Player AIPlayer { get; private set; } = aiPlayer;
        public Duel Duel { get; private set; } = new Duel();
        public bool IsFinished => !HumanPlayer.HasAliveElements || !AIPlayer.HasAliveElements;
        public IGameView View { get; private set; } = view;
        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}