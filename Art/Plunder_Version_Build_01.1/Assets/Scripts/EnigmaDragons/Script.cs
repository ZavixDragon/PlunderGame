using UnityEngine;

namespace Assets.Scripts.Temp.EnigmaDragons
{
    public abstract class Script : MonoBehaviour
    {
        public string ID { get; set; }

        public void Start()
        {
           Access.Add(this);
        }

        public void OnDestroy()
        {
            Access.Remove(this);
        }
    }
}
