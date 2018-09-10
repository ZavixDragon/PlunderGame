using Assets.Scripts.Code.CoreGame;

namespace Assets.Scripts.Code.Message
{
    public class StonesRemoved
    {
        public Player Player { get; }
        public Position Position { get; }

        public StonesRemoved(Player player, Position position)
        {
            Player = player;
            Position = position;
        }
    }
}
