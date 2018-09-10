using Assets.Scripts.Code.CoreGame;

namespace Assets.Scripts.Code.Message
{
    public class GameFinished
    {
        public Player Winner { get; }

        public GameFinished(Player winner)
        {
            Winner = winner;
        }
    }
}
