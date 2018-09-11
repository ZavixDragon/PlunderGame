using Assets.Scripts.BowlX;
using Assets.Scripts.Code.Common;
using Assets.Scripts.Code.CoreGame;
using Assets.Scripts.Code.UI;
using Assets.Scripts.Temp.EnigmaDragons;
using Assets.Scripts.Timing;
using UnityEngine;
using Bowl = Assets.Scripts.BowlX.Bowl;

namespace Assets.Scripts.Temp.StoneX
{
    public sealed class StoneScript : Script
    {
        public float Lift = 7;
        public float Speed = 5;
        public Rigidbody RigidBody;
        public Collider Collider; 
        public bool _shouldPlaySoundOnCollision = true;

        public StoneBody Body { get; set; }

        public StoneScript Spawn(StoneBody body, Transform parent)
        {
            var gameObj = Instantiate(this, body.StoneData.Position.Value, body.StoneData.Rotation.Value, parent);
            //gameObj.RigidBody.useGravity = body.StoneData.UseGravity;
            //gameObj.Collider.isTrigger = !body.StoneData.AllowCollision;
            body.StoneData.Position.OnChanged(x => gameObj.transform.position = x, gameObj);
            body.StoneData.Rotation.OnChanged(x => gameObj.transform.rotation = x, gameObj);
            body.Stone.StoneData.BowlID.OnChanged(x => GameResources.Queue.MoveStone(gameObj.ID, x), gameObj);
            //This piece code enables the stones to line up on preview to allow players to count them. But it has a few quirks so it has been temporarily disabled to produce a working build
            //body.StoneData.IsPreviewing.OnChanged(x =>
            //{
            //    if (x)
            //        gameObj.Preview();
            //    else
            //        GameResources.Queue.ReturnToBowl(gameObj.ID);
            //}, gameObj);
            gameObj.Body = body;
            gameObj.ID = body.ID;
            return gameObj;
        }

        public void MoveTo(Bowl bowl)
        {
            var bowlScript = Access.Script<BowlScript>(bowl.ID);
            gameObject.transform.SetParent(bowlScript.transform);
            Body.StoneData.Target = bowlScript.transform.position;
            var position = bowl.Position;
            var x = position == Position.Score ? 0 : 7.6f;
            var y = position == Position.Score ? Lift - 1 : Lift;
            var z = position == Position.Score ? -0.5f : 0;
            Body.StoneData.Target = new Vector3(Body.StoneData.Target.x - x, Body.StoneData.Target.y + y, Body.StoneData.Target.z + z);
            Body.StoneData.TargetReached = false;
            RigidBody.useGravity = false;
            Body.StoneData.UseGravityUponTargetReached = true;
        }

        public void Update()
        {
            if (!Body.StoneData.TargetReached)
            {
                transform.position = Vector3.MoveTowards(transform.position, Body.StoneData.Target,
                    Mathf.Min(Time.deltaTime * Speed, Vector3.Distance(transform.position, Body.StoneData.Target)));
                Body.StoneData.TargetReached = Vector3.Distance(transform.position, Body.StoneData.Target) < 0.1;
                if (Body.StoneData.TargetReached)
                {
                    RigidBody.velocity = new Vector3(0, 0, 0);
                    if (Body.StoneData.UseGravityUponTargetReached)
                    {
                        RigidBody.useGravity = true;
                        _shouldPlaySoundOnCollision = true;
                    }
                    if (Body.StoneData.AllowCollisionUponTargetReached)
                    {
                        Collider.isTrigger = false;
                    }
                }
            }
            Body.StoneData.Position.Set(this, transform.position);
            Body.StoneData.Rotation.Set(this, transform.rotation);
            Body.StoneData.UseGravity = RigidBody.useGravity;
            Body.StoneData.AllowCollision = Collider.isTrigger;
        }

        public void OnCollisionEnter(Collision other)
        {
            if (_shouldPlaySoundOnCollision)
            {
                GameResources.StoneDropSounds.Random().Play();
                _shouldPlaySoundOnCollision = false;
            }
        }

        public new void OnDestroy()
        {
            base.OnDestroy();
            Body.StoneData.Position.RemoveChangeEvents(this);
            Body.StoneData.Rotation.RemoveChangeEvents(this);
        }

        private void Preview()
        {
            var row = Body.StoneData.SortingNumber % 5;
            var column = Mathf.Floor(Body.StoneData.SortingNumber / 5);
            column += Mathf.Floor(Body.StoneData.SortingNumber / 10);
            var y = 2.5 - row * 0.1;
            var z = 3.4 - row * 0.5;
            var x = -3 + column * 0.5;
            if (GameResources.Game.PlayerToAct == Player.Two)
            {
                x = -x;
                z = -z;
            }
            Body.StoneData.Target = new Vector3((float)x, (float)y, (float)z);
            Body.StoneData.TargetReached = false;
            RigidBody.useGravity = false;
            Body.StoneData.UseGravityUponTargetReached = false;
            Collider.isTrigger = false;
            Body.StoneData.AllowCollisionUponTargetReached = false;
        }

        public void StopPreviewing()
        {
            var bowlScript = Access.Script<BowlScript>(Body.Stone.Bowl.ID);
            Body.StoneData.Target = bowlScript.transform.position;
            var position = Body.Stone.Bowl.Position;
            var x = position == Position.Score ? 0 : 7.6f;
            var y = position == Position.Score ? Lift - 1 : Lift;
            var z = position == Position.Score ? -0.5f : 0;
            Body.StoneData.Target = new Vector3(Body.StoneData.Target.x - x, Body.StoneData.Target.y + y, Body.StoneData.Target.z + z);
            Body.StoneData.TargetReached = false;
            RigidBody.useGravity = false;
            Body.StoneData.UseGravityUponTargetReached = true;
            Collider.isTrigger = true;
            Body.StoneData.AllowCollisionUponTargetReached = true;
        }
    }
}
