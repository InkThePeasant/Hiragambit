using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour {

    //Method for Exit Button in game
    //Quits the Unity Editor Play mode if testing in that environment
    //Otherwise closes the application
    public void ClickExit()
    {
        Debug.Log("clicked");

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();  
        #endif
    }

}
