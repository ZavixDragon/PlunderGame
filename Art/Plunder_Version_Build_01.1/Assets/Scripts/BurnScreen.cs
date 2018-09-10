using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class BurnScreen : MonoBehaviour
    {
        public Image Image;
        private bool _shouldTransition;
        private float _threshold;

        public void Update()
        {
            if (_shouldTransition)
            {
                _threshold = Mathf.Min(1, _threshold + (Time.deltaTime / 2));
                Image.material.SetFloat("_Threshold", _threshold);
                if (_threshold == 1)
                {
                    _shouldTransition = false;
                    gameObject.SetActive(false);
                }
            }
        }

        public void OnEnable()
        {
            _shouldTransition = false;
            Image.material.SetFloat("_Threshold", 0);
            _threshold = 0;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }

        public void Transition()
        {
            if (gameObject.activeSelf)
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }
                _shouldTransition = true;
            }
        }
    }
}
