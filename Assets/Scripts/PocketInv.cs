using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketInv : MonoBehaviour {
    //public variables
    public GameObject rightHand;
    public GameObject leftHand;
    public bool isHeld = false;

    //private variables
    private OVRGrabber grabbedObjRight;
    private OVRGrabber grabbedObjLeft;
    private OVRGrabbable heldObj;
    private Vector3 objScale;
    private GameObject newItem;

    //Variables to track objects inside
    public int numObjects;
    public GameObject currentObject = null;

    // Use this for initialization
    void Start() {
        grabbedObjRight = rightHand.GetComponent<OVRGrabber>();
        grabbedObjLeft = leftHand.GetComponent<OVRGrabber>();
    }

    // Update is called once per frame
    void Update() {
        //identifies if an object is being held in the right hand
        if (grabbedObjRight.grabbedObject != null) {
            heldObj = grabbedObjRight.grabbedObject;
        }
        else {
            heldObj = null;
        }

        //identifies if pocket container is held in left hand
        if (grabbedObjLeft.grabbedObject != null) {
            if (grabbedObjLeft.grabbedObject.name == this.gameObject.name) {
                isHeld = true;
            }
        }
        else {
            isHeld = false;
        }
    }

    void OnTriggerEnter(Collider coll) {
        //Adds obj if currentOBj already set
        if ((heldObj != null) && (currentObject != null) && (isHeld)) {
            if ((heldObj.name == currentObject.name) && (coll.name == currentObject.name)) {
                Destroy(heldObj.gameObject);
                numObjects++;
                
            }
        }
        //Adds obj if container is Empty
        if ((heldObj != null) && (currentObject == null) && (isHeld)) {
            if ((coll.gameObject == heldObj.gameObject)) {
                currentObject = heldObj.gameObject;
                objScale = heldObj.transform.localScale;
                grabbedObjRight.ForceRelease(heldObj);
                //disable all components of the item being stored
                heldObj.gameObject.GetComponent<MeshRenderer>().enabled = false;
                heldObj.gameObject.GetComponent<BoxCollider>().enabled = false;
                heldObj.gameObject.GetComponent<OVRGrabbable>().enabled = false;
                Destroy(heldObj.gameObject.GetComponent<Rigidbody>());
                numObjects++;
            }
        }
        //Takes out Obj if Container contains items and hand is empty
        if ((numObjects > 0) && (coll.name.Contains("RightHandAnchor")) && (currentObject != null) && (isHeld)) {
            newItem = Instantiate(currentObject, transform.position, Quaternion.identity) as GameObject;
            newItem.name = currentObject.name; //prevents cloned objects causing name problems
            //renable all the compoents we disabled on the original object earlier
            newItem.GetComponent<MeshRenderer>().enabled = true;
            newItem.GetComponent<BoxCollider>().enabled = true;
            newItem.GetComponent<OVRGrabbable>().enabled = true;
            newItem.AddComponent<Rigidbody>();
            newItem.GetComponent<Rigidbody>().useGravity = false;
            Transform newItemPos = newItem.GetComponent<Transform>();
            newItemPos.localRotation = Quaternion.identity;
            newItemPos.localScale = objScale;
            newItem.SetActive(true);

            //remove one object from total inside
            numObjects--;
            if (numObjects == 0) {
                currentObject = null;
            }
            newItem.SetActive(true);
        }
    }
}