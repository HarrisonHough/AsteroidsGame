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
    [SerializeField]
    private Pool _smallAsteroidPool;
    [SerializeField]
    private Pool _mediumAsteroidPool;
    [SerializeField]
    private Pool _largeAsteroidPool;

    [SerializeField]
    private GameObject[] _itemPrefabs;

    //list of different spawn points
    [SerializeField]
    private SpawnPoint[] _spawnPoints;
    //frequency of spawning
    [SerializeField]
    private float _spawnInterval = 3f;    
    //keep track of total asteroids spawned
    [SerializeField]
    private int _totalAsteroidsSpawned = 0;
    //limit number of active asteroids (for performance)
    [SerializeField]
    private int _activeAsteroidLimit = 50;

    [SerializeField]
    private float smallAsteroidSpawnOffset = 0.5f;
    [SerializeField]
    private float mediumAsteroidSpawnOffset = 1f;

    //used to prevent same point spawning multiple times 
    private int _lastSpawnPoint = -1;
    //flag used to prevent/trigger spawning
    private bool _spawning = false;
    //object used as parent for all asteroids for scene organization
    private GameObject _asteroidParent;

    private GameObject _spawnRotation;


    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start () {
        AssignSpawnPoints();
        if (_smallAsteroidPool == null)
            Debug.Log("Asteroid prefab array not assigned");

        Instantiate();


	}

    /// <summary>
    /// 
    /// </summary>
    private void Instantiate()
    {
        _asteroidParent = new GameObject("Asteroids");
        _spawnRotation = new GameObject("Spawn Rotation");

        
    }
    /// <summary>
    /// Called at start, used to automatically assign spawn points
    /// </summary>
    void AssignSpawnPoints() {
        _spawnPoints = new SpawnPoint[transform.childCount];
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnPoints[i] = transform.GetChild(i).GetComponent<SpawnPoint>();
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

        while (_spawning)
        {
            Debug.Log("Spawn 1 asteroid");
            timer += Time.deltaTime;
            if (timer > _spawnInterval && Asteroid.currentAsteroidCount < _activeAsteroidLimit)
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
        _spawnRotation.transform.rotation = sp.transform.rotation;
        _spawnRotation.transform.Rotate(0, Random.Range(-sp.maxYRotation, sp.maxYRotation), 0);

        
        GameObject pooledObject = _largeAsteroidPool.GetObject();
        pooledObject.transform.position = sp.transform.position;
        pooledObject.transform.rotation = sp.transform.rotation;
        pooledObject.SetActive(true);
    }


    /// <summary>
    /// Starts the spawning coroutine
    /// </summary>
    public void StartSpawning()
    {
        Debug.Log("Start spawning");
        if (!_spawning) { 
            _spawning = true;
            StartCoroutine(AsteroidSpawnLoop());
        }
    }

    /// <summary>
    /// Stops the spawning coroutine
    /// </summary>
    public void StopSpawning() {
        if (_spawning)
        {
            _spawning = false;
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

            
            GameObject pooledObject = _smallAsteroidPool.GetObject();
            pooledObject.transform.position = position - offset;
            pooledObject.transform.rotation = rotation;
            pooledObject.SetActive(true);
            //TODO Fix pool logic
            //GameObject asteroid = Instantiate(_asteroidPoolPrefabs[0], position - offset,
            //  rotation, _asteroidParent.transform);
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

            GameObject pooledObject = _mediumAsteroidPool.GetObject();
            pooledObject.transform.position = position - offset;
            pooledObject.transform.rotation = rotation;
            pooledObject.SetActive(true);
            //TODO Pool logic
            //GameObject asteroid = Instantiate(_asteroidPoolPrefabs[1], position + offset,
            //  rotation, _asteroidParent.transform);

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

        int index = Random.Range(0, _spawnPoints.Length);
        while (index == _lastSpawnPoint)
            index = Random.Range(0, _spawnPoints.Length);

        _lastSpawnPoint = index;
        return _spawnPoints[index];
    }

}
