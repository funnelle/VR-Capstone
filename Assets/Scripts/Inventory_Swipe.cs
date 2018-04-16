using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Swipe : MonoBehaviour {
    //public variables
    public GameObject inventory;
    public GameObject rightHand;

    //private variables
    private Transform invPosition;
    private OVRGrabber grabbedObj;
    private OVRGrabbable heldObj;

    GameObject newItem;

    //list containing all objects
    public List<GameObject> objects = new List<GameObject>();
    public int currentDisplayedObj = 0; //index in list of current selected inv obj
    public bool currentObjChanged = false;
    GameObject invItem; //current obj being displayed

    // Use this for initialization
    void Start () {
        inventory.SetActive(false);
        invPosition = GetComponent<Transform>();
        grabbedObj = rightHand.GetComponent<OVRGrabber>();
	}

    // Update is called once per frame
    void Update() {
        //identifies if the inventory is up
        if (OVRInput.Get(OVRInput.Button.Three) == true) {
            inventory.SetActive(true);
            if (objects.Count > 0) {
                showInvObj();
            }
        }
        else {
            inventory.SetActive(false);
            if (invItem != null) {
                invItem.SetActive(false);
            }
        }

        //identifies if an object is being held in the right hand
        if (grabbedObj.grabbedObject != null) {
            heldObj = grabbedObj.grabbedObject;
            //Debug.Log(heldObj);
            heldObj.transform.parent = null;
        }
        else {
            heldObj = null;
        }

    }

    //Called when a collider enters the box collider
    void OnTriggerEnter(Collider coll) {
        //if an object is held, and inventory is up, add object to inventory
        if ((heldObj != null) && (inventory.activeSelf == true)) {
            objects.Add(heldObj.gameObject);
            grabbedObj.ForceRelease(heldObj);
            heldObj.gameObject.transform.parent = this.transform; //set object to be a child of the inventory
            //Disable all components of the object
            heldObj.gameObject.GetComponent<MeshRenderer>().enabled = false;
            heldObj.gameObject.GetComponent<BoxCollider>().enabled = false;
            heldObj.gameObject.GetComponent<OVRGrabbable>().enabled = false;
            Destroy(heldObj.gameObject.GetComponent<Rigidbody>());
        }
        //if hand is empty and there are items in the inventory, spaw the currently displayed item
        if ((inventory.activeSelf ==true) && (objects.Count > 0) && (coll.name.Contains("RightHandAnchor"))) { 
            newItem = objects[currentDisplayedObj];
            newItem.GetComponent<MeshRenderer>().enabled = true;
            newItem.GetComponent<BoxCollider>().enabled = true;
            newItem.GetComponent<OVRGrabbable>().enabled = true;
            newItem.AddComponent<Rigidbody>();
            newItem.GetComponent<Rigidbody>().useGravity = false;
            Transform newItemPos = newItem.GetComponent<Transform>();
            newItemPos.localPosition = new Vector3(-0.2f, 0.01f, -0.165f);
            newItemPos.localRotation = Quaternion.identity;

            objects.RemoveAt(currentDisplayedObj); //remove object from list
            Destroy(invItem);
            currentObjChanged = true; //object removed need to signal a change of item shown
            //if an item is removed, if possible display previous item in list, if not display next
            if ((currentDisplayedObj - 1) >= 0) {
                currentDisplayedObj -= 1;
            }
            else {
                currentDisplayedObj += 1;
            }
            newItem.SetActive(true);
        }
    }

    //Displays a representation of the current selected object
    void showInvObj() {
        //display whatever is the currently viewed inventory object
        if (invItem == null || currentObjChanged == true) {
            //remove old obj if current object changed
            if (currentObjChanged == true) {
                Destroy(invItem);
            }

            currentObjChanged = false;

            //invItem = Instantiate(objects[currentDisplayedObj], new Vector3(-0.2f, 0.01f, -0.165f), Quaternion.identity, invPosition) as GameObject;
            invItem = Instantiate(objects[currentDisplayedObj],invPosition) as GameObject;
            
            //gets rid of useless components in the obj representation
            Transform invItemPos = invItem.GetComponent<Transform>();
            invItemPos.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            invItemPos.localPosition = new Vector3(-0.2f, 0.01f, -0.165f);
            invItemPos.localRotation = Quaternion.identity;
            invItem.GetComponent<MeshRenderer>().enabled = true;
            Destroy(invItem.GetComponent<Rigidbody>());
            Destroy(invItem.GetComponent<OVRGrabbable>());
            invItem.SetActive(true);
        }
        else {
            invItem.SetActive(true);
        }
    }
}
