using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2017
* VERSION: 1.0
* SCRIPT: Spawn Controller Class
*/

/// <summary>
/// Spawn Controller class controls the spawning of asteroids
/// </summary>
public class SpawnController : MonoBehaviour {

    //different asteroids to spawn (Order is important)
    public GameObject[] asteroidPrefabs;

    public GameObject[] itemPrefabs;
    //list of different spawn points
    public SpawnPoint[] spawnPoints;
    //frequency of spawning
    public float spawnInterval = 3f;    
    //keep track of total asteroids spawned
    public int totalAsteroidsSpawned = 0;
    //limit number of active asteroids (for performance)
    public int activeAsteroidLimit = 50;

    //used to prevent same point spawning multiple times 
    private int lastSpawnPoint = -1;
    //flag used to prevent/trigger spawning
    private bool spawn = false;
    //object used as parent for all asteroids for scene organization
    private GameObject asteroidParent;

    private GameObject spawnRotation;


    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start () {
        AssignSpawnPoints();
        if (asteroidPrefabs == null)
            Debug.Log("Asteroid prefab array not assigned");

        Instantiate();


	}

    /// <summary>
    /// 
    /// </summary>
    private void Instantiate()
    {
        asteroidParent = new GameObject("Asteroids");
        spawnRotation = new GameObject("Spawn Rotation");
    }
    /// <summary>
    /// Called at start, used to automatically assign spawn points
    /// </summary>
    void AssignSpawnPoints() {
        spawnPoints = new SpawnPoint[transform.childCount];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = transform.GetChild(i).GetComponent<SpawnPoint>();
        }
    }

    /// <summary>
    /// Coroutine used to continuously spawn asteroids
    /// </summary>
    /// <returns>IENumerator : Required for coroutine</returns>
    IEnumerator AsteroidSpawnLoop() {

        //used to store time passed
        float timer = 0;
        SpawnRandomAsteroid();

        while (spawn)
        {

            timer += Time.deltaTime;
            if (timer > spawnInterval && Asteroid.currentAsteroidCount < activeAsteroidLimit)
            {
                timer = 0;
                SpawnRandomAsteroid();
            }

            yield return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void SpawnRandomAsteroid()
    {
        SpawnPoint sp = RandomSpawnPoint();
        spawnRotation.transform.rotation = sp.transform.rotation;
        spawnRotation.transform.Rotate(0, Random.Range(-sp.maxYRotation, sp.maxYRotation), 0);
        GameObject asteroid = Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)], sp.transform.position,
            spawnRotation.transform.rotation, asteroidParent.transform);
    }


    /// <summary>
    /// Starts the spawning coroutine
    /// </summary>
    public void StartSpawning()
    {
        if (!spawn) { 
            spawn = true;
            StartCoroutine(AsteroidSpawnLoop());
        }
    }

    /// <summary>
    /// Stops the spawning coroutine
    /// </summary>
    public void StopSpawning() {
        if (spawn)
        {
            spawn = false;
            StopCoroutine(AsteroidSpawnLoop());
        }
    }

    /// <summary>
    /// Spawns 2 small Asteroids
    /// </summary>
    /// <param name="position">Vector3 : Position to spawn two asteroids</param>
    public void SpawnSmallAsteroid(Vector3 position)
    {
       
        //need offset to ensure both asteriods don't spawn in same spot causing a collision/destruction
        Vector3 offset = new Vector3(0.225f, 0, 0.225f);

        Quaternion rotation = RandomYRotation();
        for (int i = 0; i < 2; i++)
        {
            rotation = RandomYRotation();
            GameObject asteroid = Instantiate(asteroidPrefabs[0], position - offset,
                rotation, asteroidParent.transform);
        }

    }

    /// <summary>
    /// Spawns Medium asteroid
    /// </summary>
    /// <param name="position">Vector3 : Position to spawn asteroid at</param>
    public void SpawnMediumAsteroid(Vector3 position) {

        //need offset to ensure both asteroids don't spawn in same spot causing a collision/destruction
        Vector3 offset = new Vector3(0.39f, 0, 0.39f);

        Quaternion rotation = RandomYRotation();
        //create 2 asteroids
        for (int i = 0; i < 2; i++)
        {
            rotation = RandomYRotation();
            GameObject asteroid = Instantiate(asteroidPrefabs[1], position + offset,
                rotation, asteroidParent.transform);

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private Quaternion RandomYRotation()
    {
        return Quaternion.Euler(0, Random.Range(0, 180), 0);
    }


    /// <summary>
    /// Randomly selects and returns spawn point
    /// </summary>
    /// <returns>SpawnPoint : Used to determine spawn position and rotation of Asteroid</returns>
    public SpawnPoint RandomSpawnPoint() {

        int index = Random.Range(0, spawnPoints.Length);
        while (index == lastSpawnPoint)
            index = Random.Range(0, spawnPoints.Length);

        lastSpawnPoint = index;
        return spawnPoints[index];
    }

}
