using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/***************************************
 * 
 * Singleton class that controls logic behind the Main Menu, in the Menu scene
 * - Handles fading of UI elements
 * - Handles transitioning between scenes
 * 
 ***************************************/ 

public class MenuManager : MonoBehaviour {

    public static MenuManager instance = null;

    //Scene Game Objects
    private GameObject fadeOutImage;
    private GameObject gameSettingsGroup;

    public string kanaToLoad = "KanaText/ENGtoHiragana";

    // Use this for initialization
    void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //Prevent it from being destroyed on Scene change
        DontDestroyOnLoad(gameObject);

        fadeOutImage = GameObject.Find("FadeOutImage");
        gameSettingsGroup = GameObject.Find("Game Settings");
    }

    //Disables overlay, so that it can fade in when loading to new scene
    void Start()
    {
        fadeOutImage.SetActive(false);
        gameSettingsGroup.SetActive(false);
    }

    //Fades screen to white, then loads the main game Scene
    //Called from Launch button
    public IEnumerator FadeOutScene()
    {
        fadeOutImage.SetActive(true);
        var fadeImageComponent = fadeOutImage.GetComponent<Image>();

        for (double f = 0; f <= 1; f += .01)
        {
            fadeImageComponent.color = new Color(1f, 1f, 1f, (float)f);
            yield return null;
        }

        SceneManager.LoadScene("Main");
    }

    public IEnumerator FadeGameSettings()
    {
        gameSettingsGroup.SetActive(true);
        var canvasGroup = GameObject.Find("Game Settings").GetComponent<CanvasGroup>();
        for (double f = 0; f <= 1; f += .05)
        {
            canvasGroup.alpha = (float)f;
            Debug.Log("alpha: " + canvasGroup.alpha);
            yield return null;
        }
    }
}
