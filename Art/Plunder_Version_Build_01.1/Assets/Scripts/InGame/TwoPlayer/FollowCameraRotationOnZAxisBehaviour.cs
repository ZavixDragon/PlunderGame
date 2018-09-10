using Assets.Scripts.Code.UI;
using UnityEngine;

public class FollowCameraRotationOnZAxisBehaviour : MonoBehaviour
{
    public void Update()
    {
        transform.eulerAngles = new Vector3(90, 0, -GameResources.Cameras.ActiveCamera.transform.eulerAngles.y);
    }
}
