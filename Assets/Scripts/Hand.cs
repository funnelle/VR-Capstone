/*
 * Solution Created by RGBSchemes
 * Code can be found at http://www.rgbschemes.com/blog/oculus-touch-and-finger-stuff-part-1/ and http://www.rgbschemes.com/blog/oculus-touch-and-finger-stuff-part-2/
 * Bone structure used to detect finger tips in Wrist_Menu Scene when selecting panels, and Wrist_Inventory Scene when pressing inventory buttons, and provides more realistic hand features to other scenes
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    public enum State {
        EMPTY,
        TOUCHING,
        HOLDING
    };

    public OVRInput.Controller Controller = OVRInput.Controller.LTouch;
    public State mHandState = State.EMPTY;
    public Rigidbody AttachPoint = null;
    public bool IgnoreContactPoint = false;
    private Rigidbody mHeldObject;
    private FixedJoint mTempJoint;
    private Vector3 mOldVelocity;

    // Use this for initialization
    void Start() {
        if (AttachPoint == null) {
            AttachPoint = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update() {
        switch (mHandState) {
            case State.TOUCHING:
                if (mTempJoint == null && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, Controller) >= 0.5f) {
                    mHeldObject.velocity = Vector3.zero;
                    mTempJoint = mHeldObject.gameObject.AddComponent<FixedJoint>();
                    mTempJoint.connectedBody = AttachPoint;
                    mHandState = State.HOLDING;
                }
                break;
            case State.HOLDING:
                if (mTempJoint != null && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, Controller) < 0.5f) {
                    Object.DestroyImmediate(mTempJoint);
                    mTempJoint = null;
                    throwObject();
                    mHandState = State.EMPTY;
                }
                mOldVelocity = OVRInput.GetLocalControllerAngularVelocity(Controller);
                break;
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (mHandState == State.EMPTY && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, Controller) < 0.5f) {
            GameObject temp = collider.gameObject;
            if (temp != null && temp.layer == LayerMask.NameToLayer("grabbable") && temp.GetComponent<Rigidbody>() != null) {
                mHeldObject = temp.GetComponent<Rigidbody>();
                mHandState = State.TOUCHING;
            }
        }
    }

    void OnTriggerExit(Collider collider) {
        if (mHandState != State.HOLDING) {
            if (collider.gameObject.layer == LayerMask.NameToLayer("grabbable")) {
                mHeldObject = null;
                mHandState = State.EMPTY;
            }
        }
    }

    private void throwObject() {
        mHeldObject.velocity = OVRInput.GetLocalControllerVelocity(Controller);
        if (mOldVelocity != null) {
            mHeldObject.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(Controller);
        }
        mHeldObject.maxAngularVelocity = mHeldObject.angularVelocity.magnitude;
    }
}