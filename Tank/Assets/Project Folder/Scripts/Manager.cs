using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [Header("Spawner")]
    #region SpawnerVariables
    //Time till first spawn
    public float firstSpawn;
    //Time between spawning
    private float cycleSpawnTime;
    //Range time 1
    public float cycleTime1;
    //Range time 2
    public float cycleTime2;

    //List of all pre-placed spawn locations
    public List<GameObject> spawnLocations;
    //Records last spawn location used
    private int lastSpawnLocation;

    //List of all power-up prefabs
    public List<GameObject> powerUps;
    //Records last power up that spawned
    private int lastPowerUp;

    #endregion

    [Header("MenuInterface")]
    #region MenuVariables
    //Main Menu "Scene"
    public GameObject mainMenu;
    //Game "Scene"
    public GameObject GameScene;


    #endregion

// Use this for initialization
void Start()
    {
        //Sets Invoke Repeating
        SpawnerStart();
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region SpawnerFunctions
    private void SpawnerStart()
    {
        //Sets first cycleSpawnTime
        cycleSpawnTime = FloatTime();
        //Spawner for Power Ups
        InvokeRepeating("SpawnPowerUp", firstSpawn, cycleSpawnTime);
    }

    public void SpawnPowerUp()
    {
        //Picks random Power up from list
        int currentPowerUp = Random.Range(0, powerUps.Count);
        GameObject powerUp = powerUps[currentPowerUp];



        //Picks random Location from the list of spawn locations
        int spotToSpawn = Random.Range(0, spawnLocations.Count);
        GameObject spawnLocation = spawnLocations[spotToSpawn];

        //Prevents duplicate spawn locations & power Ups
        if (lastPowerUp == currentPowerUp || lastSpawnLocation == spotToSpawn)
        {
            SpawnPowerUp();
        }
        //Spawns Power Up on location
        else
        {
            //Instantiates Power Up & places to Spawn Location
            GameObject powerSpawned = Instantiate(powerUp);
            powerSpawned.transform.position = spawnLocation.transform.position;

            //Records new Last power up & spawn location
            lastPowerUp = currentPowerUp;
            lastSpawnLocation = spotToSpawn;

            //Changes cycleSpawnTime
            cycleSpawnTime = FloatTime();
        }
    }

    public float FloatTime()
    {
        float newTime = Random.Range(cycleTime1, cycleTime2);
        return (newTime);
    }
    #endregion

}
