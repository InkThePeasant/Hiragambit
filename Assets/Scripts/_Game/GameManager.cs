using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Unity Librariess
using UnityEngine;
using UnityEngine.UI;

/******************
 * 
 * Game Manager
 * 
 * - Class controls much of the overarching game logic
 * - Updates and controls UI elements
 * - Loads kana data from text files for use by this and other classes
 * - Manages time between disc spawns
 * - Handles triggering of game over states
 * - Uses Singleton pattern so other game elements can access some of the logic
 * 
 *******************/


public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    
    public Text gameText;  //UI element displaying the Game Text - center text showing which symbol players should look for
    [HideInInspector]public int gameTextKey; //The location of the current key being displayed in Game Text UI element

    public Text timeText;
    public Text scoreText;
    public Slider healthSlider;
    
    //Object to manage the spawning of discs
    private SpawnManager spawnManager;
    public float discSpawnDelay = 1.5f;  //delay between repeated disc spawns

    //Non-GameObject vars for use by primarily Game Manager
    private GameOverController gameOverController;
    public int gameTime = 99; //The starting time on the clock/length of the game
    public Dictionary<string, string> kana; //DS storing KvP of kana and romaji pairings
    [HideInInspector]public int currentScore;   //Score during the game, initialized at 0
    [HideInInspector] public bool gameOver = false;


    //Initializing GameManager
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //Getting controllers for game aspects
        spawnManager = GetComponent<SpawnManager>();
        gameOverController = GetComponent<GameOverController>();

        //Getting Game Objects
        gameText = GameObject.Find("GameText").GetComponent<Text>();
        timeText = GameObject.Find("TimeText").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
    }

    // Use this for initialization
    void Start ()
    {
        //Instantiates game after collecting GameObjects
        InitGame();
	}

    public void InitGame()
    {
        //Disables Game Over UI
        gameOverController.DisableGameOverUI();

        //Setting default score and Game Over state
        currentScore = 0;
        gameOver = false;

        //Load all kana from text file in Assets folder
        if(kana == null)
            LoadKana();

        //Starts Disc Spawning method, invokes it every 3 seconds
        InvokeRepeating("SpawnDiscs", 3f, discSpawnDelay);

        //Gets intial starting kana players must search for
        PopulateGameText();

        //Initialze UI elements
        scoreText.text = "Score: 0";
        healthSlider.value = 100;

        //Updates UI Time Text field, decrementing the seconds
        StartCoroutine(TimeManager(gameTime));
    }

    //Used to load kana Dictionary<> with KvP's from text file at the game's start
    private void LoadKana()
    {
        //Loads text file, and splits each line into a string array
        string textPath = MenuManager.instance.kanaToLoad;

        TextAsset textFile = Resources.Load(textPath) as TextAsset;
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
        //var randKana = Random.Range(0, kana.Count);        

        //gameText.text = kana.ElementAt(randKana).Key;
        //gameTextKey = randKana;

        gameText.text = kana.ElementAt(kana.Count - 1).Key;
        gameTextKey = kana.Count - 1;
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

        gameOver = true;
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

        if(healthSlider.value == 0)
        {
            gameOver = true;
            GameOver();
        }
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
        CancelInvoke();
        StopAllCoroutines();
        gameOverController.GameOver();
    }

}
