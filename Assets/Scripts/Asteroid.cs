using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Asteroid Class 
*/

/// <summary>
/// Asteroid class controls the collision events on the
/// asteroids in the game
/// </summary>
public class Asteroid : Enemy {
    
    public AsteroidType Type;
    public static int CurrentAsteroidCount;
    public static int TotalAsteroidCount;

    /// <summary>
    /// Run on game start
    /// </summary>
    protected override void Start()
    {
        base.Start();
        CurrentAsteroidCount++;
        TotalAsteroidCount++;

    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                Hit();
                break;
            case "Missile":
                HitByMissile();
                break;
            case "Border":
                BorderHit();
                break;
            case "Enemy":
                Hit();
                break;
            default:
                //Hit();
                break;
        }
    }

    /// <summary>
    /// Triggers events when colliding with objects
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {
        CurrentAsteroidCount--;

        
        switch (other.tag)
        {
            case "Player":
                Hit();
                break;
            case "Missile":
                HitByMissile();
                break;
            case "Border":
                BorderHit();
                break;
            case "Enemy":
                Hit();
                break;
            default:
                //Hit();
                break;

        }


        //if (other.tag != "WorldBox" && other.tag != "Untagged")
        //if (other.tag == "Player" || other.tag == "Missile")


        
    }

    /// <summary>
    /// Hides object and triggers Asteroid Hit function in GameManager
    /// </summary>
    private void HitByMissile() {

        Debug.Log("Missile hit an asteroid");

        //send asteroid hit to GameManager
        GameManager.Instance.AsteroidHit(this);
        Destroy();
    }

    /// <summary>
    /// Hides object and triggers Asteroid Hit function in GameManager
    /// </summary>
    private void Hit()
    {

        //Debug.Log("Missile hit an asteroid");

        //send asteroid hit to GameManager
        GameManager.Instance.SoundControl.PlayerExplode();
        Destroy();

    }

    /// <summary>
    /// Hides object and trigger Border Hit function in GameManager
    /// </summary>
    private void BorderHit() {

        //Debug.Log("Asteroid hit the border");

        //send asteroid hit to GameManager
        //GameManager.instance.AsteroidHitBorder(this);
        Destroy();
    }

    private void OnDisable()
    {
        
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

}
