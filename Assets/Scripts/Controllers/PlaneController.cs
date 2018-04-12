using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlaneController : MonoBehaviour
{
    Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Pitch(float amount)
    {
        rb.transform.Rotate(0, 0, amount);
    }
    public void Roll(float amount)
    {
        rb.transform.Rotate(amount, 0, 0);
    }
    public void Yaw(float amount)
    {
        rb.transform.Rotate(0, amount, 0);
    }
    public void Forward(float amount)
    {
        rb.transform.Translate(0, 0, amount);
    }
}
