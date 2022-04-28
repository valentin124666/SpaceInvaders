using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : IController
{
    private float _border = float.NegativeInfinity;

    public Camera Camera { get; private set; }

    public Vector3 MinPos
    {
        get
        {
            return Camera.ScreenToWorldPoint(new Vector3(0, 0));
        }
    }
    public Vector3 MaxPos 
    {
        get
        {
            return Camera.ScreenToWorldPoint(new Vector3(Camera.pixelWidth, Camera.pixelHeight));
        }
    }

    public float Border
    {
        get
        {
            if (_border == float.NegativeInfinity)
            {
                _border = Camera.aspect * Camera.orthographicSize;
            }
            return _border;
        }
    }
    public void Dispose()
    {

    }

    public void Init()
    {
        Camera = Camera.main;
    }

    public void ResetAll()
    {
    }

    public void Update()
    {

    }
}
