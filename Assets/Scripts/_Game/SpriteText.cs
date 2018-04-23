using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

/*
 * Handles finding and displaying text on Gambit Disc sprites
 */

public class SpriteText : MonoBehaviour {
    public float fadeOutTime = 7f;

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
        text.text = string.Format(GetDiscText(), pos.x, pos.y);

        mesh = GetComponent<MeshRenderer>();

        StartCoroutine(FadeIn());
        StartCoroutine(FadeOut(fadeOutTime));

    }

    private string GetDiscText()
    {
        //Getting location of current, correct kana user looks for
        var currentKana = GameManager.instance.gameTextKey;
        //Gets dictionary from GameManager, starting 3 elements back from the correct kana
        //This is so text display on discs is within range of the correct kana on a hiragana chart
        var kanaRange = GameManager.instance.kana.Skip(currentKana - 1);

        int kanaToChoose = Random.Range(0, 3);  //Finds kana within 3 placements of the current correct one

        //Returns a range of 5 elements from already trimmed Dictionary, selecting a random one from within that range
        //If there are not 5 elements in list, returns the correct kana
        try
        {
            return kanaRange.Take(5).ElementAt(kanaToChoose).Value;
        }
        catch(System.Exception ex)
        {
            //Returns the correct kana if at the end of the list
            Debug.Log(ex.StackTrace);
            return GameManager.instance.kana.ElementAt(GameManager.instance.gameTextKey).Value;

            //return kanaRange.ElementAt(3).Value;
        }
    }

    //Coroutine fades in GameObject by steadily increasing sprite's alpha
    IEnumerator FadeIn()
    {
        for (double f = 0; f <= 1; f += .01)
        {
           
            mesh.material.color = new Color(1f, 1f, 1f, (float)f);
            yield return null;
        }
    }

    //Coroutine fades out GameObject by steadily decreasing sprite's alpha
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
