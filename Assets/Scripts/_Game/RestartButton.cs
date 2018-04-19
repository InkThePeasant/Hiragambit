using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour {

    //Method reinitializes game for fast restart
    public void RestartClick()
    {        
        GameManager.instance.InitGame();
    }
	
}
