﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    //Disc prefabs to spawn intermitently
    public GameObject maroonDisc;
    public GameObject tealDisc;
    public GameObject purpleDisc;
    private float discSpawnDelay = 5f;


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
        InvokeRepeating("SpawnDiscs", 3f, discSpawnDelay);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnDiscs()
    {
        GameObject[] discs = new GameObject[] { maroonDisc, tealDisc, purpleDisc };
        int discToSpawn = Random.Range(0, discs.Length);
        float discX = Random.Range(-8.5f, 8.5f);    //X-Coordinates on screen discs can spawn between
        float discY = Random.Range(-3.8f, 1);   //Y-Coordinates on screen discs can spawn between

        Instantiate(discs[discToSpawn], new Vector3(discX, discY, 1), Quaternion.identity);
    }
}