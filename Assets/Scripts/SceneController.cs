using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Just adds a reset button mapped Pressing A
public class SceneController : MonoBehaviour
{

    public bool resetSceneButtonEnabled = true;
    public string resetSceneButton = "";

    void Update()
    {
        if(resetSceneButtonEnabled && OculusTouchInput.GetRightAPress()) {
            Debug.Log("---- reset Scene ----");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
      
    }
}
