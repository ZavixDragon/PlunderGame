using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.BowlX;

namespace Assets.Scripts.Code.CoreGame
{
    public class PlayerBowls
    {
        private readonly Mancala _mancala;
        private readonly Player _owner;

        private Bowl _scoreBowl => _mancala.UnsortedBowls.First(bowl => bowl.Position == Position.Score && bowl.Owner == _owner);

        public List<Bowl> Bowls => _mancala.UnsortedBowls.Where(bowl => bowl.Position != Position.Score && bowl.Owner == _owner)
            .OrderBy(x => x.Position).ToList();
        public Bowl this[Position position] => position == Position.Score ? _scoreBowl : this[(int)position];
        public Bowl this[int index] => Bowls[index];
        public bool HasAvailableMove => Bowls.Any(x => x.StoneIDs.Count != 0);


        public PlayerBowls(Mancala mancala, Player owner)
        {
            _mancala = mancala;
            _owner = owner;
        }

        public void ScoreAll()
        {
            Bowls.ForEach(x => x.Score(_scoreBowl));
        }
    }
}
