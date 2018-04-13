using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    public GameObject gameOverGroup;
    private Image blurPanel;

	// Use this for initialization
	void Start () {
        gameOverGroup = GameObject.Find("Game Over");
        blurPanel = GameObject.Find("BlurPanel").GetComponent<Image>();

        gameOverGroup.SetActive(false);        

    }
	
	// Update is called once per frame
	void Update () {

        
	}

    //Displays Game Over screen when conditions are met
    public void GameOver()
    {
        gameOverGroup.SetActive(true);
        StartCoroutine(FadeBlurIn());
        StartCoroutine(FadeInUI());
    }

    //Steadily increase the blur of the BlurPanel, obfuscating the screen
    IEnumerator FadeBlurIn()
    {
        for (double f = 0; f <= 7; f += .05)
        {
            blurPanel.material.SetFloat("_Radius", (float)f);
            Debug.Log("Blur is: " + f);
            yield return null;
        }
    }

    //Increase the alpha of a canvas group component on the Game Over UI holder
    //This increases the alpha of all child objects, aka fades in Game Over screen
    IEnumerator FadeInUI()
    {
        var canvasGroup = gameOverGroup.GetComponent<CanvasGroup>();
        for (double f = 0; f <= 1; f += .05)
        {
            canvasGroup.alpha = (float) f;
            yield return null;
        }
    }

}
