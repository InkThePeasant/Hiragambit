using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchButton : MonoBehaviour {

	public void LauchClick()
    {
        StartCoroutine(MenuManager.instance.FadeOutScene());
    }
}
