using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThumbstickCarInput : MonoBehaviour {
    CarController car;
    public float rotateAmount = 0.01f;
    public float tranSpeed = 0.01f;

	void Start () {
        car = GetComponent<CarController>(); 
	}
	
	// Update is called once per frame
	void Update () {
        car.steerRight(OculusTouchInput.GetLeftThumbstickHorizontal()* rotateAmount);
        car.GoForward(OculusTouchInput.GetLeftIndex() * tranSpeed);
    }
}
