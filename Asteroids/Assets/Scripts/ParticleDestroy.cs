using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2017
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
    private float _timeBuffer = 0.2f;
    private ParticleSystem _particle;
    #endregion

    #region Functions
    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start () {
        // store reference to particle system
        _particle = GetComponent<ParticleSystem>();

        // Start destroy timer
        StartCoroutine(AutoDestroy());

        if (!_particle.isPlaying)
            _particle.Play();
	}

    /// <summary>
    /// This Coroutine is used to destroy the object after a delay
    /// </summary>
    /// <returns>IENumerator : Required for Coroutine</returns>
    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(_particle.main.duration + _timeBuffer);
        Destroy(gameObject);
            
    }
    #endregion

}
