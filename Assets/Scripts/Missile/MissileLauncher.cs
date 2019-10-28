using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Missile Launcher Class
*/

/// <summary>
/// Missile Launcher class handles the shooting (spawning) of 
/// missiles
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
    void Awake ()
    {
        InputController.OnPrimaryFireAction += ShootMissile;
        //check for null references
        if (missileSpawnPoint == null)
        {
            Debug.Log("Must Assign Missile Spawn Point");
            transform.Find("Missile Spawn Point");
        }

        
        
        //TODO cleanup/optimise if neccessary
        soundControl = FindObjectOfType<SoundController>();
    }

    /// <summary>
    /// Creates missile at SpawnPoint
    /// </summary>
    public void ShootMissile() {
     
        GameObject pooledMissile =  missilePool.GetObject();
        pooledMissile.transform.position = missileSpawnPoint.transform.position;
        pooledMissile.transform.rotation =  missileSpawnPoint.transform.rotation;
        pooledMissile.SetActive(true);
        
        soundControl.PlayerShoot();
    }
}
