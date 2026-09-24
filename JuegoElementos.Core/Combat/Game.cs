using JuegoElementos.Core.Abstractions;
using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Combat
{
    public class Game(Player humanPlayer, Player aiPlayer, IGameView view)
    {
        private Player HumanPlayer { get; set; } = humanPlayer;
        private Player AiPlayer { get; set; } = aiPlayer;
        public Duel Duel { get; private set; } = new Duel(view);
        public bool IsFinished => !HumanPlayer.HasAliveElements || !AiPlayer.HasAliveElements;
        public IGameView View { get; private set; } = view;
        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}