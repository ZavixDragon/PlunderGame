using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public float Speed;
    public List<GameObject> Points;

    private List<Vector3> _points;
    private int _index;

    public void Start()
    {
        _points = Points.Select(x => x.transform.eulerAngles).ToList();
    }

    public void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, _points[_index], Speed * Time.deltaTime, 0.0f));
        if (Math.Abs(transform.eulerAngles.x - _points[_index].x) < 0.1
            && Math.Abs(transform.eulerAngles.y - _points[_index].y) < 0.1
            && Math.Abs(transform.eulerAngles.z - _points[_index].z) < 0.1)
            _index++;
        if (_index == Points.Count)
            _index = 0;
    }
}
