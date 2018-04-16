using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRigidBodyMirror : MonoBehaviour
{
    public OVRInput.Controller hand;
    public float damper_value;
    public type motion_type;
    public bool disableGravityOnMove;
    [Tooltip("only activate movement when the user is gripping")]
    public bool onlyOnGrip;
    private Rigidbody rb;

    public enum type
    {
        set_velocity_from_velocity,
        add_velocity_from_velocity,
        set_velocity_from_acceleration,
        add_velocity_from_acceleration,
        add_force_from_acceleration,
        add_force_from_velocity
    }

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onlyOnGrip)
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, hand) > 0.1)
            {
                rb.useGravity = !disableGravityOnMove;
                UpdateMotion();
            }
            else
            {
                rb.useGravity = true;
            }
    }
        else
        {

            UpdateMotion();
        }
        
        
        
    }

    void UpdateMotion()
    {
        
        switch (motion_type)
        {
            case type.set_velocity_from_velocity:
                rb.velocity = OVRInput.GetLocalControllerVelocity(hand) / damper_value;
                break;
            case type.set_velocity_from_acceleration:
                rb.velocity = OVRInput.GetLocalControllerAcceleration(hand) / damper_value; //forgive me physics
                break;

            case type.add_velocity_from_velocity:
                rb.velocity = rb.velocity + (OVRInput.GetLocalControllerVelocity(hand) / damper_value);
                break;
            case type.add_velocity_from_acceleration:
                rb.velocity += OVRInput.GetLocalControllerAcceleration(hand) / damper_value;
                break;

            case type.add_force_from_acceleration:
                rb.AddForce(OVRInput.GetLocalControllerAcceleration(hand) / damper_value);
                break;
                
            case type.add_force_from_velocity:
                rb.AddForce(OVRInput.GetLocalControllerVelocity(hand) / damper_value);
                break;
        }
    }
}