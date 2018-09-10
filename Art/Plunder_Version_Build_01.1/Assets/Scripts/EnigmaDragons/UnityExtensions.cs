using UnityEngine;

namespace Assets.Scripts.EnigmaDragons
{
    public static class UnityExtensions
    {
        public static void SetParentWithUnmodifiedTransform(this Transform transform, Transform parent)
        {
            var localPosition = transform.localPosition;
            var localRotation = transform.localRotation;
            var localScale = transform.localScale;
            transform.parent = parent;
            transform.localPosition = localPosition;
            transform.localRotation = localRotation;
            transform.localScale = localScale;
        }
    }
}
