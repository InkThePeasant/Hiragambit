using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchButton : MonoBehaviour {

    //Runs FadeOutScene in the MenuManager, loading the main game
	public void LauchClick()
    {
        StartCoroutine(MenuManager.instance.FadeOutScene());
    }
}
