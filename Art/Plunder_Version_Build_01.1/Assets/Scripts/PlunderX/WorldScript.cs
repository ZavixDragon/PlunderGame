using System;
using Assets.Scripts.Code.UI;
using UnityEngine;

namespace Assets.Scripts.PlunderX
{
    public class WorldScript : MonoBehaviour
    {
        public int Speed = 60;
        public int LiftSpeed = 1;

        public PlunderBody Body { get; set; }

        public WorldScript Spawn(PlunderBody body)
        {
            var world = Instantiate(GameResources.Worlds[Mathf.Abs(body.ID.GetHashCode() % GameResources.Worlds.Count)]);
            world.Body = body;
            return world;
        }

        public float RotateAmount
        {
            get { return Body.Data.RotateAmount; }
            set { Body.Data.RotateAmount = value; }
        }

        public float LiftAmount
        {
            get { return Body.Data.LiftAmount; }
            set { Body.Data.LiftAmount = value; }
        }

        public new void Start()
        {
            transform.position = Body.Data.WorldPosition;
            transform.rotation = Body.Data.WorldRotation;
        }

        public void Update()
        {
            if (RotateAmount > 0)
            {
                if (LiftAmount == 1.5f || Body.Plunder.IsPlunder)
                {
                    var thisRotation = Math.Min(Time.deltaTime * Speed, RotateAmount);
                    RotateAmount -= thisRotation;
                    transform.Rotate(0, thisRotation, 0);
                }
                else
                {
                    var lift = Math.Min(Time.deltaTime * LiftSpeed, 1.5f - LiftAmount);
                    LiftAmount += lift;
                    transform.position = new Vector3(transform.position.x, transform.position.y - lift, transform.position.z);
                }
            }
            else if (LiftAmount > 0)
            {
                var lift = Math.Min(Time.deltaTime * LiftSpeed, LiftAmount);
                LiftAmount -= lift;
                transform.position = new Vector3(transform.position.x, transform.position.y + lift, transform.position.z);
            }
            Body.Data.WorldPosition = transform.position;
            Body.Data.WorldRotation = transform.rotation;
        }
    }
}
