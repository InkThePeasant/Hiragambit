using System.Collections;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    
    public Text gameText;  //UI element displaying the Game Text - center text showing which symbol players should look for
    [HideInInspector]public int gameTextKey; //The location of the current key being displayed in Game Text UI element

    public Text timeText;
    public Text scoreText;
    public Slider healthSlider;
    public GameObject gameOverImage;
    
    //Object to manage the spawning of discs
    private SpawnManager spawnManager;
    private float discSpawnDelay = 3f;  //delay between repeated disc spawns

    public Dictionary<string, string> kana; //DS storing KvP of kana and romaji pairings
    private int currentScore = 0;   //Score during the game, initialized at 0



    //Initializing GameManager
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        spawnManager = GetComponent<SpawnManager>();
    }

    // Use this for initialization
    void Start ()
    {       
        gameText = GameObject.Find("GameText").GetComponent<Text>();
        timeText = GameObject.Find("TimeText").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
        gameOverImage = GameObject.Find("GameOverImage");

        gameOverImage.SetActive(false);

        //Load all kana from text file in Assets folder
        LoadKana();

        InvokeRepeating("SpawnDiscs", 3f, discSpawnDelay);

        //Gets intial starting kana players must search for
        PopulateGameText();

        //Updates UI Time Text field, decrementing the seconds
        StartCoroutine(TimeManager(99));
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
        spawnManager.SpawnDiscs();
    }

    //Method decrements time remaining in game
    private IEnumerator TimeManager(int startTime)
    {
        for(int i = startTime; i >= 0; i--)
        {
            timeText.text = "Time: " + i;
            yield return new WaitForSeconds(1f);
        }

        GameOver();
    }

    //Adds parameter value to currentScore, and changes the ScoreText UI element to reflect current score
    private void ScoreManager(int scoreChange)
    {
        currentScore += scoreChange;

        scoreText.text = "Score: " + currentScore;
    }


    //Adjusts health slider UI Element by the amount indicated through the parameter
    private void HealthManager(float healthChange)
    {
        healthSlider.value += healthChange;
    }

    //Examines the string of the destroyed disc against kana<string, string> to see if the destroyed disc was the correct one
    public void CheckDestroyedDisc(string discText)
    {
        if(kana[gameText.text] == discText) //If the destroyed disc was the correct one, add 1000 points and change the disc to search for
        {           
            ScoreManager(1000);
            PopulateGameText();
        }
        else    //If the disc destroyed was the wrong one, subtract 10 health
        {
            HealthManager(-10f);
        }
           
    }

    private void GameOver()
    {        
        gameOverImage.SetActive(true);
    }

}
