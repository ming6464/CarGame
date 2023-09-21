using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] private Transform _atachedVehicle;
    [SerializeField] private Transform _cam;
    
    [Range(0f,1f)]
    [SerializeField] private float _smoothTime = .5f;


    private void FixedUpdate()
    {
        transform.position = _cam.position * (1 - _smoothTime) + transform.position * _smoothTime;
        transform.LookAt(_atachedVehicle);
    }
}
