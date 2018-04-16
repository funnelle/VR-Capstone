using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviours : MonoBehaviour {
    Inventory_Swipe inventory;

    void Start() {
        inventory = GetComponentInParent<Inventory_Swipe>();
    }

    //If the tip of the right index finger enters the left or right button's capsule collider, change displayed object
    void OnTriggerEnter(Collider coll) {
        //if inventory is active and right index finger collides with button, move left or right
        if ((gameObject.activeSelf) && (coll.gameObject.name.Contains("index3"))) {
            if (gameObject.name.Contains("Left")) {
                if (inventory.currentDisplayedObj > 0) {
                    inventory.currentDisplayedObj -= 1;
                    inventory.currentObjChanged = true;
                }
            }
            if (gameObject.name.Contains("Right")) {
                if (inventory.currentDisplayedObj < (inventory.objects.Count - 1) ) {
                    inventory.currentDisplayedObj += 1;
                    inventory.currentObjChanged = true;
                }
            }
        }
    }
}
