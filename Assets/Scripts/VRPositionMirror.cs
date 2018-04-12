using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPositionMirror : MonoBehaviour
{
    public OVRInput.Controller hand;
    public float damper_value;
    public type motion_type;

    public enum type
    {
        velocity_set,
        velocity_add,
        acceleration_set,
        acceleration_add
    }


    void Start()
    {
    }

    void Update()
    {
        switch (motion_type)
        {
            case type.velocity_set:
                transform.position = OVRInput.GetLocalControllerVelocity(hand) / damper_value;
                break;
            case type.acceleration_set:
                transform.position = OVRInput.GetLocalControllerAcceleration(hand) / damper_value;
                break;
                
            case type.velocity_add:
                transform.position += OVRInput.GetLocalControllerVelocity(hand) / damper_value;
                break;
            case type.acceleration_add:
                transform.position += OVRInput.GetLocalControllerAcceleration(hand) / damper_value;
                break;
        }
    }
}