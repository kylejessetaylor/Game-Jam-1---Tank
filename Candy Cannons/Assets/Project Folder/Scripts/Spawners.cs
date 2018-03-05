using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour {

    [Header("Power Up Spawner")]
    #region SpawnerPowerUpVariables
    //Time till first spawn
    public float powerUpFirstSpawn;
    //Time between spawning
    private float cyclePowerUPSpawnTime;
    //Range time 1
    public float powerUpTimeMin;
    //Range time 2
    public float powerUpTimeMax;

    //List of all pre-placed spawn locations
    public List<GameObject> powerUpSpawnLocations;
    //Records last spawn location used
    private int lastPowerUpSpawnLocation;

    //List of all power-up prefabs
    public List<GameObject> powerUps;
    //Records last power up that spawned
    private int lastPowerUp;

    #endregion

    [Header("Mine Spawner")]
    #region SpawnerPowerUpVariables
    //Time till first spawn
    public float mineFirstSpawn;
    //Time between spawning
    private float cycleMineSpawnTime;
    //Range time 1
    public float mineTimeMin;
    //Range time 2
    public float mineTimeMax;

    //List of all pre-placed spawn locations
    public List<GameObject> mineSpawnLocations;

    //Mine GameObject
    public GameObject mine;

    #endregion

    void Start () {
        //Sets Invoke Repeating for Spawner Power Ups
        SpawnerPowerUpStart();
        //Sets Inkove Repeating for Spawner Mines
        SpawnerMineStart();
    }

    #region SpawnerPowerUpFunctions
    private void SpawnerPowerUpStart()
    {
        //Sets first cycleSpawnTime
        cyclePowerUPSpawnTime = FloatTime(powerUpTimeMin, powerUpTimeMax);
        //Spawner for Power Ups
        InvokeRepeating("SpawnPowerUp", powerUpFirstSpawn, cyclePowerUPSpawnTime);
    }

    public void SpawnPowerUp()
    {
        //Picks random Power up from list
        int currentPowerUp = Random.Range(0, powerUps.Count);
        GameObject powerUp = powerUps[currentPowerUp];



        //Picks random Location from the list of spawn locations
        int spotToSpawn = Random.Range(0, powerUpSpawnLocations.Count);
        GameObject spawnLocation = powerUpSpawnLocations[spotToSpawn];

        //Prevents duplicate spawn locations & power Ups
        if (lastPowerUp == currentPowerUp || lastPowerUpSpawnLocation == spotToSpawn || AlreadyASpawnHere(spawnLocation, "PowerUpSpawner") == true)
        {
            SpawnPowerUp();
        }
        //Spawns Power Up on location
        else
        {
            //Checks if all spawn locations have a power up on them
            if (GameObject.FindGameObjectsWithTag("PowerUpSpawner").Length <= GameObject.FindGameObjectsWithTag("PowerUp").Length)
            {
                return;
            }
            //Checks that another Power Up isn't here either
            else if (spawnLocation.transform.childCount != 0)
            {
                SpawnPowerUp();
            }
            else
            {
                //Instantiates Power Up & places to Spawn Location
                GameObject powerSpawned = Instantiate(powerUp, spawnLocation.transform);
                powerSpawned.transform.position = spawnLocation.transform.position;

                //Records new Last power up & spawn location
                lastPowerUp = currentPowerUp;
                lastPowerUpSpawnLocation = spotToSpawn;

                //Changes cycleSpawnTime
                cyclePowerUPSpawnTime = FloatTime(powerUpTimeMin, powerUpTimeMax);
            }            
        }
    }

    #endregion


    #region SpawnerMineFunctions
    private void SpawnerMineStart()
    {
        //Sets first cycleSpawnTime
        cycleMineSpawnTime = FloatTime(mineTimeMin, mineTimeMax);
        //Spawner for Power Ups
        InvokeRepeating("SpawnMine", mineFirstSpawn, cycleMineSpawnTime);
    }

    public void SpawnMine()
    {
        if (mineSpawnLocations.Count == 0)
        {
            CancelInvoke("SpawnMine");
            return;
        }

        //Picks random Location from the list of spawn locations
        int spotToSpawn = Random.Range(0, mineSpawnLocations.Count);
        GameObject spawnLocation = mineSpawnLocations[spotToSpawn];
                
        //Checks if something is already in that spot
        if (AlreadyASpawnHere(spawnLocation, "Mine") == true)
        {
            SpawnMine();
        }

        //Spawns Mine on location
        else
        {
            //Instantiates Mine & places to Spawn Location
            GameObject mineSpawned = Instantiate(mine, spawnLocation.transform);
            mineSpawned.transform.position = spawnLocation.transform.position;
            //Removes from list, reventing multiple spawns on 1 location
            mineSpawnLocations.Remove(spawnLocation);

            //Changes cycleSpawnTime
            cycleMineSpawnTime = FloatTime(mineTimeMin, mineTimeMax);
        }

    }

    #endregion

    //Checks whether power up has already spawned here with this string as a tag
    public bool AlreadyASpawnHere(GameObject mySpawnLocation, string GameObjectTag)
    {
        //Closest Power Up to Spawn Location
        float distanceToClosestPowerUp = Mathf.Infinity;
        GameObject closestPowerUp = null;
        foreach (GameObject powerUp in GameObject.FindGameObjectsWithTag(GameObjectTag))
        {
            float distanceToPowerUp = Vector3.Distance(transform.position, mySpawnLocation.transform.position);
            if (distanceToPowerUp < distanceToClosestPowerUp)
            {
                distanceToClosestPowerUp = distanceToPowerUp;
                closestPowerUp = powerUp;
            }
        }

        //Power Up here
        if (distanceToClosestPowerUp <= 0.05f)
        {
            return (true);
        }
        //No Power Up here
        else
        {
            return (false);
        }

    }

    //Gives a random float from between a range
    public float FloatTime(float min, float max)
    {
        float newTime = Random.Range(min, max);
        return (newTime);
    }
}
