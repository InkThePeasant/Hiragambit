using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class SpawnManager : MonoBehaviour {

    //Disc prefabs to spawn intermitently
    public GameObject maroonDisc;
    public GameObject tealDisc;
    public GameObject purpleDisc;

    public void SpawnDiscs()
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

        //Creates a random point to spawn within the game screen
        var randomSpawnPoint = new Vector3(Random.Range(-7f, 7f), Random.Range(-3f, 1), 1);

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
                    randomSpawnPoint = new Vector3(Random.Range(-7f, 7f), Random.Range(-3f, 1), 1);
                }
                else
                    discsCleared++;
            }
        } while (discsCleared != discLocations.Count());

        return randomSpawnPoint;
    }
}
