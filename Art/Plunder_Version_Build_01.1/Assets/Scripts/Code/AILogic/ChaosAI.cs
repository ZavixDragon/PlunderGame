using System.Linq;
using Assets.Scripts.Code.Common;
using Assets.Scripts.Code.CoreGame;

namespace Assets.Scripts.Code.AILogic
{
    public class ChaosAI : AI
    {
        public void MakeAMove(Mancala _mancala)
        {
            _mancala.Pick(_mancala.Bowls[Player.Two].Bowls.Where(x => x.StoneIDs.Count > 0).Random().Position);
        }
    }
}
