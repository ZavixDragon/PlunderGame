using Assets.Scripts.Code.CoreGame;
using Assets.Scripts.Code.Message;
using Assets.Scripts.Code.UI;
using Assets.Scripts.Temp;
using Assets.Scripts.Temp.EnigmaDragons;
using UnityEngine;

namespace Assets.Scripts.PlunderX
{
    public class PlunderBodyData : BodyData
    {
        public Vector3 WorldPosition { get; set; }
        public Quaternion WorldRotation { get; set; }
        public bool IsOriginalPosition { get; set; }
        public float RotateAmount { get; set; }
        public float LiftAmount { get; set; }

        //Serialization
        public PlunderBodyData() {}

        public PlunderBodyData(string id, Vector3 worldPosition, Quaternion worldRotation) : base(id)
        {
            WorldPosition = worldPosition;
            WorldRotation = worldRotation;
            IsOriginalPosition = true;
            RotateAmount = 0;
            LiftAmount = 0;
            Messages.ListenFor<TurnChanged>(Rotate, this);
        }

        private void Rotate(TurnChanged turnChanged)
        {
            if (Access.Data<PlunderData>(ID).GameType != GameType.HotSeat 
                    || (turnChanged.Player == Player.One && IsOriginalPosition) 
                    || (turnChanged.Player == Player.Two && !IsOriginalPosition))
                return;
            IsOriginalPosition = !IsOriginalPosition;
            GameResources.Queue.FlipBoard(ID);
        }
    }
}
