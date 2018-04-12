using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketInv : MonoBehaviour {
    public GameObject rightHand;
    public GameObject leftHand;
    public bool isHeld = false;

    private OVRGrabber grabbedObjRight;
    private OVRGrabber grabbedObjLeft;
    private OVRGrabbable heldObj;
    private Vector3 objScale;

    GameObject newItem;

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
            Debug.Log(heldObj);
            //heldObj.transform.parent = null;
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
            if (heldObj.name == currentObject.name) {
                Debug.Log(GameObject.Find(heldObj.name));
                Destroy(GameObject.Find(heldObj.name));
                numObjects++;
                
            }
        }
        //Adds obj if container is Empty
        if ((heldObj != null) && (currentObject == null) && (isHeld)) {
            Debug.Log("New Item, adding Item");
            currentObject = GameObject.Find(heldObj.name);
            objScale = heldObj.transform.localScale;
            grabbedObjRight.ForceRelease(heldObj);
            //GameObject.Find(heldObj.name).transform.parent = this.transform;
            GameObject.Find(heldObj.name).GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find(heldObj.name).GetComponent<BoxCollider>().enabled = false;
            GameObject.Find(heldObj.name).GetComponent<OVRGrabbable>().enabled = false;
            Destroy(GameObject.Find(heldObj.name).GetComponent<Rigidbody>());
            numObjects++;
            
        }
        //Takes out Obj if Container contains items and hand is empty
        if ((numObjects > 0) && (coll.name.Contains("RightHandAnchor")) && (currentObject != null) && (isHeld)) {
            newItem = Instantiate(currentObject, transform.position, Quaternion.identity) as GameObject;
            newItem.GetComponent<MeshRenderer>().enabled = true;
            newItem.GetComponent<BoxCollider>().enabled = true;
            newItem.GetComponent<OVRGrabbable>().enabled = true;
            newItem.AddComponent<Rigidbody>();
            newItem.GetComponent<Rigidbody>().useGravity = false;
            Transform newItemPos = newItem.GetComponent<Transform>();
            //newItemPos.localPosition = new Vector3(-0.2f, 0.01f, -0.165f);
            newItemPos.localRotation = Quaternion.identity;
            newItemPos.localScale = objScale;
            newItem.SetActive(true);

            numObjects--;
            if (numObjects == 0) {
                currentObject = null;
            }
            newItem.SetActive(true);
        }
    }
}