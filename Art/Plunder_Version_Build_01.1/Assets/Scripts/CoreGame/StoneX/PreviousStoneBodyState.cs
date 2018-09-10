using UnityEngine;

namespace Assets.Scripts.CoreGame.StoneX
{
    public class PreviousStoneBodyState
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public PreviousStoneBodyState PreviousState { get; set; }

        public PreviousStoneBodyState() {}

        public PreviousStoneBodyState(Vector3 position, Quaternion rotation, PreviousStoneBodyState previousState)
        {
            Position = position;
            Rotation = rotation;
            PreviousState = previousState;
        }
    }
}
