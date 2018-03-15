using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disc : MonoBehaviour
{

    public float minimum = 0.0f;
    public float maximum = 1f;
    public float duration = 1f;
    public float fadeOutTime = 6f;
    public float fadeSpeed = 1f;
    private float startTime;
    public SpriteRenderer sprite;

    private Text timeText;
    private bool fadeOut = false;

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
        sprite = GetComponent<SpriteRenderer>();
        //sprite.color = new Color(1f, 1f, 1f, 0f);


        timeText = GameObject.Find("timeText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //float t;


        if (Time.time >= 5)
        {
            fadeOut = true;
        }

        //if (!fadeOut)
        //{
        //    t = Mathf.SmoothStep(minimum, maximum, Time.time / duration);
        //    sprite.color = new Color(1f, 1f, 1f, t);
        //}
        //else
        //{
        //    timeText.text = "Should be fading";
        //    FadeOut();
        //}

        if (!fadeOut)
        {
            StartCoroutine("FadeIn");
        }
        else
        {
            StartCoroutine("FadeOut");
        }


        //timeText.text = "Time: " + Time.time;

    }

    IEnumerator FadeIn()
    {
        Debug.Log("in FadeIn");

        for(float f = 0f; f <= 1f; f += .01f)
        {
            sprite.color = new Color(1f, 1f, 1f, f);
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        Debug.Log("in FadeIn");

        for (float f = 1f; f >= 0f; f -= .01f)
        {
            sprite.color = new Color(1f, 1f, 1f, f);
            yield return null;
        }
    }


}
