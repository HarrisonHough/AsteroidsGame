using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2017
* VERSION: 1.0
* SCRIPT: Asteroid Class 
*/

/// <summary>
/// Asteroid class controls the collision events on the
/// asteroids in the game
/// </summary>
public class Asteroid : Enemy {


    public enum Type {small, medium, large }
    public Type m_Type;
    public static int currentAsteroidCount;
    public static int totalAsteroidCount;

    /// <summary>
    /// Run on game start
    /// </summary>
    protected override void Start()
    {
        base.Start();
        currentAsteroidCount++;
        totalAsteroidCount++;

    }

    /// <summary>
    /// Triggers events when colliding with objects
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {
        currentAsteroidCount--;
        if (other.tag != "WorldBox" && other.tag != "Untagged") {
            Hit();
        }
        if (other.tag == "Border") {
            BorderHit();
            return;
        }

        
    }

    /// <summary>
    /// Hides object and triggers Asteroid Hit function in GameManager
    /// </summary>
    private void Hit() {

        //Debug.Log("Missile hit an asteroid");

        //send asteroid hit to GameManager
        GameManager.instance.AsteroidHit(this);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Hides object and trigger Border Hit function in GameManager
    /// </summary>
    private void BorderHit() {

        //Debug.Log("Asteroid hit the border");

        //send asteroid hit to GameManager
        GameManager.instance.AsteroidHitBorder(this);
        gameObject.SetActive(false);
    }
    
}
