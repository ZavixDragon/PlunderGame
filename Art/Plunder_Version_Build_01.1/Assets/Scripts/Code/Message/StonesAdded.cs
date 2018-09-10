using Assets.Scripts.Code.CoreGame;

namespace Assets.Scripts.Code.Message
{
    public class StonesAdded
    {
        public Player Player { get; }
        public Position Position { get; }
        public int Count { get; }

        public StonesAdded(Player player, Position position, int count)
        {
            Player = player;
            Position = position;
            Count = count;
        }
    }
}
