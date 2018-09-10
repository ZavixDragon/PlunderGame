using Assets.Scripts.Code.Message;
using UnityEngine;

namespace Assets.Scripts.PlunderX
{
    public class LightingScript : MonoBehaviour
    {
        public GameObject PlunderLighting;
        public GameObject NormalLighting;
        public LargeClouds LargeClouds;
        public GameObject Clouds;

        public LightingScript Spawn(PlunderBody body)
        {
            var lighting = Instantiate(this);
            if (body.Plunder.IsPlunder)
                lighting.PlunderLighting.SetActive(true);
            else
                lighting.NormalLighting.SetActive(true);
            return lighting;
        }

        public void Start()
        {
            Messages.ListenFor<GameFinished>(x => Clouds.SetActive(false), this);
        }

        public void OnDestroy()
        {
            Messages.StopListening<GameFinished>(this);
        }
    }
}
