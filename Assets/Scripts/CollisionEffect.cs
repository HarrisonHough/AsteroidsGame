using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Collision Effect Class
*/

/// <summary>
/// Collision Effect class is used to spawn an effect
/// (particles) on collision with particular objects.
/// </summary>
public class CollisionEffect : MonoBehaviour {

    #region Public Variables
    //the effect to spawn on collision
    [SerializeField]
    private GameObject _particleEffect;
    [SerializeField]
    private bool _destroyOnCollision;

    //the tag to check for on object collision
    [SerializeField]
    private string _tagName;
    #endregion

    #region Functions
    /// <summary>
    /// Used for Initialization
    /// </summary>
    private void Start() {
        //check for null reference
        if (_tagName == null)
            Debug.Log("TagName is not assigned");
    }

    /// <summary>
    /// Called on collision, triggers CreateParticles function
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {

        //check for tag
        if (other.tag == _tagName) {
            CreateParticles();
            if(_destroyOnCollision)
                Destroy(gameObject);
        }
    }

    /// <summary>
    /// Creates particles at current position (+ offset)
    /// </summary>
    private void CreateParticles() {
        //spawn particles just above object to make more visible from top view
        GameObject particles = Instantiate(_particleEffect, transform.position + Vector3.up, transform.rotation);
    }
    #endregion
}
