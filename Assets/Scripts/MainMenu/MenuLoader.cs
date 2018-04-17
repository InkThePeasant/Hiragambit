using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLoader : MonoBehaviour {

    public GameObject menuManager;

    private void Awake()
    {
        if (MenuManager.instance == null)
            Instantiate(menuManager);
    }
}
