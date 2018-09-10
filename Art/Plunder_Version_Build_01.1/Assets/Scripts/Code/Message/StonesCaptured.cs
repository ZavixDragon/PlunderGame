using Assets.Scripts.Code.CoreGame;

namespace Assets.Scripts.Code.Message
{
    public class StonesCaptured
    {
        public Player Player { get; }
        public int Count { get; }

        public StonesCaptured(Player player, int count)
        {
            Player = player;
            Count = count;
        }
    }
}
