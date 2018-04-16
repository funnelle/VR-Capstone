using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviours : MonoBehaviour {
    Inventory_Swipe inventory;

    void Start() {
        inventory = GetComponentInParent<Inventory_Swipe>();
    }
    void OnTriggerEnter(Collider coll) {
        if ((gameObject.activeSelf) && (coll.gameObject.name.Contains("index3"))) {
            if (gameObject.name.Contains("Left")) {
                if (inventory.currentDisplayedObj > 0) {
                    inventory.currentDisplayedObj -= 1;
                    inventory.currentObjChanged = true;
                    Debug.Log(inventory.currentDisplayedObj);
                }
            }
            if (gameObject.name.Contains("Right")) {
                if (inventory.currentDisplayedObj < (inventory.objects.Count - 1) ) {
                    inventory.currentDisplayedObj += 1;
                    inventory.currentObjChanged = true;
                    Debug.Log(inventory.currentDisplayedObj);
                }
            }
        }
    }
}
