using System.Collections;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    private Text gameText;  //UI element displaying the Game Text - center text showing which symbol players should look for
    public int gameTextKey; //The location of the current key being displayed in Game Text UI element

    //Disc prefabs to spawn intermitently
    public GameObject maroonDisc;
    public GameObject tealDisc;
    public GameObject purpleDisc;
    private float discSpawnDelay = 5f;  //delay between repeated disc spawns

    public Dictionary<string, string> kana; //DS storing KvP of kana and romaji pairings


    //Initializing GameManager
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        gameText = GameObject.Find("GameText").GetComponent<Text>();

        //Load all kana from text file in Assets folder
        LoadKana();

        InvokeRepeating("SpawnDiscs", 3f, discSpawnDelay);

        //Gets intial starting kana players must search for
        PopulateGameText();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Used to load kana Dictionary<> with KvP's from text file at the game's start
    private void LoadKana()
    {
        //Loads text file, and splits each line into a string array
        TextAsset textFile = Resources.Load("KanaText/ENGtoHiragana") as TextAsset;
        string[] array = textFile.text.Split('\n');

        //Groups lines into key-value pairs
        kana = array
                .Select((v, i) => new { Index = i, Value = v })
                .GroupBy(p => p.Index / 2)
                .ToDictionary(g => g.First().Value, g => g.Last().Value);

    }

    //Initializes and changes the Game Text UI element, where the current kana user must search for is displayed
    public void PopulateGameText()
    {
        var randKana = Random.Range(0, kana.Count);

        gameText.text = kana.ElementAt(randKana).Key;
        gameTextKey = randKana;
    }

    //Intermitantly spawn disc prefabs at random locations on screen
    void SpawnDiscs()
    {
        GameObject[] discs = new GameObject[] { maroonDisc, tealDisc, purpleDisc };
        int discToSpawn = Random.Range(0, discs.Length);
        float discX = Random.Range(-8.5f, 8.5f);    //X-Coordinates on screen discs can spawn between
        float discY = Random.Range(-3.8f, 1);   //Y-Coordinates on screen discs can spawn between

        Instantiate(discs[discToSpawn], new Vector3(discX, discY, 1), Quaternion.identity);
    }
}
