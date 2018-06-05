using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2017
* VERSION: 1.0
* SCRIPT: Missile Launcher Class
*/

/// <summary>
/// Missile Launcher class handles the shooting (spawning) of 
/// _missiles
/// </summary>
public class MissileLauncher : MonoBehaviour {

    //reference to the prefab to spawn
    [SerializeField]
    private Pool missilePool;
    //reference to the spawn point position
    [SerializeField]
    private GameObject missileSpawnPoint;

    private SoundController soundControl;
    
    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start () {

        //check for null references
        if (missileSpawnPoint == null)
            Debug.Log("Must Assign Missile Spawn Point");
 

        soundControl = FindObjectOfType<SoundController>();

        //CreateMissilePool();

    }

    /// <summary>
    /// 
    /// </summary>
    private void CreateMissilePool()
    {
        //create parent object
        //_missileParent = new GameObject("_missiles");
        

        //_missiles = new GameObject[poolSize];
        //for (int i = 0; i < 5; i++)
        //{
            //GameObject obj = Instantiate(missilePoolPrefab, missileSpawnPoint.transform.position, missileSpawnPoint.transform.rotation, _missileParent.transform);
           // obj.SetActive(false);
           // _missiles[i] = obj;
       // }
    }

    /// <summary>
    /// Creates missile at SpawnPoint
    /// </summary>
    public void ShootMissile() {
        //GameObject missile = Instantiate(missilePrefab, missileSpawnPoint.transform.position, missileSpawnPoint.transform.rotation, _missileParent.transform);
        /*for (int i = 0; i < _missiles.Length; i++) {
            if (!_missiles[i].activeSelf)
            {
                _missiles[i].transform.position = missileSpawnPoint.transform.position;
                _missiles[i].transform.rotation = missileSpawnPoint.transform.rotation;
                _missiles[i].SetActive(true);
                break;
            }
        }*/

        GameObject pooledMissile =  missilePool.GetObject();
        pooledMissile.transform.position = missileSpawnPoint.transform.position;
        pooledMissile.transform.rotation =  missileSpawnPoint.transform.rotation;
        pooledMissile.SetActive(true);

        soundControl.PlayerShoot();
    }

    void OnDestroy() {
       // Destroy(_missileParent);
    }
}
