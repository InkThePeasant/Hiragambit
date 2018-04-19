using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************
 * 
 * Handles repositioning of BG elements to ensure smooth scrolling
 * 
 ***************************************/

public class RepeatingBackground : MonoBehaviour {

    private BoxCollider2D bgCollider;
    private float bgHorizontalLength;
    private RectTransform rect;

	// Use this for initialization
	void Start () {
        rect = GetComponent<RectTransform>();
        bgCollider = GetComponent<BoxCollider2D>();
        bgHorizontalLength = bgCollider.size.x;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Repositions BG if it has scrolled far enough
        if (rect.anchoredPosition.x < -bgHorizontalLength)
        {
            RepositionBG();
        }
	}

    //Moves background horizontally when offscreen to enable infinite scrolling
    private void RepositionBG()
    {
        Vector2 groundOffset = new Vector2(bgHorizontalLength * 2f, 0);
        rect.anchoredPosition = (Vector2)rect.anchoredPosition + groundOffset;
    }

}
