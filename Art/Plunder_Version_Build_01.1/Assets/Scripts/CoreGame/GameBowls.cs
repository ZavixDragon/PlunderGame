using System.Collections.Generic;

namespace Assets.Scripts.Code.CoreGame
{
    public class GameBowls
    {
        private readonly List<PlayerBowls> _playerBowls;
        public PlayerBowls this[Player player] => _playerBowls[(int)player - 1];

        public GameBowls(List<PlayerBowls> playerBowls)
        {
            _playerBowls = playerBowls;
        }
    }
}
