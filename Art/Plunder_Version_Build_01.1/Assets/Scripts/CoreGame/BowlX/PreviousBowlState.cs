using System.Collections.Generic;

namespace Assets.Scripts.CoreGame.BowlX
{
    public class PreviousBowlState
    {
        public List<string> StoneIDs { get; set; }
        public PreviousBowlState PreviousState { get; set; }

        public PreviousBowlState() {}

        public PreviousBowlState(List<string> stoneIDs, PreviousBowlState previousState)
        {
            StoneIDs = stoneIDs;
            PreviousState = previousState;
        }
    }
}
