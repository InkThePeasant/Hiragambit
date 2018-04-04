using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

/*
 * Handles finding and displaying text on Gambit Disc sprites
 */

public class SpriteText : MonoBehaviour {
    public float fadeOutTime = 10f;

    public MeshRenderer mesh;

	// Use this for initialization
	void Start () {
        var parent = transform.parent;

        var parentRenderer = parent.GetComponent<Renderer>();
        var renderer = GetComponent<Renderer>();
        renderer.sortingLayerID = parentRenderer.sortingLayerID;
        renderer.sortingOrder = parentRenderer.sortingOrder + 1; //Renders text one layer above sprite

        var spriteTransform = parent.transform;
        var text = GetComponent<TextMesh>();
        var pos = spriteTransform.position;
        text.text = string.Format(getDiscText(), pos.x, pos.y);

        mesh = GetComponent<MeshRenderer>();

        StartCoroutine(FadeIn());
        StartCoroutine(FadeOut(fadeOutTime));

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private string getDiscText()
    {
        return GameManager.instance.kana.ElementAt(Random.Range(0, GameManager.instance.kana.Count)).Value;
    }


    IEnumerator FadeIn()
    {
        for (double f = 0; f <= 1; f += .01)
        {
           
            mesh.material.color = new Color(1f, 1f, 1f, (float)f);
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
            mesh.material.color = new Color(1f, 1f, 1f, f);
            yield return null;
        }

    }
}
