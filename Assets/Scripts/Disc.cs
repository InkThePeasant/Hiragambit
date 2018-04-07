using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*******************
 * 
 * Class controlling behavior and functionality of the game's Gambit Discs.
 * All 3 discs utilize this class
 * 
 *******************/

public class Disc : MonoBehaviour
{

    public float fadeOutTime = 10f; //Total time disc remains on screen before fading out

    private SpriteRenderer sprite;
    private Animator animator;

    private int health = 2; //Variable controlling remaining health of disc. New discs can take two hits.

    // Use this for initialization
    //Calls fade in and fade out coroutines, with delay on the fade out until designated time
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        StartCoroutine(FadeIn());   //Fades object in as it is initialized
        StartCoroutine(IncreaseSize());   //Increases size of the disc over the course of its life
        StartCoroutine(FadeOut(fadeOutTime));   //Fades object out after set delay
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    //Method that runs when user clicks within Disc's CircleCollider2D
    void OnMouseDown()
    {
        health--;

        if(health == 0)
        {
            animator.SetTrigger("discDestroy");

            var childText = GetComponentInChildren<TextMesh>(); //Getting the text on the disc as it is a child component

            GameManager.instance.CheckDestroyedDisc(childText.text);
        }
        else
        {
            animator.SetTrigger("discBreak");
        }
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

    //Slowly increases the size of the disc
    IEnumerator IncreaseSize()
    {
        for (float f = 1; f <= 1.5f; f += 0.001f)
        {
            transform.localScale = new Vector3(f, f);
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

        Destroy(gameObject);
    }

    //Destroys the game object when called
    //Used in animation events to destroy disc after animation has played
    private void DestroyDisc()
    {
        Destroy(gameObject);
    }

}
