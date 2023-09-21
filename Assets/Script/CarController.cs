using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AxleInfo {
    public WheelCollider LeftWheel;
    public WheelCollider RightWheel;
    public Transform LeftWheelModel;
    public Transform RightWheelModel;
    public bool Motor;
    public bool Steering;
    public bool Brake;
}

public class CarController : MonoBehaviour
{
    public List<AxleInfo> AxleInfos;
    public float MaxMotorTorque;
    public float MaxSteeringAngle;
    public float BrakeTorque;
    public bool IsBrake;
    private float brakeTorque;

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider,Transform wheelTf)
    {
        if(collider == null || wheelTf == null) return;
        
        collider.GetWorldPose(out Vector3 position, out Quaternion rotation);

        wheelTf.position = position;
        wheelTf.rotation = rotation;
    }

    public void FixedUpdate()
    {
        float motor = MaxMotorTorque * Input.GetAxis("Vertical");
        float steering = MaxSteeringAngle * Input.GetAxis("Horizontal");
        
        if (Input.GetKey(KeyCode.Space)) brakeTorque = BrakeTorque;
        else brakeTorque = 0f;

        IsBrake = Input.GetKey(KeyCode.Space);
        
        foreach (AxleInfo axleInfo in AxleInfos)
        {
            if (axleInfo.Steering)
            {
                axleInfo.LeftWheel.steerAngle = steering;
                axleInfo.RightWheel.steerAngle = steering;
            }

            if (axleInfo.Motor)
            {
                axleInfo.LeftWheel.motorTorque = motor;
                axleInfo.RightWheel.motorTorque = motor;
            }

            if (axleInfo.Brake)
            {
                axleInfo.LeftWheel.brakeTorque = brakeTorque;
                axleInfo.RightWheel.brakeTorque = brakeTorque; 
            }
            
            ApplyLocalPositionToVisuals(axleInfo.LeftWheel,axleInfo.LeftWheelModel);
            ApplyLocalPositionToVisuals(axleInfo.RightWheel,axleInfo.RightWheelModel);
        }
    }
}

