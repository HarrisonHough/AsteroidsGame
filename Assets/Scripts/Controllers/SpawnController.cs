using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Spawn Controller Class
*/

/// <summary>
/// Spawn Controller class controls the spawning of asteroids
/// </summary>
public class SpawnController : MonoBehaviour {

    //different asteroids to spawn (Order is important)
    [SerializeField]
    private Pool smallAsteroidPool;
    [SerializeField]
    private Pool mediumAsteroidPool;
    [SerializeField]
    private Pool largeAsteroidPool;

    [SerializeField]
    private GameObject[] itemPrefabs;

    //list of different spawn points
    [SerializeField]
    private SpawnPoint[] spawnPoints;
    //frequency of spawning
    [SerializeField]
    private float spawnInterval = 3f;    
    //keep track of total asteroids spawned
    [SerializeField]
    private int totalAsteroidsSpawned = 0;
    //limit number of active asteroids (for performance)
    [SerializeField]
    private int activeAsteroidLimit = 50;

    [SerializeField]
    private float smallAsteroidSpawnOffset = 0.5f;
    [SerializeField]
    private float mediumAsteroidSpawnOffset = 1f;

    //used to prevent same point spawning multiple times 
    private int lastSpawnPoint = -1;
    //flag used to prevent/trigger spawning
    private bool spawning = false;
    //object used as parent for all asteroids for scene organization
    private GameObject asteroidParent;

    private GameObject spawnRotation;


    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start () {
        AssignSpawnPoints();
        if (smallAsteroidPool == null)
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
        SpawnAsteroid();

        while (spawning)
        {
            
            timer += Time.deltaTime;
            if (timer > spawnInterval && Asteroid.CurrentAsteroidCount < activeAsteroidLimit)
            {
                timer = 0;
                Debug.Log("Spawn 1 asteroid");
                SpawnAsteroid();
            }

            yield return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void SpawnAsteroid()
    {
        SpawnPoint spawnPoint = RandomSpawnPoint();
        spawnRotation.transform.rotation = spawnPoint.transform.rotation;
        spawnRotation.transform.Rotate(0, Random.Range(-spawnPoint.MaxYRotation, spawnPoint.MaxYRotation), 0);

        SpawnRandomAsteroidAtPosition(spawnPoint);
    }

    private void SpawnRandomAsteroidAtPosition(SpawnPoint spawnPoint)
    {
        AsteroidType type = (AsteroidType)Random.Range(0, 3);
        GameObject pooledObject;
        switch (type)
        { 
            case AsteroidType.Small:
                pooledObject  = smallAsteroidPool.GetObject();
            break;
            case AsteroidType.Medium:
                pooledObject  = mediumAsteroidPool.GetObject();
            break;            
            case AsteroidType.Large:
                pooledObject  = largeAsteroidPool.GetObject();
            break;
            default:
                pooledObject = largeAsteroidPool.GetObject();
                break;

        }
        pooledObject.transform.position = spawnPoint.transform.position;
        pooledObject.transform.rotation = spawnPoint.transform.rotation;
        pooledObject.SetActive(true);
    }


    /// <summary>
    /// Starts the spawning coroutine
    /// </summary>
    public void StartSpawning()
    {
        Debug.Log("Start spawning");
        if (!spawning) { 
            spawning = true;
            StartCoroutine(AsteroidSpawnLoop());
        }
    }

    /// <summary>
    /// Stops the spawning coroutine
    /// </summary>
    public void StopSpawning() {
        if (spawning)
        {
            spawning = false;
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
        Vector3 offset = new Vector3(smallAsteroidSpawnOffset, 0, smallAsteroidSpawnOffset);

        Quaternion rotation = RandomYRotation();
        for (int i = 0; i < 2; i++)
        {
            rotation.x = -rotation.x;
            rotation.y = -rotation.y;
            rotation.z = -rotation.z;
            offset = -offset;

            
            GameObject pooledObject = smallAsteroidPool.GetObject();
            pooledObject.transform.position = position - offset;
            pooledObject.transform.rotation = rotation;
            pooledObject.SetActive(true);
            //TODO Fix pool logic
            //GameObject asteroid = Instantiate(asteroidPoolPrefabs[0], position - offset,
            //  rotation, asteroidParent.transform);
        }

    }

    /// <summary>
    /// Spawns Medium asteroid
    /// </summary>
    /// <param name="position">Vector3 : Position to spawn asteroid at</param>
    public void SpawnMediumAsteroid(Vector3 position) {

        //need offset to ensure both asteroids don't spawn in same spot causing a collision/destruction
        Vector3 offset = new Vector3(mediumAsteroidSpawnOffset, 0, mediumAsteroidSpawnOffset);

        Quaternion rotation = RandomYRotation();
        //create 2 asteroids
        for (int i = 0; i < 2; i++)
        {
            rotation.x = -rotation.x;
            rotation.y = -rotation.y;
            rotation.z = -rotation.z;
            offset = -offset;

            GameObject pooledObject = mediumAsteroidPool.GetObject();
            pooledObject.transform.position = position - offset;
            pooledObject.transform.rotation = rotation;
            pooledObject.SetActive(true);
            //TODO Pool logic
            //GameObject asteroid = Instantiate(asteroidPoolPrefabs[1], position + offset,
            //  rotation, asteroidParent.transform);

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
