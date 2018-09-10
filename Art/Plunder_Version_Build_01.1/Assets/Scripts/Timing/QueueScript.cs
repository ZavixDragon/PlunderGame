using UnityEngine;

namespace Assets.Scripts.Timing
{
    public class QueueScript : MonoBehaviour
    {
        public Queue Queue { get; private set; }

        public void Awake()
        {
            Queue = new Queue();
        }

        public void Update()
        {
            Queue.Update(Time.deltaTime);
        }
    }
}
