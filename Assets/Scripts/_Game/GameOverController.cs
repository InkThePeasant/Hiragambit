using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**************************************
 * 
 * - Class controls game over functionality
 * - Works concurrently with GameManger to allow for punctual method calls
 * - Handles Game Over UI functionality, including enabling and disabling
 * 
 ***************************************/ 

public class GameOverController : MonoBehaviour {

    public GameObject gameOverGroup;
    private Image blurPanel;
    private Text gameOverScore;

	// Use this for initialization
	void Awake () {
        gameOverGroup = GameObject.Find("Game Over");
        blurPanel = GameObject.Find("BlurPanel").GetComponent<Image>();
        gameOverScore = GameObject.Find("GameOverScoreText").GetComponent<Text>();

        DisableGameOverUI();        
    }

    //In separate method to allow GameManager to disable UI during game initialization
    public void DisableGameOverUI()
    {       
        gameOverGroup.SetActive(false);
    }

    //Displays Game Over screen when conditions are met
    public void GameOver()
    {
        gameOverGroup.SetActive(true);
        gameOverScore.text = "Final Score: " + GameManager.instance.currentScore;
        StartCoroutine(FadeBlurIn());
        StartCoroutine(FadeInUI());
    }

    //Steadily increase the blur of the BlurPanel, obfuscating the screen
    IEnumerator FadeBlurIn()
    {
        for (double f = 0; f <= 7; f += .10)
        {
            blurPanel.material.SetFloat("_Radius", (float)f);
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
