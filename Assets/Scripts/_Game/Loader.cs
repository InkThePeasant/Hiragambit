using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************
 * 
 * Instantiates GameManager at the game's launch
 * 
 ********************/ 

public class Loader : MonoBehaviour {

    public GameObject gameManager;

    // Use this for initialization
    void Awake()
    {
        if (GameManager.instance == null)
            Instantiate(gameManager);

    }

}
