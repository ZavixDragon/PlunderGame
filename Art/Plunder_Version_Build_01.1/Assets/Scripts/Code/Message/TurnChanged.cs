using Assets.Scripts.Code.CoreGame;

namespace Assets.Scripts.Code.Message
{
    public class TurnChanged
    {
        public Player Player { get; }

        public TurnChanged(Player player)
        {
            Player = player;
        }
    }
}
