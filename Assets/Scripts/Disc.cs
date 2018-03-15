using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disc : MonoBehaviour
{

    public float fadeOutTime = 10f;

    public SpriteRenderer sprite;

    private Text testText;

    // Use this for initialization
    //Calls fade in and fade out coroutines, with delay on the fade out until designated time
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();        

        testText = GameObject.Find("testText").GetComponent<Text>();
        StartCoroutine(FadeIn());   //
        Debug.Log("Finished FadeIn");
        StartCoroutine(FadeOut(fadeOutTime));
    }

    // Update is called once per frame
    void Update()
    {
        testText.text = "Time: " + Time.time;

    }

    //Coroutine fades in GameObject by steadily increasing sprites alpha
    IEnumerator FadeIn()
    {
        Debug.Log("in FadeIn");

        for(double f = 0; f <= 1; f += .01)
        {
            testText.text = "fade at: " + f;
            sprite.color = new Color(1f, 1f, 1f, (float)f);
            yield return null;
        }
    }

    //Coroutine fades in GameObject by steadily decreasing sprites alpha
    IEnumerator FadeOut(float delay)
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);

        Debug.Log("in FadeOut");

        for (float f = 1f; f >= 0f; f -= .01f)
        {
            testText.text = "fade at: " + f;
            sprite.color = new Color(1f, 1f, 1f, f);
            yield return null;
        }

    }


}
