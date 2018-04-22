using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LauchClick()
    {
        StartCoroutine(MenuManager.instance.FadeOutScene());
    }

    public void SettingsClick()
    {     
        StartCoroutine(MenuManager.instance.FadeGameSettings());
    }

    public void ENGtoHiraClick()
    {
        MenuManager.instance.kanaToLoad = "KanaText/ENGtoHiragana";
        SetButtonColors(gameObject.GetComponent<Button>());
    }

    public void HiratoENGClick()
    {
        MenuManager.instance.kanaToLoad = "KanaText/HiraganatoENG";
        SetButtonColors(gameObject.GetComponent<Button>());
    }

    public void ENGtoKataClick()
    {
        MenuManager.instance.kanaToLoad = "KanaText/ENGtoKatakana";
        SetButtonColors(gameObject.GetComponent<Button>());
    }

    public void KatatoENGClick()
    {
        MenuManager.instance.kanaToLoad = "KanaText/KatakanatoENG";
        SetButtonColors(gameObject.GetComponent<Button>());
    }

    private void SetButtonColors(Button btn)
    {
        ClearButtonColors();

        ColorBlock cb = btn.colors;
        cb.normalColor = cb.highlightedColor;
        btn.colors = cb;
    }

    private void ClearButtonColors()
    {
        //Gets all the Settings buttons
        var settingsBtnObjs = GameObject.FindGameObjectsWithTag("Settings Button");
        List<Button> settingsBtns = new List<Button>();

        //Gets their Button componenets
        foreach (var o in settingsBtnObjs)
            settingsBtns.Add(o.GetComponent<Button>());

        //Sets the color to white
        ColorBlock cb;
        foreach(var b in settingsBtns)
        {
            cb = b.colors;
            cb.normalColor = new Color(1f, 1f, 1f);
            b.colors = cb;
        }
    }


}
