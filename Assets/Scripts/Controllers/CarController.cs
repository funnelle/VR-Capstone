using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO document
[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void steerRight(float amount)
    {
        rb.transform.Rotate(0, amount, 0);
    }
    public void SteerLeft(float amount)
    {
        steerRight(-amount);
    }
    public void GoForward(float amount)
    {
        rb.transform.Translate(0, 0, amount);
    }
    public void GoBackwards(float amount)
    {
        GoForward(-amount);
    }
}
