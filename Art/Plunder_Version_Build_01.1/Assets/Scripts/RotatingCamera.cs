using UnityEngine;

public class RotatingCamera : MonoBehaviour
{
    public float Speed = 1;

	public void Update ()
	{
	    transform.Rotate(Vector3.up * Time.deltaTime * Speed);
    }
}
