using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************
 * 
 * Controls speed of moving background by applying movement vector to BG elements
 * 
 ***************************************/ 

public class ScrollingObject : MonoBehaviour {

    private Rigidbody2D rb2d;

    public float scrollSpeed = -.5f;   //Scrolling speed for galaxy background

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(scrollSpeed, 0);
	}
	
}
