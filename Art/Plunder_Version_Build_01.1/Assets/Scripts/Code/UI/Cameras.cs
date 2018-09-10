using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Code.Common;
using UnityEngine;

namespace Assets.Scripts.Code.UI
{
    public class Cameras
    {
        private readonly Dictionary<string, Camera> _cameras = new Dictionary<string, Camera>();
        public Camera ActiveCamera { get; private set; }

        public void SetActive(string cameraName)
        {
            var camera = _cameras[cameraName];
            camera.enabled = true;
            _cameras.Values.Where(x => x != camera).ForEach(x => x.enabled = false);
            ActiveCamera = camera;
        }

        public void AddCamera(string cameraName, Camera camera)
        {
            if (ActiveCamera == null)
            {
                camera.enabled = true;
                ActiveCamera = camera;
            }
            else
            {
                camera.enabled = false;
            }
            _cameras[cameraName] = camera;
        }
    }
}
