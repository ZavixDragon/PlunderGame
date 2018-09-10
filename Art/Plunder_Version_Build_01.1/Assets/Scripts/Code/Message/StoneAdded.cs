using Assets.Scripts.Code.CoreGame;

namespace Assets.Scripts.Code.Message
{
    public class StoneAdded
    {
        public Player Player { get; }
        public Position Position { get; }

        public StoneAdded(Player player, Position position)
        {
            Player = player;
            Position = position;
        }
    }
}
