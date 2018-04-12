using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/Manual/OculusControllers.html
// a wrapper for input classes to get oculus touch input
// this is just for human readability. 
// This class uses the apearent names not the actual or technical names
// general syntax is Get[left|right][button name][status: touch|press]
// This class is not used with world positions of the controllers.
// forgive the java like names the touch controllers have 31 inputs NOT including the spacial position data 


//TODO finish rest of maping-> including project\input to be correct
//TODO doll up with region blocks
//TODO finish custom inspector
public class OculusTouchInput
{

    #region right A button
    public static bool GetRightAPress()
    {
        return Input.GetKey(KeyCode.JoystickButton0);
    }
    public static bool GetRightATouch()
    {
        return Input.GetKey(KeyCode.JoystickButton10);
    }
    #endregion
    
    #region right B button
    //B button 1 for press. 11 for touch.
    public static bool GetRightBPress()
    {
        return Input.GetKey(KeyCode.JoystickButton1);
    }
    public static bool GetRightBTouch()
    {
        return Input.GetKey(KeyCode.JoystickButton11);
    }
    #endregion


    //left
    //X Button  press is 2. touch is 12.
    public static bool GetRightXPress()
    {
        return Input.GetKey(KeyCode.JoystickButton2);
    }
    public static bool GetRightXTouch()
    {
        return Input.GetKey(KeyCode.JoystickButton12);
    }

    //Y button press is 3. touch is 12.
    public static bool GetRightYPress()
    {
        return Input.GetKey(KeyCode.JoystickButton3);
    }
    public static bool GetRightYTouch()
    {
        return Input.GetKey(KeyCode.JoystickButton13);
    }

    //TODO start
    //TODO reserved-> the oculus menu do not use



    //AXIS 
    // this is where I hate Unity's input method. Axis require setup in input manager. oculus give defaults -> do not change otherwise these will not work.


    //left thumbstick aka primary
    //–1.0 to 1.0
    //Unity Axis ID Horizontal id=1. Vertical id=2
    //Oculus_GearVR_RThumbstickX
    public static float GetLeftThumbstickHorizontal()
    {
        return Input.GetAxis("Oculus_GearVR_LThumbstickX");
    }
    public static float GetLeftThumbstickVertical()
    {
        return Input.GetAxis("Oculus_GearVR_LThumbstickY");
    }

    
    public static float GetRightThumbstickHorizontal()
    {
        return Input.GetAxis("Oculus_GearVR_RThumbstickX");
    }
    public static float GetRightThumbstickVertical()
    {
        return Input.GetAxis("Oculus_GearVR_RThumbstickY");
    }



    #region left index
    /* left index trigger aka primary index trigger
     - touch bool button id=14
     - near touch axis id=13
     - squeeze axis id=9 -> get Left index 0.0 to 1.0
     */
    //TODO finish this
    public static float GetLeftIndex()
    {
        return Input.GetAxis("Oculus_GearVR_LIndexTrigger");
    }
    #endregion

    #region right index
    /* right index trigger aka primary index trigger
    - touch bool button id=14
    - near touch axis id=13
    - squeeze axis id=9
    */
    public static float GetRightIndex()
    {
        return Input.GetAxis("Oculus_GearVR_RIndexTrigger");
    }

    #endregion

}
