using Assets.Scripts.Code.UI;
using UnityEngine;

namespace Assets.Scripts
{
    public class SelfRegisteringCamera : MonoBehaviour
    {
        public Camera Camera;

        public void Start()
        {
            GameResources.Cameras.AddCamera("player1", Camera);
        }
    }
}
