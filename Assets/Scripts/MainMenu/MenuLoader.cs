using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLoader : MonoBehaviour {

    public GameObject menuManager;

    //Script attached to Menu scene's camera, instantiates the MenuManager prefab
    private void Awake()
    {
        if (MenuManager.instance == null)
            Instantiate(menuManager);
    }
}
