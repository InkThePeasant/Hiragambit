using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disc : MonoBehaviour
{

    public float fadeOutTime = 10f;

    private SpriteRenderer sprite;
    private Animator animator; 

    private Text testText;

    // Use this for initialization
    //Calls fade in and fade out coroutines, with delay on the fade out until designated time
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        StartCoroutine(FadeIn());   //
        StartCoroutine(FadeOut(fadeOutTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Method that runs when user clicks within Disc's CircleCollider2D
    void OnMouseDown()
    {
        Debug.Log("clicked");
        animator.SetTrigger("discBreak");
    }

    //Coroutine fades in GameObject by steadily increasing sprites alpha
    IEnumerator FadeIn()
    {
        

        for(double f = 0; f <= 1; f += .01)
        {            
            sprite.color = new Color(1f, 1f, 1f, (float)f);
            yield return null;
        }
    }

    //Coroutine fades in GameObject by steadily decreasing sprites alpha
    IEnumerator FadeOut(float delay)
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);

        for (float f = 1f; f >= 0f; f -= .01f)
        {
            sprite.color = new Color(1f, 1f, 1f, f);
            yield return null;
        }

    }


}
