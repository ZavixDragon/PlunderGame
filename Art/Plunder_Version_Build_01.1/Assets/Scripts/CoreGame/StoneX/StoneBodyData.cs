using System;
using System.Runtime.Serialization;
using Assets.Scripts.CoreGame.StoneX;
using Assets.Scripts.EnigmaDragons;
using Assets.Scripts.Temp.EnigmaDragons;
using UnityEngine;

namespace Assets.Scripts.Temp.StoneX
{
    public sealed class StoneBodyData : BodyData
    {
        public PreviousStoneBodyState _previousStoneBodyState { get; set; }
        public Vector3 _position { get; set; }
        public Quaternion _rotation { get; set; }
        public Vector3 Target { get; set; }
        public bool TargetReached { get; set; } = true;
        public bool UseGravityUponTargetReached { get; set; } = true;
        public bool AllowCollisionUponTargetReached { get; set; } = true;
        public bool UseGravity { get; set; } = true;
        public bool AllowCollision { get; set; } = true;
        public bool _isPreviewing { get; set; }
        public int SortingNumber { get; set; }
        [IgnoreDataMember]
        public Notifying<Vector3> Position { get; }
        [IgnoreDataMember]
        public Notifying<Quaternion> Rotation { get; }
        [IgnoreDataMember]
        public Notifying<bool> IsPreviewing { get; }

        //Serialization
        public StoneBodyData()
        {
            Position = new Notifying<Vector3>(() => _position, x => _position = x);
            Rotation = new Notifying<Quaternion>(() => _rotation, x => _rotation = x);
            IsPreviewing = new Notifying<bool>(() => _isPreviewing, x => _isPreviewing = x);
        }

        public StoneBodyData(string id, Vector3 position, Quaternion rotation) : base(id)
        {
            _position = position;
            _rotation = rotation;
            Position = new Notifying<Vector3>(() => _position, x => _position = x);
            Rotation = new Notifying<Quaternion>(() => _rotation, x => _rotation = x);
            IsPreviewing = new Notifying<bool>(() => _isPreviewing, x => _isPreviewing = x);
        }

        public void PrepForStateChange()
        {
            _previousStoneBodyState = new PreviousStoneBodyState(_position, _rotation, _previousStoneBodyState);
        }

        public void Undo()
        {
            if (_previousStoneBodyState == null)
                throw new InvalidOperationException("No actions to undo");
            Position.Set(this, _previousStoneBodyState.Position);
            Rotation.Set(this, _previousStoneBodyState.Rotation);
            IsPreviewing.Set(this, false);
            _previousStoneBodyState = _previousStoneBodyState.PreviousState;
            TargetReached = true;
        }
    }
}
