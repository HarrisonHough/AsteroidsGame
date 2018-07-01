using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Missile Class
*/

/// <summary>
/// Missile Class handles the collision events and destruction
/// of missiles
/// </summary>
public class Missile : MonoBehaviour {

    public float maxDuration = 10f;

    void OnEnable() {
        StartCoroutine(DestroyTimer());
    }

    /// <summary>
    /// Triggers HitAsteroid function on collision with asteroid
    /// </summary>
    /// <param name="other">Collider : The collider of the object trigger</param>
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            Destroy();
        }
    }


    IEnumerator DestroyTimer() {
        float time = 0;
        while (time < maxDuration) {
            time += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Max duration reached, Destroy missile");
        Destroy();


    }

    void Destroy() {
        StopAllCoroutines();
        gameObject.SetActive(false);
        
    }

}
