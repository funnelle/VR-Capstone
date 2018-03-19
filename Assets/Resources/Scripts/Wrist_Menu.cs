using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrist_Menu : MonoBehaviour {
    private BoxCollider menu;
    private SphereCollider handCollider;
    private MeshRenderer menuMesh;
    public GameObject rightHand;
	// Use this for initialization
	void Start () {
        menu = GetComponent<BoxCollider>();
        menuMesh = GetComponent<MeshRenderer>();
        handCollider = rightHand.GetComponent<SphereCollider>();
        menuMesh.enabled = false;
        menu.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        //enables menu
        if (OVRInput.Get(OVRInput.Button.Three) == true) {
            menuMesh.enabled = true;
            menu.enabled = true;
        }
        else {
            menuMesh.enabled = false;
            menu.enabled = false;
        }
    }

    void OnTriggerEnter(Collider coll) {
        if ((menuMesh.enabled == true) && (coll.gameObject.name.Contains("index3"))) {
            Debug.Log("Right hand colliding");
        }
    }
}
