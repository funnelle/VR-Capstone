using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviours : MonoBehaviour {
    void OnTriggerEnter(Collider coll) {
        if ((gameObject.activeSelf) && (coll.gameObject.name.Contains("index3"))) {
            if (gameObject.name.Contains("Left")) {
                Debug.Log("Going Left!");
            }
            if (gameObject.name.Contains("Right")) {
                Debug.Log("Going Right");
            }
        }
    }
}
