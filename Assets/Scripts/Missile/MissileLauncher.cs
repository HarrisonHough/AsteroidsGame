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
/// _missiles
/// </summary>
public class MissileLauncher : MonoBehaviour {

    //reference to the prefab to spawn
    [SerializeField]
    private Pool _missilePool;
    //reference to the spawn point position
    [SerializeField]
    private GameObject _missileSpawnPoint;

    private SoundController _soundControl;
    
    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start () {

        //check for null references
        if (_missileSpawnPoint == null)
            Debug.Log("Must Assign Missile Spawn Point");
 

        _soundControl = FindObjectOfType<SoundController>();
    }

    /// <summary>
    /// Creates missile at SpawnPoint
    /// </summary>
    public void ShootMissile() {
     
        GameObject pooledMissile =  _missilePool.GetObject();
        pooledMissile.transform.position = _missileSpawnPoint.transform.position;
        pooledMissile.transform.rotation =  _missileSpawnPoint.transform.rotation;
        pooledMissile.SetActive(true);

        
        _soundControl.PlayerShoot();
    }

    void OnDestroy() {
       // Destroy(_missileParent);
    }
}
