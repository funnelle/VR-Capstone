using System;
using System.Collections.Generic;
using UnityEngine;
// Just display the information about the buttons and joysticks 
//TODO rework this into custom window and add to debug 
//todo add state change outputs to console
namespace Assets.Scripts
{
    class OculusTouchDebug : MonoBehaviour
    {

        [Serializable]
        public class Button
        {
            public bool pressed = false;
            public bool touch = false;
        }
        [Serializable]
        public class Axis2D
        {
            public float x, y;
        }
        [Header("Buttons")]
        public Button X = new Button();
        public Button Y = new Button();
        public Button A = new Button();
        public Button B = new Button();

        [Header("Axis")]
        public Axis2D leftThumbstick = new Axis2D();
        public Axis2D rightThumbstick = new Axis2D();
        public float leftTrigger;
        public float rightTrigger;

        void Update()
        {
            A.pressed = OculusTouchInput.GetRightAPress();
            A.touch = OculusTouchInput.GetRightATouch();

            B.pressed = OculusTouchInput.GetRightBPress();
            B.touch = OculusTouchInput.GetRightBTouch();

            leftThumbstick.x = OculusTouchInput.GetLeftThumbstickHorizontal();
            leftThumbstick.y = OculusTouchInput.GetLeftThumbstickVertical();

            rightThumbstick.x = OculusTouchInput.GetRightThumbstickHorizontal();
            rightThumbstick.y = OculusTouchInput.GetRightThumbstickVertical();

            leftTrigger = OculusTouchInput.GetLeftIndex();
            rightTrigger = OculusTouchInput.GetRightIndex();
        }
    }
}
