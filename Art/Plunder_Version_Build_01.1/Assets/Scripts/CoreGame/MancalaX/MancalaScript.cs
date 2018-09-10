using Assets.Scripts.Code.CoreGame;
using Assets.Scripts.Code.UI;
using Assets.Scripts.Temp.EnigmaDragons;

namespace Assets.Scripts.CoreGame.MancalaX
{
    public class MancalaScript : Script
    {
        public Mancala Mancala { get; set; }

        public MancalaScript Spawn(Mancala mancala)
        {
            var mancalaScript = Instantiate(this);
            mancalaScript.ID = mancala.ID;
            mancalaScript.Mancala = mancala;
            mancala.UnsortedBowls.ForEach(x => GameResources.Bowls[x.Owner][x.Position].Spawn(x, mancalaScript.transform));
            return mancalaScript;
        }
    }
}
