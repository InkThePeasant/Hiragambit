using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public static MenuManager instance = null;

    private GameObject fadeOutImage;

	// Use this for initialization
	void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        fadeOutImage = GameObject.Find("FadeOutImage");
        fadeOutImage.SetActive(false);
    }

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
}
