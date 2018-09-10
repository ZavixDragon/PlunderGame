namespace Assets.Scripts.CoreGame.StoneX
{
    public class PreviousStoneState
    {
        public string BowlID { get; set; }
        public PreviousStoneState PreviousState { get; set; }

        public PreviousStoneState() {}

        public PreviousStoneState(string bowlID, PreviousStoneState previousState)
        {
            BowlID = bowlID;
            PreviousState = previousState;
        }
    }
}
