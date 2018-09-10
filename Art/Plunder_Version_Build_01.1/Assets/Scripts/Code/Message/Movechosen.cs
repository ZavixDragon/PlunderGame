using Assets.Scripts.Code.CoreGame;

namespace Assets.Scripts.Code.Message
{
    public class MoveChosen
    {
        public Position Pick { get; }

        public MoveChosen(Position pick)
        {
            Pick = pick;
        }
    }
}
