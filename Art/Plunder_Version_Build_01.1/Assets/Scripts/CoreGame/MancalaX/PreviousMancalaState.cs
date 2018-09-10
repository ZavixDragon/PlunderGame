using Assets.Scripts.Code.CoreGame;

namespace Assets.Scripts.CoreGame.MancalaX
{
    public class PreviousMancalaState
    {
        public Player PlayerToAct { get; set; }
        public string LastBowlID { get; set; }
        public Position LastPick { get; set; }
        public PreviousMancalaState PreviousState { get; set; }

        public PreviousMancalaState() {}

        public PreviousMancalaState(Player playerToAct, string lastBowlID, Position lastPick, PreviousMancalaState previousState)
        {
            PlayerToAct = playerToAct;
            LastBowlID = lastBowlID;
            LastPick = lastPick;
            PreviousState = previousState;
        }
    }
}
