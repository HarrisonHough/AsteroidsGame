using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Particle Destroy Class
*/

/// <summary>
/// Particle Destroy class is used to destroy particles after 
/// they have finished playing
/// </summary>
public class ParticleDestroy : MonoBehaviour {

    #region Variables
    // Time to wait after particle sim completetion before destroying
    [SerializeField] 
    private float timeBuffer = 0.2f;
    private ParticleSystem particle;
    #endregion

    #region Functions
    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start () {
        // store reference to particle system
        particle = GetComponent<ParticleSystem>();

        // Start destroy timer
        StartCoroutine(AutoDestroy());

        if (!particle.isPlaying)
            particle.Play();
	}

    /// <summary>
    /// This Coroutine is used to destroy the object after a delay
    /// </summary>
    /// <returns>IENumerator : Required for Coroutine</returns>
    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(particle.main.duration + timeBuffer);
        Destroy(gameObject);
            
    }
    #endregion

}
