using System;
using System.Collections.Generic;
using Assets.Scripts.Code.Common;
using UnityEngine;
using Random = System.Random;

public class SwayingCamera : MonoBehaviour
{
    public float MoveVariance;
    public float RotateVariance;
    public float MoveSpeed;
    public float RotateSpeed;
    public bool XAxis;
    public bool YAxis;
    public bool ZAxis;
    public bool XRotation;
    public bool YRotation;
    public bool ZRotation;

    private AxisDetails _xAxisDetails;
    private AxisDetails _yAxisDetails;
    private AxisDetails _zAxisDetails;
    private RotationDetails _xRotationDetails;
    private RotationDetails _yRotationDetails;
    private RotationDetails _zRotationDetails;
    private Random _random;

    public void Start()
    {
        _xAxisDetails = new AxisDetails(XAxis, 
            x => transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z), 
            () => transform.position.x);
        _yAxisDetails = new AxisDetails(YAxis,
            y => transform.position = new Vector3(transform.position.x, transform.position.y + y, transform.position.z),
            () => transform.position.y);
        _zAxisDetails = new AxisDetails(ZAxis,
            z => transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + z),
            () => transform.position.z);
        _xRotationDetails = new RotationDetails(XRotation, new List<bool> { true, false }.Random(), () => transform.eulerAngles.x);
        _yRotationDetails = new RotationDetails(YRotation, new List<bool> { true, false }.Random(), () => transform.eulerAngles.y);
        _zRotationDetails = new RotationDetails(ZRotation, new List<bool> { true, false }.Random(), () => transform.eulerAngles.z);
        _random = new Random(Guid.NewGuid().GetHashCode());
    }

    public void Update()
    {
        UpdateAxis(_xAxisDetails);
        UpdateAxis(_yAxisDetails);
        UpdateAxis(_zAxisDetails);
        UpdateRotation();
    }

    private void UpdateAxis(AxisDetails details)
    {
        if (!details.Active)
            return;
        var isPositiveTrend = details.AxisTrend > details.OriginalAxis;
        var velocityChange = MoveSpeed * Time.deltaTime;
        if (!DoesPassAxisTrend(isPositiveTrend, details.GetPosition() + details.Velocity, details.AxisTrend))
            details.Velocity += isPositiveTrend ? velocityChange : -velocityChange;
        else if (DoesPassAxisTrend(isPositiveTrend, details.GetPosition() + details.Velocity - velocityChange, details.AxisTrend))
            details.Velocity += isPositiveTrend ? -velocityChange : velocityChange;
        details.SetPosition(details.Velocity);
        if (DoesPassAxisTrend(isPositiveTrend, details.GetPosition(), details.AxisTrend))
            details.AxisTrend = isPositiveTrend
                ? details.OriginalAxis - (float)_random.NextDouble() * MoveVariance / 2
                : details.OriginalAxis + (float)_random.NextDouble() * MoveVariance / 2;
    }

    private bool DoesPassAxisTrend(bool isPositiveTrend, float position, float trend)
    {
        return isPositiveTrend
            ? position >= trend
            : position <= trend;
    }

    private void UpdateRotation()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(
            transform.forward, 
            new Vector3(_xRotationDetails.RotationTrend, _yRotationDetails.RotationTrend, _zRotationDetails.RotationTrend), 
            RotateSpeed * Time.deltaTime, 
            0.0f));
        GetNewRotationTrend(_xRotationDetails);
        GetNewRotationTrend(_yRotationDetails);
        GetNewRotationTrend(_zRotationDetails);
    }

    private void GetNewRotationTrend(RotationDetails details)
    {
        if (!details.Active || details.RotationTrend != details.GetRotation())
            return;
        details.IsPositiveTrend = !details.IsPositiveTrend;
        details.RotationTrend = details.IsPositiveTrend
            ? details.OriginalRotation + RotateVariance * (float) _random.NextDouble()
            : details.OriginalRotation - RotateVariance * (float) _random.NextDouble();
        if (details.RotationTrend < 0)
            details.RotationTrend += 360;
        if (details.RotationTrend > 360)
            details.RotationTrend -= 360;
    }
}

public class AxisDetails
{
    public bool Active { get; }
    public float OriginalAxis { get; }
    public float AxisTrend { get; set; }
    public float Velocity { get; set; }
    public Action<float> SetPosition { get; }
    public Func<float> GetPosition { get; } 

    public AxisDetails(bool active, Action<float> setPosition, Func<float> getPosition)
    {
        Active = active;
        OriginalAxis = getPosition();
        AxisTrend = OriginalAxis;
        SetPosition = setPosition;
        GetPosition = getPosition;
    }
}

public class RotationDetails
{
    public bool Active { get; }
    public float OriginalRotation { get; }
    public float RotationTrend { get; set; }
    public bool IsPositiveTrend { get; set; }
    public Func<float> GetRotation { get; }

    public RotationDetails(bool active, bool isPositiveTrend, Func<float> getRotation)
    {
        Active = active;
        OriginalRotation = GetRotation();
        RotationTrend = OriginalRotation;
        IsPositiveTrend = isPositiveTrend;
        GetRotation = GetRotation;
    }
}
