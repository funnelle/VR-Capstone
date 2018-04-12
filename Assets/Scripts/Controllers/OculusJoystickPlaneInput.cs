using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusJoystickPlaneInput : MonoBehaviour {

   public float roll, pitch,thrust;
    PlaneController plane;
	void Awake () {
        plane = GetComponent<PlaneController>();
	}
	
	// Update is called once per frame
	void Update () {
        plane.Forward(OculusTouchInput.GetLeftIndex()* thrust);
        plane.Roll(OculusTouchInput.GetLeftThumbstickVertical()* roll);
        plane.Pitch(OculusTouchInput.GetLeftThumbstickHorizontal()* pitch);
    }
}
