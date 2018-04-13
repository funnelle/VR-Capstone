using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyContainer : MonoBehaviour {
    private OVRGrabbable heldObj;
    private OVRGrabber grabbedObj;
    private Rigidbody inventory;

    public GameObject leftHand;

	// Use this for initialization
	void Start () {
        grabbedObj = leftHand.GetComponent<OVRGrabber>();
	}
	
	// Update is called once per frame
	void Update () {
        if (grabbedObj.grabbedObject != null) {
            heldObj = grabbedObj.grabbedObject;
            inventory = heldObj.GetComponent<Rigidbody>();
            heldObj.transform.parent = null;
            inventory.constraints = RigidbodyConstraints.None;
        }
        else {
            heldObj = null;
        }
	}

    void OnTriggerEnter (Collider coll) {
        if (coll.gameObject.name.Contains("Pocket") && (heldObj == null)) {
            coll.transform.parent = this.transform;
            inventory.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }
}
