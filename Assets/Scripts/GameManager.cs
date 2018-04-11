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
    [HideInInspector]public int gameTextKey; //The location of the current key being displayed in Game Text UI element

    private Text timeText;
    private Text scoreText;
    private Slider healthSlider;

    //Disc prefabs to spawn intermitently
    public GameObject maroonDisc;
    public GameObject tealDisc;
    public GameObject purpleDisc;
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
    }

    // Use this for initialization
    void Start ()
    {       
        gameText = GameObject.Find("GameText").GetComponent<Text>();
        timeText = GameObject.Find("TimeText").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();        

        //Load all kana from text file in Assets folder
        LoadKana();

        InvokeRepeating("SpawnDiscs", 3f, discSpawnDelay);

        //Gets intial starting kana players must search for
        PopulateGameText();

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
        GameObject[] discs = new GameObject[] { maroonDisc, tealDisc, purpleDisc };
        int discToSpawn = Random.Range(0, discs.Length);
        Vector3 discSpawnLocation = GetDiscSpawnLocation();

        //If the returned Vector3 is -99, -99, it means there's too many discs on screen, and the function shouldn't spawn another
        if (discSpawnLocation.x == -99 && discSpawnLocation.y == -99)
            return;

        //Instantiate(discs[discToSpawn], new Vector3(discX, discY, 1), Quaternion.identity);
        Instantiate(discs[discToSpawn], GetDiscSpawnLocation(), Quaternion.identity);
    }

    //Finds a suitable location for discs to spawn to avoid overlapping
    private Vector3 GetDiscSpawnLocation()
    {
        //Get the current discs on screen and store them in a List<>
        var currentDiscs = GameObject.FindGameObjectsWithTag("Discs");

        //If there are 3 discs on screen, return a dummy Vector3 to tell SpawnDiscs() not to spawn
        //Vector3 is non-nullable, hence the strange position
        if (currentDiscs.Count() >= 3)
            return new Vector3(-99, -99, 1);

        //Creates a random point to spawn
        var randomSpawnPoint = new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-3.8f, 1), 1);
        
        //If there are no discs on screen, return the random point    
        if (currentDiscs.Count() == 0)
            return randomSpawnPoint;

        List<Vector3> discLocations = new List<Vector3>();

        //Get every disc's position for easy access later
        foreach (var d in currentDiscs)
            discLocations.Add(d.transform.position);

        /*
         * Loop to ensure there is no disc overlap
         * Calculates the Vector distance between the spawn point and the current discs
         * If the distance is less than 3, it will generate a new point
         * It must 'clear' all on-screen discs sequentially in order to work as the spawn point
         * This is to avoid redundant overlap
         */
        int discsCleared;
        do
        {
            discsCleared = 0;
            foreach (var l in discLocations)
            {
                if (Vector3.Distance(randomSpawnPoint, l) < 3)
                {
                    randomSpawnPoint = new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-3.8f, 1), 1);
                }
                else
                    discsCleared++;
            }
        } while (discsCleared != discLocations.Count());

        return randomSpawnPoint;
    }

    //Method decrements time remaining in game
    private IEnumerator TimeManager(int startTime)
    {
        for(int i = startTime; i >= 0; i--)
        {
            timeText.text = "Time: " + i;
            yield return new WaitForSeconds(1f);
        }
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

}
